using MetricsAgent.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {
    }
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private ConnectionManager connectionManager = new ConnectionManager();
        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {            
            using var connection = connectionManager.GetOpenedConnection();
            using var cmd = new SQLiteCommand(connection);
            string comText = $"SELECT * FROM metrics WHERE (time > {fromTime.ToUnixTimeSeconds()}) AND (time < {toTime.ToUnixTimeSeconds()})";
            cmd.CommandText = comText;
            cmd.ExecuteNonQuery();

            var returnList = new List<CpuMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {

                        returnList.Add(new CpuMetric
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
