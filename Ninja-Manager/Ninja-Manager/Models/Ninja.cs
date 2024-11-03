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
        public List<Gear> GearForNinja { get; set; }

        public int GetStrength()
        {
            int strength = 0;

            foreach (Gear g in GearForNinja)
            {
                strength += g.Strength;
            }

            return strength;
        }
        
        public int GetIntelligence()
        {
            int intelligence = 0;

            foreach (Gear g in GearForNinja)
            {
                intelligence += g.Intelligence;
            }

            return intelligence;
        }       
        
        public int GetAgility()
        {
            int agility = 0;

            foreach (Gear g in GearForNinja)
            {
                agility += g.Agility;
            }

            return agility;
        }
    }
}
