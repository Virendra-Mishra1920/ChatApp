using ServerSocket.ResponseHandler;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerSocket.MultiThreadManagement
{
    public class HandleMultipleClient
    {
        private readonly IResponseManager _responseManager;

        public HandleMultipleClient()
        {
            _responseManager = new ResponseManager();
        }

        public void HandleRequestFromClient(Socket listener)
        {
            Console.WriteLine("Server Started Waiting Connection...");
            HandleMultipleClient handleMultipleClient = new HandleMultipleClient();
            while (true)
            {
                Socket clientSocket = listener.Accept();
                WaitCallback wait = new WaitCallback(Recieve);
                bool status = ThreadPool.QueueUserWorkItem(wait, clientSocket);
                if (!status)
                {
                    break;
                }
            }
        }

        public static void Recieve(object clientSocket)
        {
            HandleMultipleClient handleMultipleClient = new HandleMultipleClient();
            using (Socket s = (Socket)clientSocket)
            {
                handleMultipleClient.RecieveData(s);
            }
        }

        public void RecieveData(Socket clientSocket)
        {
            _responseManager.RecieveDataFromClient(clientSocket);
        }
    }
}