using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ninja_Manager.Migrations
{
    /// <inheritdoc />
    public partial class RightCostsSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Agility", "Cost", "Strength" },
                values: new object[] { 1, 210, 3 });

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 4,
                column: "Cost",
                value: 100);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 6,
                column: "Cost",
                value: 280);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 7,
                column: "Cost",
                value: 70);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 8,
                column: "Cost",
                value: 140);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 9,
                column: "Cost",
                value: 190);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 10,
                column: "Cost",
                value: 70);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 11,
                column: "Cost",
                value: 150);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 13,
                column: "Cost",
                value: 70);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 14,
                column: "Cost",
                value: 110);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 15,
                column: "Cost",
                value: 160);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 17,
                column: "Cost",
                value: 130);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Cost", "Strength" },
                values: new object[] { 250, 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Agility", "Cost", "Strength" },
                values: new object[] { 0, 300, 2 });

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 4,
                column: "Cost",
                value: 60);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 6,
                column: "Cost",
                value: 350);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 7,
                column: "Cost",
                value: 40);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 8,
                column: "Cost",
                value: 100);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 9,
                column: "Cost",
                value: 250);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 10,
                column: "Cost",
                value: 50);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 11,
                column: "Cost",
                value: 130);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 13,
                column: "Cost",
                value: 30);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 14,
                column: "Cost",
                value: 120);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 15,
                column: "Cost",
                value: 270);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 17,
                column: "Cost",
                value: 140);

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Cost", "Strength" },
                values: new object[] { 320, 0 });
        }
    }
}
