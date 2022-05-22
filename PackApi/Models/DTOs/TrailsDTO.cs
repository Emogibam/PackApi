using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PackApi.Models.Trails;

namespace PackApi.Models.DTOs
{
    public class TrailsDTO
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public DifficultyType Difficulty { get; set; }
        [Required]
        public int NationalParkID { get; set; }
       public string NationalParkDTO { get; set; }
    }
}
