using Dapper;
using MetricsManager.Client;
using MetricsManager.DAL.Models;
using MetricsManager.Jobs;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Repository
{
    public class NetworkMetricRepository : INetworkMetricRepository
    {
        private ConnectionManager connectionManager;
        public IMetricsAgentClient _metricsAgentClient;
        public AllNetworkMetricsApiRespodse _allNetworkMetricsApiResponse;
        private IAgentsrRepository _AgentsrRepository;

        public NetworkMetricRepository(ConnectionManager ConnectionManager, IAgentsrRepository AgentsrRepository)
        {
            connectionManager = ConnectionManager;
            _AgentsrRepository = AgentsrRepository;
        }
        public DateTimeOffset FromTime()
        {
            var ConnectionString = connectionManager.GetConnection();
            long time;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var count = connection.QuerySingle<int>("SELECT COUNT(1) FROM networkmetrics");
                if (count == 0)
                {
                    time = 0;
                }
                else time = connection.QuerySingle<long>("SELECT MAX(Time) FROM networkmetrics");

                return DateTimeOffset.FromUnixTimeSeconds(time);
            }
        }
        public void Create(List<NetworkMetricDto> Metrics)
        {
            var ConnectionString = connectionManager.GetConnection();
            foreach (var item in Metrics)
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Execute("INSERT INTO networkmetrics(value, time, agentId) VALUES(@value, @time, @agentId)",
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
        public IList<NetworkMetricInquiry> GetByTimePeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
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
                    return connection.Query<NetworkMetricInquiry>("SELECT Id, Value, Time, agentId FROM networkmetrics WHERE (time >= @fromTime) AND (time <= @toTime) AND (agentId = @agentId)",
                        new
                        {
                            fromTime = fromTime.ToUnixTimeSeconds(),
                            toTime = toTime.ToUnixTimeSeconds(),
                            agentId = agentId
                        }).ToList();
                }
            }
            return new List<NetworkMetricInquiry>();
        }

        public IList<NetworkMetricInquiry> GetByAllTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var ConnectionString = connectionManager.GetConnection();
            var clientBaseAddress = _AgentsrRepository.ClientBaseAddress();
            if (clientBaseAddress.Count != 0)
            {
                var listMetrics = new List<NetworkMetricInquiry>();
                for (int i = 0; i < clientBaseAddress.Count; i++)
                {
                    using (var connection = new SQLiteConnection(ConnectionString))
                    {
                        listMetrics.AddRange(connection.Query<NetworkMetricInquiry>("SELECT Id, Value, Time, agentId FROM networkmetrics WHERE (time >= @fromTime) AND (time <= @toTime) AND (agentId = @agentId)",
                            new
                            {
                                fromTime = fromTime.ToUnixTimeSeconds(),
                                toTime = toTime.ToUnixTimeSeconds(),
                                agentId = clientBaseAddress[i].AgentId
                            }).ToList());
                    }
                }
                return listMetrics;
            }
            return new List<NetworkMetricInquiry>();
        }
    }
}
