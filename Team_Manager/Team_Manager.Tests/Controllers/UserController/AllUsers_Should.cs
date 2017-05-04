using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Team_Manager.Services.Data.Contracts;
using Team_Manager.Services.Data.ViewModels;

namespace Team_Manager.Tests.Controllers.UserController
{
    [TestClass]
    public class AllUsers_Should
    {
        [TestMethod]
        public void ReturnViewResult_WhenServicePassesCollectionWithModels()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            userService.Setup(m => m.GetAllUsers()).Returns(new List<TeamMemberViewModel>());

            var userController = new Team_Manager.Controllers.UserController(userService.Object);

            // Act
            var result = userController.AllUsers() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ReturnViewResultWithNullModel_WhenServicePassesNull()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            userService.Setup(m => m.GetAllUsers()).Returns((IEnumerable<TeamMemberViewModel>) null);

            var userController = new Team_Manager.Controllers.UserController(userService.Object);

            // Act
            var result = userController.AllUsers() as ViewResult;

            // Assert
            Assert.IsNull(result.Model);
        }
    }
}
