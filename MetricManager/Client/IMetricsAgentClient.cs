using MetricAgent.Responses.Models;
using MetricManager.Requests;
using MetricManager.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MetricManager.Client
{
    public interface IMetricsAgentClient
    {
        AllCpuMetricsResponse GetCpuMetrics();      
    }
}