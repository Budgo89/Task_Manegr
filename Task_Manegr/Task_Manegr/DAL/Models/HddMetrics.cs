using System;

namespace MetricsManager.Client
{
    public class HddMetrics
    {
        public int Id { get; set; }

        public long Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}