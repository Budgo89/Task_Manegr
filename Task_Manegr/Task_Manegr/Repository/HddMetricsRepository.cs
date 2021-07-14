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
        private ConnectionManager connectionManager;
        public IMetricsAgentClient _metricsAgentClient;
        public AllHddMetricsApiResponse _allHddMetricsApiResponse;
        private IAgentsrRepository _AgentsrRepository;
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

        public HddMetricsRepository(ConnectionManager ConnectionManager, IAgentsrRepository  AgentsrRepository)
        {
            connectionManager = ConnectionManager;
            _AgentsrRepository = AgentsrRepository;
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

        public IList<HddMetricInquiry> GetByTimePeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var ConnectionString = connectionManager.GetConnection();
            bool enabledAgent;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                enabledAgent = connection.QuerySingle<bool>("SELECT enabled FROM agents WHERE agentId = @agentId",
                    new { agentId = agentId });
            }
            if (enabledAgent == true)
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    return connection.Query<HddMetricInquiry>("SELECT Id, Value, Time, agentId FROM hddmetrics WHERE (time >= @fromTime) AND (time <= @toTime) AND (agentId = @agentId)",
                        new
                        {
                            fromTime = fromTime.ToUnixTimeSeconds(),
                            toTime = toTime.ToUnixTimeSeconds(),
                            agentId = agentId
                        }).ToList();
                }
            }
            return new List<HddMetricInquiry>();
        }

        public IList<HddMetricInquiry> GetByAllTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime) 
        {
            var ConnectionString = connectionManager.GetConnection();
            var clientBaseAddress = _AgentsrRepository.ClientBaseAddress();
            if (clientBaseAddress.Count != 0)
            {
                for (int i = 0; i < clientBaseAddress.Count; i++)
                {
                    using (var connection = new SQLiteConnection(ConnectionString))
                    {
                        return connection.Query<HddMetricInquiry>("SELECT Id, Value, Time, agentId FROM hddmetrics WHERE (time >= @fromTime) AND (time <= @toTime) AND (agentId = @agentId)",
                            new
                            {
                                fromTime = fromTime.ToUnixTimeSeconds(),
                                toTime = toTime.ToUnixTimeSeconds(),
                                agentId = clientBaseAddress[i].AgentId
                            }).ToList();
                    }
                }
            }
            return new List<HddMetricInquiry>();
        }
    }
}
