using System;

namespace MetricsManager.Client
{
    public class GetAllNetworkMetricsApiRespodse
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public string ClientBaseAddress { get; set; }
    }
}