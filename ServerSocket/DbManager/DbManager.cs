using ServerSocket.Exceptions;
using ServerSocket.Models;
using ServerSocket.Shared;
using ServerSocket.Shared.Exceptions;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ServerSocket.DbManager
{
    public class DataManager : IDataManager
    {
        public CacheModel GetData(string Key)
        {
            CacheModel cache = new CacheModel();
            IDataManager objDataManager;
            objDataManager = DbAccessPatternFactory.GetInstance(DBType.MySql);
            cache = objDataManager.GetData(Key);
            return cache;
        }

        public void WriteToDB(CacheModel cache)
        {
            IDataManager objDataManager;
            objDataManager = DbAccessPatternFactory.GetInstance(DBType.MySql);
            objDataManager.WriteToDB(cache);
        }
    }
}