using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataSeeding.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "certificates",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Linux");

            migrationBuilder.UpdateData(
                table: "certificates",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Web Api");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "certificates",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Angular");

            migrationBuilder.UpdateData(
                table: "certificates",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Angular");
        }
    }
}
