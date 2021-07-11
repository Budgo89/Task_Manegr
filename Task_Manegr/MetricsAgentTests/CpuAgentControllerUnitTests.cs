using AutoMapper;
using MetricsAgent;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuAgentControllerUnitTests
    {
        private CpuAgentController controller;
        private Mock<ICpuMetricsRepository> repMock;
        private Mock<ILogger<CpuAgentController>> _loggerMock;
        private Mock<IMapper> _mapper;
        public CpuAgentControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<CpuAgentController>>();
            repMock = new Mock<ICpuMetricsRepository>();
            _mapper = new Mock<IMapper>();
            controller = new CpuAgentController(repMock.Object, _loggerMock.Object, _mapper.Object);            
        }

        [Fact]
        public void CpuAgentController_GetMetricsFromAgent_ReturnsOk()
        {            
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            repMock.Setup(repository => repository.GetByTimePeriod(fromTime, toTime)).Returns(new List<CpuMetric>());

            var result = controller.GetAgentFromAgent(fromTime, toTime);
            repMock.Verify(repository => repository.GetByTimePeriod(fromTime, toTime), Times.AtLeastOnce());

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
