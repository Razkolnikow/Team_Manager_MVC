using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Team_Manager.Services.Data.BindindModels
{
    public class CreateTeamBindModel
    {
        [AllowHtml]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [AllowHtml]
        [StringLength(50)]
        public string Subcategory { get; set; }
    }
}
