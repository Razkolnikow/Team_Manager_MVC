using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Mappers;
using Team_Manager.Data.Common;
using Team_Manager.Data.Models;
using Team_Manager.Services.Data.BindindModels;
using Team_Manager.Services.Data.Contracts;
using Team_Manager.Services.Data.ViewModels;

namespace Team_Manager.Services.Data
{
    public class UserService : BaseDataService<ApplicationUser>, IUserService
    {
        private IDbRepository<Invitation> invitations;

        public UserService(IDbRepository<ApplicationUser> dataSet, IDbRepository<Invitation> invitations) 
            : base(dataSet)
        {
            this.invitations = invitations;
        }

        public IEnumerable<TeamMemberViewModel> GetAllUsers()
        {
            var allUsers = this.Data.All()
                .Select(MapTeamMemberViewModelToUser)
                .ToList();
            return allUsers;
        }

        public TeamManagerUserViewModel FindUserById(string userId)
        {
            var user = this.Data.GetById(userId);
            TeamManagerUserViewModel model = new TeamManagerUserViewModel()
            {
                Id = user.Id,
                TeamNames = user.MemberTeams
                .Where(t => !t.IsDeleted)
                .Select(t => t.Name)
                .ToArray(),
                UserName = user.UserName
            };

            return model;
        }

        public CurrentUserTeamsViewModel GetCurrentUserTeams(string currentUserId)
        {
            var currentUser = this.Data.GetById(currentUserId);
            IEnumerable<SimpleTeamViewModel> currentUserTeams = currentUser
                .MemberTeams
                .Where(t => !t.IsDeleted)
                .Select(MapSimpleTeamViewModelFromTeam)
                .ToList();

            CurrentUserTeamsViewModel model = new CurrentUserTeamsViewModel()
            {
                Teams = currentUserTeams,
                UserName = currentUser.UserName
            };

            return model;
        }

        private SimpleTeamViewModel MapSimpleTeamViewModelFromTeam(Team team)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Team, SimpleTeamViewModel>();

            });
            var mapper = config.CreateMapper();
            return mapper.Map<SimpleTeamViewModel>(team);
        }

        public void SendInvitationToTargetedUser(CreateInvitationBindModel model)
        {
            var targetedUser = this.Data.GetById(model.TargetedUserId);
            var sender = this.Data.GetById(model.SenderId);
            var team = sender.MemberTeams.Where(t => !t.IsDeleted).FirstOrDefault(t => t.Id == model.TeamId);

            Invitation invitation = new Invitation()
            {
                User = targetedUser,
                Team = team,
            };

            targetedUser.Invitations.Add(invitation);
            this.invitations.Add(invitation);
            this.invitations.Save();
        }

        public IEnumerable<InvitaionViewModel> GetAllInvitationsOfCurrentUser(string currentUserId)
        {
            var currentUser = this.Data.GetById(currentUserId);
            IEnumerable<InvitaionViewModel> models = 
                currentUser.Invitations
                .Select(MapInvitaionViewModelFromInvitation)
                .ToList();

            return models;
        }

        public void AcceptInvitation(int invitaionId, string currentUserId)
        {
            var invitation = this.invitations.GetById(invitaionId);
            var currentUser = this.Data.GetById(currentUserId);
            var team = invitation.Team;
            currentUser.MemberTeams.Add(team);
            team.TeamMembers.Add(currentUser);
            currentUser.Invitations.Remove(invitation);
            this.invitations.HardDelete(invitation);
            this.invitations.Save();
        }

        public void RejectInvitation(int invitaionId, string currentUserId)
        {
            var currentUser = this.Data.GetById(currentUserId);
            var invitation = this.invitations.GetById(invitaionId);
            currentUser.Invitations.Remove(invitation);
            this.invitations.HardDelete(invitation);
            this.invitations.Save();
        }

        private InvitaionViewModel MapInvitaionViewModelFromInvitation(Invitation invitation)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Invitation, InvitaionViewModel>()
                .ForMember(dto => dto.TeamName, conf => conf.MapFrom(x => x.Team.Name))
                .ForMember(dto => dto.TeamId, conf => conf.MapFrom(x => x.Team.Id));
            });
            var mapper = config.CreateMapper();
            return mapper.Map<InvitaionViewModel>(invitation);
        }

        
    }
}

