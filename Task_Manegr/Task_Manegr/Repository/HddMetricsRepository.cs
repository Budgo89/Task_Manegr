using Dapper;
using MetricsManager.Client;
using MetricsManager.DAL.Models;
using MetricsManager.Responses;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Repository
{
    public class HddMetricsRepository : IHddMetricRepository
    {
        private ConnectionManager connectionManager = new ConnectionManager();
        public IMetricsAgentClient _metricsAgentClient;
        public AllHddMetricsApiResponse _allHddMetricsApiResponse;

        public void ReadingMetricsFromAgent(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            //GetAllHddMetricsApiRequest getAllHddMetricsApiRequest = new GetAllHddMetricsApiRequest(fromTime, toTime);
            //_allHddMetricsApiResponse = _metricsAgentClient.GetAllHddMetrics(getAllHddMetricsApiRequest);
            //var response = new AllHddMetricsApiResponse()
            //{
            //    Metrics = new List<HddMetrics>()
            //};
            //response = _allHddMetricsApiResponse;
            //Create(response.Metrics);
        }
        public DateTimeOffset FromTime()
        {
            var ConnectionString = connectionManager.GetConnection();
            long time;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var count = connection.QuerySingle<int>("SELECT COUNT(1) FROM hddmetrics");
                if (count == 0)
                {
                    time = 0;
                }
                else time = connection.QuerySingle<long>("SELECT MAX(Time) FROM hddmetrics");

                return DateTimeOffset.FromUnixTimeSeconds(time);
            }
            
        }

        public void Create(List<HddMetricDto> Metrics) 
        {
            var ConnectionString = connectionManager.GetConnection();
            foreach (var item in Metrics)
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Execute("INSERT INTO hddmetrics(value, time, agentId) VALUES(@value, @time, @agentId)",
                        new
                        {
                            // value подставится на место "@value" в строке запроса
                            // значение запишется из поля Value объекта item
                            value = item.Value,
                            // записываем в поле time количество секунд
                            time = item.Time.ToUnixTimeSeconds(),
                            agentId = item.AgentId
                        });
                }
            }
        }

        public IList<HddMetricDto> GetByTimePeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var ConnectionString = connectionManager.GetConnection();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<HddMetricDto>("SELECT Id, Value, Time, agentId FROM hddmetrics WHERE (time >= @fromTime) AND (time <= @toTime) AND (agentId = @agentId)", 
                    new 
                    {                       
                        fromTime = fromTime.ToUnixTimeSeconds(), 
                        toTime = toTime.ToUnixTimeSeconds(),
                        agentId = agentId
                    }).ToList();
            }
        }

        //public void Create(HddMetrics item)
        //{
        //    var ConnectionString = connectionManager.GetConnection();
        //    using (var connection = new SQLiteConnection(ConnectionString))
        //    {
        //        connection.Execute("INSERT INTO hddmetrics(value, time) VALUES(@value, @time)",
        //            new
        //            {
        //                value = item.Value,
        //                time = item.Time.ToUnixTimeSeconds()
        //            });
        //    }
        //}
    }
}
