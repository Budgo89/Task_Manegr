using MetricsAgent.DAL.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private ICpuMetricsRepository _repository;

        // счетчик для метрики CPU
        private PerformanceCounter _cpuCounter;


        public CpuMetricJob(ICpuMetricsRepository repository)
        {
            _repository = repository;
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости CPU
            var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            // теперь можно записать что-то при помощи репозитория

            _repository.Create(new CpuMetric { Time = DateTimeOffset.FromUnixTimeSeconds(time), Value = cpuUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
