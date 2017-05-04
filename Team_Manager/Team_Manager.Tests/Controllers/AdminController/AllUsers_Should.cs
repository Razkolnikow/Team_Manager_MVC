using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Team_Manager.Services.Data.Contracts;
using Team_Manager.Services.Data.ViewModels.AdminViewModels;
using TestStack.FluentMVCTesting;

namespace Team_Manager.Tests.Controllers.AdminController
{
    [TestClass]
    public class AllUsers_Should
    {
        [TestMethod]
        public void ReturnViewWithCollectionOfUserAdminViewModels()
        {
            // Arrange
            var service = new Mock<IAdminService>();
            var adminController = new Areas.Administration.Controllers.AdminController(service.Object);

            // Act & Assert
            adminController.WithCallTo(c => c.AllUsers())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<UserAdminViewModel>>();
        }

        [TestMethod]
        public void ReturnDefaultView()
        {
            // Arrange
            var service = new Mock<IAdminService>();
            var adminController = new Areas.Administration.Controllers.AdminController(service.Object);

            // Act & Assert
            adminController.WithCallTo(c => c.AllUsers())
                .ShouldRenderDefaultView();
        }
    }
}
