using System;

namespace MetricAgent.Requests
{
    public class CpuMetricCreateRequest
    {
        public int Value { get; set; }
        public TimeSpan Time { get; set; } 
    }
}
