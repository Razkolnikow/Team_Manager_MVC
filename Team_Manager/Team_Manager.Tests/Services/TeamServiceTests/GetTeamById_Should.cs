using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Team_Manager.Data.Common;
using Team_Manager.Data.Models;
using Team_Manager.Services.Data;
using Team_Manager.Services.Data.ViewModels;

namespace Team_Manager.Tests.Services.TeamServiceTests
{
    [TestClass]
    public class GetTeamById_Should
    {
        [TestMethod]
        public void ReturnModel_WhenThereIsAModelWithThePassedId()
        {
            // Arrange
            var teamRepoMock = new Mock<IDbRepository<Team>>();
            var userRepoMock = new Mock<IDbRepository<ApplicationUser>>();
            var topicRepoMock = new Mock<IDbRepository<Topic>>();
            int teamId = 1;

            teamRepoMock.Setup(m => m.GetById(teamId)).Returns(new Team() {Id = teamId});

            var teamService = new TeamService(teamRepoMock.Object, userRepoMock.Object, topicRepoMock.Object);

            // Act
            TeamViewModel teamModel = teamService.GetTeamById(teamId);

            //Assert
            Assert.IsNotNull(teamModel);
        }

        [TestMethod]
        public void ReturnNull_WhenThereIsNoModelWithPassedId()
        {
            // Arrange
            var teamRepoMock = new Mock<IDbRepository<Team>>();
            var userRepoMock = new Mock<IDbRepository<ApplicationUser>>();
            var topicRepoMock = new Mock<IDbRepository<Topic>>();
            int teamId = 1;

            teamRepoMock.Setup(m => m.GetById(teamId)).Returns(new Team() { Id = teamId });

            var teamService = new TeamService(teamRepoMock.Object, userRepoMock.Object, topicRepoMock.Object);

            // Act
            TeamViewModel teamModel = teamService.GetTeamById(2);

            // Assert
            Assert.IsNull(teamModel);
        }
    }
}
