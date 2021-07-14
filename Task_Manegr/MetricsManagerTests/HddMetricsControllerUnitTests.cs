using AutoMapper;
using MetricsManager.Controllers;
using MetricsManager.DAL.Models;
using MetricsManager.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsManagerTests
{
    public class HddMetricsControllerUnitTests
    {
        private HddMetricsController controller;
        private Mock<ILogger<HddMetricsController>> _loggerMock;
        private Mock<IHddMetricRepository> _repository;
        private Mock<IMapper> _mapper;
        public HddMetricsControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<HddMetricsController>>();
            _repository = new Mock<IHddMetricRepository>();
            _mapper = new Mock<IMapper>();
            controller = new HddMetricsController(_loggerMock.Object,_repository.Object, _mapper.Object);
        }
        [Fact]
        public void HddMetricsController_GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            _repository.Setup(repository => repository.GetByTimePeriod(agentId, fromTime, toTime)).Returns(new List<HddMetricInquiry>());
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void HddMetricsController_GetMetricsFromAllCluster_ReturnsOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            _repository.Setup(repository => repository.GetByAllTimePeriod(fromTime, toTime)).Returns(new List<HddMetricInquiry>());
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
