using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Repository
{
    public class NetworkMetricObject
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}
