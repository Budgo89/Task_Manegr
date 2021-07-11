using MetricsManager.Client;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;

namespace MetricsManager.Repository
{
    public interface IHddMetricRepository : IRepository<HddMetrics>
    {
        public DateTimeOffset FromTime();
        public void Create(List<HddMetrics> Metrics);
    }
}