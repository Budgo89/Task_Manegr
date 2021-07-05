using MetricsAgent.Controllers;
using MetricsAgent.DAL.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _repository;
        private PerformanceCounter _NetworkCounter;
        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            var category = new PerformanceCounterCategory("Network Adapter");
            var instancename = category.GetInstanceNames();
            _NetworkCounter = new PerformanceCounter("Network Adapter", "Bytes Received/sec", instancename[0]);

        }


        public Task Execute(IJobExecutionContext context)
        {
            var NetworkUsageInPercents = Convert.ToInt32(_NetworkCounter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new NetworkMetric { Time = DateTimeOffset.FromUnixTimeSeconds(time), Value = NetworkUsageInPercents });
            return Task.CompletedTask;
        }
    }
}
