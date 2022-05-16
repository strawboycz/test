namespace Heroes_of_the_worm.Classes
{
	public class FighterBase
	{ 
		public string Name { get; private set; }
		public int MaxHp { get; private set; }
		public int CurrentHp { get; private set; }

		public FighterBase(string name, int maxHp)
		{
			this.Name = name;
			this.CurrentHp = this.MaxHp = maxHp;
		}
	}
}