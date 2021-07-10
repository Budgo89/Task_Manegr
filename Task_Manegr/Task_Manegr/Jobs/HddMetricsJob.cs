using MetricsManager.Client;
using MetricsManager.Repository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Jobs
{
    public class HddMetricsJob : IJob
    {
        private IHddMetricRepository _repository;
        private DateTimeOffset _toTime;
        private DateTimeOffset _fromTime;
        //public AllHddMetricsApiResponse _allHddMetricsApiResponse;
        public IMetricsAgentClient _metricsAgentClient;

        public HddMetricsJob(IHddMetricRepository repository, IMetricsAgentClient metricsAgentClient)
        {
            _repository = repository;
            _toTime = DateTimeOffset.UtcNow;
            _fromTime = _repository.FromTime();
            _metricsAgentClient = metricsAgentClient;
        }

        public Task Execute(IJobExecutionContext context)
        {
            GetAllHddMetricsApiRequest getAllHddMetricsApiRequest = new GetAllHddMetricsApiRequest(_fromTime, _toTime);
            var _allHddMetricsApiResponse = _metricsAgentClient.GetAllHddMetrics(getAllHddMetricsApiRequest);
            //var response = new AllHddMetricsApiResponse()
            //{
            //    Metrics = new List<HddMetrics>()
            //};
            var response = _allHddMetricsApiResponse;
            _repository.Create(response.Metrics);

            return Task.CompletedTask;
        }
    }
}
