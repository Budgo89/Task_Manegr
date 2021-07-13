using System;

namespace MetricsManager.Client
{
    public class GetAllHddMetricsApiRequest
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public string ClientBaseAddress { get; set; }

        //public GetAllHddMetricsApiRequest(DateTimeOffset fromTime, DateTimeOffset toTime, string clientBaseAddress)
        //{
        //    FromTime = fromTime;
        //    ToTime = toTime;
        //    ClientBaseAddress = clientBaseAddress;
        //}
    }
}