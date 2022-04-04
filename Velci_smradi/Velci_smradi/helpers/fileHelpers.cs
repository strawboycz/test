using System.IO;

namespace Velci_smradi.helpers
{
	public class FileHelpers
	{
		public static void EnsureDirExists(string dirName)
		{
			if (!Directory.Exists(dirName))
				Directory.CreateDirectory(dirName);
		}

		public static void EnsureFileDeleted(string filepath)
		{
			if (File.Exists(filepath))  File.Delete(filepath); 
		}
	}
}