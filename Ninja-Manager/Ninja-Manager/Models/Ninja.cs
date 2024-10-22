using System.ComponentModel.DataAnnotations;

namespace Ninja_Manager.Models
{
    public class Ninja
    {
        [Key]
        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        [Required]
        public int Gold { get; set; }
    }
}
