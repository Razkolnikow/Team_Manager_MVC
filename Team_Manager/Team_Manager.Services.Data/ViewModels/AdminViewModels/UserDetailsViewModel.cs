using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager.Services.Data.ViewModels.AdminViewModels
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public IEnumerable<TeamAdminViewModel> Teams { get; set; }

        public DateTime RegisteredOn { get; set; }

        public string EmailAdress { get; set; }

    }
}
