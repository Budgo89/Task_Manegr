using Dapper;
using MetricsAgent.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.Controllers
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric>
    {
        void Create(DotNetMetric dotNetMetric);
    }
}