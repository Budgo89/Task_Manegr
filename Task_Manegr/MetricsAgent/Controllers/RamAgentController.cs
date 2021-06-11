using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamAgentController : ControllerBase
    {
        private readonly ILogger<RamAgentController> _logger;

        public RamAgentController(ILogger<RamAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamAgentController");
        }
        [HttpGet("available")]
        public IActionResult GetAgentFromAgent()
        {
            return Ok();
        }
    }
}
