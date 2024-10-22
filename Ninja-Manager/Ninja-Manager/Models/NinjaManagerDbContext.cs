using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

namespace Ninja_Manager.Models
{
    public class NinjaManagerDbContext : DbContext
    {
        public NinjaManagerDbContext()
        {
        }

        public NinjaManagerDbContext(DbContextOptions<NinjaManagerDbContext> options)
            : base(options)
        {
        }

        private IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseSqlServer(Configuration.GetConnectionString("NinjaManagerDb"));

        DbSet<Ninja> Ninjas { get; set; }

        DbSet<Gear> Gears { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gear>().HasData(
                // Head Gear
                new Gear { Id = 1, Name = "Rusty Helm", intelligence = 1, strength = 1, agility = 0, Cost = 50, type = Category.Head },
                new Gear { Id = 2, Name = "Iron Helm", intelligence = 2, strength = 3, agility = 1, Cost = 150, type = Category.Head },
                new Gear { Id = 3, Name = "Crown of Wisdom", intelligence = 5, strength = 2, agility = 0, Cost = 300, type = Category.Head },

                // Chest Gear
                new Gear { Id = 4, Name = "Leather Armor", intelligence = 1, strength = 2, agility = 1, Cost = 60, type = Category.Chest },
                new Gear { Id = 5, Name = "Iron Chestplate", intelligence = 2, strength = 4, agility = 1, Cost = 180, type = Category.Chest },
                new Gear { Id = 6, Name = "Dragonscale Chest", intelligence = 3, strength = 6, agility = 2, Cost = 350, type = Category.Chest },

                // Hand Gear
                new Gear { Id = 7, Name = "Cloth Gloves", intelligence = 1, strength = 1, agility = 1, Cost = 40, type = Category.Hand },
                new Gear { Id = 8, Name = "Leather Gloves", intelligence = 2, strength = 2, agility = 2, Cost = 100, type = Category.Hand },
                new Gear { Id = 9, Name = "Gauntlets of Power", intelligence = 1, strength = 5, agility = 1, Cost = 250, type = Category.Hand },

                // Feet Gear
                new Gear { Id = 10, Name = "Worn Boots", intelligence = 0, strength = 1, agility = 2, Cost = 50, type = Category.Feet },
                new Gear { Id = 11, Name = "Iron Boots", intelligence = 1, strength = 3, agility = 2, Cost = 130, type = Category.Feet },
                new Gear { Id = 12, Name = "Boots of Swiftness", intelligence = 0, strength = 2, agility = 6, Cost = 280, type = Category.Feet },

                // Ring Gear
                new Gear { Id = 13, Name = "Copper Ring", intelligence = 1, strength = 1, agility = 1, Cost = 30, type = Category.Ring },
                new Gear { Id = 14, Name = "Silver Ring", intelligence = 2, strength = 1, agility = 2, Cost = 120, type = Category.Ring },
                new Gear { Id = 15, Name = "Ring of the Ancients", intelligence = 4, strength = 0, agility = 4, Cost = 270, type = Category.Ring },

                // Necklace Gear
                new Gear { Id = 16, Name = "Wooden Necklace", intelligence = 1, strength = 0, agility = 1, Cost = 40, type = Category.Necklace },
                new Gear { Id = 17, Name = "Silver Pendant", intelligence = 3, strength = 1, agility = 2, Cost = 140, type = Category.Necklace },
                new Gear { Id = 18, Name = "Amulet of the Phoenix", intelligence = 5, strength = 0, agility = 3, Cost = 320, type = Category.Necklace }
            );
        }
    }
}
