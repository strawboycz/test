using System;
using System.Threading;

namespace Heroes_of_the_worm.Classes
{
	public class Hero:FighterBase
	{
		public enum HeroType {None, Warrior, Mage};
		public enum HeroState {None, Dead, Alive};
		public Hero(string name, HeroType type, int maxHp, Attack physicalAttack, Attack magicalAttack) : base(name, maxHp)
		{
			this.Type = type;
			this.PhysicalAttack = physicalAttack;
			this.MagicalAttack = magicalAttack;
			PhysicalDefence = new Defence("Harden", Defence.DefenceType.Physical, 10, 40);
			MagicalDefence = new Defence("Substitute", Defence.DefenceType.Magical, 15, 50);
		}
		
	}
}