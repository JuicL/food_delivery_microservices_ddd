using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDelivery.Delivering.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence(
                name: "deliveryseq",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Couriers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    UserPhone = table.Column<string>(type: "text", nullable: false),
                    WorkAddress_Country = table.Column<string>(type: "text", nullable: false),
                    WorkAddress_City = table.Column<string>(type: "text", nullable: false),
                    WorkAddress_District = table.Column<string>(type: "text", nullable: true),
                    WorkStatus = table.Column<string>(type: "text", nullable: false),
                    WorkStatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Couriers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    RecipientId = table.Column<long>(type: "bigint", nullable: false),
                    RecipientName = table.Column<string>(type: "text", nullable: false),
                    UserPhone = table.Column<string>(type: "text", nullable: false),
                    TotalWeight_Grams = table.Column<long>(type: "bigint", nullable: false),
                    TotalPrice_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    SenderName = table.Column<string>(type: "text", nullable: false),
                    CountrySender = table.Column<string>(type: "text", nullable: false),
                    CitySender = table.Column<string>(type: "text", nullable: false),
                    StreetSender = table.Column<string>(type: "text", nullable: false),
                    HomeSender = table.Column<string>(type: "text", nullable: false),
                    CountryRecipient = table.Column<string>(type: "text", nullable: false),
                    CityRecipient = table.Column<string>(type: "text", nullable: false),
                    StreetRecipient = table.Column<string>(type: "text", nullable: false),
                    HomeRecipient = table.Column<string>(type: "text", nullable: false),
                    CourierId = table.Column<long>(type: "bigint", nullable: true),
                    StartDeliveryDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeliveredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Lateness = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryStatus = table.Column<string>(type: "text", nullable: false),
                    DeliveryStatusId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                });

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
                name: "AssignDeliveries",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CourierId = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    AssignDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignDeliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignDeliveries_Couriers_CourierId",
                        column: x => x.CourierId,
                        principalSchema: "dbo",
                        principalTable: "Couriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignDeliveries_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalSchema: "dbo",
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignDeliveries_CourierId",
                schema: "dbo",
                table: "AssignDeliveries",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignDeliveries_DeliveryId",
                schema: "dbo",
                table: "AssignDeliveries",
                column: "DeliveryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignDeliveries",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IntegrationEventLog",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Couriers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Deliveries",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "deliveryseq");
        }
    }
}
