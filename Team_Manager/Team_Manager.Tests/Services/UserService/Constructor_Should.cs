using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Team_Manager.Data.Common;
using Team_Manager.Data.Models;


namespace Team_Manager.Tests.Services.UserService
{
    using Team_Manager.Services.Data;

    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowException_WhenUserRepositoryIsNull()
        {
            // Arrange 
            var invitaionRepoMock = new Mock<IDbRepository<Invitation>>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Team_Manager.Services.Data.UserService(null, invitaionRepoMock.Object));
        }

        [TestMethod]
        public void ThrowException_WhenInvitationRepositoryIsNull()
        {
            // Arrange 
            var userRepoMock = new Mock<IDbRepository<ApplicationUser>>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new UserService(userRepoMock.Object, null));
        }

        [TestMethod]
        public void ReturnAnInstance_WhenAllParametersAreNotNull()
        {
            // Arrange
            var userRepoMock = new Mock<IDbRepository<ApplicationUser>>();
            var invitaionRepoMock = new Mock<IDbRepository<Invitation>>();

            // Act
            var userService = new UserService(userRepoMock.Object, invitaionRepoMock.Object);

            // Assert
            Assert.IsNotNull(userService);
        }
    }
}
