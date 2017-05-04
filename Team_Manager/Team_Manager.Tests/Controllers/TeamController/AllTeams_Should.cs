using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Team_Manager.Services.Data.Contracts;
using Team_Manager.Services.Data.ViewModels.AdminViewModels;
using TestStack.FluentMVCTesting;

namespace Team_Manager.Tests.Controllers.TeamController
{
    [TestClass]
    public class AllTeams_Should
    {
        [TestMethod]
        public void ReturnDefaultView()
        {
            // Arrange
            var teamService = new Mock<ITeamService>();
            var teamController = new Team_Manager.Controllers.TeamController(teamService.Object);

            // Act & Assert
            teamController
                .WithCallTo(c => c.AllTeams())
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void ReturnViewResultWithNullModel_WhenTheServicePassesNullModel()
        {
            // Arrange
            var teamService = new Mock<ITeamService>();
            teamService.Setup(m => m.GetAllTeams()).Returns((IEnumerable<TeamAdminViewModel>) null);

            var teamController = new Team_Manager.Controllers.TeamController(teamService.Object);

            // Act
            var result = teamController.AllTeams() as ViewResult;

            // Assert
            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void RenderDefaultViewWithModelTeamAdminViewModels()
        {
            // Arrange
            var teamService = new Mock<ITeamService>();
            var teamController = new Team_Manager.Controllers.TeamController(teamService.Object);
            
            // Act & Assert 
            teamController.WithCallTo(c => c.AllTeams())
                .ShouldRenderDefaultView().WithModel<IEnumerable<TeamAdminViewModel>>();
        }
    }
}
