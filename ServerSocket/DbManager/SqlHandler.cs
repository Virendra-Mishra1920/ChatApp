using ServerSocket.Models;
using ServerSocket.Shared;
using ServerSocket.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace ServerSocket.DbManager
{
    public class SqlHandler : IDataManager
    {
        public static SqlConnection sqlConnection;

        public CacheModel GetData(string Key)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            CacheModel cache = new CacheModel();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                {
                    myConnection.Open();
                    string getDataFromSql = "Select * from CacheData where [Key]=@Key";

                    SqlCommand command = new SqlCommand(getDataFromSql, sqlConnection);
                    command.Connection = myConnection;

                    command.Parameters.AddWithValue("@Key", Key);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cache.Key = reader["Key"].ToString();

                            cache.Value = reader["Value"].ToString();
                        }
                        if (cache.Key == null)
                        {
                            throw new DbKeyNotAvailableException(AppConstant.KeyNotAvailableInDb);
                        }

                        myConnection.Close();
                    };
                };
            }
            catch (DbKeyNotAvailableException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return cache;
        }

        public void WriteToDB(CacheModel cache)
        {
            MongoDbHandler mongoDbManager = new MongoDbHandler();
            string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            string key = cache.Key;
            string value = cache.Value;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var sql = "INSERT INTO CacheData VALUES(@key, @value)";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@key", key);
                        cmd.Parameters.AddWithValue("@value", value);

                        cmd.ExecuteNonQuery();
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}