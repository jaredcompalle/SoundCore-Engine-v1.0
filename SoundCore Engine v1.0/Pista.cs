// SoundCore Engine v2.0 - TecNM Campus Monclova
// Integrantes: [Nombre completo] - [No. de control]
// Fecha: 28/09/2026 | Versión: 2.0

namespace SoundCore_Engine_v1
{
    /// <summary>Pista de audio real: incluye la ruta del archivo elegido en el explorador.</summary>
    public record Pista(int Id, string Titulo, string Artista, int Bpm, int DuracionSegundos, string RutaArchivo)
    {
        public override string ToString() =>
            $"[ID: {Id:D3}] {Titulo} - {Artista} | {Bpm} BPM ({DuracionSegundos}s)";
    }
}
