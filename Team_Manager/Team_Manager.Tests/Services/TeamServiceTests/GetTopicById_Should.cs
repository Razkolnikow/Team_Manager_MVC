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
    public class GetTopicById_Should
    {
        [TestMethod]
        public void ReturnModel_WhenThereIsModelWithThePassedId()
        {
            // Arrange
            var teamRepoMock = new Mock<IDbRepository<Team>>();
            var userRepoMock = new Mock<IDbRepository<ApplicationUser>>();
            var topicRepoMock = new Mock<IDbRepository<Topic>>();
            int topicId = 1;

            topicRepoMock.Setup(m => m.GetById(topicId)).Returns(new Topic()
            {
                Id = topicId,
                Comments = new List<Comment>(), 
                Team = new Team()
            } );

            // Act
            var teamService = new TeamService(teamRepoMock.Object, userRepoMock.Object, topicRepoMock.Object);
            var topicModel = teamService.GetTopicById(topicId);

            // Assert
            Assert.IsNotNull(topicModel);
        }

        [TestMethod]
        public void ReturnNull_WhenThereIsNoModelWithThePassedId()
        {
            // Arrange
            var teamRepoMock = new Mock<IDbRepository<Team>>();
            var userRepoMock = new Mock<IDbRepository<ApplicationUser>>();
            var topicRepoMock = new Mock<IDbRepository<Topic>>();
            int topicId = 1;

            topicRepoMock.Setup(m => m.GetById(topicId)).Returns(new Topic()
            {
                Id = topicId,
                Comments = new List<Comment>(),
                Team = new Team()
            });

            // Act
            var teamService = new TeamService(teamRepoMock.Object, userRepoMock.Object, topicRepoMock.Object);
            var topicModel = teamService.GetTopicById(2);

            // Assert
            Assert.IsNull(topicModel);
        }
    }
}
