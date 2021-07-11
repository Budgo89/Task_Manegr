using System;

namespace MetricsManager.Client
{
    public class GetAllHddMetricsApiRequest
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public Uri ClientBaseAddress { get; set; }

        public GetAllHddMetricsApiRequest(DateTimeOffset fromTime, DateTimeOffset toTime, Uri clientBaseAddress)
        {
            FromTime = fromTime;
            ToTime = toTime;
            ClientBaseAddress = clientBaseAddress;
        }
    }
}