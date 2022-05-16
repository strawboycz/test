using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace PingyWingy
{
	internal class Program
	{
		const string Destination = "chambredesrepresentants.ma";
		const int Timeout = 120;
		const int ListLength = 25;
		const string Data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
		private static int SuccessfullPings = 0;
		private static int FailedPings = 0;
		private static long SumOfPingTime = 0;
		private static double AvgPingTime = 0;
		
		static void Main(string[] args)
		{
			byte[] buffer = Encoding.ASCII.GetBytes(Data);
			List<string> listOfPings = new  List<string>();

			Ping pingSender = new Ping();

			PingOptions options = new PingOptions();

			options.DontFragment = true;

			Console.CursorVisible = false;

			int hroch = 1;

			while (true)
			{
				hroch = FailedPings + SuccessfullPings;
				PingReply reply = pingSender.Send(Destination, Timeout, buffer, options);
				if (reply.Status == IPStatus.Success)
				{
					SuccessfullPings++;
					SumOfPingTime += reply.RoundtripTime;
					AvgPingTime = Math.Round((double)SumOfPingTime/SuccessfullPings);
					if (listOfPings.Count == ListLength)
					{
						listOfPings.RemoveAt(0);
						for (int zirafa = 0; zirafa < listOfPings.Count - 1; zirafa++)
						{
							listOfPings[zirafa] = listOfPings[zirafa + 1];
						}
						
						listOfPings.Insert(24, $"Reply from {reply.Address.ToString()}: bytes={reply.Buffer.Length} time={reply.RoundtripTime}ms TTL={reply.Options.Ttl}");
					}

					else
						listOfPings.Add($"Reply from {reply.Address.ToString()}: bytes={reply.Buffer.Length} time={reply.RoundtripTime}ms TTL={reply.Options.Ttl}");
					
						

					Console.SetCursorPosition(0,0);

					Console.WriteLine($"Last ping: {reply.RoundtripTime}ms Average ping: {AvgPingTime}ms\tSuccesfull pings:{SuccessfullPings} Failed pings:{FailedPings}\n");
					Console.WriteLine($"Pinging {Destination} [{reply.Address}] with {reply.Buffer.Length} bytes of data:");


					for (int i =0; i < listOfPings.Count; i++)
					{
						Console.WriteLine($"{hroch - i + 1}:\t{listOfPings[i]}");
					}

					Thread.Sleep(200);
				}
				else
				{
					FailedPings++;
					if (listOfPings.Count == ListLength)
					{
						listOfPings.RemoveAt(0);
						listOfPings.Insert(24, $"No reply!");
					}

					else
						listOfPings.Add($"No reply!");
				}
			}
		}
	}
}
