using System.Collections.Generic;

namespace MetricsManager.Client
{
    public class AllRamMetricsApiResponse
    {
        public List<RamMetrics> Metrics { get; set; }
    }
}