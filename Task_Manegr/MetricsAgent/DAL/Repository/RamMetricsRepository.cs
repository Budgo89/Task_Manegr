using Dapper;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Repository
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private ConnectionManager connectionManager = new ConnectionManager();
        public RamMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        public IList<RamMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var ConnectionString = connectionManager.GetConnection();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<RamMetric>("SELECT id, value, time FROM rammetrics WHERE (time >= @fromTime) AND (time <= @toTime)",
                    new { fromTime = fromTime.ToUnixTimeSeconds(), toTime = toTime.ToUnixTimeSeconds() }).ToList();
            }
        }
        public void Create(RamMetric item)
        {
            var ConnectionString = connectionManager.GetConnection();
            using (var connection = new SQLiteConnection(ConnectionString))
            {

                connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)",
                    new { value = item.Value, time = item.Time.ToUnixTimeSeconds() });
            }
        }
    }
}
