using MetricsManagerClient.Metrics.ApiResponse;

namespace MetricsManagerClient.Agents.Repository
{
    public interface IMetricsRepository
    {
        public CpuMetricApiResponse CpuMetricLoading(string id, string fromTime, string toTime);
        public CpuMetricApiResponse CpuMetricLoadingAll(string fromTime, string toTime);
        public DotNetMetricsApiResponse DotNetMetricLoading(string id, string fromTime, string toTime);
        public DotNetMetricsApiResponse DotNetMetricLoadingAll(string fromTime, string toTime);
        public HddMetricsApiResponse HddMetricLoading(string id, string fromTime, string toTime);
        public HddMetricsApiResponse HddMetricLoadingAll(string fromTime, string toTime);
        public NetworkMetricsApiResponse NetworkMetricLoading(string id, string fromTime, string toTime);
        public NetworkMetricsApiResponse NetworkMetricLoadingAll(string fromTime, string toTime);
        public RamMetricsApiResponse RamkMetricLoading(string id, string fromTime, string toTime);
        public RamMetricsApiResponse RamMetricLoadingAll(string fromTime, string toTime);
    }
}