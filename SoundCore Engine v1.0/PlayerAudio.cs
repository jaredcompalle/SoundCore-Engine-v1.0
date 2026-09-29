// SoundCore Engine v2.0 - TecNM Campus Monclova
// Integrantes: Rosembert Jared Ortiz Reyes - I25050406
// Fecha: 28/09/2026 | Versión: 1.0

using NAudio.Wave;

namespace SoundCore.EstructurasPropias
{
  
    public sealed class PlayerAudio : IDisposable
    {
        private WaveOutEvent? _salida;
        private AudioFileReader? _lector;
        private float _volumen = 0.8f;

        public event EventHandler? PistaTerminada;

        public bool TieneAudio => _lector != null;
        public bool EstaReproduciendo => _salida?.PlaybackState == PlaybackState.Playing;
        public TimeSpan Duracion => _lector?.TotalTime ?? TimeSpan.Zero;

        public TimeSpan Posicion
        {
            get => _lector?.CurrentTime ?? TimeSpan.Zero;
            set { if (_lector != null) _lector.CurrentTime = value; }
        }

        public float Volumen
        {
            get => _volumen;
            set
            {
                _volumen = Math.Clamp(value, 0f, 1f);
                if (_lector != null) _lector.Volume = _volumen;
            }
        }

        public void Play(string ruta)
        {
            Stop();

            var lector = new AudioFileReader(ruta) { Volume = _volumen };
            var salida = new WaveOutEvent();
            try
            {
                salida.Init(lector);
            }
            catch
            {
                salida.Dispose();
                lector.Dispose();
                throw;
            }

            salida.PlaybackStopped += UponCompletion;
            _lector = lector;
            _salida = salida;
            salida.Play();
        }

        public void TogglePause()
        {
            if (_salida == null) return;

            if (_salida.PlaybackState == PlaybackState.Playing) _salida.Pause();
            else if (_salida.PlaybackState == PlaybackState.Paused) _salida.Play();
        }

        public void Stop()
        {
            if (_salida != null)
            {
                _salida.PlaybackStopped -= UponCompletion;
                _salida.Stop();
                _salida.Dispose();
                _salida = null;
            }
            _lector?.Dispose();
            _lector = null;
        }

        private void UponCompletion(object? sender, StoppedEventArgs e)
        {
            Stop();
            PistaTerminada?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose() => Stop();
    }
}
