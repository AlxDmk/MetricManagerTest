using System.Collections.Generic;

namespace MetricAgent.Responses.Models
{
    public class AllCpuMetricsResponse
    {
        public IList<CpuMetricDto> Metrics {get; set;}

    }
}
