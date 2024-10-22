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
        public Category type { get; set; }

        [Required]
        public int strength { get; set; }

        [Required]
        public int intelligence { get; set; }

        [Required]
        public int agility { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        public List<Ninja> NinjasForGear { get; set; }
    }
}
