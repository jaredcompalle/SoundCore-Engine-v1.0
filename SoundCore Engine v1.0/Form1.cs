// SoundCore Engine v2.0 - TecNM Campus Monclova
// Authors: Rosembert Jared Ortiz Reyes - I25050406
// Date: 28/09/2026 | Version: 1.0

using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using SoundCore.EstructurasPropias;
using System.Diagnostics;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            int bpm = (int)Math.Round(BpmDetector.Detect(path));

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
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                openFileDialog.Title = "Select Dataset File for Benchmark";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    btnBenchmark.Enabled = false;
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        txtResultsBenchmark.Text = "Loading dataset from file...";
                        Track[] tracks = await Task.Run(() => LoadTracksFromFile(filePath));

                        if (tracks.Length == 0)
                        {
                            txtResultsBenchmark.Text = "Benchmark error: Selected file is empty.";
                            return;
                        }

                        txtResultsBenchmark.Text = $"Executing {tracks.Length:N0} intermediate insertions in each structure...";
                        txtResultsBenchmark.Text = await Task.Run(() => RunBenchmark(tracks));
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
            }
        }

        private static string RunBenchmark(Track[] tracks)
        {
            int count = tracks.Length;
            var headTrack = new Track(-1, "Head", "DJ", 120, 200, "");

            var stopwatch = new Stopwatch();

            // 1. Custom Linked List Test
            var ownList = new SimpleLinkedList<Track>();
            ownList.AddToEnd(headTrack);
            stopwatch.Restart();
            foreach (var track in tracks) ownList.PlayNext(track);
            stopwatch.Stop();
            double msOwn = stopwatch.Elapsed.TotalMilliseconds;

            // 2. .NET LinkedList<T> Test
            var linkedList = new LinkedList<Track>();
            var headNode = linkedList.AddFirst(headTrack);
            stopwatch.Restart();
            foreach (var track in tracks) linkedList.AddAfter(headNode, track);
            stopwatch.Stop();
            double msLinked = stopwatch.Elapsed.TotalMilliseconds;

            // 3. .NET List<T> Test
            var dynamicArray = new List<Track> { headTrack };
            stopwatch.Restart();
            foreach (var track in tracks) dynamicArray.Insert(1, track);
            stopwatch.Stop();
            double msList = stopwatch.Elapsed.TotalMilliseconds;

            return
                $"=== STRESS TEST RESULTS ({count:N0} INTERMEDIATE INSERTIONS) ===\r\n" +
                $"• Custom Linked List (Nodes):      {msOwn,9:F1} ms  [O(1) intermediate insertion via reference updates]\r\n" +
                $"• .NET LinkedList<T>:              {msLinked,9:F1} ms  [O(1) insertion using LinkedListNode]\r\n" +
                $"• .NET List<T> (Dynamic Array):     {msList,9:F1} ms  [Insert(idx) O(n): shifts elements using Array.Copy]\r\n\r\n" +
                "Technical Conclusion: For frequent intermediate insertions, linked lists only update memory references, " +
                "whereas List<T> must shift all subsequent elements in memory and resize its internal buffer as it grows.";
        }

        private async void btnGenerateFile_Click(object? sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                saveFileDialog.FileName = "tracks.txt";
                saveFileDialog.Title = "Save Dataset File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    btnGenerateFile.Enabled = false;
                    txtResultsBenchmark.Text = "Generating 25,000 tracks in file...";

                    try
                    {
                        string filePath = saveFileDialog.FileName;
                        await Task.Run(() => GenerateTracksFile(filePath, (int)numBenchMark.Value));
                        txtResultsBenchmark.Text = $"File successfully created at:\r\n{filePath}";
                    }
                    catch (Exception ex)
                    {
                        txtResultsBenchmark.Text = "File generation error: " + ex.Message;
                    }
                    finally
                    {
                        btnGenerateFile.Enabled = true;
                    }
                }
            }
        }

        public static void GenerateTracksFile(string filePath, int totalTracks = 25000)
        {
            string[] genres = { "Electronic", "Rock", "Pop", "HipHop", "House", "Techno", "Ambient" };
            string[] artists = { "DJ Nova", "Beatmaker", "EchoPulse", "Syntax", "LunarShift", "Aura", "Vortex" };

            var random = new Random();

            using (var writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                for (int i = 1; i <= totalTracks; i++)
                {
                    string artist = artists[random.Next(artists.Length)];
                    string genre = genres[random.Next(genres.Length)];
                    string title = $"{genre} Track #{i}";
                    int bpm = random.Next(80, 180);
                    int duration = random.Next(120, 360);
                    string path = $"C:\\Music\\{genre}\\track_{i}.mp3";

                    writer.WriteLine($"{i}|{title}|{artist}|{bpm}|{duration}|{path}");
                }
            }
        }

        public static Track[] LoadTracksFromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            var tracks = new Track[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('|'); // Change separator if using commas or tabs

                int id = int.Parse(parts[0]);
                string title = parts[1];
                string artist = parts[2];
                int bpm = int.Parse(parts[3]);
                int duration = int.Parse(parts[4]);
                string path = parts[5];

                tracks[i] = new Track(id, title, artist, bpm, duration, path);
            }

            return tracks;
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

        public static class BpmDetector
        {
            private const int HopSize = 512;        // muestras por ventana de energía
            private const double MinBpm = 70;
            private const double MaxBpm = 180;

            public static double Detect(string path)
            {
                // 1) Leer audio (mp3, wav, aac, etc.) como float y calcular la energía por ventana
                var envelope = new List<double>();
                int sampleRate;

                using (var reader = new AudioFileReader(path))
                {
                    sampleRate = reader.WaveFormat.SampleRate;
                    int channels = reader.WaveFormat.Channels;
                    var byteBuffer = new byte[HopSize * channels * sizeof(float)];
                    var buffer = new float[HopSize * channels];
                    int bytesRead;

                    while ((bytesRead = reader.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                    {
                        // Convertir bytes a floats
                        for (int i = 0; i < bytesRead / sizeof(float); i++)
                            buffer[i] = BitConverter.ToSingle(byteBuffer, i * sizeof(float));

                        double sum = 0;
                        int frames = bytesRead / (channels * sizeof(float));
                        for (int f = 0; f < frames; f++)
                        {
                            // mezclar a mono
                            double mono = 0;
                            for (int c = 0; c < channels; c++)
                                mono += buffer[f * channels + c];
                            mono /= channels;
                            sum += mono * mono;
                        }
                        double rms = Math.Sqrt(sum / Math.Max(frames, 1));
                        envelope.Add(Math.Log(1 + 100 * rms)); // compresión logarítmica
                    }
                }

                if (envelope.Count < 100)
                    throw new InvalidOperationException("El audio es demasiado corto.");

                // 2) Función de onsets: aumentos positivos de energía
                var onset = new double[envelope.Count];
                for (int i = 1; i < envelope.Count; i++)
                    onset[i] = Math.Max(0, envelope[i] - envelope[i - 1]);

                double mean = onset.Average();
                for (int i = 0; i < onset.Length; i++)
                    onset[i] -= mean;

                // 3) Autocorrelación en el rango de BPM buscado
                double fps = (double)sampleRate / HopSize;   // ventanas por segundo
                double bestBpm = 0, bestScore = double.MinValue;

                for (double bpm = MinBpm; bpm <= MaxBpm; bpm += 0.25)
                {
                    double lag = 60.0 * fps / bpm;            // lag fraccionario
                    double score = Autocorrelation(onset, lag);
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestBpm = bpm;
                    }
                }

                return bestBpm;
            }

            // Autocorrelación con interpolación lineal para lags no enteros
            private static double Autocorrelation(double[] x, double lag)
            {
                int l0 = (int)Math.Floor(lag);
                double frac = lag - l0;
                double sum = 0;
                int n = 0;

                for (int i = 0; i + l0 + 1 < x.Length; i++)
                {
                    double shifted = x[i + l0] * (1 - frac) + x[i + l0 + 1] * frac;
                    sum += x[i] * shifted;
                    n++;
                }
                return n > 0 ? sum / n : 0;
            }
        }
    }
}


