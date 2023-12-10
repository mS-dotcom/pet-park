using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.EntityFrameworkCore;
using perpark_api.Models.Entities;

namespace perpark_api.Models
{
	public class Context: Microsoft.EntityFrameworkCore.DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("");
        }
        public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<City> Cities { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UserCity> UserCities { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<District> Districts { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UserDistrict> UserDistricts { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Animal> Animals { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<AnimalType> AnimalType { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UserType> UserTypes { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<AnimalWalker> AnimalWalkers { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<PetHotel> PetHotels { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Veterinary> Veterinary { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Sickness> Sickess { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Vaccine> Vaccines { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Log> Logs { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Advert> Adverts { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Clinic> Clinics { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Comment> Comments { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Post> Posts { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Like> Likes { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UserFollowers> UserFollowers { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<ChatMessages> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }
    }
}