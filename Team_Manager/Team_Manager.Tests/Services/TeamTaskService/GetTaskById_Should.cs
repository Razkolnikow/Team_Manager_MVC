namespace Team_Manager.Tests.Services.TeamTaskService
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Team_Manager.Data.Common;
    using Team_Manager.Data.Models;
    using Team_Manager.Services.Data;

    [TestClass]
    public class GetTaskById_Should
    {
        [TestMethod]
        public void ReturnModel_WhenValidIdIsPassed()
        {
            // Arrange
            var teamTaskRepoMock = new Mock<IDbRepository<TeamTask>>();
            var userRepoMock = new Mock<IDbRepository<ApplicationUser>>();
            var teamRepoMock = new Mock<IDbRepository<Team>>();

            int taskId = 1;
            teamTaskRepoMock.Setup(m => m.GetById(taskId)).Returns(new TeamTask() {Id = taskId});
            var taskService = new TeamTaskService(teamTaskRepoMock.Object, userRepoMock.Object, teamRepoMock.Object);

            // Act
            var taskModel = taskService.GetTaskById(taskId);

            // Assert
            Assert.IsNotNull(taskModel);
        }

        [TestMethod]
        public void ReturnNull_WhenInvalidIdIsPassed()
        {
            // Arrange
            var teamTaskRepoMock = new Mock<IDbRepository<TeamTask>>();
            var userRepoMock = new Mock<IDbRepository<ApplicationUser>>();
            var teamRepoMock = new Mock<IDbRepository<Team>>();

            int taskId = 1;
            teamTaskRepoMock.Setup(m => m.GetById(taskId)).Returns(new TeamTask() { Id = taskId });
            var taskService = new TeamTaskService(teamTaskRepoMock.Object, userRepoMock.Object, teamRepoMock.Object);

            // Act
            var taskModel = taskService.GetTaskById(2);

            // Assert
            Assert.IsNull(taskModel);
        }
    }
}
