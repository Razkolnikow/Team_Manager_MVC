using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager.Services.Data.ViewModels
{
    public class ShowTeamViewModel
    {
        public TeamViewModel BasicTeamInfo { get; set; }

        public IEnumerable<TeamMemberViewModel> TeamMembers { get; set; }

        public IEnumerable<TopicViewModel> Topics { get; set; }

        public TeamMemberViewModel Creator { get; set; }
    }
}
