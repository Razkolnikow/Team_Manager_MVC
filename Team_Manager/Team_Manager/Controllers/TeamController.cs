using System.Collections.Generic;
using System.Web.Mvc;
using Team_Manager.Services.Data.BindindModels;
using Team_Manager.Services.Data.Contracts;
using Team_Manager.Services.Data.ViewModels;

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

        public ActionResult MyTeamMates()
        {
            IEnumerable<TeamMemberViewModel> myTeamMates = this.service.GetTeamMates(this.CurrentUserId);
            return this.View(myTeamMates);
        }

        
        public ActionResult CreateTeam()
        {
            return this.View();
        }

        public ActionResult CreateTopic(int teamId = 1)
        {
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
                return this.RedirectToAction("CreateTopic", model.TeamId);
            }
        }

        public ActionResult TeamMembers(int? teamId)
        {
            if (!this.IsValidParameter(teamId))
            {
                return this.RedirectToAction("MyTeams");
            }

            ViewBag.TeamId = teamId.Value;
            IEnumerable<TeamMemberViewModel> teamMembers = this.service.GetTeamMembers(teamId.Value);
            if (!this.IsValidParameter(teamMembers))
            {
                return this.RedirectToAction("MyTeams");
            }

            return this.View(teamMembers);
        }

        public ActionResult TeamTopics(int? teamId)
        {
            if (!this.IsValidParameter(teamId))
            {
                return this.RedirectToAction("MyTeams");
            }

            IEnumerable<TopicViewModel> teamTopics = this.service.GetAllTeamTopics(teamId.Value);
            if (!this.IsValidParameter(teamTopics))
            {
                return this.RedirectToAction("MyTeams");
            }

            return this.View(teamTopics);
        }

        public ActionResult ShowTeamTopic(int? topicId)
        {
            if (!this.IsValidParameter(topicId))
            {
                return this.RedirectToAction("MyTeams");
            }

            TopicWithCommentsViewModel model = this.service.GetTopicById(topicId.Value);
            if (!this.IsValidParameter(model))
            {
                return this.RedirectToAction("MyTeams");
            }

            return this.View(model);
        }

        public ActionResult AddComment()
        {
            return this.PartialView("_AddComment");
        }

        public ActionResult ShowTeam(int? teamId)
        {
            if (!this.IsValidParameter(teamId))
            {
                return this.RedirectToAction("MyTeams");
            }

            ShowTeamViewModel teamViewModel = this.service.GetShowTeamViewModel(teamId.Value);
            if (!this.IsValidParameter(teamViewModel))
            {
                return this.RedirectToAction("MyTeams");
            }

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

            return this.View("CreateTeam");
        }
    }
}