using System;

public class Monster
{
    internal readonly int Length;
    private Monster randomMonster;

    public int Number { get; set; }
    public string Name { get; set; }
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Level { get; set; }
    public int Damage { get; set; }
    public bool IsDead { get; set; }

    public Monster(int number, int level, string name, float hp, float maxhp, int atk, int def)
    {
        Number = number;
        Level = level;
        Name = name;
        MaxHealth = maxhp;
        Health = hp;
        Atk = atk;
        Def = def;
        IsDead = false;
    }
}
