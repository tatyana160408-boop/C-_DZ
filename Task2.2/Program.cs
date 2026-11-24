using System;
using System.Collections.Generic;

public enum Rarity
{
    Common,
    Rare,
    Legendary
}

public interface IUsableItem
{
    string Name { get; }
    string Description { get; }
    Rarity Rarity { get; }

    void Use(Player player);
}

public class Player
{
    public int HP = 100;
    public int Damage = 20;
    public string Name;

    public Player(string name)
    {
        Name = name;
    }

    public void Heal(int amount)
    {
        HP += amount;
    }

    public void IncreaseDamage(int amount)
    {
        Damage += amount;
    }
}

public class HealthPotion : IUsableItem
{
    public string Name => "Зелье здоровья";
    public string Description => "Восстанавливает 50 HP.";
    public Rarity Rarity => Rarity.Common;

    public void Use(Player player)
    {
        player.Heal(50);
        Console.WriteLine("Вы выпили зелье и восстановили 50 HP.");
    }
}

public class Sword : IUsableItem
{
    public string Name => "Меч";
    public string Description => "Увеличивает урон на 10.";
    public Rarity Rarity => Rarity.Rare;

    public void Use(Player player)
    {
        player.IncreaseDamage(10);
        Console.WriteLine("Вы взяли меч. Урон увеличился на 10.");
    }
}

public class AncientScroll : IUsableItem
{
    public string Name => "Древний свиток";
    public string Description => "Отнимает 20 HP и увеличивает урон на 30.";
    public Rarity Rarity => Rarity.Legendary;

    public void Use(Player player)
    {
        player.HP -= 20;
        player.IncreaseDamage(30);
        Console.WriteLine("Свиток нанёс вам 20 урона и увеличил ваш урон на 30.");
    }
}

public class Program
{
    public static void Main()
    {
        Player player = new Player("Артур");

        List<IUsableItem> items = new List<IUsableItem>
        {
            new HealthPotion(),
            new Sword(),
            new AncientScroll()
        };

        foreach (IUsableItem item in items)
        {
            item.Use(player);
        }

        Console.WriteLine($"Игрок: {player.Name}");
        Console.WriteLine($"HP: {player.HP}");
        Console.WriteLine($"Урон: {player.Damage}");

        if (player.HP > 0)
        {
            Console.WriteLine("Игрок жив.");
        }
        else
        {
            Console.WriteLine("Игрок мёртв.");
        }
    }
}
