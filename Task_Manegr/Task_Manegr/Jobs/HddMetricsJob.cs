using AutoMapper;
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
        private readonly IMapper _mapper;
        private AllHddMetricsApiResponse _allHddMetricsApiResponse;

        public HddMetricsJob(IHddMetricRepository repository, IMetricsAgentClient metricsAgentClient, IAgentsrRepository AgentsrRepository, IMapper mapper, AllHddMetricsApiResponse allHddMetricsApiResponse)
        {
            _repository = repository;
            _toTime = DateTimeOffset.UtcNow;
            _fromTime = _repository.FromTime();
            _metricsAgentClient = metricsAgentClient;
            _AgentsrRepository = AgentsrRepository;
            _mapper = mapper;
            _allHddMetricsApiResponse = allHddMetricsApiResponse;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var countAgentHdd = _AgentsrRepository.CountAgent();
            if (countAgentHdd != 0)
            {
                var clientBaseAddress = _AgentsrRepository.ClientBaseAddress();
                for (int i = 0; i < clientBaseAddress.Count; i++)
                {
                    GetAllHddMetricsApiRequest getAllHddMetricsApiRequest = new GetAllHddMetricsApiRequest(_fromTime, _toTime, clientBaseAddress[i].AgentAddress);
                    //Временная заглушка
            //GetAllHddMetricsApiRequest getAllHddMetricsApiRequest = new GetAllHddMetricsApiRequest(_fromTime, _toTime, "http://localhost:5000");
            _allHddMetricsApiResponse = _metricsAgentClient.GetAllHddMetrics(getAllHddMetricsApiRequest);
                    var response = new AllHddMetricsApiResponse
                    {
                        Metrics = new List<HddMetrics>()
                    };
                    foreach (var metric in _allHddMetricsApiResponse.Metrics)
                    {
                        response.Metrics.Add(_mapper.Map<HddMetrics>(metric));
                    }
                    _repository.Create(response.Metrics);
        }

    }
            return Task.CompletedTask;
        }
    }
}
