using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace Zipper3000
{
	internal class Program
	{
		// TEXT FILE: https://www.gutenberg.org/cache/epub/67766/pg67766.txt
		// MP4 FILE: https://jsoncompare.org/LearningContainer/SampleFiles/Video/MP4/sample-mp4-file.mp4

		/*
		 * Compress each of the file in each of the CompressionLevel 10 times and measure how long it takes to compress it
		 * Run all tests in a row, collect results and then print all results in a table, use escape characters to make it nice
		 * Use functions, try not to have one big pile of code, follow the principles you have already learn 
		 */

		static void Main(string[] args)
		{
			long sum;
			double avg;
			string mode = "";
			const string zipFilePath = @"c:\users\Mirek\documents\testy\hroch.zip";
			const string textFilePath = @"c:\users\Mirek\documents\testy\neco.txt";
			const string videoFilePath = @"c:\users\Mirek\documents\testy\sample-mp4-file.mp4";
			Dictionary<string, double> timeTable = new Dictionary<string, double>();
			File.Delete(zipFilePath);
			

			using (FileStream zipToOpen = File.Create(zipFilePath))
			using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
			{
				{
					for (int j = 0; j < 3; j++)
					{
						sum = 0;
						var stopwatch = Stopwatch.StartNew();
						for (int i = 1; i <= 10; i++)
						{
							mode = getMode(j);
							createFiles(archive, j, textFilePath, videoFilePath);
							stopwatch.Stop();
							var timeToZip = stopwatch.ElapsedMilliseconds;
							sum += timeToZip;
							Console.WriteLine($"Completed {j * 10 + i}/30");
						}
						avg = sum / 10;
						timeTable.Add(mode, avg);
					}
				}
			}
			printTable(timeTable);
		}

		private static void printTable(Dictionary<string, double> timeTable)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Fully competed!");
			Console.ResetColor();
			foreach (KeyValuePair<string, double> time in timeTable)
			{
				Console.WriteLine($"{time.Key}:\t{time.Value}ms");
			}
		}


		private static string getMode(int j)
		{
			switch (j)
			{
				case 0:
					return "Optimal";
					break;
				case 1:
					return "Fastest";
					break;
				case 2:
					return "no compression";
			}

			return "";
		}

		private static void createFiles(ZipArchive archive, int j,string path1, string path2)
		{
			archive.CreateEntryFromFile(path1, "text.txt", (CompressionLevel) j);
			archive.CreateEntryFromFile(path2, "blbost.mp4", (CompressionLevel) j);
		}
	}
}