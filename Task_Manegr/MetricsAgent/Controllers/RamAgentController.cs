﻿using Microsoft.AspNetCore.Http;
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
        private IRamMetricsRepository repository;
        private readonly ILogger<RamAgentController> _logger;

        public RamAgentController(IRamMetricsRepository repository, ILogger<RamAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamAgentController");
            this.repository = repository;
        }
        [HttpGet("available")]
        public IActionResult GetAgentFromAgent()
        {
            _logger.LogInformation("Данные Ram");

            var metrics = repository.GetAll();

            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
    }
    public class RamMetric
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
