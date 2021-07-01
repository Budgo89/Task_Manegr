using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.Controllers
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>
    {
    }
}