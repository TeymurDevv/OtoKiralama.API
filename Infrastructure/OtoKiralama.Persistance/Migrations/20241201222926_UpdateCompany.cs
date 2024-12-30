using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoKiralama.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 2, 2, 29, 25, 906, DateTimeKind.Local).AddTicks(390),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 22, 14, 16, 54, 934, DateTimeKind.Local).AddTicks(8214));

            migrationBuilder.AddColumn<string>(
                name: "Essentials",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentInformation",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Requirements",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Trust",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 2, 2, 29, 25, 905, DateTimeKind.Local).AddTicks(8950),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 22, 14, 16, 54, 934, DateTimeKind.Local).AddTicks(2231));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Essentials",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "PaymentInformation",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Requirements",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Trust",
                table: "Companies");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 22, 14, 16, 54, 934, DateTimeKind.Local).AddTicks(8214),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 2, 2, 29, 25, 906, DateTimeKind.Local).AddTicks(390));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 22, 14, 16, 54, 934, DateTimeKind.Local).AddTicks(2231),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 2, 2, 29, 25, 905, DateTimeKind.Local).AddTicks(8950));
        }
    }
}
