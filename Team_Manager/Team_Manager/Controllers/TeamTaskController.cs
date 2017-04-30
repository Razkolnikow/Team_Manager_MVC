using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team_Manager.Services.Data.BindindModels;
using Team_Manager.Services.Data.Contracts;
using Team_Manager.Services.Data.ViewModels.TaskViewModels;

namespace Team_Manager.Controllers
{
    [Authorize]
    [Route("TeamTask/{action}")]
    public class TeamTaskController : BaseController
    {
        private ITeamTaskService service;

        public TeamTaskController(ITeamTaskService service)
        {
            this.service = service;
        }
        public ActionResult AssignTask(string userId, int? teamId)
        {
            ViewBag.IdTeam = teamId;
            var taskModel = new TaskViewModel() {TeamMemberId = userId };
            return View(taskModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignTask([Bind(Include = "Content, FinalDate, TeamMemberId, TeamId")] TaskBindModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AssignTaskToUser(model);
            }

            return this.Redirect("/team/myteams");
        }

        public ActionResult MyTasks()
        {
            var taskModels = this.service.GetMyTasks(this.CurrentUserId);
            return this.View(taskModels);
        }

        public ActionResult ShowTask(int taskId)
        {
            TaskViewModel taskModel = this.service.GetTaskById(taskId);
            return this.View(taskModel);
        }
    }
}