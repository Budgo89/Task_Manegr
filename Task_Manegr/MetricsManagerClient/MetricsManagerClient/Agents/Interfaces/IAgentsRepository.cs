using MetricsManagerClient.Agents.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerClient.Agents.Interfaces
{
    public interface IAgentsRepository
    {
        public AgentApiResponse ReceivingAgentById();
        public void EnableAgent(string IdClientText);
        public void DisableAgent(string IdClientText);
        public void RegisterAgent(string IdClientText, string url);
    }
}
