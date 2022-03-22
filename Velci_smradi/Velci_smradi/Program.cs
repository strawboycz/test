using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;

namespace Velci_smradi
{
    internal class Program
    {
        enum SmellLevel { None, OnionRinger, HorseAss, HobosLeg }



        static void Main(string[] args)
        {
            Dictionary<string, SmellLevel> smellyBoys = new Dictionary<string, SmellLevel>();
            const char separator = ';';
            loadTable(smellyBoys,separator);
            while (true)
            {
                
                var name = getName();

                if (maybeExit(name)) 
                    return;
                if (mabeSave(name, smellyBoys, separator)) 
                    continue;
                if (checkForDuplicateName(smellyBoys, name)) continue;

                var level = calculateSmell(name);
                addPlayer(name, level, smellyBoys);

                printTable(smellyBoys);

                

            }
        }

        private static bool checkForDuplicateName(Dictionary<string, SmellLevel> smellyBoys, string name)
        {
            if (smellyBoys.ContainsKey(name))
            {
                Console.WriteLine("\nTento smraďoch již existuje!\n");
                return true;
            }

            return false;
        }


        private static void loadTable(Dictionary<string, SmellLevel> smellyBoys, char separator)
        {
            if (File.Exists("C://tmp/smradosi.txt"))
            {
                var lines = File.ReadAllLines("C://tmp/smradosi.txt");
                //if (lines.Length == 0) return;
                foreach (var line in lines)
                {
                    if (line.Length == 0) return;
                    int split = line.IndexOf(separator);
                    var name = line.Substring(0, split);
                    var level = line.Substring(split + 1);
                    switch (level)
                    {
                        case "HobosLeg":
                            smellyBoys.Add(name, SmellLevel.HobosLeg);
                            break;
                        case "HorseAss":
                            smellyBoys.Add(name, SmellLevel.HorseAss);
                            break;
                        case "OnionRinger":
                            smellyBoys.Add(name, SmellLevel.OnionRinger);
                            break;
                        case "None":
                            smellyBoys.Add(name, SmellLevel.None);
                            break;
                    }
                }

                printTable(smellyBoys);
                
            }
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

        private static bool mabeSave(string name, Dictionary<string, SmellLevel> smellyBoys, char separator)
        {
            if (name == Texts.SaveKeyword)
            {
                File.Delete("C://tmp/smradosi.txt");
                foreach (KeyValuePair<string, SmellLevel> smellyBoy in smellyBoys)
                {
                    File.AppendAllText("C://tmp/smradosi.txt", $"{smellyBoy.Key}{separator}{smellyBoy.Value}\n");
                }

                Console.WriteLine("Saved successfully");
                return true;
            }

            return false;
        }

        private static bool maybeExit(string name)
        {
            if (name == Texts.ExitKeyword) return true;
            return false;
        }

        private static void printTable(Dictionary<string, SmellLevel> smellyBoys)
        {
            Console.WriteLine("Tabulka smraďochů:\n------------------");
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

        private static SmellLevel calculateSmell(string name)
        {
            float avg;
            int sum = 0;
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] == ' ') continue;
                sum += (int) name[i];
            }

            avg = (float) sum / name.Length;
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

        private static string getName()
        {
            string name;
            Console.Write("Zadejte jméno smraďocha: ");
            name = Console.ReadLine();
            return name;
        }
    }
}
