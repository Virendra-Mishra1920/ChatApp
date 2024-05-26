using ClientSocket.MessageValidator;
using ClientSocket.RequestHandler;
using System;

namespace ClientSocket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Client client = new Client();
            client.ConnectToServer();
        }
    }
}