using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Heroes_of_the_worm.Classes
{


				public class Attack
				{
						public enum AttackType { None, Physical, Magical };
						public string Name { get; private set; }

						public AttackType Type { get; private set; }
						public int BaseDamage { get; private set; }
						public int Cooldown { get; private set; }

						public Attack(string name, AttackType type, int baseDamage, int cooldown)
						{
								this.Name = name;
								this.Type = type;
								this.BaseDamage = baseDamage;
								this.Cooldown = cooldown;
						}
				}
}
