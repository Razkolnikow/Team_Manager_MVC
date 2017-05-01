using System.Collections.Generic;
using System.Web.Mvc;
using Team_Manager.Controllers;
using Team_Manager.Services.Data.BindindModels.AdminBindingModels;
using Team_Manager.Services.Data.Contracts;
using Team_Manager.Services.Data.ViewModels.AdminViewModels;

namespace Team_Manager.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Admin/{action}")]
    public class AdminController : BaseController
    {
        private IAdminService service;

        public AdminController(IAdminService service)
        {
            this.service = service;
        }

        public ActionResult AdminHome()
        {
            return View();
        }

        public ActionResult AllUsers()
        {
            IEnumerable<UserAdminViewModel> users = this.service.GetAllUsers();
            return this.View(users);
        }

        public ActionResult ShowUserInfo(string userId)
        {
            UserDetailsViewModel model = this.service.GetUserDetailsViewModel(userId);
            if (! this.IsValidObject(model))
            {
                return this.RedirectToAction("AllUsers");
            }

            return this.View(model);
        }
        
        public ActionResult AllTeams()
        {
            IEnumerable<TeamAdminViewModel> teams = this.service.GetAllTeams();
            return this.View(teams);
        }

        public ActionResult ShowTeamInfo(int? teamId)
        {
            if (! this.IsValidObject(teamId))
            {
                return this.RedirectToAction("AllTeams");
            }

            TeamDetailsVIewModel model = this.service.GetTeamDetailsViewModel(teamId.Value);
            if (!this.IsValidObject(model))
            {
                return this.RedirectToAction("AllTeams");
            }

            return this.View(model);
        }

        public ActionResult DeleteTeam(int teamId)
        {
            return this.View(teamId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTeam([Bind(Include = "teamId")] DeleteTeamBindModel model)
        {
            this.service.DeleteTeam(model.TeamId);
            return this.RedirectToAction("AllTeams");
        }
    }
}