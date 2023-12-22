using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Zones",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Workers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "WorkerType",
                table: "Workers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Withdraws",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Teams",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Subscriptions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Staffs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Slots",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Rules",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Prices",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Places",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Jobs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Customers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Columns",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Class",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Cars",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "CarModels",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Brands",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Basements",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Companys",
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
                    Logo = table.Column<string>(maxLength: 256, nullable: true),
                    ParentId = table.Column<string>(maxLength: 256, nullable: true),
                    SharedCompanyIds = table.Column<string>(maxLength: 256, nullable: true),
                    Color = table.Column<string>(maxLength: 256, nullable: true),
                    Banner = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companys", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companys");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "WorkerType",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Withdraws");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Basements");
        }
    }
}
