using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ninja_Manager.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gears",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    strength = table.Column<int>(type: "int", nullable: false),
                    intelligence = table.Column<int>(type: "int", nullable: false),
                    agility = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ninjas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Gold = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ninjas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GearNinja",
                columns: table => new
                {
                    GearForNinjaId = table.Column<int>(type: "int", nullable: false),
                    NinjasForGearId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearNinja", x => new { x.GearForNinjaId, x.NinjasForGearId });
                    table.ForeignKey(
                        name: "FK_GearNinja_Gears_GearForNinjaId",
                        column: x => x.GearForNinjaId,
                        principalTable: "Gears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GearNinja_Ninjas_NinjasForGearId",
                        column: x => x.NinjasForGearId,
                        principalTable: "Ninjas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GearNinja_NinjasForGearId",
                table: "GearNinja",
                column: "NinjasForGearId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GearNinja");

            migrationBuilder.DropTable(
                name: "Gears");

            migrationBuilder.DropTable(
                name: "Ninjas");
        }
    }
}
