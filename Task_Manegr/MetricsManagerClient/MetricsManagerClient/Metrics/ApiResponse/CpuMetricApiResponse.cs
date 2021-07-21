using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerClient.Agents.Repository
{
    public class CpuMetricApiResponse
    {
        public List<CpuMetric> Metrics { get; set; }
    }
}