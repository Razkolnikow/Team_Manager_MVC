using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Manager.Services.Data.BindindModels;
using Team_Manager.Services.Data.ViewModels.TaskViewModels;

namespace Team_Manager.Services.Data.Contracts
{
    public interface ITeamTaskService
    {
        void AssignTaskToUser(CreateTaskBindModel model);
        IEnumerable<TaskViewModel> GetMyTasks(string currentUserId);
        TaskViewModel GetTaskById(int taskId);
        IEnumerable<TeamTaskViewModel> GetAllTeamTasks(int teamId);
        void AcceptTask(int taskId);
        void RejectTask(RejectTaskBindModel model);
        string GetTeamCreatorId(int teamId);
        int DeleteTask(int taskId);
        bool IsCurrentUserMemberOfTeam(int teamId, string currentUserId);
        bool CheckIfCurrentUserIsMemberOfTeamTask(int taskId, string currentUserId);
        bool IsTaskAssignedToCurrentUser(int taskId, string currentUserId);
    }
}
