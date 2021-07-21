using MetricsManagerClient.Metrics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerClient.Metrics.ApiResponse
{
    public class RamMetricsApiResponse
    {
        public List<RamMetrics> Metrics { get; set; }
    }
}
