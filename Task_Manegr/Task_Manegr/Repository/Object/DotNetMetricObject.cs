using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Repository.Object
{
    public class DotNetMetricObject
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }
}
