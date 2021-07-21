using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Repository.Object
{
    public class HddMetricsObject
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
}
