using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ninja_Manager.Migrations
{
    /// <inheritdoc />
    public partial class UpperCaseStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "type",
                table: "Gears",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "strength",
                table: "Gears",
                newName: "Strength");

            migrationBuilder.RenameColumn(
                name: "intelligence",
                table: "Gears",
                newName: "Intelligence");

            migrationBuilder.RenameColumn(
                name: "agility",
                table: "Gears",
                newName: "Agility");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Gears",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Strength",
                table: "Gears",
                newName: "strength");

            migrationBuilder.RenameColumn(
                name: "Intelligence",
                table: "Gears",
                newName: "intelligence");

            migrationBuilder.RenameColumn(
                name: "Agility",
                table: "Gears",
                newName: "agility");
        }
    }
}
