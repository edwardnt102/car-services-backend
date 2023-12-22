using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class update17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckInImageOriginalName",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "CheckOutImageOriginalName",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "BannerOriginalName",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "BannerReName",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "LogoOriginalName",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "LogoReName",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CarImageOriginalName",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarImageReName",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ModelImageOriginalName",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "ModelImageReName",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "LogoOriginalName",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "LogoReName",
                table: "Brands");

            migrationBuilder.AddColumn<string>(
                name: "Banner",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Company",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarImage",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelImage",
                table: "CarModels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Brands",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Banner",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CarImage",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ModelImage",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Brands");

            migrationBuilder.AddColumn<string>(
                name: "CheckInImageOriginalName",
                table: "Slots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckOutImageOriginalName",
                table: "Slots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BannerOriginalName",
                table: "Company",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BannerReName",
                table: "Company",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoOriginalName",
                table: "Company",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoReName",
                table: "Company",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarImageOriginalName",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarImageReName",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelImageOriginalName",
                table: "CarModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelImageReName",
                table: "CarModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoOriginalName",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoReName",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
