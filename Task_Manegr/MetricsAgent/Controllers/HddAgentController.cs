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
        private IHddMetricsRepository repository;
        private readonly ILogger<HddAgentController> _logger;

        public HddAgentController(IHddMetricsRepository repository,ILogger<HddAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddAgentController");
            this.repository = repository;
        }

        [HttpGet("left")]
        public IActionResult GetAgentFromAgent()
        {
            _logger.LogInformation("Данные НДД");
            var metrics = repository.GetAll();

            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
    }
    public class HddMetric
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
