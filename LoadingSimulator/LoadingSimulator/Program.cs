using System;
using System.Text;
using System.Threading;

namespace LoadingSimulator
{
	internal class Program
	{
		

		static void Main(string[] args)
		{
			#region Declarations

			Console.OutputEncoding = Encoding.UTF8;
			const int TaskOneSteps = 40;
			const int TaskTwoSteps = 50;
			const int lowestSteps = 1, highestSteps = 25, lowestTimeOut = 1, higestTimeOut = 500;
			const char ThirdDivisionChar = '\u2593';
			const char SecondDivisionChar = '\u2592';
			const char FirstDivisionChar = '\u2591';

			#endregion

			#region Task1

			for (int i = 1; i <= TaskOneSteps; i++)
			{
				inctementBasicBar(i, ThirdDivisionChar, TaskOneSteps, 0);
			}

			#endregion

			#region Task2


			prepareAdvancedBar(TaskTwoSteps, FirstDivisionChar , 3);

			for (int i = 0; i < TaskTwoSteps; i++)
			{
				incrementAdvancedBar(i,TaskTwoSteps,SecondDivisionChar,ThirdDivisionChar);
			}

			#endregion

			#region Task3

			int taskThreeSteps = getRandomNumber(lowestSteps, highestSteps);
			int taskThreeTimeOut = getRandomNumber(lowestTimeOut, higestTimeOut);
			for (int i = 1; i <= taskThreeSteps; i++)
			{
				inctementBasicBar(i, ThirdDivisionChar, taskThreeSteps, 6);
			}


			#endregion

		}

		#region Functions

		private static void inctementBasicBar(int i, char barCharacter, int steps, int top)
		{
			Console.SetCursorPosition(i - 1, top);
			Console.Write($"{barCharacter}  {i}/{steps} steps");
			Thread.Sleep(200);
		}

		private static int getRandomNumber(int low, int high)
		{
			Random randObj = new Random();
			return randObj.Next(low, high);
		}

		private static void incrementAdvancedBar(int i, int steps, char level2Char, char level3Char)
		{
			Console.SetCursorPosition(steps, 3);
			Console.Write($"  {i + 1}/{steps} steps");
			Console.SetCursorPosition(i, 3);
			Console.Write(level2Char);
			Thread.Sleep(100);
			Console.SetCursorPosition(i, 3);
			Console.Write(level3Char);
			Thread.Sleep(100);
		}

		private static void prepareAdvancedBar(int steps, char level1Char, int top)
		{
			Console.SetCursorPosition(0, top);
			for (int i = 1; i <= steps; i++) Console.Write(level1Char);
		}

		#endregion

	}
}
