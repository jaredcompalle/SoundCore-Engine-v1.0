// SoundCore Engine v2.0 - TecNM Campus Monclova
// Authors: Rosembert Jared Ortiz Reyes - I25050406
// Date: 28/09/2026 | Version: 1.0

using NAudio.Wave;

namespace SoundCore.EstructurasPropias
{
    public sealed class PlayerAudio : IDisposable
    {
        private WaveOutEvent? _output;
        private AudioFileReader? _reader;
        private float _volume = 0.8f;

        public event EventHandler? TrackEnded;

        public bool HasAudio => _reader != null;
        public bool IsPlaying => _output?.PlaybackState == PlaybackState.Playing;
        public TimeSpan Duration => _reader?.TotalTime ?? TimeSpan.Zero;

        public TimeSpan Position
        {
            get => _reader?.CurrentTime ?? TimeSpan.Zero;
            set { if (_reader != null) _reader.CurrentTime = value; }
        }

        public float Volume
        {
            get => _volume;
            set
            {
                _volume = Math.Clamp(value, 0f, 1f);
                if (_reader != null) _reader.Volume = _volume;
            }
        }

        public void Play(string filePath)
        {
            Stop();

            var reader = new AudioFileReader(filePath) { Volume = _volume };
            var output = new WaveOutEvent();
            try
            {
                output.Init(reader);
            }
            catch
            {
                output.Dispose();
                reader.Dispose();
                throw;
            }

            output.PlaybackStopped += OnPlaybackStopped;
            _reader = reader;
            _output = output;
            output.Play();
        }

        public void TogglePause()
        {
            if (_output == null) return;

            if (_output.PlaybackState == PlaybackState.Playing) _output.Pause();
            else if (_output.PlaybackState == PlaybackState.Paused) _output.Play();
        }

        public void Stop()
        {
            if (_output != null)
            {
                _output.PlaybackStopped -= OnPlaybackStopped;
                _output.Stop();
                _output.Dispose();
                _output = null;
            }
            _reader?.Dispose();
            _reader = null;
        }

        private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
        {
            Stop();
            TrackEnded?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose() => Stop();
    }
}