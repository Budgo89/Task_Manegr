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
    public class CpuMetricsJob : IJob
    {
        private ICpuMetricRepository _repository;
        private DateTimeOffset _toTime;
        private DateTimeOffset _fromTime;
        private IMetricsAgentClient _metricsAgentClient;
        private IAgentsrRepository _AgentsrRepository;
        private readonly IMapper _mapper;

        public CpuMetricsJob(ICpuMetricRepository repository, IMetricsAgentClient metricsAgentClient, IAgentsrRepository AgentsrRepository, IMapper mapper)
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
                    var _allCpuMetricsApiResponse = _metricsAgentClient.GetAllCpuMetrics(new GetAllCpuMetricsApiRequest
                    {
                        FromTime = _fromTime,
                        ToTime = _toTime,
                        ClientBaseAddress = clientBaseAddress[i].AgentUrl
                    });
                    var MetricsDto = new List<CpuMetricDto>();
                    foreach (var metric in _allCpuMetricsApiResponse.Metrics)
                    {
                        MetricsDto.Add(new CpuMetricDto
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
