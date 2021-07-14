using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;

namespace MetricsManager.Repository
{
    public interface IRamMetricRepository : IRepository<RamMetricInquiry>
    {
        public DateTimeOffset FromTime();
        public void Create(List<RamMetricDto> Metrics);
    }
}