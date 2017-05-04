using System.Web.Mvc;

namespace Team_Manager.Tests.Controllers.UserController
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Team_Manager.Services.Data.Contracts;
    using Team_Manager.Services.Data.ViewModels;
    using Team_Manager.Controllers;

    [TestClass]
    public class UserInfo_Should
    {
        [TestMethod]
        public void ReturnViewResult_WhenValidUserIdIsPassed()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            string userId = Guid.NewGuid().ToString();
            userService.Setup(m => m.FindUserById(userId)).Returns(new TeamManagerUserViewModel() {Id = userId});

            var userController = new UserController(userService.Object);

            // Act
            var result = userController.UserInfo(userId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ReturnViewResultWithNullModel_WhenInvalidUserIdIsPassed()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            string userId = Guid.NewGuid().ToString();
            userService.Setup(m => m.FindUserById(userId)).Returns(new TeamManagerUserViewModel() { Id = userId });

            var userController = new UserController(userService.Object);

            // Act
            var result = userController.UserInfo(Guid.NewGuid().ToString()) as ViewResult;

            // Assert
            Assert.IsNull(result.Model);
        }
    }
}
