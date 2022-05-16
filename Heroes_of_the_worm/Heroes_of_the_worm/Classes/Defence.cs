namespace Heroes_of_the_worm.Classes
{
	public class Defence
	{
		public enum DefenceType {None, Physical, Magical};
		public string Name { get; private set; }
		public DefenceType Type { get; private set; }
		public int BaseBlock { get; private set; }
		public int Cooldown { get; private set; }

		public Defence(string name, DefenceType type, int baseBlock, int cooldown)
		{
			this.Name = name;
			this.Type = type;
			this.BaseBlock = baseBlock;
			this.Cooldown = cooldown;
		}
	}
}