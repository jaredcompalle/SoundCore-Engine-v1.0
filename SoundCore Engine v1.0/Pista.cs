namespace SoundCore.Modelos
{
    public record Pista(int Id, string Titulo, string Artista, int Bpm, int DuracionSegundos)
    {
        public override string ToString() =>
            $"[ID: {Id:D3}] {Titulo} - {Artista} | {Bpm} BPM ({DuracionSegundos}s)";
    }
}
