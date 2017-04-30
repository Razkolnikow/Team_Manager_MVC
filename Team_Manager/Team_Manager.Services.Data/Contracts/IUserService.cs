using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Manager.Services.Data.BindindModels;
using Team_Manager.Services.Data.ViewModels;

namespace Team_Manager.Services.Data.Contracts
{
    public interface IUserService
    {
        IEnumerable<TeamMemberViewModel> GetAllUsers();
        TeamManagerUserViewModel FindUserById(string userId);
        CurrentUserTeamsViewModel GetCurrentUserTeams(string currentUserId);
        void SendInvitationToTargetedUser(CreateInvitationBindModel model);
        IEnumerable<InvitaionViewModel> GetAllInvitationsOfCurrentUser(string currentUserId);
        void AcceptInvitation(int invitaionId, string currentUserId);
        void RejectInvitation(int invitaionId, string currentUserId);
    }
}
