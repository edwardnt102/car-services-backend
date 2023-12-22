using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class update5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Companys");

            migrationBuilder.AddColumn<string>(
                name: "LogoOriginalName",
                table: "Companys",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoReName",
                table: "Companys",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoOriginalName",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "LogoReName",
                table: "Companys");

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Companys",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
