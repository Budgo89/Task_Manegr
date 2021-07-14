using AutoMapper;
using MetricsManager.Client;
using MetricsManager.DAL.Models;
using MetricsManager.Repository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class NetworkMetricsJob : IJob
    {
        private INetworkMetricRepository _repository;
        private DateTimeOffset _toTime;
        private DateTimeOffset _fromTime;
        public IMetricsAgentClient _metricsAgentClient;
        private IAgentsrRepository _AgentsrRepository;
        private readonly IMapper _mapper;

        public NetworkMetricsJob(INetworkMetricRepository repository, IMetricsAgentClient metricsAgentClient, IAgentsrRepository AgentsrRepository, IMapper mapper)
        {
            _repository = repository;
            _metricsAgentClient = metricsAgentClient;
            _AgentsrRepository = AgentsrRepository;
            _mapper = mapper;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _toTime = DateTimeOffset.UtcNow;
            _fromTime = _repository.FromTime();
            var countAgentHdd = _AgentsrRepository.CountAgent();
            if (countAgentHdd != 0)
            {
                var clientBaseAddress = _AgentsrRepository.ClientBaseAddress();
                for (int i = 0; i < clientBaseAddress.Count; i++)
                {
                    var _allNetworkMetricsApiResponse = _metricsAgentClient.GetAllNetworkMetrics(new GetAllNetworkMetricsApiRespodse
                    {
                        FromTime = _fromTime,
                        ToTime = _toTime,
                        ClientBaseAddress = clientBaseAddress[i].AgentUrl
                    });
                    var MetricsDto = new List<NetworkMetricDto>();
                    foreach (var metric in _allNetworkMetricsApiResponse.Metrics)
                    {
                        MetricsDto.Add(new NetworkMetricDto
                        {
                            Id = metric.Id,
                            Value = metric.Value,
                            Time = metric.Time,
                            AgentId = clientBaseAddress[i].AgentId
                        });
                    }
                    _repository.Create(MetricsDto);
                }

            }
            return Task.CompletedTask;
        }
    }
}
