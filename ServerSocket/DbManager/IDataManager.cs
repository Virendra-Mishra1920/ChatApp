using ServerSocket.DTO;
using ServerSocket.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSocket.DbManager
{
    public interface IDataManager
    {
        public CacheModel GetData(string key);

        public void WriteToDB(CacheModel cache);
    }
}