using Dapper;
using MetricsManager.Client;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Repository
{
    public class AgentsRepository : IAgentsrRepository
    {
        private ConnectionManager _connectionManager;
        public AgentsRepository(ConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void AgenRegister(AgentInfo agentInfo)
        {
            var ConnectionString = _connectionManager.GetConnection();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO agents(agentId, agentUrl, enabled) VALUES(@agentId, @agentUrl, @enabled)",
                    // анонимный объект с параметрами запроса
                    new
                    {
                        agentId = agentInfo.AgentId,                        
                        agentUrl = agentInfo.AgentAddress.ToString(),
                        enabled = true
                    });
            }
        }

        public List<Agent> ClientBaseAddress()
        {
            var ConnectionString = _connectionManager.GetConnection();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<Agent>("SELECT AgentId, AgentUrl FROM agents WHERE enabled = true").ToList();
            }
        }
        public int CountAgent()
        {
            var ConnectionString = _connectionManager.GetConnection();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<int>("SELECT COUNT(1) FROM agents WHERE enabled = true");
            }
        }

        public void DisableAgentById(int agentId)
        {
            var ConnectionString = _connectionManager.GetConnection();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE agents SET enabled = @enabled WHERE AgentId = AgentId",
                    new
                    {
                        AgentId = agentId,
                        enabled = false
                    });
            }
        }

        public void EnableAgentById(int agentId)
        {
            var ConnectionString = _connectionManager.GetConnection();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE agents SET enabled = @enabled WHERE AgentId = AgentId",
                    new
                    {
                        AgentId = agentId,
                        enabled = true
                    });
            }
        }

        public List<Agent> Receiving()
        {
            var ConnectionString = _connectionManager.GetConnection();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<Agent>("SELECT AgentId, AgentUrl FROM agents").ToList();
            }
        }
    }
}
