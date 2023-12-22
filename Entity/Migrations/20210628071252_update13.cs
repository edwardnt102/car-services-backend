using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class update13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "SharedCompanyIds",
                table: "Company");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "Company",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SharedCompanyIds",
                table: "Company",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
