using MetricAgent.DAL.Interfaces;
using MetricAgent.Responses.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using MetricAgent.DAL.Models;
using MetricAgent.Requests;
using System;

namespace MetricAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly IMapper _mapper;

        public CpuMetricsController(ICpuMetricsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();
            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }                       

            return Ok(response);
        }

        [HttpPut("update")]
        public IActionResult Update ([FromForm] CpuMetric request)
        {
            _repository.Update(request);
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete ([FromRoute] int id)
        {
            _repository.Delete(id);
            return Ok();
        }


        [HttpGet("get/{id}")]
        public IActionResult GetById ([FromRoute] int id)
        {
            var result = _repository.GetById(id);            
            var response = new CpuMetricDto();
            _mapper.Map(result, response);

            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult Create (CpuMetricCreateRequest request)
        {
            _repository.Create(new CpuMetric
            {
                Time = request.Time,
                Value = request.Value
            });
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] double fromTime, [FromRoute] double toTime)
        {

            var result = _repository.Select(TimeSpan.FromSeconds(fromTime), TimeSpan.FromSeconds (toTime));
            var response = new AllCpuMetricsResponse()
            {
                Metrics =  new List<CpuMetricDto>()
            };

            foreach (var metric in result)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }
            
           
            return Ok(response);
        }
       
    }
}
