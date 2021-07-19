using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerClient
{
    class ConnectionManager : IConnectionManager
    {
        public const string ConnectionString = "http://localhost:5153";
        public string GetConnection()
        {
            return ConnectionString;
        }
    }
}
