using MetricsManager.DAL.Models;
using MetricsManager.Repository;
using System;
using System.Collections.Generic;

namespace MetricsManager.Jobs
{
    public interface INetworkMetricRepository : IRepository<NetworkMetricInquiry>
    {
        public DateTimeOffset FromTime();
        public void Create(List<NetworkMetricDto> Metrics);
    }
}