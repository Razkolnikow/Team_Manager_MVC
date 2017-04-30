using System.ComponentModel.DataAnnotations;

namespace Team_Manager.Services.Data.BindindModels
{
    public class CreateTeamBindModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        
        [StringLength(50)]
        public string Subcategory { get; set; }
    }
}
