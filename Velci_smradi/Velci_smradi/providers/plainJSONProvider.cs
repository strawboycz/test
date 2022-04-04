using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using Velci_smradi.enums;
using Velci_smradi.helpers;

namespace Velci_smradi.providers
{
		internal class plainJSONProvider
		{
			public static void SaveDataToFile(Dictionary<string, SmellLevel> smellyBoys, string filePath)
			{
				FileHelpers.EnsureFileDeleted(filePath);

				var dirName = Path.GetDirectoryName(filePath);

				FileHelpers.EnsureDirExists(dirName);

				var sb = new StringBuilder();
				sb.AppendLine($"\"SmellyBoys\" : {{");
				foreach (KeyValuePair<string, SmellLevel> smellyBoy in smellyBoys)
				{
					sb.AppendLine($"\t\"{smellyBoy.Key}\" : {{\n\t\t\"SmellLevel\":\"{smellyBoy.Value}\"\n\t}},");
				}
				sb.AppendLine("}");
				File.AppendAllText(filePath, sb.ToString());
			}

			public Dictionary<string, SmellLevel> LoadDataFromFile(string filepath)
			{
				if (!File.Exists(filepath))
				{
					return null;
				}
				var smellyBoys = new Dictionary<string, SmellLevel>();
				var lines = File.ReadAllLines(filepath);
				string name = "";
				foreach (var line in lines)
				{
					var temp = line;
					char[] charactersToRemove = {'\t', '"', '"', ':', '{', '"', '"'};
					if (temp.Length == 0)
					{
						return smellyBoys;
					}
					if (temp.Length <= 3 || temp == "\"SmellyBoys\" : {")
						continue;
					foreach (var character in charactersToRemove)
					{
						temp = temp.Replace($"{character}" ,"");
					}
					temp = temp.Replace("SmellLevel", "$");
					if (temp.IndexOf('$') == 0)
					{
						var smell = temp.Split('$')[1];
						SmellLevel level = (SmellLevel)Enum.Parse((typeof(SmellLevel)), smell);
						smellyBoys.Add(name, level);
					
					}
					else
					{
						name = temp;
					}


				}
				return smellyBoys;
			}
		}
}
