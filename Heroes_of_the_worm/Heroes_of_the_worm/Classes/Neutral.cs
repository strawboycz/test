using System;

namespace Heroes_of_the_worm.Classes
{
	public class Neutral : FighterBase
	{
		private static Random rnd = new Random();
		public int ChanceToReflect { get; private set; } = rnd.Next(10,80);
		public Neutral(string name, int maxHp) : base(name,maxHp) {}
		public Neutral(string name, int maxHp,int chanceToReflect) : base(name, maxHp)
		{
			ChanceToReflect = chanceToReflect;
			PhysicalAttack = MagicalAttack = new Attack("Reflect", Attack.AttackType.Physical, 10, 1000);
		}
	}
}