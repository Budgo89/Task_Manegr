using System;

namespace MetricsManager.Client
{
    public class GetAllHddMetricsApiRequest
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public string ClientBaseAddress { get; set; }

        public GetAllHddMetricsApiRequest(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            FromTime = fromTime;
            ToTime = toTime;
            ClientBaseAddress = "http://localhost:5000";
        }
    }
}