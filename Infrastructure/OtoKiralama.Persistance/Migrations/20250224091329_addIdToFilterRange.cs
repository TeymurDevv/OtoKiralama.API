using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoKiralama.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addIdToFilterRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 2, 24, 13, 13, 28, 739, DateTimeKind.Local).AddTicks(9509),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 2, 23, 1, 20, 45, 945, DateTimeKind.Local).AddTicks(1913));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 2, 24, 13, 13, 28, 731, DateTimeKind.Local).AddTicks(1648),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 2, 23, 1, 20, 45, 935, DateTimeKind.Local).AddTicks(3384));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 2, 23, 1, 20, 45, 945, DateTimeKind.Local).AddTicks(1913),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 2, 24, 13, 13, 28, 739, DateTimeKind.Local).AddTicks(9509));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 2, 23, 1, 20, 45, 935, DateTimeKind.Local).AddTicks(3384),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 2, 24, 13, 13, 28, 731, DateTimeKind.Local).AddTicks(1648));
        }
    }
}
