using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class update21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "StreetId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "UserProfile");

            migrationBuilder.AddColumn<long>(
                name: "DistrictId",
                table: "Workers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProvinceId",
                table: "Workers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WardId",
                table: "Workers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DistrictId",
                table: "Staffs",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProvinceId",
                table: "Staffs",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WardId",
                table: "Staffs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "Staffs");

            migrationBuilder.AddColumn<long>(
                name: "DistrictId",
                table: "UserProfile",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProvinceId",
                table: "UserProfile",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StreetId",
                table: "UserProfile",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WardId",
                table: "UserProfile",
                type: "bigint",
                nullable: true);
        }
    }
}
