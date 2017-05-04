namespace Team_Manager.Tests.Services.TeamTaskService
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Team_Manager.Data.Common;
    using Team_Manager.Data.Models;
    using Team_Manager.Services.Data;

    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowException_WhenTeamTaskRepositoryIsNull()
        {
            // Arrange
            var userRepoMock = new Mock<IDbRepository<ApplicationUser>>();
            var teamRepoMock = new Mock<IDbRepository<Team>>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => new TeamTaskService(null, userRepoMock.Object, teamRepoMock.Object));
        }

        [TestMethod]
        public void ThrowException_WhenUserRepositoryIsNull()
        {
            // Arrange
            var teamTaskRepoMock = new Mock<IDbRepository<TeamTask>>();
            var teamRepoMock = new Mock<IDbRepository<Team>>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => new TeamTaskService(teamTaskRepoMock.Object, null, teamRepoMock.Object));
        }

        [TestMethod]
        public void ThrowException_WhenTeamRepositoryIsNull()
        {
            // Arrange
            var teamTaskRepoMock = new Mock<IDbRepository<TeamTask>>();
            var userRepoMock = new Mock<IDbRepository<ApplicationUser>>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => new TeamTaskService(teamTaskRepoMock.Object, userRepoMock.Object, null));
        }
    }
}
