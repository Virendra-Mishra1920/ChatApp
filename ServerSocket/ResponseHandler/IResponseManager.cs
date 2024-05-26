using ServerSocket.Models;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ServerSocket.ResponseHandler
{
    public interface IResponseManager
    {
        void RecieveDataFromClient(Socket clientSocket);

        void SendDataToClient(Socket clientSocket, CacheModel cache);
    }
}