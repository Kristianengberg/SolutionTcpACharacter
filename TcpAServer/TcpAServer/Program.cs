﻿using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TcpAServer
    
{
	public class TcpAServer
	{
		public static void Main()
		{
            TcpServer server = new TcpServer(1234);


        }

        public class TcpServer
        {
            private TcpListener server;
            private bool isRunning;
            private Resource resource = new Resource();
            private int numberOfClients;


            public TcpServer(int port)
            {
                

                Console.WriteLine("Starting echo server...");
                
                server = new TcpListener(IPAddress.Loopback, port);
                server.Start();

                isRunning = true;

                LoopClients();

            }
            public void LoopClients()
            {
                while (true)
                {
                    TcpClient newClient = server.AcceptTcpClient();

                    Thread thread = new Thread(new ParameterizedThreadStart(HandleClient));
                    thread.Start(newClient);
                    
                }
            }
            public void HandleClient(object obj)
            {
                TcpClient client = (TcpClient)obj;

                StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.ASCII) { AutoFlush = true };
                StreamReader reader = new StreamReader(client.GetStream(), Encoding.ASCII);
                StreamReader newReader = new StreamReader(client.GetStream(), Encoding.ASCII);

                bool ClientConnected = true;
                string data = null;
                string intData = null;

                numberOfClients++;
                Console.WriteLine("Current number of clients connected: {0}", numberOfClients);
                

                while (ClientConnected)
                {
                    intData = newReader.ReadLine();
                    int ii = Int32.Parse(intData);

                    data = reader.ReadLine();

                    Console.WriteLine("Message recieved from client: {0}", data);
                    Console.WriteLine("THIS IS THE INTEGER {0}", ii);
                    writer.WriteLine("Number of A's so far: {0}", resource.AddToCount(data));
                    Console.WriteLine("Number of A's so far: {0}", resource.currentCount);
                }
            }

        }
        
        public class Resource
        {
            private int count;

            public int AddToCount(string userInput)
            {
                foreach (char c in userInput)
                    if (c == 'a') count++;
                return count;                
            }
            public int currentCount
            {
               get { return count; }
            }
         
        }
        
	}
}