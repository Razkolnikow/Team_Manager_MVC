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

namespace Team_Manager.Tests.Services.TeamServiceTests
{
    [TestClass]
    public class GetAllTeamTopics_Should
    {
        [TestMethod]
        public void ReturnCollectionOfTeamTopicsModels_WhenThereIsTeamWithPassedId()
        {
            // Arrange
            var teamRepoMock = new Mock<IDbRepository<Team>>();
            var userRepoMock = new Mock<IDbRepository<ApplicationUser>>();
            var topicRepoMock = new Mock<IDbRepository<Topic>>();
            int teamId = 1;

            teamRepoMock.Setup(m => m.GetById(teamId)).Returns(new Team()
            {
                Id = teamId,
                Topics = new List<Topic>()
            });

            var teamService = new TeamService(teamRepoMock.Object, userRepoMock.Object, topicRepoMock.Object);
            // Act
            var collectionOfTeamTopicModels = teamService.GetAllTeamTopics(teamId);

            // Assert
            Assert.IsNotNull(collectionOfTeamTopicModels);
        }

        [TestMethod]
        public void ReturnNull_WhenThereIsNoTeamWithPassedId()
        {
            // Arrange
            var teamRepoMock = new Mock<IDbRepository<Team>>();
            var userRepoMock = new Mock<IDbRepository<ApplicationUser>>();
            var topicRepoMock = new Mock<IDbRepository<Topic>>();
            int teamId = 1;

            teamRepoMock.Setup(m => m.GetById(teamId)).Returns(new Team()
            {
                Id = teamId,
                Topics = new List<Topic>()
            });

            var teamService = new TeamService(teamRepoMock.Object, userRepoMock.Object, topicRepoMock.Object);
            // Act
            var collectionOfTeamTopicModels = teamService.GetAllTeamTopics(2);

            // Assert
            Assert.IsNull(collectionOfTeamTopicModels);
        }
    }
}
