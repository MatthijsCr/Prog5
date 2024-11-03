using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Ninja_Manager.Models
{
    public class Gear
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Category Type { get; set; }

        [Required]
        public int Strength { get; set; }

        [Required]
        public int Intelligence { get; set; }

        [Required]
        public int Agility { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        public List<Ninja> NinjasForGear { get; set; } = new List<Ninja>();
    }
}
