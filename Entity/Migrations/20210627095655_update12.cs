using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class update12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarImages",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ModelImages",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Brands");

            migrationBuilder.AlterColumn<string>(
                name: "PictureOriginalName",
                table: "Workers",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PictureOriginalName",
                table: "Staffs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CheckOutImageOriginalName",
                table: "Slots",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CheckOutImage",
                table: "Slots",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CheckInImageOriginalName",
                table: "Slots",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CheckInImage",
                table: "Slots",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PictureOriginalName",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BannerReName",
                table: "Company",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BannerOriginalName",
                table: "Company",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarImageOriginalName",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarImageReName",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelImageOriginalName",
                table: "CarModels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelImageReName",
                table: "CarModels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoOriginalName",
                table: "Brands",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoReName",
                table: "Brands",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "PictureOriginalName",
                table: "Workers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PictureOriginalName",
                table: "Staffs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CheckOutImageOriginalName",
                table: "Slots",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CheckOutImage",
                table: "Slots",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CheckInImageOriginalName",
                table: "Slots",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CheckInImage",
                table: "Slots",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PictureOriginalName",
                table: "Customers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BannerReName",
                table: "Company",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BannerOriginalName",
                table: "Company",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarImages",
                table: "Cars",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelImages",
                table: "CarModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Brands",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
