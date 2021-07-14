using System;

namespace MetricsManager.Client
{
    public class GetAllDonNetHeapMetrisApiRequest
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public string ClientBaseAddress { get; set; }
    }
}