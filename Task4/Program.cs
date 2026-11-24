using System;
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

            GameCharacter restored = JsonSerializer.Deserialize<GameCharacter>(json)!;

            Console.WriteLine($"Имя персонажа: {restored.Name}");
            Console.WriteLine($"Предметов в инвентаре: {restored.Inventory.Length}");
        }
    }
}
