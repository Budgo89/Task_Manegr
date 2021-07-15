﻿using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private IDotNetMetricRepository _repository;
        private readonly IMapper _mapper;
        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Получение dotnet Метрик для агента с ID
        /// </summary>
        /// <param name="agentId">ID агента</param>
        /// <param name="fromTime">Дата и время начального периода загрузки. Формат: 2021-06-14T12:04:00Z</param>
        /// <param name="toTime">Дата и время конечного периода загрузки. Формат: 2021-06-14T12:04:00Z</param>
        /// <returns></returns>
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Входные данные {agentId} {fromTime} , {toTime}", agentId, fromTime, toTime);
            fromTime = new DateTimeOffset(fromTime.UtcDateTime);
            toTime = new DateTimeOffset(toTime.UtcDateTime);
            var metrics = _repository.GetByTimePeriod(agentId, fromTime, toTime);
            var response = new List<DotNetMetricDto>();
            foreach (var metric in metrics)
            {
                response.Add(_mapper.Map<DotNetMetricDto>(metric));
            }
            return Ok(response);
        }
        /// <summary>
        /// Получение dotnet Метрик для всех включённых агентов
        /// </summary>
        /// <param name="fromTime">Дата и время начального периода загрузки. Формат: 2021-06-14T12:04:00Z</param>
        /// <param name="toTime">Дата и время конечного периода загрузки. Формат: 2021-06-14T12:04:00Z</param>
        /// <returns></returns>
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Входные данные {fromTime} , {toTime}", fromTime, toTime);
            fromTime = new DateTimeOffset(fromTime.UtcDateTime);
            toTime = new DateTimeOffset(toTime.UtcDateTime);
            var metrics = _repository.GetByAllTimePeriod(fromTime, toTime);
            var response = new List<DotNetMetricDto>();
            foreach (var metric in metrics)
            {
                response.Add(_mapper.Map<DotNetMetricDto>(metric));
            }
            return Ok(response);
        }
    }
}
