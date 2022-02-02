using  System;
using System.Data;
using Dapper;

namespace MetricAgent.DAL
{
    public class TimeSpanHandler : SqlMapper.TypeHandler<TimeSpan>
    {
        public override void SetValue(IDbDataParameter parameter, TimeSpan value)
            => parameter.Value = value;

        public override TimeSpan Parse(object value)
            => TimeSpan.FromSeconds((long) value);
    }
}