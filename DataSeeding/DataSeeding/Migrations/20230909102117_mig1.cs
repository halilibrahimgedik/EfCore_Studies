using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataSeeding.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    University = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "certificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Institution = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_certificates_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "Id", "Name", "University" },
                values: new object[] { 1, "Halil", "Duzce University" });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "Id", "Name", "University" },
                values: new object[] { 2, "Ela", "Duzce University" });

            migrationBuilder.InsertData(
                table: "certificates",
                columns: new[] { "Id", "Institution", "Name", "StudentId" },
                values: new object[] { 1, "Udemy", ".Net Core", 1 });

            migrationBuilder.InsertData(
                table: "certificates",
                columns: new[] { "Id", "Institution", "Name", "StudentId" },
                values: new object[] { 2, "Udemy", "Docker", 1 });

            migrationBuilder.InsertData(
                table: "certificates",
                columns: new[] { "Id", "Institution", "Name", "StudentId" },
                values: new object[] { 3, "Turkcell", "Angular", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_certificates_StudentId",
                table: "certificates",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "certificates");

            migrationBuilder.DropTable(
                name: "students");
        }
    }
}
