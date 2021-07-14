using System.Collections.Generic;

namespace MetricsManager.Client
{
    public class AllCpuMetricsApiResponse
    {
        public List<CpuMetrics> Metrics { get; set; }
    }
}