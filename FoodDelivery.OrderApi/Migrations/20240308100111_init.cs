using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FoodDelivery.OrderApi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence(
                name: "orderseq",
                incrementBy: 10);

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

            migrationBuilder.CreateTable(
                name: "OrderRequest",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddress_Country = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddress_City = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddress_Street = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddress_Home = table.Column<string>(type: "text", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    RestaurantAddress_Country = table.Column<string>(type: "text", nullable: false),
                    RestaurantAddress_City = table.Column<string>(type: "text", nullable: false),
                    RestaurantAddress_Street = table.Column<string>(type: "text", nullable: false),
                    RestaurantAddress_Home = table.Column<string>(type: "text", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "integer", nullable: false),
                    OrderStatus = table.Column<string>(type: "text", nullable: false),
                    OrderStatusId = table.Column<int>(type: "integer", nullable: false),
                    OrderTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dishes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DishId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Weight_Grams = table.Column<long>(type: "bigint", nullable: false),
                    Price_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Units = table.Column<int>(type: "integer", nullable: false),
                    OrderRequestId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dishes_OrderRequest_OrderRequestId",
                        column: x => x.OrderRequestId,
                        principalSchema: "dbo",
                        principalTable: "OrderRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_OrderRequestId",
                schema: "dbo",
                table: "Dishes",
                column: "OrderRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dishes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IntegrationEventLog",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OrderRequest",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "orderseq");
        }
    }
}
