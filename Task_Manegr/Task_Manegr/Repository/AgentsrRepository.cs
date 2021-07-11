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
        private ConnectionManager connectionManager = new ConnectionManager();

        public void AgenRegister(AgentInfo agentInfo)
        {
            var ConnectionString = connectionManager.GetConnection();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO agents(AgentId, AgentUrl) VALUES(@AgentId, @AgentUrl)",
                    // анонимный объект с параметрами запроса
                    new
                    {
                        AgentId = agentInfo.AgentId,                        
                        AgentUrl = agentInfo.AgentAddress
                    });
            }
        }

        public List<AgentInfo> ClientBaseAddress()
        {
            var ConnectionString = connectionManager.GetConnection();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<AgentInfo>("SELECT AgentId, AgentUrl FROM agents").ToList();
            }
        }
    }
}
