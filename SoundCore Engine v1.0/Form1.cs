// SoundCore Engine v2.0 - TecNM Campus Monclova
// Authors: Rosembert Jared Ortiz Reyes - I25050406
// Date: 28/09/2026 | Version: 1.0

using SoundCore.EstructurasPropias;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using NAudio.Wave;

namespace SoundCore_Engine_v1._0
{
    public partial class Form1 : Form
    {
        private enum Mode { Own, LinkedList, List }

        private readonly SimpleLinkedList<Track> _ownQueue = new();
        private readonly LinkedList<Track> _linkedListQueue = new();
        private readonly List<Track> _listQueue = new();

        private readonly PlayerAudio _player = new();
        private Mode _mode = Mode.Own;
        private int _idCounter = 1;
        private Track? _playingTrack;
        private bool _isDraggingPosition;

        private const string AudioFilter =
            "Audio (*.mp3;*.wav;*.flac;*.aiff;*.aif;*.m4a;*.aac;*.wma)|*.mp3;*.wav;*.flac;*.aiff;*.aif;*.m4a;*.aac;*.wma|All files (*.*)|*.*";

        public Form1()
        {
            InitializeComponent();
            ConfigureColumnsGrid();
            _player.TrackEnded += (_, _) => AdvanceEnqueue(showWarningIfEmpty: false);
            RefreshView();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            timerPlayer.Stop();
            _player.Dispose();
            base.OnFormClosed(e);
        }

        private IEnumerable<Track> Collection => _mode switch
        {
            Mode.Own => _ownQueue,
            Mode.LinkedList => _linkedListQueue,
            _ => _listQueue
        };

        private void rbStructure_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender is not RadioButton { Checked: true }) return;

            var newMode = rbOwn.Checked ? Mode.Own : rbLinkedList.Checked ? Mode.LinkedList : Mode.List;
            if (newMode == _mode) return;

            var snapshot = Collection.ToList();
            _ownQueue.Clear();
            _linkedListQueue.Clear();
            _listQueue.Clear();
            _mode = newMode;

            foreach (var track in snapshot) EnqueueLast(track);
            RefreshView();
        }

        private void EnqueueLast(Track track)
        {
            switch (_mode)
            {
                case Mode.Own: _ownQueue.AddToEnd(track); break;
                case Mode.LinkedList: _linkedListQueue.AddLast(track); break;
                default: _listQueue.Add(track); break;
            }
        }

        private void EnqueueNext(Track track)
        {
            switch (_mode)
            {
                case Mode.Own:
                    _ownQueue.PlayNext(track);
                    break;
                case Mode.LinkedList:
                    if (_linkedListQueue.First == null) _linkedListQueue.AddFirst(track);
                    else _linkedListQueue.AddAfter(_linkedListQueue.First, track);
                    break;
                default:
                    if (_listQueue.Count <= 1) _listQueue.Add(track);
                    else _listQueue.Insert(1, track);
                    break;
            }
        }

        private Track? DequeueTrack()
        {
            switch (_mode)
            {
                case Mode.Own:
                    return _ownQueue.IsEmpty ? null : _ownQueue.AdvanceTrack();
                case Mode.LinkedList:
                    {
                        var first = _linkedListQueue.First;
                        if (first == null) return null;
                        _linkedListQueue.RemoveFirst();
                        return first.Value;
                    }
                default:
                    {
                        if (_listQueue.Count == 0) return null;
                        var track = _listQueue[0];
                        _listQueue.RemoveAt(0);
                        return track;
                    }
            }
        }

        private List<Track> PromptForTracks(string title)
        {
            using var dlg = new OpenFileDialog
            {
                Title = title,
                Filter = AudioFilter,
                Multiselect = true,
                CheckFileExists = true
            };
            if (dlg.ShowDialog(this) != DialogResult.OK) return [];

            var tracks = new List<Track>();
            var failed = new List<string>();

            foreach (var path in dlg.FileNames)
            {
                try { tracks.Add(CreateTrack(path)); }
                catch (Exception) { failed.Add(Path.GetFileName(path)); }
            }

            if (failed.Count > 0)
                MessageBox.Show("Could not read these audio files:\n\n" + string.Join("\n", failed),
                    "Files skipped", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return tracks;
        }

        private Track CreateTrack(string path)
        {
            using var reader = new AudioFileReader(path);
            int duration = (int)Math.Round(reader.TotalTime.TotalSeconds);

            string name = Path.GetFileNameWithoutExtension(path);
            string artist = "Unknown Artist";
            string title = name;

            uint bpm = 0;
            using (var file = TagLib.File.Create(path))
            {
                bpm = file.Tag.BeatsPerMinute;
                if (bpm == 0)
                {
                    bpm = (uint)numBpm.Value;
                }
            }

            int separator = name.IndexOf(" - ", StringComparison.Ordinal);
            if (separator > 0)
            {
                artist = name[..separator].Trim();
                title = name[(separator + 3)..].Trim();
            }

            return new Track(_idCounter++, title, artist, (int)bpm, duration, path);
        }

        private void PlayTrack(Track track)
        {
            try
            {
                _player.Play(track.FilePath);
                _playingTrack = track;
                lblNowPlaying.Text = $"▶ Playing: {track.Title} - {track.Artist} ({track.Bpm} BPM)";
                lblNowPlaying.ForeColor = Color.DarkGreen;
            }
            catch (Exception ex)
            {
                _playingTrack = null;
                lblNowPlaying.Text = "⚠ Could not play track";
                lblNowPlaying.ForeColor = Color.Firebrick;
                MessageBox.Show($"Could not play \"{track.Title}\":\n{ex.Message}", "Audio Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddMusic_Click(object? sender, EventArgs e)
        {
            foreach (var track in PromptForTracks("Select the music you want to enqueue at the end"))
                EnqueueLast(track);
            RefreshView();
        }

        private void btnPlayNext_Click(object? sender, EventArgs e)
        {
            var tracks = PromptForTracks("Select music for Up Next");

            for (int i = tracks.Count - 1; i >= 0; i--)
                EnqueueNext(tracks[i]);

            RefreshView();
        }

        private void btnAdvance_Click(object? sender, EventArgs e) => AdvanceEnqueue(showWarningIfEmpty: true);

        private void AdvanceEnqueue(bool showWarningIfEmpty)
        {
            var nextTrack = DequeueTrack();

            if (nextTrack == null)
            {
                if (showWarningIfEmpty)
                {
                    MessageBox.Show("There are no pending tracks in the queue.", "End of Setlist",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _player.Stop();
                    _playingTrack = null;
                    lblNowPlaying.Text = "⏹ End of setlist";
                    lblNowPlaying.ForeColor = Color.DimGray;
                }
            }
            else
            {
                PlayTrack(nextTrack);
            }

            RefreshView();
        }

        private void btnInvest_Click(object? sender, EventArgs e)
        {
            switch (_mode)
            {
                case Mode.Own:
                    _ownQueue.Reverse();
                    break;
                case Mode.LinkedList:
                    var reversed = _linkedListQueue.Reverse().ToList();
                    _linkedListQueue.Clear();
                    foreach (var track in reversed) _linkedListQueue.AddLast(track);
                    break;
                default:
                    _listQueue.Reverse();
                    break;
            }
            RefreshView();
        }

        private void btnSortBpm_Click(object? sender, EventArgs e)
        {
            switch (_mode)
            {
                case Mode.Own:
                    _ownQueue.Sort((a, b) => a.Bpm.CompareTo(b.Bpm));
                    break;
                case Mode.LinkedList:
                    var sortedLinkedList = _linkedListQueue.OrderBy(p => p.Bpm).ToList();
                    _linkedListQueue.Clear();
                    foreach (var track in sortedLinkedList) _linkedListQueue.AddLast(track);
                    break;
                default:
                    var sortedList = _listQueue.OrderBy(p => p.Bpm).ToList();
                    _listQueue.Clear();
                    _listQueue.AddRange(sortedList);
                    break;
            }
            RefreshView();
        }

        private void btnPurge_Click(object? sender, EventArgs e)
        {
            switch (_mode)
            {
                case Mode.Own:
                    _ownQueue.DebugDuplicates((a, b) => a.Title.Equals(b.Title, StringComparison.OrdinalIgnoreCase));
                    break;
                case Mode.LinkedList:
                    var uniqueLinkedList = _linkedListQueue.DistinctBy(p => p.Title, StringComparer.OrdinalIgnoreCase).ToList();
                    _linkedListQueue.Clear();
                    foreach (var track in uniqueLinkedList) _linkedListQueue.AddLast(track);
                    break;
                default:
                    var uniqueList = _listQueue.DistinctBy(p => p.Title, StringComparer.OrdinalIgnoreCase).ToList();
                    _listQueue.Clear();
                    _listQueue.AddRange(uniqueList);
                    break;
            }
            RefreshView();
        }

        private void btnPause_Click(object? sender, EventArgs e)
        {
            if (_player.HasAudio) _player.TogglePause();
            else AdvanceEnqueue(showWarningIfEmpty: true);
        }

        private void btnStop_Click(object? sender, EventArgs e)
        {
            _player.Stop();
            _playingTrack = null;
            lblNowPlaying.Text = "⏹ Stopped";
            lblNowPlaying.ForeColor = Color.DimGray;
        }

        private void tbPosition_MouseUp(object? sender, MouseEventArgs e)
        {
            if (_player.HasAudio && _player.Duration.TotalSeconds > 0)
            {
                double fraction = (double)tbPosition.Value / tbPosition.Maximum;
                _player.Position = TimeSpan.FromSeconds(_player.Duration.TotalSeconds * fraction);
            }
            _isDraggingPosition = false;
        }

        private void timerPlayer_Tick(object? sender, EventArgs e)
        {
            if (!_player.HasAudio)
            {
                if (!_isDraggingPosition) tbPosition.Value = 0;
                lblTime.Text = "00:00 / 00:00";
                return;
            }

            var duration = _player.Duration;
            var position = _player.Position;

            if (!_isDraggingPosition && duration.TotalSeconds > 0)
                tbPosition.Value = (int)Math.Clamp(position.TotalSeconds / duration.TotalSeconds * tbPosition.Maximum, 0, tbPosition.Maximum);

            lblTime.Text = $"{FormatTime(position)} / {FormatTime(duration)}";
        }

        private static string FormatTime(TimeSpan t) => $"{(int)t.TotalMinutes:D2}:{t.Seconds:D2}";

        private void ConfigureColumnsGrid()
        {
            dgvTail.ColumnCount = 6;
            dgvTail.Columns[0].Name = "Pos";
            dgvTail.Columns[1].Name = "ID";
            dgvTail.Columns[2].Name = "Title / Artist";
            dgvTail.Columns[3].Name = "BPM";
            dgvTail.Columns[4].Name = "Duration";
            dgvTail.Columns[5].Name = "File";
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
            int totalDuration = 0;

            foreach (var track in Collection)
            {
                dgvTail.Rows.Add(index++, track.Id, $"{track.Title} — {track.Artist}", $"{track.Bpm} BPM",
                    FormatTime(TimeSpan.FromSeconds(track.DurationSeconds)), Path.GetFileName(track.FilePath));
                totalDuration += track.DurationSeconds;
            }

            string structure = _mode switch
            {
                Mode.Own => "Custom Simple List",
                Mode.LinkedList => "LinkedList<T>",
                _ => "List<T>"
            };
            lblStadistics.Text = $"Total en cola: {index - 1} Pistas | Duracion acumulada: {FormatTime(TimeSpan.FromSeconds(totalDuration))} | Estructura: {structure}";
        }

        private async void btnBenchmark_Click(object? sender, EventArgs e)
        {
            int n = (int)numBenchMark.Value;
            btnBenchmark.Enabled = false;
            txtResultsBenchmark.Text = $"Executing {n:N0} intermediate insertions in each structure...";

            try
            {
                txtResultsBenchmark.Text = await Task.Run(() => RunBenchmark(n));
            }
            catch (Exception ex)
            {
                txtResultsBenchmark.Text = "Benchmark error: " + ex.Message;
            }
            finally
            {
                btnBenchmark.Enabled = true;
            }
        }

        private static string RunBenchmark(int n)
        {
            var random = new Random(42);
            var tracks = new Track[n];
            for (int i = 0; i < n; i++)
                tracks[i] = new Track(i, $"Track {i}", "DJ", random.Next(100, 150), 180, "");
            var head = new Track(-1, "Head", "DJ", 120, 200, "");

            var sw = new Stopwatch();

            var ownList = new SimpleLinkedList<Track>();
            ownList.AddToEnd(head);
            sw.Restart();
            foreach (var track in tracks) ownList.PlayNext(track);
            sw.Stop();
            double msOwn = sw.Elapsed.TotalMilliseconds;

            var linked = new LinkedList<Track>();
            var headNode = linked.AddFirst(head);
            sw.Restart();
            foreach (var track in tracks) linked.AddAfter(headNode, track);
            sw.Stop();
            double msLinked = sw.Elapsed.TotalMilliseconds;

            var list = new List<Track> { head };
            sw.Restart();
            foreach (var track in tracks) list.Insert(1, track);
            sw.Stop();
            double msList = sw.Elapsed.TotalMilliseconds;

            return
                $"=== RESULTADOS DE LA PRUEBA DE ESTRÉS ({n:N0} INSERCIONES INTERMEDIAS) ===\r\n" +
                $"• Lista Enlazada Propia (Nodos):  {msOwn,9:F1} ms  [Inserción intermedia O(1) por reconexión]\r\n" +
                $"• .NET LinkedList<T>:            {msLinked,9:F1} ms  [Inserción O(1) con LinkedListNode]\r\n" +
                $"• .NET List<T> (Arreglo Dinámico):  {msList,9:F1} ms  [Insert(idx) O(n): desplaza elementos con Array.Copy]\r\n\r\n" +
                "Conclusión Técnica: En inserciones intermedias frecuentes, las listas enlazadas solo actualizan referencias, " +
                "mientras que List<T> debe desplazar todos los elementos posteriores en memoria y redimensionar su búfer interno a medida que crece.";
        }

        private void tbVolume_Scroll(object sender, EventArgs e)
        {
            float calculatedVolume = tbVolume.Value / 100f;
            _player.Volume = calculatedVolume;
        }

        private void tbPosition_MouseDown(object sender, MouseEventArgs e)
        {
            _isDraggingPosition = true;
        }
    }
}