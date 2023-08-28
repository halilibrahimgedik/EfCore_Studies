using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneToOne_RelationShip.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Husbands",
                columns: table => new
                {
                    HusbandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HusbandName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Husbands", x => x.HusbandId);
                });

            migrationBuilder.CreateTable(
                name: "Wives",
                columns: table => new
                {
                    WifeId = table.Column<int>(type: "int", nullable: false),
                    WifeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wives", x => x.WifeId);
                    table.ForeignKey(
                        name: "FK_Wives_Husbands_WifeId",
                        column: x => x.WifeId,
                        principalTable: "Husbands",
                        principalColumn: "HusbandId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wives");

            migrationBuilder.DropTable(
                name: "Husbands");
        }
    }
}
