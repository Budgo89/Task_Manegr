using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Models
{
    public class HddMetricInquiry
    {
        public int Id { get; set; }

        public long Value { get; set; }

        public long Time { get; set; }

        public int AgentId { get; set; }
    }
}
