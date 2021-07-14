using System;

namespace MetricsManager.Client
{
    public class DonNetMetrics
    {
        public DateTimeOffset Time { get; set; }
        public long Value { get; set; }
        public int Id { get; set; }
    }
}