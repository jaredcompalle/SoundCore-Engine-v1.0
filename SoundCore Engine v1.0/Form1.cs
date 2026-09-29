using SoundCore.EstructurasPropias;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using NAudio.Wave;

namespace SoundCore_Engine_v1._0
{
    public partial class Form1 : Form
    {
        private enum Modo { Propia, LinkedList, List }

        // Estructuras paralelas (solo la activa contiene la cola en cada momento)
        private readonly ListaSimpleEnlazada<Pista> _colaPropia = new();
        private readonly LinkedList<Pista> _colaLinkedList = new();
        private readonly List<Pista> _colaList = new();

        private readonly ReproductorAudio _reproductor = new();
        private Modo _modo = Modo.Propia;
        private int _contadorId = 1;
        private Pista? _pistaSonando;
        private bool _arrastrandoPosicion;

        private const string FiltroAudio =
            "Audio (*.mp3;*.wav;*.flac;*.aiff;*.aif;*.m4a;*.aac;*.wma)|*.mp3;*.wav;*.flac;*.aiff;*.aif;*.m4a;*.aac;*.wma|Todos los archivos (*.*)|*.*";

        public Form1()
        {
            InitializeComponent();
            ConfigurarColumnasGrid();
            _reproductor.PistaTerminada += (_, _) => AvanzarCola(mostrarAvisoSiVacia: false); // auto-avance
            RefrescarVista();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            timerReproductor.Stop();
            _reproductor.Dispose();
            base.OnFormClosed(e);
        }

        // ───────────────────────── Estructura activa ─────────────────────────

        private IEnumerable<Pista> Coleccion => _modo switch
        {
            Modo.Propia => _colaPropia,
            Modo.LinkedList => _colaLinkedList,
            _ => _colaList
        };

        private void rbEstructura_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender is not RadioButton { Checked: true }) return;

            var nuevo = rbPropia.Checked ? Modo.Propia : rbLinkedList.Checked ? Modo.LinkedList : Modo.List;
            if (nuevo == _modo) return;

            // Migrar la cola actual a la nueva estructura para que el cambio sea reactivo y coherente
            var snapshot = Coleccion.ToList();
            _colaPropia.Limpiar();
            _colaLinkedList.Clear();
            _colaList.Clear();
            _modo = nuevo;

            foreach (var p in snapshot) EncolarFinal(p);
            RefrescarVista();
        }

        private void EncolarFinal(Pista p)
        {
            switch (_modo)
            {
                case Modo.Propia: _colaPropia.AgregarAlFinal(p); break;
                case Modo.LinkedList: _colaLinkedList.AddLast(p); break;
                default: _colaList.Add(p); break;
            }
        }

        private void EncolarUpNext(Pista p)
        {
            switch (_modo)
            {
                case Modo.Propia:
                    _colaPropia.ReproducirSiguiente(p);
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

        private Pista? Desencolar()
        {
            switch (_modo)
            {
                case Modo.Propia:
                    return _colaPropia.EstaVacia ? null : _colaPropia.AvanzarPista();
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

        // ───────────────────────── Explorador de archivos + NAudio ─────────────────────────

        /// <summary>Abre el explorador de archivos y crea una Pista por cada audio elegido.</summary>
        private List<Pista> PedirPistas(string titulo)
        {
            using var dlg = new OpenFileDialog
            {
                Title = titulo,
                Filter = FiltroAudio,
                Multiselect = true,
                CheckFileExists = true
            };
            if (dlg.ShowDialog(this) != DialogResult.OK) return [];

            var pistas = new List<Pista>();
            var fallidos = new List<string>();

            foreach (var ruta in dlg.FileNames)
            {
                try { pistas.Add(CrearPista(ruta)); }
                catch (Exception) { fallidos.Add(Path.GetFileName(ruta)); }
            }

            if (fallidos.Count > 0)
                MessageBox.Show("No se pudieron leer estos archivos de audio:\n\n" + string.Join("\n", fallidos),
                    "Archivos omitidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return pistas;
        }

        private Pista CrearPista(string ruta)
        {
            // NAudio abre el archivo para obtener la duración real
            using var lector = new AudioFileReader(ruta);
            int duracion = (int)Math.Round(lector.TotalTime.TotalSeconds);

            // NAudio no lee etiquetas ID3: se deduce "Artista - Título" del nombre del archivo
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

            return new Pista(_contadorId++, titulo, artista, (int)bpm, duracion, ruta);
        }

        private void ReproducirPista(Pista p)
        {
            try
            {
                _reproductor.Reproducir(p.RutaArchivo);
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

        // ───────────────────────── Acciones de cola ─────────────────────────

        private void btnAgregarMusica_Click(object? sender, EventArgs e)
        {
            foreach (var p in PedirPistas("Selecciona la música que quieres encolar al final"))
                EncolarFinal(p);
            RefrescarVista();
        }

        private void btnReproducirSiguiente_Click(object? sender, EventArgs e)
        {
            var pistas = PedirPistas("Selecciona la música para Up Next");

            // Se insertan en orden inverso para que queden en el orden en que se eligieron
            for (int i = pistas.Count - 1; i >= 0; i--)
                EncolarUpNext(pistas[i]);

            RefrescarVista();
        }

        private void btnAvanzar_Click(object? sender, EventArgs e) => AvanzarCola(mostrarAvisoSiVacia: true);

        private void AvanzarCola(bool mostrarAvisoSiVacia)
        {
            var siguiente = Desencolar();

            if (siguiente == null)
            {
                if (mostrarAvisoSiVacia)
                {
                    MessageBox.Show("No hay pistas pendientes en la cola.", "Fin del Setlist",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _reproductor.Detener();
                    _pistaSonando = null;
                    lblNowPlaying.Text = "⏹ Fin del setlist";
                    lblNowPlaying.ForeColor = Color.DimGray;
                }
            }
            else
            {
                ReproducirPista(siguiente);
            }

            RefrescarVista();
        }

        private void btnInvertir_Click(object? sender, EventArgs e)
        {
            switch (_modo)
            {
                case Modo.Propia:
                    _colaPropia.Invertir();
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
            RefrescarVista();
        }

        private void btnOrdenarBpm_Click(object? sender, EventArgs e)
        {
            switch (_modo)
            {
                case Modo.Propia:
                    _colaPropia.Ordenar((a, b) => a.Bpm.CompareTo(b.Bpm));
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
            RefrescarVista();
        }

        private void btnPurgar_Click(object? sender, EventArgs e)
        {
            switch (_modo)
            {
                case Modo.Propia:
                    _colaPropia.DepurarDuplicados((a, b) => a.Titulo.Equals(b.Titulo, StringComparison.OrdinalIgnoreCase));
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
            RefrescarVista();
        }

        // ───────────────────────── Controles del reproductor ─────────────────────────

        private void btnPausa_Click(object? sender, EventArgs e)
        {
            if (_reproductor.TieneAudio) _reproductor.AlternarPausa();
            else AvanzarCola(mostrarAvisoSiVacia: true); // nada cargado: arranca la siguiente de la cola
        }

        private void btnDetener_Click(object? sender, EventArgs e)
        {
            _reproductor.Detener();
            _pistaSonando = null;
            lblNowPlaying.Text = "⏹ Detenido";
            lblNowPlaying.ForeColor = Color.DimGray;
        }

        private void tbPosicion_MouseUp(object? sender, MouseEventArgs e)
        {
            if (_reproductor.TieneAudio && _reproductor.Duracion.TotalSeconds > 0)
            {
                double fraccion = (double)tbPosicion.Value / tbPosicion.Maximum;
                _reproductor.Posicion = TimeSpan.FromSeconds(_reproductor.Duracion.TotalSeconds * fraccion);
            }
            _arrastrandoPosicion = false;
        }

        private void timerReproductor_Tick(object? sender, EventArgs e)
        {
            if (!_reproductor.TieneAudio)
            {
                if (!_arrastrandoPosicion) tbPosicion.Value = 0;
                lblTiempo.Text = "00:00 / 00:00";
                return;
            }

            var dur = _reproductor.Duracion;
            var pos = _reproductor.Posicion;

            if (!_arrastrandoPosicion && dur.TotalSeconds > 0)
                tbPosicion.Value = (int)Math.Clamp(pos.TotalSeconds / dur.TotalSeconds * tbPosicion.Maximum, 0, tbPosicion.Maximum);

            lblTiempo.Text = $"{Formato(pos)} / {Formato(dur)}";
        }

        private static string Formato(TimeSpan t) => $"{(int)t.TotalMinutes:D2}:{t.Seconds:D2}";

        // ───────────────────────── Vista ─────────────────────────

        private void ConfigurarColumnasGrid()
        {
            dgvCola.ColumnCount = 6;
            dgvCola.Columns[0].Name = "Pos";
            dgvCola.Columns[1].Name = "ID";
            dgvCola.Columns[2].Name = "Título / Artista";
            dgvCola.Columns[3].Name = "BPM";
            dgvCola.Columns[4].Name = "Duración";
            dgvCola.Columns[5].Name = "Archivo";
            dgvCola.Columns[0].FillWeight = 8;
            dgvCola.Columns[1].FillWeight = 8;
            dgvCola.Columns[2].FillWeight = 45;
            dgvCola.Columns[3].FillWeight = 12;
            dgvCola.Columns[4].FillWeight = 12;
            dgvCola.Columns[5].FillWeight = 30;
        }

        private void RefrescarVista()
        {
            dgvCola.Rows.Clear();

            int index = 1;
            int duracionTotal = 0;

            foreach (var p in Coleccion)
            {
                dgvCola.Rows.Add(index++, p.Id, $"{p.Titulo} — {p.Artista}", $"{p.Bpm} BPM",
                    Formato(TimeSpan.FromSeconds(p.DuracionSegundos)), Path.GetFileName(p.RutaArchivo));
                duracionTotal += p.DuracionSegundos;
            }

            string estructura = _modo switch
            {
                Modo.Propia => "Lista Simple Propia",
                Modo.LinkedList => "LinkedList<T>",
                _ => "List<T>"
            };
            lblEstadisticas.Text = $"Total en cola: {index - 1} pistas | Duración acumulada: {Formato(TimeSpan.FromSeconds(duracionTotal))} | Estructura: {estructura}";
        }

        // ───────────────────────── Benchmark (fuera del hilo de UI) ─────────────────────────

        private async void btnBenchmark_Click(object? sender, EventArgs e)
        {
            int n = (int)numBenchMark.Value;
            btnBenchmark.Enabled = false;
            txtResultadosBenchmark.Text = $"Ejecutando {n:N0} inserciones intermedias en cada estructura...";

            try
            {
                txtResultadosBenchmark.Text = await Task.Run(() => EjecutarBenchmark(n));
            }
            catch (Exception ex)
            {
                txtResultadosBenchmark.Text = "Error en el benchmark: " + ex.Message;
            }
            finally
            {
                btnBenchmark.Enabled = true;
            }
        }

        private static string EjecutarBenchmark(int n)
        {
            // Las pistas se crean antes de medir para no contaminar el tiempo con asignaciones
            var random = new Random(42);
            var pistas = new Pista[n];
            for (int i = 0; i < n; i++)
                pistas[i] = new Pista(i, $"Pista {i}", "DJ", random.Next(100, 150), 180, "");
            var cabeza = new Pista(-1, "Head", "DJ", 120, 200, "");

            var sw = new Stopwatch();

            // 1. Lista propia: ReproducirSiguiente = O(1) por reconexión
            var propia = new ListaSimpleEnlazada<Pista>();
            propia.AgregarAlFinal(cabeza);
            sw.Restart();
            foreach (var p in pistas) propia.ReproducirSiguiente(p);
            sw.Stop();
            double msPropia = sw.Elapsed.TotalMilliseconds;

            // 2. LinkedList<T>: AddAfter con LinkedListNode = O(1)
            var linked = new LinkedList<Pista>();
            var nodoCabeza = linked.AddFirst(cabeza);
            sw.Restart();
            foreach (var p in pistas) linked.AddAfter(nodoCabeza, p);
            sw.Stop();
            double msLinked = sw.Elapsed.TotalMilliseconds;

            // 3. List<T>: Insert(1, x) = O(n) por Array.Copy
            var lista = new List<Pista> { cabeza };
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

        private void tbVolumen_Scroll(object sender, EventArgs e)
        {

            // 1. Tomamos el valor de la barra (ej. 50) y lo dividimos entre 100 para obtener un decimal (0.5)
            float volumenCalculado = tbVolumen.Value / 100f;

            // 2. Le pasamos ese decimal al reproductor 
            // (Nota: Si usas WaveOutEvent de NAudio directo, la propiedad es en inglés: Volume)
            _reproductor.Volumen = volumenCalculado;
        }

        private void tbPosicion_MouseDown(object sender, MouseEventArgs e)
        {
            
            // Le indicamos al programa que el usuario está tocando la barra
            _arrastrandoPosicion = true;
        }
    
    }
}