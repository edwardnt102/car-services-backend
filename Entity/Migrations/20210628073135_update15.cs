using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class update15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "B2B",
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
                    CompanyShareds = table.Column<string>(nullable: true),
                    DataType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_B2B", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "B2B");
        }
    }
}
