using System;

namespace MetricsManager.DAL.Models
{
    public class HddMetrics
    {
        public DateTimeOffset Time { get; set; }
        public long Value { get; set; }
        public int Id { get; set; }
    }
}