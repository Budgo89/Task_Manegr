using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public interface IConnectionManager
    {
        string CreateOpenedConnection();
        SQLiteConnection GetOpenedConnection();
    }
}
