using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Team_Manager.Data.Common;
using Team_Manager.Data.Models;
using Team_Manager.Services.Data.Contracts;
using Team_Manager.Services.Data.ViewModels;
using Team_Manager.Services.Data.ViewModels.AdminViewModels;

namespace Team_Manager.Services.Data
{
    public class AdminService : BaseDataService<ApplicationUser>, IAdminService
    {
        private IDbRepository<Team> teams;

        public AdminService(IDbRepository<ApplicationUser> dataSet, IDbRepository<Team> teams) 
            : base(dataSet)
        {
            this.teams = teams;
        }

        public IEnumerable<UserAdminViewModel> GetAllUsers()
        {
            var users = this.Data.All().Select(user => new
            {
                user.Id,
                user.UserName
            }).Select(u => new UserAdminViewModel()
            {
                Id = u.Id,
                UserName = u.UserName
            }).ToList();

            return users;
        }

        public IEnumerable<TeamAdminViewModel> GetAllTeams()
        {
            var teamModels = this.teams.All().Select(team => new
            {
                team.Id,
                team.Name
            }).Select(t => new TeamAdminViewModel()
            {
                Id = t.Id,
                TeamName = t.Name
            }).ToList();

            return teamModels;
        }

        public UserDetailsViewModel GetUserDetailsViewModel(string userId)
        {
            var user = this.Data.GetById(userId);
            UserDetailsViewModel model = new UserDetailsViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                EmailAdress = user.Email,
                RegisteredOn = user.CreatedOn,
                Teams = user.MemberTeams.Select(team => new
                {
                    team.Id,
                    team.Name
                }).Select(t => new TeamAdminViewModel()
                {
                    Id = t.Id,
                    TeamName = t.Name
                })
            };

            return model;
        }

        public TeamDetailsVIewModel GetTeamDetailsViewModel(int teamId)
        {
            var team = this.teams.GetById(teamId);
            TeamDetailsVIewModel model = MapTeamDetailsFromTeam(team);
            return model;
        }

        public void DeleteTeam(int modelTeamId)
        {
            var team = this.teams.GetById(modelTeamId);
            this.teams.Delete(team);
            this.Save();
        }

        private TeamDetailsVIewModel MapTeamDetailsFromTeam(Team team)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Team, TeamDetailsVIewModel>()
                .ForMember(dto => dto.CreatedBy, conf => conf.MapFrom(x => x.Creator.UserName))
                .ForMember(dto => dto.Members, conf => conf.MapFrom(x => x.TeamMembers.Select(m => new
                    {
                        Id = m.Id,
                        UserName = m.UserName
                    })
                    .Select(m => new UserAdminViewModel()
                            {
                                Id = m.Id,
                                UserName = m.UserName
                            })));
            });
            var mapper = config.CreateMapper();
            return mapper.Map<TeamDetailsVIewModel>(team);
        }

        
    }
}
