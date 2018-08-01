using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest
{
    class Program
    {
        private static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
            LoopConnect();
            SendLoop();
            Console.ReadLine();
        }

        private static void SendLoop()
        {
            while (true)
            {
                Console.Write("Message: ");
                string text = Console.ReadLine();
                byte[] buffer = Encoding.ASCII.GetBytes(text);
                _clientSocket.Send(buffer);

                byte[] recBuf = new byte[1024];
                int rec = _clientSocket.Receive(recBuf);
                byte[] data = new byte[rec];
                Array.Copy(recBuf, data, rec);
                Console.WriteLine("Message: " + Encoding.ASCII.GetString(data));
            }
        }

        private static void LoopConnect()
        {
            while (!_clientSocket.Connected)
            {
                try
                {
                    _clientSocket.Connect(IPAddress.Loopback, 100);
                }
                catch(SocketException)
                {
                    Console.Clear();
                    Console.WriteLine("Connecting...");
                }
            }

            Console.Clear();
            Console.WriteLine("Connected.");
        }
    }
}
