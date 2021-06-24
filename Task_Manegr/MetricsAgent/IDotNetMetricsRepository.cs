using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.Controllers
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric>
    {
    }
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {

        public IList<DotNetMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var connectionManager = new ConnectionManager();
            var ConnectionString = connectionManager.CreateOpenedConnection();
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);

            string comText = $"SELECT * FROM metrics WHERE (time > {fromTime.ToUnixTimeSeconds()}) AND (time < {toTime.ToUnixTimeSeconds()})";
            cmd.CommandText = comText;
            cmd.ExecuteNonQuery();

            var returnList = new List<DotNetMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {                    
                    returnList.Add(new DotNetMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32(2))
                    });
                }
            }
            return returnList;
        }

    }
}