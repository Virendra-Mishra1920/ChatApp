using Newtonsoft.Json;
using ServerSocket.CacheManager;
using ServerSocket.Models;
using ServerSocket.Shared;
using ServerSocket.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ServerSocket.ResponseHandler
{
    public class ResponseManager : IResponseManager
    {
        public void RecieveDataFromClient(Socket clientSocket)
        {
            try
            {
                byte[] bytes = new Byte[1024];
                string data = null;
                int numByte = clientSocket.Receive(bytes);

                data += Encoding.ASCII.GetString(bytes,
                                           0, numByte);
                CacheModel cache = new CacheModel();
                cache = JsonConvert.DeserializeObject<CacheModel>(data);

                if (cache == null)
                {
                    throw new MessageParsingException(AppConstant.MessageParsingError);
                }
                CacheDataManager cacheDataManager = new CacheDataManager();
                cache = cacheDataManager.ManageCache(cache, clientSocket);
                Console.WriteLine("Text received -> {0} ", cache.Key + cache.Value);
                SendDataToClient(clientSocket, cache);
            }
            catch (MessageParsingException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SendDataToClient(Socket clientSocket, CacheModel cache)
        {
            byte[] message = Encoding.ASCII.GetBytes("message from Server" + cache.Key + cache.Value);

            clientSocket.Send(message);
        }
    }
}