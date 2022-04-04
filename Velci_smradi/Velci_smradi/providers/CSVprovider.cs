using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Velci_smradi.enums;

namespace Velci_smradi.providers
{

		public class Record
		{
		public string Name { get; set; }
		public SmellLevel level { get; set; }
		}
		internal class CSVprovider
		{
			public static void SaveDataToFile(Dictionary<string, SmellLevel> data, string filename)
			{

				using (var writer = new StreamWriter(filename))
				using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
				{
					var list = new List<Record>();
					foreach (var item in data)
					{
						list.Add(new Record() {Name = item.Key, level = item.Value});
					}

					csv.WriteRecords(list);
				}
			}

			public Dictionary<string, SmellLevel> LoadDataFromFile(string filename)
			{
				if (!File.Exists(filename))
					return null;

				using (var reader = new StreamReader(filename))
				using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
				{
					var records = csv.GetRecords<Record>().ToList();

					return records.ToDictionary(x => x.Name, x => x.level);
				}
			}
		}
}
