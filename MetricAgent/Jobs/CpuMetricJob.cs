using System.Diagnostics;
using MetricAgent.DAL.Interfaces;
using Quartz;
using System.Threading.Tasks;
using System;
using MetricAgent.DAL.Models;

namespace MetricAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly PerformanceCounter _cpuCounter;

        public CpuMetricJob(ICpuMetricsRepository repository)
        {
            _repository = repository;
            _cpuCounter = new PerformanceCounter("Processor",
                                                 "% Processor Time",
                                                 "_Total");
        }

        public Task Execute (IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            
            _repository.Create(new CpuMetric { Time = time, Value = cpuUsageInPercents});

            return Task.CompletedTask;
        }

        
    }
}
