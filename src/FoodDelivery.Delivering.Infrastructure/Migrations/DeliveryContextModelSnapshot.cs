﻿// <auto-generated />
using System;
using FoodDelivery.Delivering.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FoodDelivery.Delivering.Infrastructure.Migrations
{
    [DbContext(typeof(DeliveryContext))]
    partial class DeliveryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("deliveryseq")
                .IncrementsBy(10);

            modelBuilder.Entity("FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate.AssignDelivery", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"), "deliveryseq");

                    b.Property<DateTime>("AssignDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("CourierId")
                        .HasColumnType("bigint");

                    b.Property<long>("DeliveryId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CourierId");

                    b.HasIndex("DeliveryId");

                    b.ToTable("AssignDeliveries", "dbo");
                });

            modelBuilder.Entity("FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate.Delivery", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"), "deliveryseq");

                    b.Property<long?>("CourierId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeliveredAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("Lateness")
                        .HasColumnType("bigint");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<long>("RecipientId")
                        .HasColumnType("bigint");

                    b.Property<string>("RecipientName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SenderName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("StartDeliveryDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Deliveries", "dbo");
                });

            modelBuilder.Entity("FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate.Courier", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"), "deliveryseq");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Couriers", "dbo");
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

            modelBuilder.Entity("FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate.AssignDelivery", b =>
                {
                    b.HasOne("FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate.Courier", "Courier")
                        .WithMany()
                        .HasForeignKey("CourierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate.Delivery", "Delivery")
                        .WithMany()
                        .HasForeignKey("DeliveryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate.AssignDeliveryStatus", "Status", b1 =>
                        {
                            b1.Property<long>("AssignDeliveryId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .HasColumnType("integer")
                                .HasColumnName("StatusId");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Status");

                            b1.HasKey("AssignDeliveryId");

                            b1.ToTable("AssignDeliveries", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("AssignDeliveryId");
                        });

                    b.Navigation("Courier");

                    b.Navigation("Delivery");

                    b.Navigation("Status")
                        .IsRequired();
                });

            modelBuilder.Entity("FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate.Delivery", b =>
                {
                    b.OwnsOne("FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate.Address", "RecipientAddress", b1 =>
                        {
                            b1.Property<long>("DeliveryId")
                                .HasColumnType("bigint");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("CityRecipient");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("CountryRecipient");

                            b1.Property<string>("Home")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("HomeRecipient");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("StreetRecipient");

                            b1.HasKey("DeliveryId");

                            b1.ToTable("Deliveries", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("DeliveryId");
                        });

                    b.OwnsOne("FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate.Address", "SenderAddress", b1 =>
                        {
                            b1.Property<long>("DeliveryId")
                                .HasColumnType("bigint");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("CitySender");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("CountrySender");

                            b1.Property<string>("Home")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("HomeSender");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("StreetSender");

                            b1.HasKey("DeliveryId");

                            b1.ToTable("Deliveries", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("DeliveryId");
                        });

                    b.OwnsOne("FoodDelivery.Delivering.Domain.AgregationModels.ValueObjects.Phone", "UserPhoneNumber", b1 =>
                        {
                            b1.Property<long>("DeliveryId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("UserPhone");

                            b1.HasKey("DeliveryId");

                            b1.ToTable("Deliveries", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("DeliveryId");
                        });

                    b.OwnsOne("FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate.DeliveryStatus", "DeliveryStatus", b1 =>
                        {
                            b1.Property<long>("DeliveryId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .HasColumnType("integer")
                                .HasColumnName("DeliveryStatusId");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("DeliveryStatus");

                            b1.HasKey("DeliveryId");

                            b1.ToTable("Deliveries", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("DeliveryId");
                        });

                    b.OwnsOne("FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate.PaymentMethod", "PaymentMethod", b1 =>
                        {
                            b1.Property<long>("DeliveryId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("PaymentMethod");

                            b1.HasKey("DeliveryId");

                            b1.ToTable("Deliveries", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("DeliveryId");
                        });

                    b.OwnsOne("FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate.Price", "TotalPrice", b1 =>
                        {
                            b1.Property<long>("DeliveryId")
                                .HasColumnType("bigint");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric");

                            b1.HasKey("DeliveryId");

                            b1.ToTable("Deliveries", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("DeliveryId");
                        });

                    b.OwnsOne("FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate.Weight", "TotalWeight", b1 =>
                        {
                            b1.Property<long>("DeliveryId")
                                .HasColumnType("bigint");

                            b1.Property<long>("Grams")
                                .HasColumnType("bigint");

                            b1.HasKey("DeliveryId");

                            b1.ToTable("Deliveries", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("DeliveryId");
                        });

                    b.Navigation("DeliveryStatus")
                        .IsRequired();

                    b.Navigation("PaymentMethod")
                        .IsRequired();

                    b.Navigation("RecipientAddress")
                        .IsRequired();

                    b.Navigation("SenderAddress")
                        .IsRequired();

                    b.Navigation("TotalPrice")
                        .IsRequired();

                    b.Navigation("TotalWeight")
                        .IsRequired();

                    b.Navigation("UserPhoneNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate.Courier", b =>
                {
                    b.OwnsOne("FoodDelivery.Delivering.Domain.AgregationModels.ValueObjects.Phone", "PhoneNumber", b1 =>
                        {
                            b1.Property<long>("CourierId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("UserPhone");

                            b1.HasKey("CourierId");

                            b1.ToTable("Couriers", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("CourierId");
                        });

                    b.OwnsOne("FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate.WorkAddress", "WorkAddress", b1 =>
                        {
                            b1.Property<long>("CourierId")
                                .HasColumnType("bigint");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("District")
                                .HasColumnType("text");

                            b1.HasKey("CourierId");

                            b1.ToTable("Couriers", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("CourierId");
                        });

                    b.OwnsOne("FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate.WorkStatus", "WorkStatus", b1 =>
                        {
                            b1.Property<long>("CourierId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .HasColumnType("integer")
                                .HasColumnName("WorkStatusId");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("WorkStatus");

                            b1.HasKey("CourierId");

                            b1.ToTable("Couriers", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("CourierId");
                        });

                    b.Navigation("PhoneNumber")
                        .IsRequired();

                    b.Navigation("WorkAddress")
                        .IsRequired();

                    b.Navigation("WorkStatus")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
