using Dapper;
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
                connection.Execute("INSERT INTO agents(agentId, agentUrl) VALUES(@agentId, @agentUrl)",
                    // анонимный объект с параметрами запроса
                    new
                    {
                        agentId = agentInfo.AgentId,                        
                        agentUrl = agentInfo.AgentAddress.ToString()
                    });
            }
        }

        public List<AgentInfo> ClientBaseAddress()
        {
            var ConnectionString = _connectionManager.GetConnection();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<AgentInfo>("SELECT AgentId, AgentUrl FROM agents").ToList();
            }
        }
        public int CountAgent()
        {
            var ConnectionString = _connectionManager.GetConnection();

            using (var connection = new SQLiteConnection(ConnectionString))
            {

                return connection.QuerySingle<int>("SELECT COUNT(1) FROM agents");
            }
        }
    }
}
