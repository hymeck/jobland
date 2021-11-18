﻿// <auto-generated />
using System;
using Jobland.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Jobland.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Jobland.Models.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Added")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("IconUrl")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Jobland.Models.Subcategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Added")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Subcategories");
                });

            modelBuilder.Entity("Jobland.Models.Work", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Added")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasMaxLength(65535)
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("FinishedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("LowerPriceBound")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("StartedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<long?>("UpperPriceBound")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Works");
                });

            modelBuilder.Entity("Jobland.Models.Subcategory", b =>
                {
                    b.HasOne("Jobland.Models.Category", "Category")
                        .WithMany("Subcategories")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Jobland.Models.Category", b =>
                {
                    b.Navigation("Subcategories");
                });
#pragma warning restore 612, 618
        }
    }
}
