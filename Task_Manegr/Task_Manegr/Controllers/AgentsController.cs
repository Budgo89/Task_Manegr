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
        /// <summary>
        /// Регистрация Метрик Агента. Агент по умолчанию включён
        /// </summary>
        /// <param name="agentInfo"> агент Url, Id </param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _agentsrRepository.AgenRegister(agentInfo);
            _logger.LogInformation("Входные данные {agentInfo}", agentInfo);
            return Ok();
        }
        /// <summary>
        /// Включение Агента
        /// </summary>
        /// <param name="agentId">Id агента</param>
        /// <returns></returns>
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _agentsrRepository.EnableAgentById(agentId);
            _logger.LogInformation("Входные данные {agentId}", agentId);
            return Ok();
        }
        /// <summary>
        /// Выключение агента
        /// </summary>
        /// <param name="agentId">Id агента</param>
        /// <returns></returns>
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _agentsrRepository.DisableAgentById(agentId);
            _logger.LogInformation("Входные данные {agentId}", agentId);
            return Ok();
        }
        /// <summary>
        /// Список агентов
        /// </summary>
        /// <returns></returns>
        [HttpGet("receiving")]
        public IActionResult ReceivingAgentById()
        {
            var receiving = _agentsrRepository.Receiving();
            _logger.LogInformation("Входные данные");
            return Ok(receiving);
        }
    }

}
