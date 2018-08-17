using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace Databases
{
    [TestClass]
    public class Test_Databases
    {
        [TestMethod]
        public void Test_DB_Sync()
        {
            string connectionString;
            connectionString = "Data Source=ALTAIR\\ALTAIR_2014;Initial Catalog=master;Integrated Security=True";

            string sqlSelect = "SELECT @@VERSION";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(sqlSelect, sqlConnection))
                {
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = reader[0].ToString();
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void Test_DB_ASync()
        {
            string connectionString;
            connectionString = "Data Source=ALTAIR\\ALTAIR_2014;Initial Catalog=master;Integrated Security=True";

            string sqlSelect = "SELECT @@VERSION";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();


                var sqlCommand = new SqlCommand(sqlSelect, sqlConnection);

                var callback = new AsyncCallback(DataAvailable);
                var ar = sqlCommand.BeginExecuteReader(callback, sqlCommand);
               

                ar.AsyncWaitHandle.WaitOne();
            }

           
        }
        private static void DataAvailable(IAsyncResult ar)
        {
            var sqlCommand = ar.AsyncState as SqlCommand;

            using (var reader = sqlCommand.EndExecuteReader(ar))
            {
                while (reader.Read())
                {
                    var data = reader[0].ToString();
                }
            }
         }


    }
}
