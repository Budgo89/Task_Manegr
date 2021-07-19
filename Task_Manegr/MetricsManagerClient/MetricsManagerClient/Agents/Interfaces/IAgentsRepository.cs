using MetricsManagerClient.Agents.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerClient.Agents.Interfaces
{
    internal interface IAgentsRepository
    {
        AgentApiResponse ReceivingAgentById();
        public void EnableAgent(string IdClientText);
        public void DisableAgent(string IdClientText);
    }
}
