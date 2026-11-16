using System;
using System.IO;
using System.Text.Json;

namespace Task6
{
    public class GameSettings
    {
        public string PlayerName { get; set; } = "";
        public int Difficulty { get; set; }
        public int MusicVolume { get; set; }
    }

    internal class Program
    {
        static void Main()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "settings.json");

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                GameSettings settings = JsonSerializer.Deserialize<GameSettings>(json)!;

                Console.WriteLine($"Имя игрока: {settings.PlayerName}");
                Console.WriteLine($"Сложность: {settings.Difficulty}");
                Console.WriteLine($"Громкость музыки: {settings.MusicVolume}");
            }
            else
            {
                Console.WriteLine("Файл настроек не найден. Создаём новые настройки.");

                GameSettings settings = new GameSettings();

                Console.Write("Имя игрока: ");
                settings.PlayerName = Console.ReadLine() ?? "";

                Console.Write("Сложность (1 — легко, 2 — нормально, 3 — сложно): ");
                settings.Difficulty = int.Parse(Console.ReadLine()!);

                Console.Write("Громкость музыки (0–100): ");
                settings.MusicVolume = int.Parse(Console.ReadLine()!);

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(settings, options);
                File.WriteAllText(path, json);
            }
        }
    }
}
