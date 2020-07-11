using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Contexts
{
    public class CityInfoContext : DbContext
    {
        //The DbSet class represents an entity set that can be used for create, read, update, and delete operations.
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
           : base(options)
        {
            // Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                 .HasData(// use to provide data for an entity
                new City()
                {
                    Id = 1,
                    Name = "Lahore City",
                    Description = "The Heart of Pakistan."
                },
                new City()
                {
                    Id = 2,
                    Name = "Karachi",
                    Description = "The City Of Lights."
                },
                new City()
                {
                    Id = 3,
                    Name = "IslamAbad",
                    Description = "The City of Burgers"
                });


            modelBuilder.Entity<PointOfInterest>()
              .HasData(
                new PointOfInterest()
                {
                    Id = 1,
                    CityId = 1,
                    Name = "Jinnah Park",
                    Description = "The most visited urban park in the Lahore City."

                },
                new PointOfInterest()
                {
                    Id = 2,
                    CityId = 1,
                    Name = "Emporium Building",
                    Description = "A 4 story mall which have many shops."
                },
                  new PointOfInterest()
                  {
                      Id = 3,
                      CityId = 2,
                      Name = "Anarkali",
                      Description = "A bazar named Anarkali."
                  },
                new PointOfInterest()
                {
                    Id = 4,
                    CityId = 2,
                    Name = "Railway Station",
                    Description = "The the finest example of railway architecture in Lahore."
                },
                new PointOfInterest()
                {
                    Id = 5,
                    CityId = 3,
                    Name = "Eiffel Tower",
                    Description = "A fake tower in Bahria Town."
                },
                new PointOfInterest()
                {
                    Id = 6,
                    CityId = 3,
                    Name = "The meusuim",
                    Description = "The UET largest museum."
                }
                );

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}

    }
}
