using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Team_Manager.Data.Common;
using Team_Manager.Data.Models;
using Team_Manager.Services.Data.BindindModels;
using Team_Manager.Services.Data.Contracts;
using Team_Manager.Services.Data.ViewModels;
using Team_Manager.Services.Data.ViewModels.TaskViewModels;

namespace Team_Manager.Services.Data
{
    public class TeamTaskService : BaseDataService<TeamTask>, ITeamTaskService
    {
        private IDbRepository<ApplicationUser> users;
        private IDbRepository<Team> teams;

        public TeamTaskService(IDbRepository<TeamTask> dataSet, 
            IDbRepository<ApplicationUser> users, IDbRepository<Team> teams) 
            : base(dataSet)
        {
            this.users = users;
            this.teams = teams;
        }

        public void AssignTaskToUser(TaskBindModel model)
        {
            var user = this.users.GetById(model.TeamMemberId);
            var team = this.teams.GetById(model.TeamId);
            TeamTask task = new TeamTask()
            {
                Content = model.Content,
                FinalDate = model.FinalDate,
                TeamMember = user,
                Team = team
            };

            this.Data.Add(task);
            user.TeamTasks.Add(task);
            team.TeamTasks.Add(task);
            this.Data.Save();
        }

        public IEnumerable<TaskViewModel> GetMyTasks(string currentUserId)
        {
            var currentUser = this.users.GetById(currentUserId);
            return currentUser.TeamTasks.Select(MapTaskViewModelFromTeamTask).ToList();
        }

        public TaskViewModel GetTaskById(int taskId)
        {
            return MapTaskViewModelFromTeamTask(this.Data.GetById(taskId));
        }

        public IEnumerable<TeamTaskViewModel> GetAllTeamTasks(int teamId)
        {
            var team = this.teams.GetById(teamId);
            IEnumerable<TeamTaskViewModel> teamTasks = team.TeamTasks.Select(t => new TeamTaskViewModel()
            {
                Id = t.Id,
                TeamMemberName = t.TeamMember.UserName
            });

            return teamTasks;
        }

        private TaskViewModel MapTaskViewModelFromTeamTask(TeamTask task)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TeamTask, TaskViewModel>()
                .ForMember(dto => dto.TeamMemberId, conf => conf.MapFrom(x => x.TeamMember.Id))
                .ForMember(dto => dto.TeamName, conf => conf.MapFrom(x => x.Team.Name));
            });
            var mapper = config.CreateMapper();

            return mapper.Map<TaskViewModel>(task);
        }

        
    }
}
