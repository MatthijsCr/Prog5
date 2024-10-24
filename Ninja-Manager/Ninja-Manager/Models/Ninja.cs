using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Ninja_Manager.Models
{
    public class Ninja
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        [Required]
        public int Gold { get; set; }

        [Required]
        public List<Gear> GearForNinja { get; set; } = new List<Gear>();

        internal void AddGear(Gear buyGear)
        {
            if(GearForNinja == null) { GearForNinja = new List<Gear>();}
            GearForNinja.Add(buyGear);
        }

        internal List<Gear> getGear()
        {
            return GearForNinja;
        }
    }
}
