using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SHO_Task.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_base_SHO_entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SHO");

            migrationBuilder.CreateTable(
                name: "outbox_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    occurred_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    processed_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingOrders",
                schema: "SHO",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sho_number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    purchase_order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    state = table.Column<int>(type: "int", nullable: false),
                    pallet_count = table.Column<int>(type: "int", nullable: false),
                    delivery_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shipping_orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingItems",
                schema: "SHO",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    good_code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    serial_number = table.Column<int>(type: "int", nullable: false),
                    shipping_order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    price_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    price_currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shipping_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_shipping_items_shipping_orders_shipping_order_id",
                        column: x => x.shipping_order_id,
                        principalSchema: "SHO",
                        principalTable: "ShippingOrders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_shipping_items_shipping_order_id",
                schema: "SHO",
                table: "ShippingItems",
                column: "shipping_order_id");

            migrationBuilder.CreateIndex(
                name: "ix_shipping_orders_purchase_order_id",
                schema: "SHO",
                table: "ShippingOrders",
                column: "purchase_order_id");

            migrationBuilder.CreateIndex(
                name: "ix_shipping_orders_sho_number",
                schema: "SHO",
                table: "ShippingOrders",
                column: "sho_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "outbox_messages");

            migrationBuilder.DropTable(
                name: "ShippingItems",
                schema: "SHO");

            migrationBuilder.DropTable(
                name: "ShippingOrders",
                schema: "SHO");
        }
    }
}
