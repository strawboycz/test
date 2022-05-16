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
}