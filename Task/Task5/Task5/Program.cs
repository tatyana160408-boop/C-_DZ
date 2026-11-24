using System;
using System.IO;
using System.Text.Json;

namespace HW8
{
    public class GameCharacter
    {
        public string Name { get; set; } = "";
        public int Level { get; set; }
        public double Health { get; set; }
        public string[] Inventory { get; set; } = Array.Empty<string>();
    }

    internal class Program
    {
        static void Main()
        {
            GameCharacter character = new GameCharacter
            {
                Name = "Alex",
                Level = 5,
                Health = 87.5,
                Inventory = new string[] { "Sword", "Shield", "Potion" }
            };

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(character, options);
            Console.WriteLine(json);

            string path = Path.Combine(AppContext.BaseDirectory, "save.json");
            File.WriteAllText(path, json);

            string savedJson = File.ReadAllText(path);
            GameCharacter restored = JsonSerializer.Deserialize<GameCharacter>(savedJson)!;

            Console.WriteLine("=== Загруженный персонаж ===");
            Console.WriteLine($"Имя: {restored.Name}");
            Console.WriteLine($"Уровень: {restored.Level}");
            Console.WriteLine($"Здоровье: {restored.Health}");
            Console.WriteLine($"Инвентарь: {string.Join(", ", restored.Inventory)}");
        }
    }
}
