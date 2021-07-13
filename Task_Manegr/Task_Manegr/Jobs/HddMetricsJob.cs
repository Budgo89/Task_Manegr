using AutoMapper;
using MetricsManager.Client;
using MetricsManager.DAL.Models;
using MetricsManager.Repository;
using Quartz;
using System;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;
        //private AllHddMetricsApiResponse _allHddMetricsApiResponse;

        public HddMetricsJob(IHddMetricRepository repository, IMetricsAgentClient metricsAgentClient, IAgentsrRepository AgentsrRepository, IMapper mapper)
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
                    var _allHddMetricsApiResponse = _metricsAgentClient.GetAllHddMetrics(new GetAllHddMetricsApiRequest
                    {
                        FromTime = _fromTime,
                        ToTime = _toTime,
                        ClientBaseAddress = clientBaseAddress[i].AgentUrl
                    });
                    var MetricsDto = new List<HddMetricDto>();
                    foreach (var metric in _allHddMetricsApiResponse.Metrics)
                    {
                        MetricsDto.Add(new HddMetricDto
                        {
                            Id = metric.Id,
                            Value = metric.Value,
                            Time = metric.Time,
                            AgentId = clientBaseAddress[i].AgentId
                        });
                        
                        //_repository.Create(_mapper.Map<HddMetrics>(metric));
                    }
                    _repository.Create(MetricsDto);
        }
                
    }
            return Task.CompletedTask;
        }
    }
}
