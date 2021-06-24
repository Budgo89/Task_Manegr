using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.Controllers
{
    public interface IRamMetricsRepository : IRepository<RamMetric>
    {
    }
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private ConnectionManager connectionManager = new ConnectionManager();

        public IList<RamMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {            
            using var connection = connectionManager.GetOpenedConnection();
            using var cmd = new SQLiteCommand(connection);

            string comText = $"SELECT * FROM metrics WHERE (time > {fromTime.ToUnixTimeSeconds()}) AND (time < {toTime.ToUnixTimeSeconds()})";
            cmd.CommandText = comText;
            cmd.ExecuteNonQuery();

            var returnList = new List<RamMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new RamMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        // налету преобразуем прочитанные секунды в метку времени
                        Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32(2))
                    });
                }
            }

            return returnList;
        }
        
    }
}