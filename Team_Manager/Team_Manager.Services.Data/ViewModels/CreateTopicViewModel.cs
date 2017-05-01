using System;
using System.ComponentModel.DataAnnotations;

namespace Team_Manager.Services.Data.ViewModels
{
    public class CreateTopicViewModel
    {
        public TeamViewModel TeamViewModel { get; set; }

        //[StringLength(200)]
        [Required]
        public string Title { get; set; }
        
        public string AuthorName { get; set; }

        public int TeamId { get; set; }
    }
}
