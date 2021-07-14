using MetricsManager.Client;
using System.Collections.Generic;

namespace MetricsManager.Repository
{
    public interface IAgentsrRepository
    {
        void AgenRegister(AgentInfo agentInfo);
        public List<Agent> ClientBaseAddress();
        public int CountAgent();
        public void EnableAgentById(int agentId);
        public void DisableAgentById(int agentId);        
    }
}