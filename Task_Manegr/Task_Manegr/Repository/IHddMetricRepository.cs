using MetricsManager.Client;
using System;
using System.Collections.Generic;

namespace MetricsManager.Repository
{
    public interface IHddMetricRepository
    {
        public DateTimeOffset FromTime();
        public void Create(List<HddMetrics> Metrics);
    }
}