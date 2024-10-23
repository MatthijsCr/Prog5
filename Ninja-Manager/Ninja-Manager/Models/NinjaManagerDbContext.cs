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

        public DbSet<Ninja> Ninjas { get; set; }

        public DbSet<Gear> Gears { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gear>().HasData(
                // Head Gear
                new Gear { Id = 1, Name = "Rusty Helm", Intelligence = 1, Strength = 1, Agility = 0, Cost = 50, Type = Category.Head },
                new Gear { Id = 2, Name = "Iron Helm", Intelligence = 2, Strength = 3, Agility = 1, Cost = 150, Type = Category.Head },
                new Gear { Id = 3, Name = "Crown of Wisdom", Intelligence = 5, Strength = 2, Agility = 0, Cost = 300, Type = Category.Head },

                // Chest Gear
                new Gear { Id = 4, Name = "Leather Armor", Intelligence = 1, Strength = 2, Agility = 1, Cost = 60, Type = Category.Chest },
                new Gear { Id = 5, Name = "Iron Chestplate", Intelligence = 2, Strength = 4, Agility = 1, Cost = 180, Type = Category.Chest },
                new Gear { Id = 6, Name = "Dragonscale Chest", Intelligence = 3, Strength = 6, Agility = 2, Cost = 350, Type = Category.Chest },

                // Hand Gear
                new Gear { Id = 7, Name = "Cloth Gloves", Intelligence = 1, Strength = 1, Agility = 1, Cost = 40, Type = Category.Hand },
                new Gear { Id = 8, Name = "Leather Gloves", Intelligence = 2, Strength = 2, Agility = 2, Cost = 100, Type = Category.Hand },
                new Gear { Id = 9, Name = "Gauntlets of Power", Intelligence = 1, Strength = 5, Agility = 1, Cost = 250, Type = Category.Hand },

                // Feet Gear
                new Gear { Id = 10, Name = "Worn Boots", Intelligence = 0, Strength = 1, Agility = 2, Cost = 50, Type = Category.Feet },
                new Gear { Id = 11, Name = "Iron Boots", Intelligence = 1, Strength = 3, Agility = 2, Cost = 130, Type = Category.Feet },
                new Gear { Id = 12, Name = "Boots of Swiftness", Intelligence = 0, Strength = 2, Agility = 6, Cost = 280, Type = Category.Feet },

                // Ring Gear
                new Gear { Id = 13, Name = "Copper Ring", Intelligence = 1, Strength = 1, Agility = 1, Cost = 30, Type = Category.Ring },
                new Gear { Id = 14, Name = "Silver Ring", Intelligence = 2, Strength = 1, Agility = 2, Cost = 120, Type = Category.Ring },
                new Gear { Id = 15, Name = "Ring of the Ancients", Intelligence = 4, Strength = 0, Agility = 4, Cost = 270, Type = Category.Ring },

                // Necklace Gear
                new Gear { Id = 16, Name = "Wooden Necklace", Intelligence = 1, Strength = 0, Agility = 1, Cost = 40, Type = Category.Necklace },
                new Gear { Id = 17, Name = "Silver Pendant", Intelligence = 3, Strength = 1, Agility = 2, Cost = 140, Type = Category.Necklace },
                new Gear { Id = 18, Name = "Amulet of the Phoenix", Intelligence = 5, Strength = 0, Agility = 3, Cost = 320, Type = Category.Necklace }
            );
        }
    }
}
