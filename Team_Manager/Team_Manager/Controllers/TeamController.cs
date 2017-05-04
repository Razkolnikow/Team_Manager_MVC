using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Team_Manager.Services.Data.BindindModels;
using Team_Manager.Services.Data.Contracts;
using Team_Manager.Services.Data.ViewModels;
using Team_Manager.Services.Data.ViewModels.AdminViewModels;

namespace Team_Manager.Controllers
{
    [Authorize]
    [RoutePrefix("Team/{action}")]
    public class TeamController : BaseController
    {
        private ITeamService service;

        public TeamController(ITeamService service)
        {
            this.service = service;
        }

        // GET: Team
        public ActionResult MyTeams()
        {
            var myTeams = this.service.GetAllTeamsOfCurrentUser(this.CurrentUserId);
            return View(myTeams);
        }

        [OutputCache(Duration = 60*60, VaryByParam = "*")]
        public ActionResult AllTeams()
        {
            IEnumerable<TeamAdminViewModel> models = this.service.GetAllTeams();
            return this.View(models);
        }

        public ActionResult MyTeamMates()
        {
            IEnumerable<TeamMemberViewModel> myTeamMates = this.service.GetTeamMates(this.CurrentUserId);
            return this.View(myTeamMates);
        }
        
        public ActionResult CreateTeam()
        {
            return this.View();
        }

        public ActionResult CreateTopic(int teamId)
        {
            this.ValidateIfCurrentUserIsMemberOfTeam(teamId);
            CreateTopicViewModel createTopicViewModel = this.service.GetCreateTopicViewModel(teamId);
            createTopicViewModel.AuthorName = User.Identity.Name;
            return this.View(createTopicViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateTopic([Bind(Include = "Title, TeamId, AuthorId")] TopicBindModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddTopic(model);
                // TO DO redirect to my Topics
                return this.RedirectToAction("MyTeams");
            }
            else
            {
                return this.RedirectToAction("CreateTopic",new {teamId = model.TeamId});
            }
        }

        [OutputCache(Duration = 1*60, VaryByParam = "*")]
        public ActionResult TeamMembers(int teamId)
        {
            this.ValidateIfCurrentUserIsMemberOfTeam(teamId);
            ViewBag.TeamId = teamId;
            IEnumerable<TeamMemberViewModel> teamMembers = this.service.GetTeamMembers(teamId);

            return this.View(teamMembers);
        }

        public ActionResult TeamTopics(int teamId)
        {
            this.ValidateIfCurrentUserIsMemberOfTeam(teamId);
            IEnumerable<TopicViewModel> teamTopics = this.service.GetAllTeamTopics(teamId);
            return this.View(teamTopics);
        }

        [OutputCache(Duration = 1*60, VaryByParam = "*")]
        public ActionResult ShowTeamTopic(int topicId)
        {
            TopicWithCommentsViewModel model = this.service.GetTopicById(topicId);
            this.ValidateIfCurrentUserIsMemberOfTeam(model.TeamId);
            return this.View(model);
        }

        public ActionResult AddComment()
        {
            return this.PartialView("_AddComment");
        }

        public ActionResult ShowTeam(int teamId)
        {
            this.ValidateIfCurrentUserIsMemberOfTeam(teamId);

            ShowTeamViewModel teamViewModel = this.service.GetShowTeamViewModel(teamId);
            return this.View(teamViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment([Bind(Include = "Content, AuthorId, TopicId")] CreateCommentBindModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddComment(model);
            }

            return this.RedirectToAction("ShowTeamTopic", new { topicId = model.TopicId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTeam([Bind(Include = "Name, Category, Subcategory")] CreateTeamBindModel model)
        {
            if (ModelState.IsValid)
            {
                this.service.CreateTeam(model, this.CurrentUserId);
                return this.Redirect("/team/myTeams");
            }

            return this.View();
        }

        private bool CheckIfCurrentUserIsMemberOfTeam(int teamId)
        {
            var isMember = this.service.IsCurrentUserMemberOfTeam(teamId, this.CurrentUserId);

            return isMember;
        }

        private void ValidateIfCurrentUserIsMemberOfTeam(int teamId)
        {
            if (!this.CheckIfCurrentUserIsMemberOfTeam(teamId))
            {
                throw new InvalidOperationException();
            }
        }
    }
}