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
            ModelState.AddModelError("Content", "The content can not be longer than 200 characters.");
            var taskModel = new TaskViewModel() {TeamMemberId = userId };
            return View(taskModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignTask([Bind(Include = "Content, FinalDate, TeamMemberId, TeamId")] CreateTaskBindModel model)
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
        
        public ActionResult AcceptTask(int taskId)
        {
            
            return this.View(taskId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptTask([Bind(Include = "TaskId")] AcceptTaskBindModel model)
        {
            this.service.AcceptTask(model.TaskId);
            return this.RedirectToAction("MyTasks");
        }

        public ActionResult RejectTask(int taskId)
        {
            return this.View(taskId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RejectTask([Bind(Include = "TaskId, RejectionReason")] RejectTaskBindModel model)
        {
            this.service.RejectTask(model);
            return this.RedirectToAction("MyTasks");
        }

        public ActionResult ShowTask(int taskId)
        {
            TaskViewModel taskModel = this.service.GetTaskById(taskId);
            return this.View(taskModel);
        }

        public ActionResult ShowTaskContent(string content)
        {
            return this.PartialView("_ShowTaskContent", content);
        }

        public ActionResult AllTasksOfTeam(int teamId)
        {
            IEnumerable<TeamTaskViewModel> teamTasks = this.service.GetAllTeamTasks(teamId);
            return this.View(teamTasks);
        }
    }
}