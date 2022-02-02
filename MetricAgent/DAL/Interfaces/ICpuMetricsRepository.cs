using Core.DAL.Interfaces;
using MetricAgent.DAL.Models;

namespace MetricAgent.DAL.Interfaces
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {
    }
}
