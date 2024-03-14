using System;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts

//DbContext - session with DB & can be used to query and save instances of entities
//Migrations - allow to provide code to change the DB from one version to another
//be careful with - .FromSqlRaw method

{
	public class CityInfoContext : DbContext
	{
		public DbSet<City> Cities { get; set; } = null!;
		public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

		public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
		{ 

		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                new City("New Delhi")
                {
                    Id = 1,
                    Description = "Capital of India."
                },
                new City("Jaipur")
                {
                    Id = 2,
                    Description = "Capital of Rajasthan, India."
                },
                new City("Bengaluru")
                {
                    Id = 3,
                    Description = "Silicon valley of India."
                }
            );
            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                new PointOfInterest("Qutub Minar")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "Long Pillar"
                },
                new PointOfInterest("JaiGarh fort")
                {
                    Id = 2,
                    CityId = 2,
                    Description = "Fort in Jaipur with Asia's largest canon."
                },
                new PointOfInterest("Albert hall museum")
                {
                    Id = 3,
                    CityId = 2,
                    Description = "Tourist place in Jaipur."
                },
                new PointOfInterest("Nandi Hills")
                {
                    Id = 4,
                    CityId = 3,
                    Description = "Hill station near Bengaluru."
                },
                new PointOfInterest("KempaGowda International Airport")
                {
                    Id = 5,
                    CityId = 3,
                    Description = "A beatiful airport in India."
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}

