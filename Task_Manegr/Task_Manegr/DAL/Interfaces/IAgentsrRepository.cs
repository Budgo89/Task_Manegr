using System.Collections.Generic;

namespace MetricsManager.Repository
{
    public interface IAgentsrRepository
    {
        void AgenRegister(AgentInfo agentInfo);
        public List<AgentInfo> ClientBaseAddress();
        public int CountAgent();
    }
}