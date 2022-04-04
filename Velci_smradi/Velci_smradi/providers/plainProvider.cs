using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Velci_smradi.enums;
using Velci_smradi.helpers;

namespace Velci_smradi.providers
{
		public class plainProvider
		{
			public static void SaveDataToFile(Dictionary<string, SmellLevel> smellyBoys, char separator, string filePath)
			{
				FileHelpers.EnsureFileDeleted(filePath);

				var dirName = Path.GetDirectoryName(filePath);

				FileHelpers.EnsureDirExists(dirName);
				foreach (KeyValuePair<string, SmellLevel> smellyBoy in smellyBoys)
				{
					File.AppendAllText(filePath, $"{smellyBoy.Key}{separator}{smellyBoy.Value}\n");
				}
			}


			public static Dictionary<string, SmellLevel> LoadDataFromFile(string filepath, char separator)
			{
				if (!File.Exists(filepath))
				{
					return null;
				}

				var smellyBoys = new Dictionary<string, SmellLevel>();
				var lines = File.ReadAllLines(filepath);
				foreach (var line in lines)
				{
					if (line.Length == 0)
					{
						return smellyBoys;
					}
					string name = line.Split(separator)[0];
					SmellLevel level = (SmellLevel)Enum.Parse((typeof(SmellLevel)), (line.Split(separator)[1]));
					smellyBoys.Add(name, level);
				}
				return smellyBoys;
			}
		}
}
