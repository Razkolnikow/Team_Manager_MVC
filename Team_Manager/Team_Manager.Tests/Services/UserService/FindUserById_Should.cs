using System;

namespace Team_Manager.Tests.Services.UserService
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Team_Manager.Data.Common;
    using Team_Manager.Data.Models;
    using Team_Manager.Services.Data;

    [TestClass]
    public class FindUserById_Should
    {
        [TestMethod]
        public void ReturnUserModel_WhenThereIsUaserWithPassedId()
        {
            // Arrange
            var userRepoMock = new Mock<IDbRepository<ApplicationUser>>();
            var invitaionRepoMock = new Mock<IDbRepository<Invitation>>();
            string userId = Guid.NewGuid().ToString();
            userRepoMock.Setup(m => m.GetById(userId)).Returns(new ApplicationUser() {Id = userId});

            var userService = new UserService(userRepoMock.Object, invitaionRepoMock.Object);

            // Act
            var userModel = userService.FindUserById(userId);

            // Assert
            Assert.IsNotNull(userModel);
        }

        [TestMethod]
        public void ReturnNull_WhenThereIsNoUaserWithPassedId()
        {
            // Arrange
            var userRepoMock = new Mock<IDbRepository<ApplicationUser>>();
            var invitaionRepoMock = new Mock<IDbRepository<Invitation>>();
            string userId = Guid.NewGuid().ToString();
            userRepoMock.Setup(m => m.GetById(userId)).Returns(new ApplicationUser() { Id = userId });

            var userService = new UserService(userRepoMock.Object, invitaionRepoMock.Object);

            // Act
            var userModel = userService.FindUserById(Guid.NewGuid().ToString());

            // Assert
            Assert.IsNull(userModel);
        }
    }
}
