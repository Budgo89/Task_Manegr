using System;

namespace MetricsManager.Client
{
    public class CpuMetrics
    {
        public DateTimeOffset Time { get; set; }
        public long Value { get; set; }
        public int Id { get; set; }
    }
}