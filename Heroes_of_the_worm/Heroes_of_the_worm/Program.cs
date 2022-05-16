using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Heroes_of_the_worm.Classes;

namespace Heroes_of_the_worm
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<Attack> PhysicalAttacks = new List<Attack>();
			PhysicalAttacks.Add(new Attack("Punch", Attack.AttackType.Physical, 10, 1000));
			PhysicalAttacks.Add(new Attack("Kick", Attack.AttackType.Physical, 20, 1000));
			PhysicalAttacks.Add(new Attack("Bodyslam", Attack.AttackType.Physical, 50, 1000));


			List<Attack> MagicalAttacks = new List<Attack>();
			MagicalAttacks.Add(new Attack("Zap", Attack.AttackType.Magical, 15, 1000));
			MagicalAttacks.Add(new Attack("Fireball", Attack.AttackType.Magical, 30, 1000));
			MagicalAttacks.Add(new Attack("Meteorite", Attack.AttackType.Magical, 50, 1000));

			

			List<Hero> Heroes = new List<Hero>();
			Heroes.Add(new Hero("Worm", Hero.HeroType.Mage, 75 , PhysicalAttacks[0], MagicalAttacks[0]));
			Heroes.Add(new Hero("Dog", Hero.HeroType.Warrior, 300 , PhysicalAttacks[1], MagicalAttacks[1]));
			Heroes.Add(new Hero("Cat", Hero.HeroType.Warrior, 250 , PhysicalAttacks[2], MagicalAttacks[2]));
			Heroes.Add(new Hero("Lizard", Hero.HeroType.Mage, 250 , PhysicalAttacks[0], MagicalAttacks[1]));
			Heroes.Add(new Hero("Bird", Hero.HeroType.Warrior, 150 , PhysicalAttacks[0], MagicalAttacks[2]));
			Heroes.Add(new Hero("Frog", Hero.HeroType.Mage, 125 , PhysicalAttacks[1], MagicalAttacks[2]));


			Random rnd = new Random(Convert.ToInt32(DateTime.Now.Second));
			int challenger = 0,reciever = 0;
			while (challenger==reciever)
			{
				challenger = getHero(rnd, Heroes);
				reciever = getHero(rnd, Heroes);
			}

			Console.WriteLine($"{Heroes[challenger].Name} HP:{Heroes[challenger].CurrentHp}/{Heroes[challenger].MaxHp} vs {Heroes[reciever].Name} HP:{Heroes[reciever].CurrentHp}/{Heroes[reciever].MaxHp}");
			while (isNooneDead(Heroes, challenger, reciever))
			{
				Heroes[challenger].Fight(Heroes[reciever]);
				Heroes[reciever].Fight(Heroes[challenger]);
			}

			
		}

		private static bool isNooneDead(List<Hero> Heroes, int challenger, int reciever)
		{
			return !(Heroes[challenger].State == Hero.HeroState.Dead || Heroes[reciever].State == Hero.HeroState.Dead);
		}

		private static int getHero(Random rnd, List<Hero> Heroes)
		{
			return rnd.Next(0, Heroes.Count);
		}
	}
}
