using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Heroes_of_the_worm.Classes
{
		#region Hero

		
				public class Hero
				{
					private const int LowestDelay = 1000;
					private const int HighestDelay = 5000;
					private const int CritChance = 2;
					public enum Herotype {none, warrior, mage};
					public enum Herostate {none, dead, alive};
					public Attack PhysicalAttack { get; private set; }
					public Attack MagicalAttack { get; private set; }
					public string Name { get; private set; }
					public Herotype Type { get; private set; } = Herotype.none;
					public Herostate State { get; private set; } = Herostate.none;
					public int CurrentHp { get; private set; }
					public int MaxHp { get; private set; }

					public Hero(string name, Herotype type, int maxHp, Attack physicalAttack, Attack magicalAttack)
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

						dealDmg(whom, attack, rnd);

						if (shouldCrit(rnd))
							printCritAttackMessage(this,whom, attack);
						else
							printAttackMessage(this, whom, attack);
						
						if (shouldKill(whom))
								kill(whom);
						if (shouldKill(this))
								kill(this);

						Thread.Sleep(rnd.Next(LowestDelay, HighestDelay));
					}

					private static void printCritAttackMessage(Hero challenger,Hero reciever, Attack attack)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Critical hit!");
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.WriteLine(
							$"{challenger.Name}({challenger.Type}) attacked {reciever.Name}({reciever.Type}) with {attack.Name}({attack.Type}) and dealt {attack.BaseDamage * 3}dmg...");
						Console.ResetColor();
					}

					private static void dealDmg(Hero whom, Attack attack,Random rnd)
					{
						if (shouldCrit(rnd))
							whom.CurrentHp -= attack.BaseDamage*3;
						else
							whom.CurrentHp -= attack.BaseDamage;
					}

					private static bool shouldCrit(Random rnd)
					{
						return rnd.Next(1, 100) <= CritChance;
					}

					private Attack returnAttack(Random rnd)
					{
						Attack attack = new Attack("", Attack.Attacktype.None, 0, 0);
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
						hero.State = Herostate.dead;
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
						if (hero.State == Herostate.dead)
						{
							return true;
						}

						return false;
					}
				}

		#endregion

		#region Attack

				public class Attack
				{
						public enum Attacktype { None, Physical, Magical };
						public string Name { get; private set; }

						public Attacktype Type { get; private set; }
						public int BaseDamage { get; private set; }
						public int Cooldown { get; private set; }

						public Attack(string name, Attacktype type, int baseDamage, int cooldown)
						{
								this.Name = name;
								this.Type = type;
								this.BaseDamage = baseDamage;
								this.Cooldown = cooldown;
						}
				}

		#endregion
}
