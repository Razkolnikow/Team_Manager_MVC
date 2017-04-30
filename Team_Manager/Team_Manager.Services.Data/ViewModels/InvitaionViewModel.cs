using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager.Services.Data.ViewModels
{
    public class InvitaionViewModel
    {
        public int Id { get; set; }

        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
