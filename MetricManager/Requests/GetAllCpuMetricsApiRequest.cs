using System;

namespace MetricManager.Requests
{
    public class GetAllCpuMetricsApiRequest 
    {
        public GetAllCpuMetricsApiRequest(int agentId, double fromTime, double toTime)
        {
            FromTime = fromTime;
            ToTime = toTime;
            AgentId = agentId;
        }
        
        public double FromTime { get; set; }
        public double ToTime { get; set; }
        public int AgentId { get; set; }
    }
}