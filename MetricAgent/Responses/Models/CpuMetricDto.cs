using System;

namespace MetricAgent.Responses.Models
{
    public class CpuMetricDto
    {
        public int Value { get; set; }
        public TimeSpan Time { get; set;}
        public int Id { get; set; }
    }
}
