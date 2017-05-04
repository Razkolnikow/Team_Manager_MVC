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
    public class AllTeams_Should
    {
        [TestMethod]
        public void ReturnDefaultView()
        {
            // Arrange
            var service = new Mock<IAdminService>();
            var adminController = new Areas.Administration.Controllers.AdminController(service.Object);

            // Act & Assert
            adminController.WithCallTo(c => c.AllTeams())
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void ReturnDefaultViewWithCollectionOfTeamAdminViewModels()
        {
            // Arrange
            var service = new Mock<IAdminService>();
            var adminController = new Areas.Administration.Controllers.AdminController(service.Object);

            // Act & Assert
            adminController.WithCallTo(c => c.AllTeams())
                .ShouldRenderDefaultView().WithModel<IEnumerable<TeamAdminViewModel>>();
        }
    }
}
