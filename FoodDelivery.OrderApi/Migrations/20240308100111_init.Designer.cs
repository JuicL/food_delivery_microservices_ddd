﻿// <auto-generated />
using System;
using FoodDelivery.OrderApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FoodDelivery.OrderApi.Migrations
{
    [DbContext(typeof(OrderingContext))]
    [Migration("20240308100111_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("orderseq")
                .IncrementsBy(10);

            modelBuilder.Entity("FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate.Dishes", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("DishId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("OrderRequestId")
                        .HasColumnType("bigint");

                    b.Property<int>("Units")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderRequestId");

                    b.ToTable("Dishes", "dbo");
                });

            modelBuilder.Entity("FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate.OrderRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"), "orderseq");

                    b.Property<int>("BranchId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OrderRequest", "dbo");
                });

            modelBuilder.Entity("FoodDelivery.OrderApi.Domain.AgregationModels.UserAgregate.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User", "dbo");
                });

            modelBuilder.Entity("FoodDelibery.IntegrationEventLogEF.IntegrationEventLogEntry", b =>
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

            modelBuilder.Entity("FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate.Dishes", b =>
                {
                    b.HasOne("FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate.OrderRequest", null)
                        .WithMany("Dishes")
                        .HasForeignKey("OrderRequestId");

                    b.OwnsOne("FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate.Price", "Price", b1 =>
                        {
                            b1.Property<long>("DishesId")
                                .HasColumnType("bigint");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric");

                            b1.HasKey("DishesId");

                            b1.ToTable("Dishes", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("DishesId");
                        });

                    b.OwnsOne("FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate.Weight", "Weight", b1 =>
                        {
                            b1.Property<long>("DishesId")
                                .HasColumnType("bigint");

                            b1.Property<long>("Grams")
                                .HasColumnType("bigint");

                            b1.HasKey("DishesId");

                            b1.ToTable("Dishes", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("DishesId");
                        });

                    b.Navigation("Price")
                        .IsRequired();

                    b.Navigation("Weight")
                        .IsRequired();
                });

            modelBuilder.Entity("FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate.OrderRequest", b =>
                {
                    b.OwnsOne("FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate.Address", "DeliveryAddress", b1 =>
                        {
                            b1.Property<long>("OrderRequestId")
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

                            b1.HasKey("OrderRequestId");

                            b1.ToTable("OrderRequest", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("OrderRequestId");
                        });

                    b.OwnsOne("FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate.Address", "RestaurantAddress", b1 =>
                        {
                            b1.Property<long>("OrderRequestId")
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

                            b1.HasKey("OrderRequestId");

                            b1.ToTable("OrderRequest", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("OrderRequestId");
                        });

                    b.OwnsOne("FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate.OrderStatus", "OrderStatus", b1 =>
                        {
                            b1.Property<long>("OrderRequestId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .HasColumnType("integer")
                                .HasColumnName("OrderStatusId");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("OrderStatus");

                            b1.HasKey("OrderRequestId");

                            b1.ToTable("OrderRequest", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("OrderRequestId");
                        });

                    b.OwnsOne("FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate.PaymentMethod", "PaymentMethod", b1 =>
                        {
                            b1.Property<long>("OrderRequestId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .HasColumnType("integer")
                                .HasColumnName("PaymentMethodId");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("PaymentMethod");

                            b1.HasKey("OrderRequestId");

                            b1.ToTable("OrderRequest", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("OrderRequestId");
                        });

                    b.Navigation("DeliveryAddress")
                        .IsRequired();

                    b.Navigation("OrderStatus")
                        .IsRequired();

                    b.Navigation("PaymentMethod")
                        .IsRequired();

                    b.Navigation("RestaurantAddress")
                        .IsRequired();
                });

            modelBuilder.Entity("FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate.OrderRequest", b =>
                {
                    b.Navigation("Dishes");
                });
#pragma warning restore 612, 618
        }
    }
}
