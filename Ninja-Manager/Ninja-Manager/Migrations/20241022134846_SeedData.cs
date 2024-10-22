using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ninja_Manager.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "Gears",
                columns: new[] { "Id", "Cost", "Name", "agility", "intelligence", "strength", "type" },
                values: new object[,]
                {
                    { 1, 50, "Rusty Helm", 0, 1, 1, 0 },
                    { 2, 150, "Iron Helm", 1, 2, 3, 0 },
                    { 3, 300, "Crown of Wisdom", 0, 5, 2, 0 },
                    { 4, 60, "Leather Armor", 1, 1, 2, 1 },
                    { 5, 180, "Iron Chestplate", 1, 2, 4, 1 },
                    { 6, 350, "Dragonscale Chest", 2, 3, 6, 1 },
                    { 7, 40, "Cloth Gloves", 1, 1, 1, 2 },
                    { 8, 100, "Leather Gloves", 2, 2, 2, 2 },
                    { 9, 250, "Gauntlets of Power", 1, 1, 5, 2 },
                    { 10, 50, "Worn Boots", 2, 0, 1, 3 },
                    { 11, 130, "Iron Boots", 2, 1, 3, 3 },
                    { 12, 280, "Boots of Swiftness", 6, 0, 2, 3 },
                    { 13, 30, "Copper Ring", 1, 1, 1, 4 },
                    { 14, 120, "Silver Ring", 2, 2, 1, 4 },
                    { 15, 270, "Ring of the Ancients", 4, 4, 0, 4 },
                    { 16, 40, "Wooden Necklace", 1, 1, 0, 5 },
                    { 17, 140, "Silver Pendant", 2, 3, 1, 5 },
                    { 18, 320, "Amulet of the Phoenix", 3, 5, 0, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.InsertData(
                table: "Ninjas",
                columns: new[] { "Id", "Gold", "Name" },
                values: new object[] { 1, 2000, "Nonja" });
        }
    }
}
