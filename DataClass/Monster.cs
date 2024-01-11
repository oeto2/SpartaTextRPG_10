using System;

public class Monster
{
    internal readonly int Length;
    private Monster randomMonster;

    public string Name { get; set; }
    public float Health { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Level { get; set; }
    public int Damage { get; set; }
    public bool IsDead { get; set; }

    public Monster(int level, string name, float hp, int atk, int def)
    {
        Level = level;
        Name = name;
        Health = hp;
        Atk = atk;
        Def = def;
        IsDead = false;
    }

    public void OnDamage(int damage)
    {
        Health -= damage;
        Console.WriteLine($"{Name}은(는) {damage}의 피해를 입었다!");
    }

    public bool CheckIsDead()
    {
        return Health <= 0;
    }
}
