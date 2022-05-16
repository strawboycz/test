using System;

namespace Heroes_of_the_worm.Classes
{
	public class Neutral : FighterBase
	{
		private static Random rnd = new Random();

		public Neutral(string name, int maxHp) : base(name, maxHp)
		{
			PhysicalAttack = MagicalAttack = new Attack("Reflect", Attack.AttackType.Physical, 10, 1000);
			ChanceToReflect = rnd.Next(10, 80);
			State = Hero.HeroState.Alive;
		}
		public Neutral(string name, int maxHp,int chanceToReflect) : base(name, maxHp)
		{
			PhysicalAttack = MagicalAttack = new Attack("Reflect", Attack.AttackType.Physical, 10, 1000);
			ChanceToReflect = rnd.Next(10, 80);
			State = Hero.HeroState.Alive;
				}
	}
}