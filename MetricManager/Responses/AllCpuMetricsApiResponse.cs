using System.Collections.Generic;
using MetricAgent.Responses.Models;


namespace MetricManager.Responses
{
    public class AllCpuMetricsApiResponse    {
        // взял dto из агента
        public IList<CpuMetricDto> Metrics { get; set; }
    }
}