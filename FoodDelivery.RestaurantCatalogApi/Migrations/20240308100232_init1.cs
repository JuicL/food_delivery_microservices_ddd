using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDelivery.RestaurantCatalogApi.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Restaurants_RestaurantId1",
                schema: "dbo",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_DishAvaibles_DishAvaiblesStatus_DishAvaibleStatusId",
                schema: "dbo",
                table: "DishAvaibles");

            migrationBuilder.DropTable(
                name: "DishAvaiblesStatus",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_DishAvaibles_DishAvaibleStatusId",
                schema: "dbo",
                table: "DishAvaibles");

            migrationBuilder.DropIndex(
                name: "IX_DishAvaibles_DishId",
                schema: "dbo",
                table: "DishAvaibles");

            migrationBuilder.DropIndex(
                name: "IX_Branches_RestaurantId1",
                schema: "dbo",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "DishAvaibleStatusId",
                schema: "dbo",
                table: "DishAvaibles");

            migrationBuilder.DropColumn(
                name: "RestaurantId1",
                schema: "dbo",
                table: "Branches");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvaible",
                schema: "dbo",
                table: "DishAvaibles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "IntegrationEventLog",
                schema: "dbo",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventTypeName = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    TimesSent = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationEventLog", x => x.EventId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishAvaibles_DishId_BranchId",
                schema: "dbo",
                table: "DishAvaibles",
                columns: new[] { "DishId", "BranchId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntegrationEventLog",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_DishAvaibles_DishId_BranchId",
                schema: "dbo",
                table: "DishAvaibles");

            migrationBuilder.DropColumn(
                name: "IsAvaible",
                schema: "dbo",
                table: "DishAvaibles");

            migrationBuilder.AddColumn<int>(
                name: "DishAvaibleStatusId",
                schema: "dbo",
                table: "DishAvaibles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "RestaurantId1",
                schema: "dbo",
                table: "Branches",
                type: "bigint",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Branches_RestaurantId1",
                schema: "dbo",
                table: "Branches",
                column: "RestaurantId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Restaurants_RestaurantId1",
                schema: "dbo",
                table: "Branches",
                column: "RestaurantId1",
                principalSchema: "dbo",
                principalTable: "Restaurants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DishAvaibles_DishAvaiblesStatus_DishAvaibleStatusId",
                schema: "dbo",
                table: "DishAvaibles",
                column: "DishAvaibleStatusId",
                principalSchema: "dbo",
                principalTable: "DishAvaiblesStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
