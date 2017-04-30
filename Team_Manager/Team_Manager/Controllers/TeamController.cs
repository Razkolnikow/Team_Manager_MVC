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

        public ActionResult ShowTeamTopic(int topicId = 1)
        {
            TopicWithCommentsViewModel model = this.service.GetTopicById(topicId);
            return this.View(model);
        }

        public ActionResult AddComment()
        {
            return this.PartialView("_AddComment");
        }

        public ActionResult ShowTeam(int teamId = 1)
        {
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

            return this.View("CreateTeam");
        }
    }
}