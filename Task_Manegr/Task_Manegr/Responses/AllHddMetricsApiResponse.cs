using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class AllHddMetricsApiResponse
    {        
        public List<HddMetrics> Metrics { get; set; }

    }
}