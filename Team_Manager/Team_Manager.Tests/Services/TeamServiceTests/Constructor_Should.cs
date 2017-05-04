using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Team_Manager.Data;
using Team_Manager.Data.Common;
using Team_Manager.Data.Models;
using Team_Manager.Services.Data;
using TestStack.FluentMVCTesting;

namespace Team_Manager.Tests.Services.TeamServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowException_WhenTeamRepositoryIsNull()
        {
            //Arrange
            var userRepo = new Mock<IDbRepository<ApplicationUser>>();
            var topicRepo = new Mock<IDbRepository<Topic>>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TeamService(null, userRepo.Object, topicRepo.Object));
        }

        [TestMethod]
        public void ThrowException_WhenUserRepositoryIsNull()
        {
            //Arrange
            var teamRepo = new Mock<IDbRepository<Team>>();
            var topicRepo = new Mock<IDbRepository<Topic>>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TeamService(teamRepo.Object, null, topicRepo.Object));
        }

        [TestMethod]
        public void ThrowExceptionWhenTopicRepositoryIsNull()
        {
            //Arrange
            var teamRepo = new Mock<IDbRepository<Team>>();
            var userRepo = new Mock<IDbRepository<ApplicationUser>>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TeamService(teamRepo.Object, userRepo.Object, null));
        }

        [TestMethod]
        public void ReturnsAnInstance_WhenAllParametersAreNotNull()
        {
            //Arrange
            var teamRepo = new Mock<IDbRepository<Team>>();
            var userRepo = new Mock<IDbRepository<ApplicationUser>>();
            var topicRepo = new Mock<IDbRepository<Topic>>();

            //Act
            var teamService = new TeamService(teamRepo.Object, userRepo.Object, topicRepo.Object);

            //Assert
            Assert.IsNotNull(teamService);
        }
    }
}
