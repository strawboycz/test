using System;
using System.Threading;

namespace Heroes_of_the_worm.Classes
{
	public class Hero
	{
		private const int LowestDelay = 1000;
		private const int HighestDelay = 5000;
		private const int CritChance = 2;
		public enum HeroType {None, Warrior, Mage};
		public enum HeroState {None, Dead, Alive};
		public Attack PhysicalAttack { get; private set; }
		public Attack MagicalAttack { get; private set; }
		public string Name { get; private set; }
		public HeroType Type { get; private set; } = HeroType.None;
		public HeroState State { get; private set; } = HeroState.Alive;
		public int CurrentHp { get; private set; }
		public int MaxHp { get; private set; }
		public Defence PhysicalDefence = new Defence("Harden", Defence.DefenceType.Physical, 10, 40);
		public Defence MagicalDefence = new Defence("Substitute", Defence.DefenceType.Magical, 15, 50);

		public Hero(string name, HeroType type, int maxHp, Attack physicalAttack, Attack magicalAttack)
		{
			this.MaxHp = maxHp;
			this.CurrentHp = maxHp;
			this.Name = name;
			this.Type = type;
			this.PhysicalAttack = physicalAttack;
			this.MagicalAttack = magicalAttack;
		}
		public void Fight(Hero whom)
		{
			if (heroIsDead(whom))
			{
				printDeathMessage(whom);
				printVictoryMessage(this);
				return;
			}
			if (heroIsDead(this))
			{
				Console.ResetColor();
				printDeathMessage(this);
				printVictoryMessage(whom);
				return;
			}
			printStatus(this,whom);

			Random rnd = new Random(Convert.ToInt32(DateTime.Now.Second));

			Attack attack = returnAttack(rnd);

			Defence defence = returnDeffence(rnd,attack);

			dealDmg(whom, attack, rnd,defence);


			if (!shouldCrit(rnd))
				printAttackMessage(this, whom, attack);
			else
				printCritAttackMessage(this, whom, attack);

			if (defenceExists(defence))
			{
				printDefenceMessage(this, defence);
			}

			if (shouldKill(whom))
				kill(whom);
			if (shouldKill(this))
				kill(this);

			Thread.Sleep(rnd.Next(LowestDelay, HighestDelay));
		}

		private static bool defenceExists(Defence defence)
		{
			if (defence.BaseBlock > 0) return true;
			return false;
		}

		private void printDefenceMessage(Hero hero, Defence defence)
		{
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine($"{hero.Name} defended itself with {defence.Name}({defence.BaseBlock} block)");
			Console.ResetColor();

		}

		private Defence returnDeffence(Random rnd,Attack attack)
		{
			Defence defence = new Defence("", Defence.DefenceType.None, 0, 0);
			if (attack.Type == Attack.AttackType.Physical)
			{
				if (rnd.Next(1, 100) <= PhysicalDefence.Cooldown)
				{
					defence = PhysicalDefence;
				}
			}
			else
			{
				if (rnd.Next(1, 100) <= MagicalDefence.Cooldown)
				{
					defence = MagicalDefence;
				}
			}
			return defence;
		}

		private static void printCritAttackMessage(Hero challenger,Hero reciever, Attack attack)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Critical hit! The attack dealt triple damage!");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(
				$"{challenger.Name}({challenger.Type}) attacked {reciever.Name}({reciever.Type}) with {attack.Name}({attack.Type}) and dealt {attack.BaseDamage * 3}dmg...");
			Console.ResetColor();
		}

		private static void dealDmg(Hero whom, Attack attack,Random rnd,Defence defence)
		{
			if (shouldCrit(rnd))
				whom.CurrentHp -= attack.BaseDamage*3 - defence.BaseBlock;
			else
				whom.CurrentHp -= attack.BaseDamage - defence.BaseBlock;
		}

		private static bool shouldCrit(Random rnd)
		{
			return rnd.Next(1, 100) <= CritChance;
		}

		private Attack returnAttack(Random rnd)
		{
			Attack attack = new Attack("", Attack.AttackType.None, 0, 0);
			if (rnd.Next(1, 100) <= 50)
			{
				attack = this.PhysicalAttack;
			}
			else
			{
				attack = this.MagicalAttack;
			}

			return attack;
		}

		private void printStatus(Hero challenger, Hero reciever)
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine($"{challenger.Name} now has {challenger.CurrentHp}/{challenger.MaxHp} HP left");
			Console.WriteLine($"{reciever.Name} now has {reciever.CurrentHp}/{reciever.MaxHp} HP left");
			Console.ResetColor();
		}

		private bool shouldKill(Hero hero)
		{
			if (hero.CurrentHp <= 0)
				return true;
			return false;
		}

		private void kill(Hero hero)
		{
			hero.CurrentHp = 0;
			hero.State = HeroState.Dead;
		}

		private void printVictoryMessage(Hero hero)
		{
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine($"{hero.Name} has won!");
			Console.ResetColor();
		}

		private void printAttackMessage(Hero challenger, Hero reciever, Attack attack)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"{challenger.Name}({challenger.Type}) attacked {reciever.Name}({reciever.Type}) with {attack.Name}({attack.Type}) and dealt {attack.BaseDamage}dmg...");
			Console.ResetColor();
		}

		private static void printDeathMessage(Hero hero)
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine($"\n{hero.Name} has been killed");
			Console.ResetColor();

		}

		private static bool heroIsDead(Hero hero)
		{
			if (hero.State == HeroState.Dead)
			{
				return true;
			}

			return false;
		}
	}
}