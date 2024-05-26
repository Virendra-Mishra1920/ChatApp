using ServerSocket.DbManager;
using ServerSocket.ServerConnection;
using System;
using System.Data.SqlClient;

namespace ServerSocket
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Server.StartServer();
        }
    }
}