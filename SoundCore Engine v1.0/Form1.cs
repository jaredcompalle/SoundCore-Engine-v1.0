// SoundCore Engine v2.0 - TecNM Campus Monclova
// Integrantes: Rosembert Jared Ortiz Reyes - I25050406
// Fecha: 28/09/2026 | Versión: 1.0

using SoundCore.EstructurasPropias;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using NAudio.Wave;

namespace SoundCore_Engine_v1._0
{
    public partial class Form1 : Form
    {
        private enum Modo { Own, LinkedList, List }

        private readonly SimpleLinkedList<Track> _colaPropia = new();
        private readonly LinkedList<Track> _colaLinkedList = new();
        private readonly List<Track> _colaList = new();

        private readonly PlayerAudio _reproductor = new();
        private Modo _modo = Modo.Own;
        private int _contadorId = 1;
        private Track? _pistaSonando;
        private bool _arrastrandoPosicion;

        private const string FiltroAudio =
            "Audio (*.mp3;*.wav;*.flac;*.aiff;*.aif;*.m4a;*.aac;*.wma)|*.mp3;*.wav;*.flac;*.aiff;*.aif;*.m4a;*.aac;*.wma|Todos los archivos (*.*)|*.*";

        public Form1()
        {
            InitializeComponent();
            ConfigureColumnsGrid();
            _reproductor.PistaTerminada += (_, _) => AdvanceEnqueue(mostrarAvisoSiVacia: false); 
            RefreshView();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            timerPlayer.Stop();
            _reproductor.Dispose();
            base.OnFormClosed(e);
        }


        private IEnumerable<Track> Coleccion => _modo switch
        {
            Modo.Own => _colaPropia,
            Modo.LinkedList => _colaLinkedList,
            _ => _colaList
        };

        private void rbStructure_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender is not RadioButton { Checked: true }) return;

            var nuevo = rbOwn.Checked ? Modo.Own : rbLinkedList.Checked ? Modo.LinkedList : Modo.List;
            if (nuevo == _modo) return;

            var snapshot = Coleccion.ToList();
            _colaPropia.Clean();
            _colaLinkedList.Clear();
            _colaList.Clear();
            _modo = nuevo;

            foreach (var p in snapshot) GlueFinal(p);
            RefreshView();
        }

        private void GlueFinal(Track p)
        {
            switch (_modo)
            {
                case Modo.Own: _colaPropia.AddToFinal(p); break;
                case Modo.LinkedList: _colaLinkedList.AddLast(p); break;
                default: _colaList.Add(p); break;
            }
        }

        private void GlueUpNext(Track p)
        {
            switch (_modo)
            {
                case Modo.Own:
                    _colaPropia.PlayNext(p);
                    break;
                case Modo.LinkedList:
                    if (_colaLinkedList.First == null) _colaLinkedList.AddFirst(p);
                    else _colaLinkedList.AddAfter(_colaLinkedList.First, p);
                    break;
                default:
                    if (_colaList.Count <= 1) _colaList.Add(p);
                    else _colaList.Insert(1, p);
                    break;
            }
        }

        private Track? Uncollect()
        {
            switch (_modo)
            {
                case Modo.Own:
                    return _colaPropia.EstaVacia ? null : _colaPropia.AdvanceTrack();
                case Modo.LinkedList:
                    {
                        var primero = _colaLinkedList.First;
                        if (primero == null) return null;
                        _colaLinkedList.RemoveFirst();
                        return primero.Value;
                    }
                default:
                    {
                        if (_colaList.Count == 0) return null;
                        var p = _colaList[0];
                        _colaList.RemoveAt(0);
                        return p;
                    }
            }
        }


        private List<Track> AskTracks(string titulo)
        {
            using var dlg = new OpenFileDialog
            {
                Title = titulo,
                Filter = FiltroAudio,
                Multiselect = true,
                CheckFileExists = true
            };
            if (dlg.ShowDialog(this) != DialogResult.OK) return [];

            var pistas = new List<Track>();
            var fallidos = new List<string>();

            foreach (var ruta in dlg.FileNames)
            {
                try { pistas.Add(CreateTrack(ruta)); }
                catch (Exception) { fallidos.Add(Path.GetFileName(ruta)); }
            }

            if (fallidos.Count > 0)
                MessageBox.Show("No se pudieron leer estos archivos de audio:\n\n" + string.Join("\n", fallidos),
                    "Archivos omitidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return pistas;
        }

        private Track CreateTrack(string ruta)
        {
            using var lector = new AudioFileReader(ruta);
            int duracion = (int)Math.Round(lector.TotalTime.TotalSeconds);

            string nombre = Path.GetFileNameWithoutExtension(ruta);
            string artista = "Artista desconocido";
            string titulo = nombre;

            uint bpm = 0;
            using (var archivo = TagLib.File.Create(ruta))
            {
                bpm = archivo.Tag.BeatsPerMinute;
                if (bpm == 0)
                {
                    bpm = (uint)numBpm.Value;
                }
            }

            int sep = nombre.IndexOf(" - ", StringComparison.Ordinal);
            if (sep > 0)
            {
                artista = nombre[..sep].Trim();
                titulo = nombre[(sep + 3)..].Trim();
            }

            return new Track(_contadorId++, titulo, artista, (int)bpm, duracion, ruta);
        }

        private void PlayTrack(Track p)
        {
            try
            {
                _reproductor.Play(p.RutaArchivo);
                _pistaSonando = p;
                lblNowPlaying.Text = $"▶ Sonando: {p.Titulo} - {p.Artista} ({p.Bpm} BPM)";
                lblNowPlaying.ForeColor = Color.DarkGreen;
            }
            catch (Exception ex)
            {
                _pistaSonando = null;
                lblNowPlaying.Text = "⚠ No se pudo reproducir la pista";
                lblNowPlaying.ForeColor = Color.Firebrick;
                MessageBox.Show($"No se pudo reproducir \"{p.Titulo}\":\n{ex.Message}", "Error de audio",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAddMusic_Click(object? sender, EventArgs e)
        {
            foreach (var p in AskTracks("Selecciona la música que quieres encolar al final"))
                GlueFinal(p);
            RefreshView();
        }

        private void btnPlayNext_Click(object? sender, EventArgs e)
        {
            var pistas = AskTracks("Selecciona la música para Up Next");

            for (int i = pistas.Count - 1; i >= 0; i--)
                GlueUpNext(pistas[i]);

            RefreshView();
        }

        private void btnAdvance_Click(object? sender, EventArgs e) => AdvanceEnqueue(mostrarAvisoSiVacia: true);

        private void AdvanceEnqueue(bool mostrarAvisoSiVacia)
        {
            var siguiente = Uncollect();

            if (siguiente == null)
            {
                if (mostrarAvisoSiVacia)
                {
                    MessageBox.Show("No hay pistas pendientes en la cola.", "Fin del Setlist",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _reproductor.Stop();
                    _pistaSonando = null;
                    lblNowPlaying.Text = "⏹ Fin del setlist";
                    lblNowPlaying.ForeColor = Color.DimGray;
                }
            }
            else
            {
                PlayTrack(siguiente);
            }

            RefreshView();
        }

        private void btnInvest_Click(object? sender, EventArgs e)
        {
            switch (_modo)
            {
                case Modo.Own:
                    _colaPropia.Invest();
                    break;
                case Modo.LinkedList:
                    var invertida = _colaLinkedList.Reverse().ToList();
                    _colaLinkedList.Clear();
                    foreach (var p in invertida) _colaLinkedList.AddLast(p);
                    break;
                default:
                    _colaList.Reverse();
                    break;
            }
            RefreshView();
        }

        private void btnSortBpm_Click(object? sender, EventArgs e)
        {
            switch (_modo)
            {
                case Modo.Own:
                    _colaPropia.Sort((a, b) => a.Bpm.CompareTo(b.Bpm));
                    break;
                case Modo.LinkedList:
                    var ordenadasLl = _colaLinkedList.OrderBy(p => p.Bpm).ToList();
                    _colaLinkedList.Clear();
                    foreach (var p in ordenadasLl) _colaLinkedList.AddLast(p);
                    break;
                default:
                    var ordenadasL = _colaList.OrderBy(p => p.Bpm).ToList();
                    _colaList.Clear();
                    _colaList.AddRange(ordenadasL);
                    break;
            }
            RefreshView();
        }

        private void btnPurge_Click(object? sender, EventArgs e)
        {
            switch (_modo)
            {
                case Modo.Own:
                    _colaPropia.DebugDuplicates((a, b) => a.Titulo.Equals(b.Titulo, StringComparison.OrdinalIgnoreCase));
                    break;
                case Modo.LinkedList:
                    var unicosLl = _colaLinkedList.DistinctBy(p => p.Titulo, StringComparer.OrdinalIgnoreCase).ToList();
                    _colaLinkedList.Clear();
                    foreach (var p in unicosLl) _colaLinkedList.AddLast(p);
                    break;
                default:
                    var unicosL = _colaList.DistinctBy(p => p.Titulo, StringComparer.OrdinalIgnoreCase).ToList();
                    _colaList.Clear();
                    _colaList.AddRange(unicosL);
                    break;
            }
            RefreshView();
        }


        private void btnPause_Click(object? sender, EventArgs e)
        {
            if (_reproductor.TieneAudio) _reproductor.TogglePause();
            else AdvanceEnqueue(mostrarAvisoSiVacia: true); 
        }

        private void btnStop_Click(object? sender, EventArgs e)
        {
            _reproductor.Stop();
            _pistaSonando = null;
            lblNowPlaying.Text = "⏹ Detenido";
            lblNowPlaying.ForeColor = Color.DimGray;
        }

        private void tbPosition_MouseUp(object? sender, MouseEventArgs e)
        {
            if (_reproductor.TieneAudio && _reproductor.Duracion.TotalSeconds > 0)
            {
                double fraccion = (double)tbPosition.Value / tbPosition.Maximum;
                _reproductor.Posicion = TimeSpan.FromSeconds(_reproductor.Duracion.TotalSeconds * fraccion);
            }
            _arrastrandoPosicion = false;
        }

        private void timerPlayer_Tick(object? sender, EventArgs e)
        {
            if (!_reproductor.TieneAudio)
            {
                if (!_arrastrandoPosicion) tbPosition.Value = 0;
                lblTime.Text = "00:00 / 00:00";
                return;
            }

            var dur = _reproductor.Duracion;
            var pos = _reproductor.Posicion;

            if (!_arrastrandoPosicion && dur.TotalSeconds > 0)
                tbPosition.Value = (int)Math.Clamp(pos.TotalSeconds / dur.TotalSeconds * tbPosition.Maximum, 0, tbPosition.Maximum);

            lblTime.Text = $"{Formato(pos)} / {Formato(dur)}";
        }

        private static string Formato(TimeSpan t) => $"{(int)t.TotalMinutes:D2}:{t.Seconds:D2}";


        private void ConfigureColumnsGrid()
        {
            dgvTail.ColumnCount = 6;
            dgvTail.Columns[0].Name = "Pos";
            dgvTail.Columns[1].Name = "ID";
            dgvTail.Columns[2].Name = "Título / Artista";
            dgvTail.Columns[3].Name = "BPM";
            dgvTail.Columns[4].Name = "Duración";
            dgvTail.Columns[5].Name = "Archivo";
            dgvTail.Columns[0].FillWeight = 8;
            dgvTail.Columns[1].FillWeight = 8;
            dgvTail.Columns[2].FillWeight = 45;
            dgvTail.Columns[3].FillWeight = 12;
            dgvTail.Columns[4].FillWeight = 12;
            dgvTail.Columns[5].FillWeight = 30;
        }

        private void RefreshView()
        {
            dgvTail.Rows.Clear();

            int index = 1;
            int duracionTotal = 0;

            foreach (var p in Coleccion)
            {
                dgvTail.Rows.Add(index++, p.Id, $"{p.Titulo} — {p.Artista}", $"{p.Bpm} BPM",
                    Formato(TimeSpan.FromSeconds(p.DuracionSegundos)), Path.GetFileName(p.RutaArchivo));
                duracionTotal += p.DuracionSegundos;
            }

            string estructura = _modo switch
            {
                Modo.Own => "Lista Simple Propia",
                Modo.LinkedList => "LinkedList<T>",
                _ => "List<T>"
            };
            lblStadistics.Text = $"Total en cola: {index - 1} pistas | Duración acumulada: {Formato(TimeSpan.FromSeconds(duracionTotal))} | Estructura: {estructura}";
        }


        private async void btnBenchmark_Click(object? sender, EventArgs e)
        {
            int n = (int)numBenchMark.Value;
            btnBenchmark.Enabled = false;
            txtResultsBenchmark.Text = $"Ejecutando {n:N0} inserciones intermedias en cada estructura...";

            try
            {
                txtResultsBenchmark.Text = await Task.Run(() => RunBenchmark(n));
            }
            catch (Exception ex)
            {
                txtResultsBenchmark.Text = "Error en el benchmark: " + ex.Message;
            }
            finally
            {
                btnBenchmark.Enabled = true;
            }
        }

        private static string RunBenchmark(int n)
        {
            var random = new Random(42);
            var pistas = new Track[n];
            for (int i = 0; i < n; i++)
                pistas[i] = new Track(i, $"Pista {i}", "DJ", random.Next(100, 150), 180, "");
            var cabeza = new Track(-1, "Head", "DJ", 120, 200, "");

            var sw = new Stopwatch();

            var propia = new SimpleLinkedList<Track>();
            propia.AddToFinal(cabeza);
            sw.Restart();
            foreach (var p in pistas) propia.PlayNext(p);
            sw.Stop();
            double msPropia = sw.Elapsed.TotalMilliseconds;

            var linked = new LinkedList<Track>();
            var nodoCabeza = linked.AddFirst(cabeza);
            sw.Restart();
            foreach (var p in pistas) linked.AddAfter(nodoCabeza, p);
            sw.Stop();
            double msLinked = sw.Elapsed.TotalMilliseconds;

            var lista = new List<Track> { cabeza };
            sw.Restart();
            foreach (var p in pistas) lista.Insert(1, p);
            sw.Stop();
            double msLista = sw.Elapsed.TotalMilliseconds;

            return
                $"=== RESULTADOS DE ESTRÉS ({n:N0} INSERCIONES INTERMEDIAS) ===\r\n" +
                $"• Lista Enlazada Propia (Nodos):  {msPropia,9:F1} ms  [Inserción intermedia O(1) por reconexión]\r\n" +
                $"• .NET LinkedList<T>:             {msLinked,9:F1} ms  [Inserción con LinkedListNode O(1)]\r\n" +
                $"• .NET List<T> (Arreglo Dinámico):{msLista,9:F1} ms  [Insert(idx) O(n): desplaza elementos con Array.Copy]\r\n\r\n" +
                "Conclusión Técnica: en inserciones intermedias frecuentes, las listas enlazadas solo redirigen referencias, " +
                "mientras que List<T> debe desplazar en memoria todos los elementos posteriores al índice y, " +
                "al crecer, redimensionar su búfer interno.";
        }

        private void tbVolume_Scroll(object sender, EventArgs e)
        {

            float volumenCalculado = tbVolume.Value / 100f;

            _reproductor.Volumen = volumenCalculado;
        }

        private void tbPosition_MouseDown(object sender, MouseEventArgs e)
        {
            
            _arrastrandoPosicion = true;
        }
    
    }
}