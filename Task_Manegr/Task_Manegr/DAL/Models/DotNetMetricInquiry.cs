namespace MetricsManager.DAL.Interfaces
{
    public class DotNetMetricInquiry
    {
        public int Id { get; set; }

        public long Value { get; set; }

        public long Time { get; set; }

        public int AgentId { get; set; }
    }
}