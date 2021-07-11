using System;

namespace MetricsManager.DAL.Models
{
    public class HddMetrics
    {
        public int Id { get; set; }

        public long Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}