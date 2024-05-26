using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSocket.DbManager
{
    public class DbAccessPatternFactory
    {
        public static IDataManager GetInstance(DBType typeOfDb)
        {
            switch (typeOfDb)
            {
                case DBType.MySql:
                    return new SqlHandler();

                case DBType.MongoDb:
                    return new MongoDbHandler();

                default:
                    return null;
            }
        }
    }
}