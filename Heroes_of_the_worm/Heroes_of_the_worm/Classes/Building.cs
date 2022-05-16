using System;

namespace Heroes_of_the_worm.Classes
{
	public class Building : FighterBase
	{
		public enum BuildingType {None, Neutral, Barracks, Defensive}

		public BuildingType Type { get; private set; } = BuildingType.None;

		public Attack AutoAttack { get; private set; }
		public int Cooldown { get; private set; }

		public Building(string name,int maxHp, BuildingType type) : base(name,maxHp)
		{
			Type = type;
		}
		public Building(string name, int maxHp, BuildingType type, Attack autoAttack, int cooldown ) : base(name, maxHp)
		{
			Type = type;
			AutoAttack = autoAttack;
			Cooldown = cooldown;
		}
	}

	public class Neutral : FighterBase
	{
		private static Random rnd = new Random();
		public int ChanceToReflect { get; private set; } = rnd.Next(10,80);

		public int ReflectedDamage { get; private set; } = 25;
		public Neutral(string name, int maxHp) : base(name,maxHp) {}
		public Neutral(string name, int maxHp,int chanceToReflect, int reflectedDamage) : base(name, maxHp)
		{
			ChanceToReflect = chanceToReflect;
			ReflectedDamage = reflectedDamage;
		}
	}
}