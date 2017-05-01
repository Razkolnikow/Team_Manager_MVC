using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager.Services.Data.ViewModels.TaskViewModels
{
    public class TeamTaskViewModel
    {
        public int Id { get; set; }

        [Required]
        public string TeamMemberName { get; set; }

        public bool IsAccepted { get; set; }

        public bool IsRejected { get; set; }

        public string RejectionReason { get; set; }
    }
}
