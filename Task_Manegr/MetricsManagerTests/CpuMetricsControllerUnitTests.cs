using AutoMapper;
using MetricsManager;
using MetricsManager.Controllers;
using MetricsManager.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsManagerTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController controller;
        private Mock<ILogger<CpuMetricsController>> _loggerMock;
        private Mock<ICpuMetricRepository> _repository;
        private Mock<IMapper> _mapper;
        public CpuMetricsControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<CpuMetricsController>>();
            _repository = new Mock<ICpuMetricRepository>();
            _mapper = new Mock<IMapper>();
            controller = new CpuMetricsController(_loggerMock.Object,_repository.Object, _mapper.Object);
        }

        [Fact]
        public void CpuMetricsController_GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            //Act
            _repository.Setup(repository => repository.GetByTimePeriod(agentId, fromTime, toTime)).Returns(new List<CpuMetricInquiry>());
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void CpuMetricsController_GetMetricsFromAllCluster_ReturnsOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            //Act
            _repository.Setup(repository => repository.GetByAllTimePeriod(fromTime, toTime)).Returns(new List<CpuMetricInquiry>());
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
