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
        public IMetricsAgentClient _metricsAgentClient;
        private IAgentsrRepository _AgentsrRepository;

        public HddMetricsJob(IHddMetricRepository repository, IMetricsAgentClient metricsAgentClient, IAgentsrRepository AgentsrRepository)
        {
            _repository = repository;
            _toTime = DateTimeOffset.UtcNow;
            _fromTime = _repository.FromTime();
            _metricsAgentClient = metricsAgentClient;
            _AgentsrRepository = AgentsrRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var countAgentHdd = _repository.CountAgentHdd();
            if (countAgentHdd != 0)
            {
                var clientBaseAddress = _AgentsrRepository.ClientBaseAddress();
                for (int i = 0; i < clientBaseAddress.Count; i++)
                {
                    GetAllHddMetricsApiRequest getAllHddMetricsApiRequest = new GetAllHddMetricsApiRequest(_fromTime, _toTime, clientBaseAddress[i].AgentAddress);
                    var _allHddMetricsApiResponse = _metricsAgentClient.GetAllHddMetrics(getAllHddMetricsApiRequest);
                    var response = _allHddMetricsApiResponse;
                    _repository.Create(response.Metrics);
                }

            }
            return Task.CompletedTask;
        }
    }
}
