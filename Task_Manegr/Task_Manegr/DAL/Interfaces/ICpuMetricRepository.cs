using MetricsManager.DAL.Models;
using MetricsManager.Repository;
using System;
using System.Collections.Generic;

namespace MetricsManager
{
    public interface ICpuMetricRepository : IRepository<CpuMetricInquiry>
    {
        public DateTimeOffset FromTime();
        public void Create(List<CpuMetricDto> Metrics);
    }
}