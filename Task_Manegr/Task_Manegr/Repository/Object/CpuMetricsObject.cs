using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Repository
{
    public class CpuMetricsObject
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}
