using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoKiralama.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class p : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 7, 1, 8, 20, 342, DateTimeKind.Local).AddTicks(4900),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 7, 1, 3, 46, 806, DateTimeKind.Local).AddTicks(2410));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 7, 1, 8, 20, 323, DateTimeKind.Local).AddTicks(9760),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 7, 1, 3, 46, 795, DateTimeKind.Local).AddTicks(1150));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 7, 1, 3, 46, 806, DateTimeKind.Local).AddTicks(2410),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 7, 1, 8, 20, 342, DateTimeKind.Local).AddTicks(4900));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 7, 1, 3, 46, 795, DateTimeKind.Local).AddTicks(1150),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 1, 7, 1, 8, 20, 323, DateTimeKind.Local).AddTicks(9760));
        }
    }
}
