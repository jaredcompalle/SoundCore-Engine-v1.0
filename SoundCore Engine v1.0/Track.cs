// SoundCore Engine v2.0 - TecNM Campus Monclova
// Authors: Rosembert Jared Ortiz Reyes - I25050406
// Date: 28/09/2026 | Version: 1.0

namespace SoundCore_Engine_v1
{
    public record Track(int Id, string Title, string Artist, int Bpm, int DurationSeconds, string FilePath)
    {
        public override string ToString() =>
            $"[ID: {Id:D3}] {Title} - {Artist} | {Bpm} BPM ({DurationSeconds}s)";
    }
}