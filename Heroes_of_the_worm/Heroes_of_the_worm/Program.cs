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
			PhysicalAttacks.Add(new Attack("Punch", Attack.Attacktype.Physical, 10, 1000));
			PhysicalAttacks.Add(new Attack("Kick", Attack.Attacktype.Physical, 20, 1000));
			PhysicalAttacks.Add(new Attack("Bodyslam", Attack.Attacktype.Physical, 50, 1000));


			List<Attack> MagicalAttacks = new List<Attack>();
			MagicalAttacks.Add(new Attack("Zap", Attack.Attacktype.Magical, 15, 1000));
			MagicalAttacks.Add(new Attack("Fireball", Attack.Attacktype.Magical, 30, 1000));
			MagicalAttacks.Add(new Attack("Meteorite", Attack.Attacktype.Magical, 50, 1000));


			List<Hero> Heroes = new List<Hero>();
			Heroes.Add(new Hero("Worm", Hero.Herotype.mage, 75 , PhysicalAttacks[0], MagicalAttacks[0]));
			Heroes.Add(new Hero("Dog", Hero.Herotype.warrior, 300 , PhysicalAttacks[1], MagicalAttacks[1]));
			Heroes.Add(new Hero("Cat", Hero.Herotype.warrior, 250 , PhysicalAttacks[2], MagicalAttacks[2]));
			Heroes.Add(new Hero("Lizard", Hero.Herotype.mage, 250 , PhysicalAttacks[0], MagicalAttacks[1]));
			Heroes.Add(new Hero("Bird", Hero.Herotype.warrior, 150 , PhysicalAttacks[0], MagicalAttacks[2]));
			Heroes.Add(new Hero("Frog", Hero.Herotype.mage, 125 , PhysicalAttacks[1], MagicalAttacks[2]));


			Random rnd = new Random(Convert.ToInt32(DateTime.Now.Second));
			int challenger = 0,reciever = 0;
			while (challenger==reciever)
			{
				challenger = getHero(rnd, Heroes);
				reciever = getHero(rnd, Heroes);
			}
			
			while (!(Heroes[challenger].State == Hero.Herostate.dead || Heroes[reciever].State == Hero.Herostate.dead))
			{
				Heroes[challenger].Fight(Heroes[reciever]);
				Heroes[reciever].Fight(Heroes[challenger]);
			}

			
		}

		private static int getHero(Random rnd, List<Hero> Heroes)
		{
			return rnd.Next(0, Heroes.Count);
		}
	}
}
