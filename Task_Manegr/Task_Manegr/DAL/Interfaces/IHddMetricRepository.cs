using MetricsManager.Client;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;

namespace MetricsManager.Repository
{
    public interface IHddMetricRepository : IRepository<HddMetricInquiry>
    {
        public DateTimeOffset FromTime();
        public void Create(List<HddMetricDto> Metrics);
    }
}