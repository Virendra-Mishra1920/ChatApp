using ClientSocket.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using ClientSocket.RequestHandler;
using ClientSocket.MessageValidator;
using ClientSocket.GetUserChoice;
using ServerSocket.Shared.Exceptions;
using ServerSocket.Shared;

namespace ClientSocket
{
    public class Client
    {
        private readonly IRequestManager _requestManager;
        private readonly IValidator _validator;

        public Client()
        {
            _requestManager = new RequestManager();
            _validator = new Validator();
        }

        public void ConnectToServer()
        {
            try
            {
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 6379);

                try
                {
                    while (true)
                    {
                        Client client = new Client();
                        Socket sender = new Socket(ipAddr.AddressFamily,
                           SocketType.Stream, ProtocolType.Tcp);
                        sender.Connect(localEndPoint);
                        Console.WriteLine("Socket connected to -> {0} ",
                                      sender.RemoteEndPoint.ToString());
                        client.RequestHandle(sender);
                    }
                }
                // Manage of Socket's Exceptions
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (ConnectionNotFoundException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void RequestHandle(Socket sender)
        {
            //CacheModel cache = new CacheModel();
            ICSModel cache = new ICSModel();
            cache = UserInput.GetUserInput();
            try
            {
                if (!_validator.ValidateKey(cache.Key))
                {
                    throw new KeyValidationException(AppConstant.KeyValidationError);
                }
                if (!_validator.ValidateValue(cache.Value))
                {
                    throw new ValueValidationException(AppConstant.ValueValidationError);
                }
            }
            catch (KeyValidationException ex)
            {
                Console.WriteLine("Warning", ex.Message);
                return;
            }
            catch (ValueValidationException ex)
            {
                Console.WriteLine("Warning", ex.Message);
                return;
            }

            _requestManager.SendDataToServer(cache, sender);
            _requestManager.RecieveDataFromServer(sender);
        }
    }
}