using System;
using MetricManager.Client;
using MetricManager.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MetricManager.Controllers
{
    [ApiController]
    [Route("api/metrics/cpu")]
    public class CpuMetricsController : ControllerBase
    {
        private readonly IMetricsAgentClient _metricsAgentClient;


        public CpuMetricsController(IMetricsAgentClient metricsAgentClient)
        {
            _metricsAgentClient = metricsAgentClient;
        }

        
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(int agentId = 1, double fromTime = 1643650494,  double toTime = 1643650494)
        {
            var response = _metricsAgentClient.GetCpuMetrics();
            return response == null ? Problem() : Ok(response);

        }
       
        
    }
}