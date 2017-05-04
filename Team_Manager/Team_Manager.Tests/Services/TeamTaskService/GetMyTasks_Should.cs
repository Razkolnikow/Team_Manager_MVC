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
    public class GetMyTasks_Should
    {
        [TestMethod]
        public void ReturnCollectionOfTaskModels_WhenValidUserIdIsPassed()
        {
            // Arrange
            var teamTaskRepoMock = new Mock<IDbRepository<TeamTask>>();
            var userRepoMock = new Mock<IDbRepository<ApplicationUser>>();
            var teamRepoMock = new Mock<IDbRepository<Team>>();
            string userId = Guid.NewGuid().ToString();

            userRepoMock.Setup(m => m.GetById(userId)).Returns(new ApplicationUser()
            {
                Id = userId,
                TeamTasks = new List<TeamTask>()
            });

            var teamTaskService = new TeamTaskService(
                teamTaskRepoMock.Object, userRepoMock.Object, teamRepoMock.Object);

            // Act
            var tasks = teamTaskService.GetMyTasks(userId);

            // Assert
            Assert.IsNotNull(tasks);
        }

        [TestMethod]
        public void ReturnNull_WhenInvalidUserIdIsPassed()
        {
            // Arrange
            var teamTaskRepoMock = new Mock<IDbRepository<TeamTask>>();
            var userRepoMock = new Mock<IDbRepository<ApplicationUser>>();
            var teamRepoMock = new Mock<IDbRepository<Team>>();
            string userId = Guid.NewGuid().ToString();

            userRepoMock.Setup(m => m.GetById(userId)).Returns(new ApplicationUser()
            {
                Id = userId,
                TeamTasks = new List<TeamTask>()
            });

            var teamTaskService = new TeamTaskService(
                teamTaskRepoMock.Object, userRepoMock.Object, teamRepoMock.Object);

            // Act
            var tasks = teamTaskService.GetMyTasks(Guid.NewGuid().ToString());

            // Assert
            Assert.IsNull(tasks);
        }
    }
}
