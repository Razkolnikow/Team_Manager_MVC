using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager.Services.Data.ViewModels.AdminViewModels
{
    public class TeamDetailsVIewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<UserAdminViewModel> Members { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
    }
}
