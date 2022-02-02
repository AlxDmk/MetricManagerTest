using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MetricAgent.Responses.Models;
using MetricManager.Requests;

namespace MetricManager.Client
{
    public class MetricsAgentClient :IMetricsAgentClient
    {
        private HttpClient _httpClient;
        
        
        public MetricsAgentClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
           
        }
        public AllCpuMetricsResponse GetCpuMetrics()
        {
            //var fromParameter = request.FromTime;
            //var toParameter = request.ToTime;

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "https://localhost:4001/api/metrics/cpu/from/1643650494/to/1643650555");
            httpRequest.Headers.Add("Accept", "application/json");

            
                var response =_httpClient.SendAsync(httpRequest).Result;

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<AllCpuMetricsResponse>(responseStream).Result;
            }
            else
            {
                Console.WriteLine("ОЩИБКА");
                return null;
            }
            
          
        }

    }
}