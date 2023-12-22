using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class update7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Banner",
                table: "Company");

            migrationBuilder.AddColumn<string>(
                name: "BannerOriginalName",
                table: "Company",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BannerReName",
                table: "Company",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerOriginalName",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "BannerReName",
                table: "Company");

            migrationBuilder.AddColumn<string>(
                name: "Banner",
                table: "Company",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
