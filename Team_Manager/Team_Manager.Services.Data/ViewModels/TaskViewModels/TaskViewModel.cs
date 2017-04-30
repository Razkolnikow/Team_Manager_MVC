using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager.Services.Data.ViewModels.TaskViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string TeamName { get; set; }

        [Required]
        public DateTime FinalDate { get; set; }

        [Required]
        public string TeamMemberId { get; set; }
    }
}
