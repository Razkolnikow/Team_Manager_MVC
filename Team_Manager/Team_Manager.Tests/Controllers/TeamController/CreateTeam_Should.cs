using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Team_Manager.Services.Data.Contracts;
using TestStack.FluentMVCTesting;

namespace Team_Manager.Tests.Controllers.TeamController
{
    [TestClass]
    public class CreateTeam_Should
    {
        [TestMethod]
        public void RenderViewCreateTeam()
        {
            // Arrange
            var teamService = new Mock<ITeamService>();
            var teamController = new Team_Manager.Controllers.TeamController(teamService.Object);

            // Act & Assert
            teamController
                .WithCallTo(c => c.CreateTeam())
                .ShouldRenderView("CreateTeam");
        }
    }
}
