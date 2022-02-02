using AutoMapper;
using MetricAgent.DAL.Models;
using MetricAgent.Responses.Models;

namespace MetricAgent.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>();
        }

    }
}
