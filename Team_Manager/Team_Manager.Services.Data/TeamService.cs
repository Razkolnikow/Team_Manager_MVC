using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Team_Manager.Data.Common;
using Team_Manager.Data.Models;
using Team_Manager.Data.Models.Enums;
using Team_Manager.Services.Data.BindindModels;
using Team_Manager.Services.Data.Contracts;
using Team_Manager.Services.Data.ViewModels;
using Team_Manager.Services.Data.ViewModels.AdminViewModels;

namespace Team_Manager.Services.Data
{
    public class TeamService : BaseDataService<Team>, ITeamService
    {
        private IDbRepository<Topic> topics;
        private IDbRepository<ApplicationUser> users;

        public TeamService(IDbRepository<Team> dataSet, IDbRepository<ApplicationUser> users, IDbRepository<Topic> topics) 
            : base(dataSet)
        {
            this.users = users;
            this.topics = topics;
        }

        public void CreateTeam(CreateTeamBindModel model, string currentUserId)
        {

            var creator = this.GetCurrentUser(currentUserId);
            Team team = new Team()
            {
                Name = model.Name,
                Category = (TeamCategory)Enum.Parse(typeof(TeamCategory), model.Category),
                Subcategory = model.Subcategory,
                Creator = creator
            };

            team.TeamMembers.Add(creator);
            creator.CreatedTeams.Add(team);
            creator.MemberTeams.Add(team);
            this.Data.Add(team);
            this.Data.Save();
        }

        public IEnumerable<TeamViewModel> GetAllTeamsOfCurrentUser(string currentUserId)
        {
            var currentUser = this.GetCurrentUser(currentUserId);
            var teams = currentUser.MemberTeams.Where(t => !t.IsDeleted).Select(MapTeamViewModelFromTeam).ToList();
            return teams;
        }

        public ShowTeamViewModel GetShowTeamViewModel(int teamId)
        {
            var team = this.Data.GetById(teamId);
            if (team == null)
            {
                return null;
            }

            IEnumerable<TeamMemberViewModel> teamMembers = team.TeamMembers
                .Select(MapTeamMemberViewModelToUser).ToList();

            IEnumerable<TopicViewModel> topics = team.Topics.Select(MapTopicViewModelFromTopic).ToList();
            TeamMemberViewModel teamCreator = new TeamMemberViewModel() {UserName = team.Creator.UserName};
            TeamViewModel teamInfoBasic = MapTeamViewModelFromTeam(team);
            ShowTeamViewModel showTeamModel = new ShowTeamViewModel()
            {
                BasicTeamInfo = teamInfoBasic,
                Creator = teamCreator,
                TeamMembers = teamMembers,
                Topics = topics
            };

            return showTeamModel;
        }

        public TeamViewModel GetTeamById(int teamId)
        {
            var teamModel = this.Data.GetById(teamId);
            if (teamModel == null)
            {
                return null;
            }

            return MapTeamViewModelFromTeam(teamModel);
        }

        public CreateTopicViewModel GetCreateTopicViewModel(int teamId)
        {
            var teamModel = this.GetTeamById(teamId);
            var createTopicViewModel = new CreateTopicViewModel()
            {
                TeamViewModel = teamModel,
                TeamId = teamId
            };

            return createTopicViewModel;
        }

        public void AddTopic(TopicBindModel model)
        {
            var currentUser = this.GetCurrentUser(model.AuthorId);
            Topic topic = new Topic()
            {
                Title = model.Title,
                Author = currentUser,
                Team = this.Data.GetById(model.TeamId)
            };

            this.topics.Add(topic);
            currentUser.Topics.Add(topic);
            this.topics.Save();
        }

        public TopicWithCommentsViewModel GetTopicById(int topicId)
        {
            var topic = this.topics.GetById(topicId);
            if (topic == null)
            {
                return null;
            }

            return MapTopicWithCommentsViewModelFromTopic(topic);
        }

        public void AddComment(CreateCommentBindModel model)
        {
            var topic = this.topics.GetById(model.TopicId);

            var comment = new Comment()
            {
                Content = model.Content,
                Author = this.GetCurrentUser(model.AuthorId),
                Topic = topic
            };

            topic.Comments.Add(comment);
            this.topics.Save();
        }

        public IEnumerable<TeamMemberViewModel> GetTeamMembers(int teamId)
        {
            var team = this.Data.GetById(teamId);

            IEnumerable<TeamMemberViewModel> teamMembers = 
                team?.TeamMembers
                    .Select(this.MapTeamMemberViewModelToUser)
                    .ToList();
            return teamMembers;
        }

        public IEnumerable<TopicViewModel> GetAllTeamTopics(int teamId)
        {
            var team = this.Data.GetById(teamId);
            IEnumerable<TopicViewModel> topics = 
                team?
                .Topics
                .Select(this.MapTopicViewModelFromTopic)
                .ToList();
            return topics;
        }

        public IEnumerable<TeamAdminViewModel> GetAllTeams()
        {
            return this.Data.All().Select(team => new
            {
                team.Id,
                team.Name
            }).Select(t => new TeamAdminViewModel()
            {
                Id = t.Id,
                TeamName = t.Name
            }).ToList();
        }

        public bool IsCurrentUserMemberOfTeam(int teamId, string currentUserId)
        {
            var team = this.Data.GetById(teamId);
            if (team.TeamMembers.Any(u => u.Id == currentUserId))
            {
                return true;
            }

            return false;
        }

        private TopicWithCommentsViewModel MapTopicWithCommentsViewModelFromTopic(Topic topic)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Comment, CommentViewModel>()
                .ForMember(dto => dto.Author, conf => conf.MapFrom(x => x.Author.UserName))
                .ForMember(dto => dto.Content, conf => conf.MapFrom(x => x.Content))
                .ForMember(dto => dto.CreatedOn, conf => conf.MapFrom(x => x.CreatedOn));
            });
            var mapper = config.CreateMapper();
            IEnumerable<CommentViewModel> commentModels = topic.Comments.Select(mapper.Map<CommentViewModel>);

            var topicModel = new TopicWithCommentsViewModel()
            {
                Id = topic.Id,
                Title = topic.Title,
                Comments = commentModels,
                TeamId = topic.Team.Id
            };

            return topicModel;
        }

        private ApplicationUser GetCurrentUser(string currentUserId)
        {
            return this.users.GetById(currentUserId);
        }


        private TeamViewModel MapTeamViewModelFromTeam(Team team)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Team, TeamViewModel>();

            });
            var mapper = config.CreateMapper();
            return mapper.Map<TeamViewModel>(team);
        }

        private TopicViewModel MapTopicViewModelFromTopic(Topic topic)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Topic, TopicViewModel>();

            });
            var mapper = config.CreateMapper();
            return mapper.Map<TopicViewModel>(topic);
        }

        public IEnumerable<TeamMemberViewModel> GetTeamMates(string currentUserId)
        {
            var currentUser = this.users.GetById(currentUserId);
            var initialMembers = currentUser.MemberTeams
                .Where(team => !team.IsDeleted)
                .SelectMany(t => t.TeamMembers)
                .Distinct().ToList();
            initialMembers.Remove(currentUser);
            IEnumerable<TeamMemberViewModel> teamMembers = initialMembers
                .Select(this.MapTeamMemberViewModelToUser)
                .ToList();

            return teamMembers;
        }

       
    }
}
