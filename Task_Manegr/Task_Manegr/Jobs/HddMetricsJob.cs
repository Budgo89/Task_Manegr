using MetricsManager.Client;
using MetricsManager.Repository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
        private ConnectionManager connectionManager = new ConnectionManager();

        public HddMetricsJob(IHddMetricRepository repository, IMetricsAgentClient metricsAgentClient)
        {
            _repository = repository;
            _toTime = DateTimeOffset.UtcNow;
            _fromTime = _repository.FromTime();
            _metricsAgentClient = metricsAgentClient;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var countAgentHdd = _repository.CountAgentHdd();
            if (countAgentHdd != 0)
            {
                GetAllHddMetricsApiRequest getAllHddMetricsApiRequest = new GetAllHddMetricsApiRequest(_fromTime, _toTime);
                var _allHddMetricsApiResponse = _metricsAgentClient.GetAllHddMetrics(getAllHddMetricsApiRequest);
                var response = _allHddMetricsApiResponse;
                _repository.Create(response.Metrics);
            }
            return Task.CompletedTask;
        }
    }
}
