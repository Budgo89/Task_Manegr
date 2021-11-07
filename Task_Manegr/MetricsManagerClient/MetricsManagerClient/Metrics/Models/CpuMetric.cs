using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerClient.Agents.Repository
{
    public class CpuMetric
    {
        public int Id { get; set; }

        public long Value { get; set; }

        public DateTimeOffset Time { get; set; }

        public int AgentId { get; set; }
    }
}
