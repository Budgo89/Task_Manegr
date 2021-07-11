using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MetricsAgentTests
{
    public class RamAgentControllerUnitTests
    {
        private RamAgentController controller;
        private Mock<IRamMetricsRepository> repMock;
        private Mock<ILogger<RamAgentController>> _loggerMock;
        private Mock<IMapper> _mapper;

        public RamAgentControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<RamAgentController>>();
            repMock = new Mock<IRamMetricsRepository>();
            _mapper = new Mock<IMapper>();
            controller = new RamAgentController(repMock.Object, _loggerMock.Object, _mapper.Object);
        }

        [Fact]
        public void DotNetAgentController_GetMetricsFromAgent_ReturnsOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            repMock.Setup(repository => repository.GetByTimePeriod(fromTime, toTime)).Returns(new List<RamMetric>());
            var result = controller.GetAgentFromAgent(fromTime, toTime);
            repMock.Verify(repository => repository.GetByTimePeriod(fromTime, toTime), Times.AtLeastOnce());
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
