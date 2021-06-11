using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddAgentController : ControllerBase
    {
        private readonly ILogger<HddAgentController> _logger;

        public HddAgentController(ILogger<HddAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddAgentController");
        }

        [HttpGet("left")]
        public IActionResult GetAgentFromAgent()
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог");
            return Ok();
        }
    }
}
