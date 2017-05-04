using System.Collections.Generic;

namespace Team_Manager.Tests.Services.TeamTaskService
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Team_Manager.Data.Common;
    using Team_Manager.Data.Models;
    using Team_Manager.Services.Data;

    [TestClass]
    public class GetAllTeamTasks_Should
    {
        [TestMethod]
        public void ReturnModel_WhenValidTeamIdIsPassed()
        {
            // Arrange
            var teamTaskRepoMock = new Mock<IDbRepository<TeamTask>>();
            var userRepoMock = new Mock<IDbRepository<ApplicationUser>>();
            var teamRepoMock = new Mock<IDbRepository<Team>>();
            int teamId = 1;

            teamRepoMock.Setup(m => m.GetById(teamId)).Returns(new Team()
            {
                Id = teamId,
                TeamTasks = new List<TeamTask>()
            });

            var service = new TeamTaskService(teamTaskRepoMock.Object, userRepoMock.Object, teamRepoMock.Object);

            // Act
            var model = service.GetAllTeamTasks(teamId);

            // Assert
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void ReturnNull_WhenInvalidTeamIdIsPassed()
        {
            // Arrange
            var teamTaskRepoMock = new Mock<IDbRepository<TeamTask>>();
            var userRepoMock = new Mock<IDbRepository<ApplicationUser>>();
            var teamRepoMock = new Mock<IDbRepository<Team>>();
            int teamId = 1;

            teamRepoMock.Setup(m => m.GetById(teamId)).Returns(new Team()
            {
                Id = teamId,
                TeamTasks = new List<TeamTask>()
            });

            var service = new TeamTaskService(teamTaskRepoMock.Object, userRepoMock.Object, teamRepoMock.Object);

            // Act
            var model = service.GetAllTeamTasks(2);

            // Assert
            Assert.IsNull(model);
        }
    }
}
