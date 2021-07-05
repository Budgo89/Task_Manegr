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
    public class HddMetricsJob : IJob
    {
        private IHddMetricsRepository _repository;

        // счетчик для метрики CPU
        private PerformanceCounter _HddCounter;


        public HddMetricsJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _HddCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости CPU
            var hddUsageInPercents = Convert.ToInt32(_HddCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            // теперь можно записать что-то при помощи репозитория

            _repository.Create(new HddMetric { Time = DateTimeOffset.FromUnixTimeSeconds(time), Value = hddUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
