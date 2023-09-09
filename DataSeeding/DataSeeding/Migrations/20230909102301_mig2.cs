using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataSeeding.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "certificates",
                columns: new[] { "Id", "Institution", "Name", "StudentId" },
                values: new object[] { 4, "Turkcell", "Angular", 2 });

            migrationBuilder.InsertData(
                table: "certificates",
                columns: new[] { "Id", "Institution", "Name", "StudentId" },
                values: new object[] { 5, "Turkcell", "Angular", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "certificates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "certificates",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
