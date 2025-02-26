using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoKiralama.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addFilterRangeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 2, 23, 0, 24, 19, 13, DateTimeKind.Local).AddTicks(3020),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 2, 16, 17, 36, 19, 687, DateTimeKind.Local).AddTicks(6119));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 2, 23, 0, 24, 19, 0, DateTimeKind.Local).AddTicks(9831),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 2, 16, 17, 36, 19, 678, DateTimeKind.Local).AddTicks(375));

            migrationBuilder.CreateTable(
                name: "FilterRanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    MinValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MaxValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterRanges", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DropOfLocationId",
                table: "Reservations",
                column: "DropOfLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Locations_DropOfLocationId",
                table: "Reservations",
                column: "DropOfLocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Locations_DropOfLocationId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "FilterRanges");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_DropOfLocationId",
                table: "Reservations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 2, 16, 17, 36, 19, 687, DateTimeKind.Local).AddTicks(6119),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 2, 23, 0, 24, 19, 13, DateTimeKind.Local).AddTicks(3020));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 2, 16, 17, 36, 19, 678, DateTimeKind.Local).AddTicks(375),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 2, 23, 0, 24, 19, 0, DateTimeKind.Local).AddTicks(9831));
        }
    }
}
