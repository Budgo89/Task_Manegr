using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerClient.Agents.Models
{
    public class Agent
    {
        public int AgentId { get; set; }
        public string AgentUrl { get; set; }
        public bool Enabled { get; set; }
    }
}
