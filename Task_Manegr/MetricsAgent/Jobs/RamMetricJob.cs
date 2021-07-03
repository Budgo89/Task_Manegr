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
    public class RamMetricJob : IJob
    {
        private IRamMetricsRepository _repository;
        private PerformanceCounter _ramCounter;
        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }


        public Task Execute(IJobExecutionContext context)
        {
            var ramUsageInPercents = Convert.ToInt32(_ramCounter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new RamMetric { Time = DateTimeOffset.FromUnixTimeSeconds(time), Value = ramUsageInPercents });
            return Task.CompletedTask;
        }
    }
}
