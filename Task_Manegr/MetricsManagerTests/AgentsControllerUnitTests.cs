using MetricsManager;
using MetricsManager.Controllers;
using MetricsManager.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private AgentsController controller;
        private Mock<ILogger<AgentsController>> _loggerMock;
        private Mock<IAgentsrRepository> _agentsrRepository;
        public AgentsControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<AgentsController>>();
            _agentsrRepository = new Mock<IAgentsrRepository>();
            controller = new AgentsController(_loggerMock.Object, _agentsrRepository.Object);
        }
        [Fact]
        public void RegisterAgent_ReturnsOk()
        {

            var agentInfo = new AgentInfo() { };


            var result = controller.RegisterAgent(agentInfo);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            var agentId = 1;

            var result = controller.EnableAgentById(agentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            var agentId = 1;

            var result = controller.DisableAgentById(agentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
