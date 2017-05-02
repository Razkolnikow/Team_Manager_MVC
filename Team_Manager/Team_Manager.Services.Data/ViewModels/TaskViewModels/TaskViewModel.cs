using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Team_Manager.Services.Data.ViewModels.TaskViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The content field is required and can not be longer than 200 characters!")]
        [StringLength(200)]
        [RegularExpression(@"[^<>]+", ErrorMessage = "Html tag symbols are not allowed!")]
        public string Content { get; set; }

        public string TeamName { get; set; }

        [Required]
        public DateTime FinalDate { get; set; }

        [Required]
        public string TeamMemberId { get; set; }

        public bool IsAccepted { get; set; }
    }
}
