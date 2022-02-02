using MetricAgent.DAL.Interfaces;
using MetricAgent.DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Dapper;
using System.Linq;

namespace MetricAgent.DAL.Repositories
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly SQLiteConnection _connection;

        public CpuMetricsRepository(IConfiguration configuration)
        {
            _connection = new SQLiteConnection(configuration.GetConnectionString("Sql"));
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }
        
        public void Create(CpuMetric item) =>
            _connection.Execute("INSERT INTO cpumetrics (value, time) VALUES (@value, @time)",
                new { value = item.Value, time = item.Time.TotalSeconds });
        

        public void Delete(int id) =>
            _connection.Execute("DELETE FROM cpumetrics WHERE id = @id", new { id });        

        public IList<CpuMetric> GetAll() =>
            _connection.Query<CpuMetric>("SELECT id, time, value FROM cpumetrics").ToList();


        public CpuMetric GetById(int id) =>
            _connection.QuerySingle<CpuMetric>("SELECT id, time, value FROM cpumetrics WHERE id = @id", new { id = id });


        public IList<CpuMetric> Select(TimeSpan fromTime, TimeSpan toTime) =>
            _connection.Query<CpuMetric>("SELECT id, value, time FROM cpumetrics WHERE time >@fromTime AND time < @toTime",
                new
                {
                    fromTime = fromTime.TotalSeconds,
                    toTime = toTime.TotalSeconds
                })
            .ToList();

      
        public void Update(CpuMetric item) =>
            _connection.Execute("UPDATE cpumetrics SET value = @value, time = @time WHERE id = @id",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    id = item.Id
                });
        
    }
}
