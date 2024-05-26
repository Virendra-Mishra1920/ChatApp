using ClientSocket.Models;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ClientSocket.RequestHandler
{
    public interface IRequestManager
    {
        public void SendDataToServer(ICSModel cache, Socket sender);

        public void RecieveDataFromServer(Socket sender);
    }
}