// SoundCore Engine v2.0 - TecNM Campus Monclova
// Integrantes: [Nombre completo] - [No. de control]
// Fecha: 28/09/2026 | Versión: 2.0

using NAudio.Wave;

namespace SoundCore.EstructurasPropias
{
    /// <summary>
    /// Envoltorio sobre NAudio (WaveOutEvent + AudioFileReader).
    /// Debe crearse en el hilo de UI para que PlaybackStopped llegue a ese mismo hilo.
    /// </summary>
    public sealed class ReproductorAudio : IDisposable
    {
        private WaveOutEvent? _salida;
        private AudioFileReader? _lector;
        private float _volumen = 0.8f;

        /// <summary>Se dispara solo cuando la pista termina de forma natural (no al detener manualmente).</summary>
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

        public void Reproducir(string ruta)
        {
            Detener();

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

            salida.PlaybackStopped += AlTerminar;
            _lector = lector;
            _salida = salida;
            salida.Play();
        }

        public void AlternarPausa()
        {
            if (_salida == null) return;

            if (_salida.PlaybackState == PlaybackState.Playing) _salida.Pause();
            else if (_salida.PlaybackState == PlaybackState.Paused) _salida.Play();
        }

        public void Detener()
        {
            if (_salida != null)
            {
                _salida.PlaybackStopped -= AlTerminar; // evita disparar PistaTerminada al detener a mano
                _salida.Stop();
                _salida.Dispose();
                _salida = null;
            }
            _lector?.Dispose();
            _lector = null;
        }

        private void AlTerminar(object? sender, StoppedEventArgs e)
        {
            Detener();
            PistaTerminada?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose() => Detener();
    }
}
