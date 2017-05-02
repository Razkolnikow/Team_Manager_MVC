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
        public ActionResult AssignTask(string userId, int teamId)
        {
            this.ValidateIfCurrentUserIsMemberOfTeam(teamId);
            ViewBag.IdTeam = teamId;
            //ModelState.AddModelError("Content", "The content can not be longer than 200 characters.");
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
            this.ValidateIfTaskIsAssignedToCurrentUser(taskId);
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
            this.ValidateIfTaskIsAssignedToCurrentUser(taskId);
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
            this.ValidateTaskRightsOfCurrentUser(taskId);
            TaskViewModel taskModel = this.service.GetTaskById(taskId);

            return this.View(taskModel);
        }

        public ActionResult ShowTaskContent(string content)
        {
            return this.PartialView("_ShowTaskContent", content);
        }

        public ActionResult AllTasksOfTeam(int teamId)
        {
            this.ValidateIfCurrentUserIsMemberOfTeam(teamId);
            IEnumerable<TeamTaskViewModel> teamTasks = this.service.GetAllTeamTasks(teamId);
            string creatorId = this.service.GetTeamCreatorId(teamId);
            ViewBag.CreatorId = creatorId;
            ViewBag.CurrentUserId = this.CurrentUserId;
            return this.View(teamTasks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTask(int taskId)
        {
            int teamId = this.service.DeleteTask(taskId);
            return this.RedirectToAction("AllTasksOfTeam", new {teamId = teamId});
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

        private bool IsCurrentUserMemberOfTeamTask(int taskId)
        {
            var isMemberOfTeamTask = this.service.CheckIfCurrentUserIsMemberOfTeamTask(taskId, this.CurrentUserId);
            return isMemberOfTeamTask;
        }

        private void ValidateTaskRightsOfCurrentUser(int taskId)
        {
            if (! this.IsCurrentUserMemberOfTeamTask(taskId))
            {
                throw new InvalidOperationException();
            }            
        }

        private bool IsTaskAssignedToCurrentUser(int taskId)
        {
            return this.service.IsTaskAssignedToCurrentUser(taskId, this.CurrentUserId);
        }

        private void ValidateIfTaskIsAssignedToCurrentUser(int taskId)
        {
            if (!this.IsTaskAssignedToCurrentUser(taskId))
            {
                throw new InvalidOperationException();
            }
        }
    }
}