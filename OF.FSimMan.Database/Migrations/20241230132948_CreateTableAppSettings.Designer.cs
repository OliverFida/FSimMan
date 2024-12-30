﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OF.FSimMan.Database.Context;

#nullable disable

namespace OF.FSimMan.Database.Migrations
{
    [DbContext(typeof(SettingsDbContext))]
    [Migration("20241230132948_CreateTableAppSettings")]
    partial class CreateTableAppSettings
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("OF.FSimMan.Management.AppSettingsData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ApplicationMode")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastSelectedView")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AppSettings");
                });
#pragma warning restore 612, 618
        }
    }
}
