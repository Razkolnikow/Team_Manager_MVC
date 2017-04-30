using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager.Services.Data.BindindModels
{
    public class TaskBindModel
    {
        [Required]
        public string Content { get; set; }

        public int TeamId { get; set; }

        [Required]
        public DateTime FinalDate { get; set; }

        [Required]
        public string TeamMemberId { get; set; }
    }
}
