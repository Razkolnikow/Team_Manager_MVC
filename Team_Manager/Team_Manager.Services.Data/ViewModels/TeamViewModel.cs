using AutoMapper;
using Team_Manager.Data.Models;
using Team_Manager.Data.Models.Enums;

namespace Team_Manager.Services.Data.ViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TeamCategory Category { get; set; }

        public string SubCategory { get; set; }
    }
}
