using Dapper;
using Intercom.Core;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {
        void Create(CpuMetric cpuMetric);
    }
}
