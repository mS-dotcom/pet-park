﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using perpark_api.Models;

#nullable disable

namespace perpark_api.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230818012958_addIsApprovedColumnFromClinic")]
    partial class addIsApprovedColumnFromClinic
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("perpark_api.Models.Entities.Advert", b =>
                {
                    b.Property<int>("AdvertId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdvertId"));

                    b.Property<string>("AdvertDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdvertHeader")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdvertImageLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AnimalId")
                        .HasColumnType("int");

                    b.Property<bool?>("AnimalInfoShare")
                        .HasColumnType("bit");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AdvertId");

                    b.ToTable("Adverts");
                });

            modelBuilder.Entity("perpark_api.Models.Entities.Animal", b =>
                {
                    b.Property<int>("AnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnimalId"));

                    b.Property<int?>("AnimalAge")
                        .HasColumnType("int");

                    b.Property<int>("AnimalTypeId")
                        .HasColumnType("int");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QRCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QRImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("VeterinaryId")
                        .HasColumnType("int");

                    b.HasKey("AnimalId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("perpark_api.Models.Entities.AnimalType", b =>
                {
                    b.Property<int>("AnimalTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnimalTypeId"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AnimalTypeId");

                    b.ToTable("AnimalType");
                });

            modelBuilder.Entity("perpark_api.Models.Entities.AnimalWalker", b =>
                {
                    b.Property<int>("AnimalWalkerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnimalWalkerId"));

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<string>("Lat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lng")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AnimalWalkerId");

                    b.ToTable("AnimalWalkers");
                });

            modelBuilder.Entity("perpark_api.Models.Entities.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CityId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CityId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("perpark_api.Models.Entities.Clinic", b =>
                {
                    b.Property<int>("ClinicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClinicId"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VeterinaryId")
                        .HasColumnType("int");

                    b.HasKey("ClinicId");

                    b.ToTable("Clinics");
                });

            modelBuilder.Entity("perpark_api.Models.Entities.District", b =>
                {
                    b.Property<int?>("DistrictId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("DistrictId"));

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DistrictId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("perpark_api.Models.Entities.Log", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogId"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LogDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LogId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("perpark_api.Models.Entities.PetHotel", b =>
                {
                    b.Property<int>("PetHotelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PetHotelId"));

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<string>("Lat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lng")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PetHotelId");

                    b.ToTable("PetHotels");
                });

            modelBuilder.Entity("perpark_api.Models.Entities.Sickness", b =>
                {
                    b.Property<int>("SicknessId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SicknessId"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SicknessId");

                    b.ToTable("Sickess");
                });

            modelBuilder.Entity("perpark_api.Models.Entities.Vaccine", b =>
                {
                    b.Property<int>("VaccineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VaccineId"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NextDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("VaccineName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VaccineId");

                    b.ToTable("Vaccines");
                });

            modelBuilder.Entity("perpark_api.Models.Entities.Veterinary", b =>
                {
                    b.Property<int>("VeteniaryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VeteniaryId"));

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<string>("Lat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lng")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("VeteniaryId");

                    b.ToTable("Veterinary");
                });

            modelBuilder.Entity("perpark_api.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("perpark_api.Models.UserType", b =>
                {
                    b.Property<int?>("UserTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("UserTypeId"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserTypeId");

                    b.ToTable("UserTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
