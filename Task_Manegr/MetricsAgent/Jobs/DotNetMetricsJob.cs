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
    public class DotNetMetricsJob : IJob
    {
        private IDotNetMetricsRepository _repository;

        private PerformanceCounter _dotNetCounter;

        public DotNetMetricsJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            var category = new PerformanceCounterCategory(".NET CLR Memory");
            var instancename = category.GetInstanceNames();
            _dotNetCounter = new PerformanceCounter(".NET CLR Memory", "# Bytes in all Heaps", instancename[0]);
        }

        public Task Execute(IJobExecutionContext context)
        {
            var DotNetUsageInPercents = Convert.ToInt32(_dotNetCounter.NextValue());

            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            _repository.Create(new DotNetMetric { Time = DateTimeOffset.FromUnixTimeSeconds(time), Value = DotNetUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
