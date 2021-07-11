using MetricsManager.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private IAgentsrRepository _agentsrRepository;
        public AgentsController(ILogger<AgentsController> logger, IAgentsrRepository AgentsrRepository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в AgentsController");
            _agentsrRepository = AgentsrRepository;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _agentsrRepository.AgenRegister(agentInfo);
            _logger.LogInformation("Входные данные {agentInfo}", agentInfo);
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation("Входные данные {agentId}", agentId);
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation("Входные данные {agentId}", agentId);
            return Ok();
        }
        [HttpGet("receiving")]
        public IActionResult ReceivingAgentById()
        {
            _logger.LogInformation("Входные данные");
            return Ok();
        }
    }

}
