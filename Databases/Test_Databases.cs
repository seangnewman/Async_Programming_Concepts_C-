using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Threading;

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
        public async Task Test_DB_AsyncAwait()
        {
            string connectionString;
            connectionString = "Data Source=ALTAIR\\ALTAIR_2014;Initial Catalog=master;Integrated Security=True";

            string sqlSelect = "SELECT @@VERSION";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                using (var sqlCommand = new SqlCommand(sqlSelect, sqlConnection))
                {
                    using (var reader = await sqlCommand.ExecuteReaderAsync())
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

        [TestMethod]
        public void Test_DB_ASyncTask()
        {
            ManualResetEvent mre = new ManualResetEvent(false);
            string connectionString;
            connectionString = "Data Source=ALTAIR\\ALTAIR_2014;Initial Catalog=master;Integrated Security=True";

            string sqlSelect = "SELECT @@VERSION";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                Task taskSqlConnection = sqlConnection.OpenAsync();

                taskSqlConnection.ContinueWith((Task tx, object state) => {
                    var sqlConn = state as SqlConnection;
                    Assert.IsTrue(sqlConn.State == System.Data.ConnectionState.Open);
                    var sqlCommand = new SqlCommand(sqlSelect, sqlConn);
                    Task<SqlDataReader> taskDataReader = sqlCommand.ExecuteReaderAsync();
                    Task taskProcessData = taskDataReader.ContinueWith((Task<SqlDataReader> txx) =>
                    {
                        using (var reader = txx.Result)
                        {
                            while (reader.Read())
                            {
                                var data = reader[0].ToString();
                            }
                        }
                    }, TaskContinuationOptions.OnlyOnRanToCompletion);

                }, sqlConnection, TaskContinuationOptions.OnlyOnRanToCompletion);
                mre.WaitOne();
            }
            
            
        }
       

    }
}
