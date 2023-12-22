using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class update16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Subscriptions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ColorCode",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<long>(nullable: true),
                    ColorCodeLevel1 = table.Column<string>(maxLength: 256, nullable: true),
                    ColorCodeLevel2 = table.Column<string>(maxLength: 256, nullable: true),
                    ColorCodeLevel3 = table.Column<string>(maxLength: 256, nullable: true),
                    ColorCodeLevel4 = table.Column<string>(maxLength: 256, nullable: true),
                    ColorCodeLevel5 = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorCode", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColorCode");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Subscriptions");
        }
    }
}
