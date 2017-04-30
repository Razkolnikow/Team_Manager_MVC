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
        void AssignTaskToUser(TaskBindModel model);
        IEnumerable<TaskViewModel> GetMyTasks(string currentUserId);
        TaskViewModel GetTaskById(int taskId);
    }
}
