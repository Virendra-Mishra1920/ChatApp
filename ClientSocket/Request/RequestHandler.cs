using ClientSocket.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ClientSocket.RequestHandler
{
    public class RequestManager : IRequestManager
    {
        public void RecieveDataFromServer(Socket sender)
        {
            try
            {
                byte[] messageReceived = new byte[1024];
                int byteRecv = sender.Receive(messageReceived);
                Console.WriteLine("Message from Server -> {0}",
                      Encoding.ASCII.GetString(messageReceived,
                                                 0, byteRecv));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SendDataToServer(ICSModel cache, Socket sender)
        {
            try
            {
                string message = JsonConvert.SerializeObject(cache);
                byte[] messageSent = Encoding.ASCII.GetBytes(message);
                int byteSent = sender.Send(messageSent);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
    }
}