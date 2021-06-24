using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    public class AllRamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
    public class RamMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public long Value { get; set; }
        public int Id { get; set; }
    }
}