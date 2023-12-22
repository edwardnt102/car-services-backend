using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class update22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "Staffs");

            migrationBuilder.AddColumn<long>(
                name: "WorkerIntroduceId",
                table: "Workers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StaffPlace",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<long>(nullable: false),
                    PlaceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffPlace", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffPlace");

            migrationBuilder.DropColumn(
                name: "WorkerIntroduceId",
                table: "Workers");

            migrationBuilder.AddColumn<long>(
                name: "PlaceId",
                table: "Staffs",
                type: "bigint",
                nullable: true);
        }
    }
}
