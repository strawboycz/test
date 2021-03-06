using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using Velci_smradi.enums;
using Velci_smradi.providers;

namespace Velci_smradi
{
    internal class Program
    {

	    static void Main(string[] args)
        {
            Dictionary<string, SmellLevel> smellyBoys = new Dictionary<string, SmellLevel>();
            const string filePath = "C://tmp/smradosi.txt";
            const string plainCsvFilePath = "C://tmp/smradosi_plain_csv.txt";
            const string CSVFilePath = "C://tmp/smradosi_csv.csv";
            const string plainJSONFilePath = "C://tmp/smradosi_plain_JSON.txt";
						const char separator = ';';

            var provider = new plainJSONProvider();

						//smellyBoys = provider.LoadDataFromFile(filePath,separator);
						//smellyBoys = provider.LoadDataFromFile(plainCsvFilePath,separator);
						//smellyBoys = provider.LoadDataFromFile(CSVFilePath);
						smellyBoys = provider.LoadDataFromFile(plainJSONFilePath);
            printTable(smellyBoys);

						while (true)
            {
	            var name = getName();

                if (maybeExit(name)) 
                    return;
                if (mabeSave(name, smellyBoys, separator, filePath, plainCsvFilePath, CSVFilePath, plainJSONFilePath)) 
                    continue;
                if (checkForDuplicateName(smellyBoys, name)) continue;

                var level = calculateSmell(name);
                addPlayer(name, level, smellyBoys);
								
                printTable(smellyBoys);
            }
        }

        #region File manipulation
						private static bool mabeSave(string name, Dictionary<string, SmellLevel> smellyBoys, char separator, string filePath, string plainCsvFilePath, string CSVFilePath, string plainJSONFilePath)
						{
							if (name.ToLower() == Texts.SaveKeyword)
							{
								plainProvider.SaveDataToFile(smellyBoys, separator, filePath);
								plainCsvProvider.SaveDataToFile(smellyBoys,separator, plainCsvFilePath);
								CSVprovider.SaveDataToFile(smellyBoys,CSVFilePath);
								plainJSONProvider.SaveDataToFile(smellyBoys,plainJSONFilePath);
								Console.WriteLine("Saved successfully");
								return true;
							}

							return false;
						}

						#endregion

				#region Helpers

						private static string getName()
						{
							Console.Write("Zadejte jm??no smra??ocha: ");
							return Console.ReadLine();

						}

						private static bool checkForDuplicateName(Dictionary<string, SmellLevel> smellyBoys, string name)
						{
							if (smellyBoys.ContainsKey(name))
							{
								Console.WriteLine("\nTento smra??och ji?? existuje!\n");
								return true;
							}

							return false;
						}

				private static SmellLevel calculateSmell(string name)
						{
							float avg;
							int sum = 0;
							for (int i = 0; i < name.Length; i++)
							{
								if (name[i] == ' ') continue;
								sum += (int)name[i];
							}

							avg = (float)sum / name.Length;
							if (Math.Round(avg) % 7 == 0 || name.ToUpper() == "HONZA RADA")
							{
								return SmellLevel.HobosLeg;
							}
							if (Math.Round(avg) % 5 == 0)
							{
								return SmellLevel.HorseAss;
							}
							if (Math.Round(avg) % 3 == 0)
							{
								return SmellLevel.OnionRinger;
							}
							return SmellLevel.None;
						}


						private static void addPlayer(string name, SmellLevel level, Dictionary<string, SmellLevel> smellyBoys)
						{
							switch (level)
							{
								case SmellLevel.HobosLeg: Console.WriteLine($"\n{name} {Texts.HobosLeg}."); break;
								case SmellLevel.HorseAss: Console.WriteLine($"\n{name} {Texts.HobosLeg}."); break;
								case SmellLevel.OnionRinger: Console.WriteLine($"\n{name} {Texts.HobosLeg}."); break;
								case SmellLevel.None: Console.WriteLine($"\n{name} {Texts.HobosLeg}."); break;
							}
							Console.WriteLine();
							smellyBoys.Add(name, level);
						}

						private static bool maybeExit(string name)
						{
							if (name.ToLower() == Texts.ExitKeyword) return true;
							return false;
						}

						private static void printTable(Dictionary<string, SmellLevel> smellyBoys)
						{
							Console.WriteLine("Tabulka smra??och??:\n------------------");
							foreach (KeyValuePair<string, SmellLevel> smellyBoy in smellyBoys)
							{
								Console.Write($"{smellyBoy.Key}\t");
								switch (smellyBoy.Value)
								{
									case SmellLevel.HobosLeg:
										Console.ForegroundColor = ConsoleColor.Red;
										Console.WriteLine(Texts.HobosLeg);
										break;
									case SmellLevel.HorseAss:
										Console.ForegroundColor = ConsoleColor.DarkYellow;
										Console.WriteLine(Texts.HorseAss);
										break;
									case SmellLevel.OnionRinger:
										Console.ForegroundColor = ConsoleColor.Yellow;
										Console.WriteLine(Texts.OnionRinger);
										break;
									case SmellLevel.None:
										Console.ForegroundColor = ConsoleColor.Green;
										Console.WriteLine(Texts.None);
										break;
								}
								Console.ForegroundColor = ConsoleColor.Gray;
							}
							Console.WriteLine();
							Console.WriteLine();
						}

				#endregion

		}
}
