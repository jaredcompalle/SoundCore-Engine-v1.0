// SoundCore Engine v2.0 - TecNM Campus Monclova
// Integrantes: Rosembert Jared Ortiz Reyes - I25050406
// Fecha: 28/09/2026 | Versión: 1.0

namespace SoundCore_Engine_v1
{
    public record Track(int Id, string Titulo, string Artista, int Bpm, int DuracionSegundos, string RutaArchivo)
    {
        public override string ToString() =>
            $"[ID: {Id:D3}] {Titulo} - {Artista} | {Bpm} BPM ({DuracionSegundos}s)";
    }
}
