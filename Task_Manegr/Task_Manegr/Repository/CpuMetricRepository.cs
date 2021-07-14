﻿using Dapper;
using MetricsManager.Client;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Repository
{
    public class CpuMetricRepository : ICpuMetricRepository
    {
        private ConnectionManager connectionManager;
        public IMetricsAgentClient _metricsAgentClient;
        public AllCpuMetricsApiResponse _allHddMetricsApiResponse;
        private IAgentsrRepository _AgentsrRepository;
        public CpuMetricRepository(ConnectionManager ConnectionManager, IAgentsrRepository AgentsrRepository)
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
                var count = connection.QuerySingle<int>("SELECT COUNT(1) FROM cpumetrics");
                if (count == 0)
                {
                    time = 0;
                }
                else time = connection.QuerySingle<long>("SELECT MAX(Time) FROM cpumetrics");

                return DateTimeOffset.FromUnixTimeSeconds(time);
            }
        }
        public void Create(List<CpuMetricDto> Metrics)
        {
            var ConnectionString = connectionManager.GetConnection();
            foreach (var item in Metrics)
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Execute("INSERT INTO cpumetrics(value, time, agentId) VALUES(@value, @time, @agentId)",
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
        public IList<CpuMetricInquiry> GetByTimePeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
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
                    return connection.Query<CpuMetricInquiry>("SELECT Id, Value, Time, agentId FROM cpumetrics WHERE (time >= @fromTime) AND (time <= @toTime) AND (agentId = @agentId)",
                        new
                        {
                            fromTime = fromTime.ToUnixTimeSeconds(),
                            toTime = toTime.ToUnixTimeSeconds(),
                            agentId = agentId
                        }).ToList();
                }
            }
            return new List<CpuMetricInquiry>();
        }
        public IList<CpuMetricInquiry> GetByAllTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var ConnectionString = connectionManager.GetConnection();
            var clientBaseAddress = _AgentsrRepository.ClientBaseAddress();
            if (clientBaseAddress.Count != 0)
            {
                var listMetrics = new List<CpuMetricInquiry>();
                for (int i = 0; i < clientBaseAddress.Count; i++)
                {
                    using (var connection = new SQLiteConnection(ConnectionString))
                    {
                        listMetrics.AddRange(connection.Query<CpuMetricInquiry>("SELECT Id, Value, Time, agentId FROM cpumetrics WHERE (time >= @fromTime) AND (time <= @toTime) AND (agentId = @agentId)",
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
            return new List<CpuMetricInquiry>();
        }
    }
}
