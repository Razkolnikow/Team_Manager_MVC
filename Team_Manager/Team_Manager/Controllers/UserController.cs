using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Team_Manager.Services.Data.BindindModels;
using Team_Manager.Services.Data.Contracts;
using Team_Manager.Services.Data.ViewModels;

namespace Team_Manager.Controllers
{
    [Authorize]
    [RoutePrefix("User")]
    public class UserController : BaseController
    {
        private IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        [Route("MyProfile")]
        public ActionResult MyProfile()
        {
            return this.RedirectToAction("UserInfo", new { userId = this.CurrentUserId });
        }

        public ActionResult InviteToTeam(string userId)
        {
            CurrentUserTeamsViewModel currentUserTeams = this.service.GetCurrentUserTeams(this.CurrentUserId);
            currentUserTeams.TargetedUserId = userId;
            return this.PartialView("_InviteToTeam", currentUserTeams);
        }

        [Route("UserInfo")]
        public ActionResult UserInfo(string userId)
        {
            var user = this.service.FindUserById(userId);
            return View(user);
        }

        public ActionResult AllUsers()
        {
            var users = this.service.GetAllUsers();
            return View(users);
        }

        public ActionResult MyInvitations()
        {
            IEnumerable<InvitaionViewModel> allInvitations = this.service
                .GetAllInvitationsOfCurrentUser(this.CurrentUserId);
            return this.View(allInvitations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendInvitation([Bind(Include = "TargetedUserId, TeamId, SenderId")] CreateInvitationBindModel model)
        {
            this.service.SendInvitationToTargetedUser(model);
            return this.RedirectToAction("AllUsers");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptInvitation(int invitationId)
        {
            this.service.AcceptInvitation(invitationId, this.CurrentUserId);
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RejectInvitation(string invitationId)
        {
            this.service.RejectInvitation(int.Parse(invitationId), this.CurrentUserId);
            return this.View();
        }

        public ActionResult Testing()
        {
            return this.View("RejectInvitation");
        }
    }
}