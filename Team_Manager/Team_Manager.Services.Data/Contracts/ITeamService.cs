using System.Collections.Generic;
using Team_Manager.Data.Common;
using Team_Manager.Data.Models;
using Team_Manager.Services.Data.BindindModels;
using Team_Manager.Services.Data.ViewModels;

namespace Team_Manager.Services.Data.Contracts
{
    public interface ITeamService
    {
        void CreateTeam(CreateTeamBindModel model, string currentUserId);

        IDbRepository<ApplicationUser> Users { get; }
        IEnumerable<TeamViewModel> GetAllTeamsOfCurrentUser(string currentUserId);
        ShowTeamViewModel GetShowTeamViewModel(int teamId);
        TeamViewModel GetTeamById(int teamId);
        CreateTopicViewModel GetCreateTopicViewModel(int teamId);
        void AddTopic(TopicBindModel model);
        TopicWithCommentsViewModel GetTopicById(int topicId);
        void AddComment(CreateCommentBindModel model);
        IEnumerable<TeamMemberViewModel> GetTeamMates(string currentUserId);
        IEnumerable<TeamMemberViewModel> GetTeamMembers(int teamId);
        IEnumerable<TopicViewModel> GetAllTeamTopics(int teamId);
    }
}
