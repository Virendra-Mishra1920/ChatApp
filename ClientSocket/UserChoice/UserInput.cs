using ClientSocket.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientSocket.GetUserChoice
{
    internal class UserInput
    {
        public static ICSModel GetUserInput()
        {
            ICSModel iCSModel = new ICSModel();
            //CacheModel cache = new CacheModel();
            while (true)
            {
                Console.WriteLine("Enter Your Choice");
                Console.WriteLine("ICS GET");
                Console.WriteLine("ICS SET");
                Console.WriteLine("3. Exit");
                string choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "ICS GET":
                        Console.WriteLine("Enter Key");
                        iCSModel.Key = Console.ReadLine();
                        iCSModel.Value = "";
                        iCSModel.TypeOfOperation = "GET";
                        break;

                    case "ICS SET":
                        Console.WriteLine("Enter Key");
                        iCSModel.Key = Console.ReadLine();

                        Console.WriteLine("Enter value");
                        iCSModel.Value = Console.ReadLine();
                        iCSModel.TypeOfOperation = "SET";
                        break;

                    default:
                        break;
                }
                return iCSModel;
            }
        }
    }
}