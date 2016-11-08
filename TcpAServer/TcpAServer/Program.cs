using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace TcpAServer
{
	public class TcpAServer
	{
		public static void Main()
		{
			Console.WriteLine("Starting echo server...");

			int port = 1234;
			TcpListener listener = new TcpListener(IPAddress.Loopback, port);
			listener.Start();

			TcpClient client = listener.AcceptTcpClient();
			NetworkStream stream = client.GetStream();
			StreamWriter writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };
			StreamReader reader = new StreamReader(stream, Encoding.ASCII);

			while (true)
			{
				string inputLine = "";
				while (inputLine != null)
				{
					inputLine = reader.ReadLine();
					int count = 0;
					foreach (char c in inputLine)
						if (c == 'a') count++;
					writer.WriteLine("Number of a's found: {0}", count);
					Console.WriteLine("Number of a's found: {0}", count);
				}
				Console.WriteLine("Server saw disconnect from client.");
			}
		}
	}
}