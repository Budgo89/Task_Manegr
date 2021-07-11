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
    public class DotNetAgentControllerUnitTests
    {
        private DotNetAgentController controller;
        private Mock<IDotNetMetricsRepository> repMock;
        private Mock<ILogger<DotNetAgentController>> _loggerMock;
        private Mock<IMapper> _mapper;

        public DotNetAgentControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<DotNetAgentController>>();
            repMock = new Mock<IDotNetMetricsRepository>();
            _mapper = new Mock<IMapper>();
            controller = new DotNetAgentController(repMock.Object, _loggerMock.Object, _mapper.Object);
        }

        [Fact]
        public void DotNetAgentController_GetMetricsFromAgent_ReturnsOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            repMock.Setup(repository => repository.GetByTimePeriod(fromTime, toTime)).Returns(new List<DotNetMetric>());
            var result = controller.GetAgentFromAgent(fromTime, toTime);
            repMock.Verify(repository => repository.GetByTimePeriod(fromTime, toTime), Times.AtLeastOnce());
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
