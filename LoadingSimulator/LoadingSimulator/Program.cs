using System;
using System.Text;
using System.Threading;

namespace LoadingSimulator
{
	internal class Program
	{
		const char ThirdDivisionChar = '\u2593';
		const char SecondDivisionChar = '\u2592';
		const char FirstDivisionChar = '\u2591';
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;

			#region Task1

			const int taskOneSteps = 40;
			string taskOneProgressBar="";
			for (int i = 1; i <= taskOneSteps; i++)
			{
				Console.Clear();
				taskOneProgressBar += ThirdDivisionChar;
				Console.Write($"{taskOneProgressBar}  {i}/{taskOneSteps} steps");
				Thread.Sleep(200);
			}
			#endregion

			#region Task2

			const int taskTwoSteps = 50;
			char[] taskTwoProgressBar = new char[taskTwoSteps+1];
			fillBar(taskTwoSteps, taskTwoProgressBar);
			int x = 0, y = 0;
			while (x < taskTwoSteps)
			{
				Console.Clear();
				fillCharacterOfBar(y, taskTwoSteps, taskTwoProgressBar, x);
				x = maybeIncrement(y, taskTwoSteps, taskTwoProgressBar, x);
				printBar(taskTwoSteps, taskTwoProgressBar);
				Console.WriteLine($"  {x}/{taskTwoSteps} steps");
				y++;
				Thread.Sleep(100);
			}

			#endregion

			#region Task3
			Random randObj = new Random();
			int taskThreeSteps = randObj.Next(1,25);
			string taskThreeProgressBar = "";
			for (int i = 1; i <= taskThreeSteps; i++)
			{
				Console.Clear();
				taskThreeProgressBar += ThirdDivisionChar;
				Console.Write($"{taskThreeProgressBar}  {i}/{taskThreeSteps} steps");
				Thread.Sleep(randObj.Next(0,500));
			}


			#endregion

		}

		private static int maybeIncrement(int y, int taskTwoSteps, char[] taskTwoProgressBar, int x)
		{
			if (!((y == 0 || y % 2 != 0) && y != taskTwoSteps))
			{
				x++;
			}
			return x;
		}

		private static void fillCharacterOfBar(int y, int taskTwoSteps, char[] taskTwoProgressBar, int x)
		{
			if ((y == 0 || y % 2 != 0) && y != taskTwoSteps)
				taskTwoProgressBar[x] = SecondDivisionChar;
			else
			{
				taskTwoProgressBar[x] = ThirdDivisionChar;
			}
		}

		private static void printBar(int taskTwoSteps, char[] taskTwoProgressBar)
		{
			for (int j = 0; j < taskTwoSteps; j++)
			{
				Console.Write(taskTwoProgressBar[j]);
			}
		}

		private static void fillBar(int taskTwoSteps, char[] taskTwoProgressBar)
		{
			for (int i = 0; i < taskTwoSteps; i++) taskTwoProgressBar[i] = FirstDivisionChar;
		}
	}
}
