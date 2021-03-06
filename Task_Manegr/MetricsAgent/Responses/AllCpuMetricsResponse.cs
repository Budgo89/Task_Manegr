using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public class AllCpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
    public class CpuMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public long Value { get; set; }
        public int Id { get; set; }
    }
}
