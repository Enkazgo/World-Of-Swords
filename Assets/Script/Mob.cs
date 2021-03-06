using System;


public abstract class Mob
{
    public int HP;
    public int Resist;
    public int Speed;
    public int Attack;
    public int Money;
    public int XP;
    //public List<string> Loot;
}

class Sanglier : Mob
{
	public Sanglier(int hp, int resist, int speed, int money, int xp, int attack)
	{
        HP = hp;
        Resist = resist;
        Speed = speed;
        Money = money;
        XP = xp;
        Attack = attack;
	}
}
