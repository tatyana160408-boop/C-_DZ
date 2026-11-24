using System;

public interface IDamageCalculator
{
    int CalculateDamage(int baseDamage, int targetArmor);
}

public class PhysicalDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        return Math.Max(1, baseDamage - targetArmor / 2);
    }
}

public class MagicDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = Math.Max(1, baseDamage - targetArmor / 4);

        if (targetArmor == 0)
        {
            damage *= 2;
        }

        return damage;
    }
}

public class FireDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = Math.Max(1, baseDamage - targetArmor / 10);

        if (baseDamage > 50)
        {
            damage += 10;
        }

        return damage;
    }
}

public class Unit
{
    public string Name;
    public int HP;
    public int Armor;

    public Unit(string name, int hp, int armor)
    {
        Name = name;
        HP = hp;
        Armor = armor;
    }
}

public class DamageSystem
{
    private readonly IDamageCalculator calculator;

    public DamageSystem(IDamageCalculator calculator)
    {
        this.calculator = calculator;
    }

    public void ApplyDamage(Unit target, int baseDamage)
    {
        int damage = calculator.CalculateDamage(baseDamage, target.Armor);
        target.HP = Math.Max(0, target.HP - damage);

        Console.WriteLine($"Юнит {target.Name} получил {damage} урона. Осталось HP: {target.HP}");
    }
}

public class Program
{
    public static void Main()
    {
        Unit target = new Unit("Орк", 200, 20);

        DamageSystem physical = new DamageSystem(new PhysicalDamageCalculator());
        DamageSystem magic = new DamageSystem(new MagicDamageCalculator());
        DamageSystem fire = new DamageSystem(new FireDamageCalculator());

        physical.ApplyDamage(target, 60);
        magic.ApplyDamage(target, 60);
        fire.ApplyDamage(target, 60);

        // Для ледяного урона нужно создать IceDamageCalculator,
        // реализовать в нём IDamageCalculator и передать его в DamageSystem.
        // Код DamageSystem при этом менять не нужно.
    }
}
