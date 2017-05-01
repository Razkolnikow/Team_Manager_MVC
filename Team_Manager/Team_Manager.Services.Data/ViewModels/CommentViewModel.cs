using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager.Services.Data.ViewModels
{
    public class CommentViewModel
    {
        [Required]
        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
