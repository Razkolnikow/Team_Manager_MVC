using System;
using System.Collections.Generic;
using Team_Manager.Services.Data.ViewModels.AdminViewModels;

namespace Team_Manager.Services.Data.Contracts
{
    public interface IAdminService
    {
        IEnumerable<UserAdminViewModel> GetAllUsers();
        IEnumerable<TeamAdminViewModel> GetAllTeams();
        UserDetailsViewModel GetUserDetailsViewModel(string userId);
        TeamDetailsVIewModel GetTeamDetailsViewModel(int teamId);
        void DeleteTeam(int modelTeamId);
    }
}
