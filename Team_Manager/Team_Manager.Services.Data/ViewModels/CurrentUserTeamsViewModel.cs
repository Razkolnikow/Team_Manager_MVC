using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager.Services.Data.ViewModels
{
    public class CurrentUserTeamsViewModel
    {
        public string TargetedUserId { get; set; }

        public string UserName { get; set; }

        public IEnumerable<SimpleTeamViewModel> Teams { get; set; }
    }
}
