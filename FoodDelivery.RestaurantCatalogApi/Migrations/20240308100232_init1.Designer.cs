﻿// <auto-generated />
using System;
using System.Collections.Generic;
using FoodDelivery.RestaurantCatalogApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FoodDelivery.RestaurantCatalogApi.Migrations
{
    [DbContext(typeof(RestaurantCatalogContext))]
    [Migration("20240308100232_init1")]
    partial class init1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate.Branch", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<long>("RestaurantId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Branches", "dbo");
                });

            modelBuilder.Entity("FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate.Dish", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("DishTypeId")
                        .HasColumnType("integer");

                    b.Property<List<string>>("Ingredients")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DishTypeId");

                    b.ToTable("Dish", "dbo");
                });

            modelBuilder.Entity("FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate.DishType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DishType", "dbo");
                });

            modelBuilder.Entity("FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAvaibleAgregate.DishAvaible", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("BranchId")
                        .HasColumnType("bigint");

                    b.Property<long>("DishId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsAvaible")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("DishId", "BranchId")
                        .IsUnique();

                    b.ToTable("DishAvaibles", "dbo");
                });

            modelBuilder.Entity("FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.RestaurantAgreagate.Restaurant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Restaurants", "dbo");
                });

            modelBuilder.Entity("FoodDelivery.IntegrationEventLogEF.IntegrationEventLogEntry", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EventTypeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<int>("TimesSent")
                        .HasColumnType("integer");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uuid");

                    b.HasKey("EventId");

                    b.ToTable("IntegrationEventLog", "dbo");
                });

            modelBuilder.Entity("FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate.Branch", b =>
                {
                    b.HasOne("FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.RestaurantAgreagate.Restaurant", "Restaurant")
                        .WithMany("Branches")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsOne("FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate.Address", "Address", b1 =>
                        {
                            b1.Property<long>("BranchId")
                                .HasColumnType("bigint");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Home")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("BranchId");

                            b1.ToTable("Branches", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("BranchId");
                        });

                    b.OwnsOne("FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate.WorkingHours", "WorkingHours", b1 =>
                        {
                            b1.Property<long>("BranchId")
                                .HasColumnType("bigint");

                            b1.Property<TimeOnly>("End")
                                .HasColumnType("time")
                                .HasColumnName("closing_time");

                            b1.Property<TimeOnly>("Start")
                                .HasColumnType("time")
                                .HasColumnName("opening_time");

                            b1.HasKey("BranchId");

                            b1.ToTable("Branches", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("BranchId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("WorkingHours")
                        .IsRequired();
                });

            modelBuilder.Entity("FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate.Dish", b =>
                {
                    b.HasOne("FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate.DishType", "DishType")
                        .WithMany()
                        .HasForeignKey("DishTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate.Price", "Price", b1 =>
                        {
                            b1.Property<long>("DishId")
                                .HasColumnType("bigint");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("Price");

                            b1.HasKey("DishId");

                            b1.ToTable("Dish", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("DishId");
                        });

                    b.OwnsOne("FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate.Weight", "Weight", b1 =>
                        {
                            b1.Property<long>("DishId")
                                .HasColumnType("bigint");

                            b1.Property<long>("Gram")
                                .HasColumnType("bigint")
                                .HasColumnName("Weight");

                            b1.HasKey("DishId");

                            b1.ToTable("Dish", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("DishId");
                        });

                    b.Navigation("DishType");

                    b.Navigation("Price")
                        .IsRequired();

                    b.Navigation("Weight")
                        .IsRequired();
                });

            modelBuilder.Entity("FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAvaibleAgregate.DishAvaible", b =>
                {
                    b.HasOne("FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate.Branch", "Branch")
                        .WithMany("Dishes")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate.Dish", "Dish")
                        .WithMany()
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Dish");
                });

            modelBuilder.Entity("FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate.Branch", b =>
                {
                    b.Navigation("Dishes");
                });

            modelBuilder.Entity("FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.RestaurantAgreagate.Restaurant", b =>
                {
                    b.Navigation("Branches");
                });
#pragma warning restore 612, 618
        }
    }
}
