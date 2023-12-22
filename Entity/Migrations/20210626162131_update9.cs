using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class update9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfPurchase",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Subscriptions");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Subscriptions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ArgentId",
                table: "Subscriptions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CarOwner",
                table: "Subscriptions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ClassId",
                table: "Subscriptions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PlaceId",
                table: "Subscriptions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarImages",
                table: "Cars",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelImages",
                table: "CarModels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Brands",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ArgentId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "CarOwner",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "CarImages",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ModelImages",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Brands");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfPurchase",
                table: "Subscriptions",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Subscriptions",
                type: "datetime",
                nullable: true);
        }
    }
}
