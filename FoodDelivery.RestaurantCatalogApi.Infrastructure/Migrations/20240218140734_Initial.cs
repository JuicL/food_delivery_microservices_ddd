﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FoodDelivery.RestaurantCatalogApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "DishAvaiblesStatus",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishAvaiblesStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DishType",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dish",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Ingredients = table.Column<List<string>>(type: "text[]", nullable: false),
                    Weight = table.Column<long>(type: "bigint", nullable: false),
                    DishTypeId = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dish", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dish_DishType_DishTypeId",
                        column: x => x.DishTypeId,
                        principalSchema: "dbo",
                        principalTable: "DishType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RestaurantId = table.Column<long>(type: "bigint", nullable: false),
                    Address_Country = table.Column<string>(type: "text", nullable: false),
                    Address_City = table.Column<string>(type: "text", nullable: false),
                    Address_Street = table.Column<string>(type: "text", nullable: false),
                    Address_Home = table.Column<string>(type: "text", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    opening_time = table.Column<TimeOnly>(type: "time", nullable: false),
                    closing_time = table.Column<TimeOnly>(type: "time", nullable: false),
                    RestaurantId1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branches_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalSchema: "dbo",
                        principalTable: "Restaurants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Branches_Restaurants_RestaurantId1",
                        column: x => x.RestaurantId1,
                        principalSchema: "dbo",
                        principalTable: "Restaurants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DishAvaibles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DishId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    DishAvaibleStatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishAvaibles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DishAvaibles_Branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "dbo",
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishAvaibles_DishAvaiblesStatus_DishAvaibleStatusId",
                        column: x => x.DishAvaibleStatusId,
                        principalSchema: "dbo",
                        principalTable: "DishAvaiblesStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishAvaibles_Dish_DishId",
                        column: x => x.DishId,
                        principalSchema: "dbo",
                        principalTable: "Dish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_RestaurantId",
                schema: "dbo",
                table: "Branches",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_RestaurantId1",
                schema: "dbo",
                table: "Branches",
                column: "RestaurantId1");

            migrationBuilder.CreateIndex(
                name: "IX_Dish_DishTypeId",
                schema: "dbo",
                table: "Dish",
                column: "DishTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DishAvaibles_BranchId",
                schema: "dbo",
                table: "DishAvaibles",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DishAvaibles_DishAvaibleStatusId",
                schema: "dbo",
                table: "DishAvaibles",
                column: "DishAvaibleStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DishAvaibles_DishId",
                schema: "dbo",
                table: "DishAvaibles",
                column: "DishId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishAvaibles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Branches",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DishAvaiblesStatus",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Dish",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Restaurants",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DishType",
                schema: "dbo");
        }
    }
}
