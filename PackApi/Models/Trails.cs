using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PackApi.Models
{
    public class Trails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public enum DifficultyType { Easy, Moderate, DIfficult, Expert}
        public DifficultyType Difficulty { get; set; }
        [Required]
        public int NationalParkID { get; set; }
        [ForeignKey("NationalParkID")]
        public string NationalPark { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
