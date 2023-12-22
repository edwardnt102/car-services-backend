using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class update11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarOwner",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Subscriptions");

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "Subscriptions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Subscriptions");

            migrationBuilder.AddColumn<long>(
                name: "CarOwner",
                table: "Subscriptions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StaffId",
                table: "Subscriptions",
                type: "bigint",
                nullable: true);
        }
    }
}
