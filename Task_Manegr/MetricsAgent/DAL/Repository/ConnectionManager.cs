using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Repository
{
    public class ConnectionManager : IConnectionManager
    {
        public const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        public string CreateOpenedConnection()
        {
            return ConnectionString;
        }
        public SQLiteConnection GetOpenedConnection()
        {
            var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            return connection;
        }
        public string GetConnection()
        {
            return ConnectionString;
        }
    }
}
