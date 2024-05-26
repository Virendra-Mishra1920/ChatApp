using Newtonsoft.Json;
using ServerSocket.CacheManager;
using ServerSocket.DbManager;
using ServerSocket.Models;
using ServerSocket.MultiThreadManagement;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerSocket.ServerConnection
{
    public class Server
    {
        public static void StartServer()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 6379);
            Socket listener = new Socket(ipAddr.AddressFamily,
                         SocketType.Stream, ProtocolType.Tcp);
            HandleMultipleClient handleMultipleClient = new HandleMultipleClient();

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);
                handleMultipleClient.HandleRequestFromClient(listener);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}