using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoKiralama.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class updateintdecimaltodouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "MinValue",
                table: "FilterRanges",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<double>(
                name: "MaxValue",
                table: "FilterRanges",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 2, 23, 1, 20, 45, 945, DateTimeKind.Local).AddTicks(1913),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 2, 23, 0, 24, 19, 13, DateTimeKind.Local).AddTicks(3020));

            migrationBuilder.AlterColumn<double>(
                name: "Limit",
                table: "Cars",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DepositAmount",
                table: "Cars",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 2, 23, 1, 20, 45, 935, DateTimeKind.Local).AddTicks(3384),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 2, 23, 0, 24, 19, 0, DateTimeKind.Local).AddTicks(9831));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MinValue",
                table: "FilterRanges",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxValue",
                table: "FilterRanges",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 2, 23, 0, 24, 19, 13, DateTimeKind.Local).AddTicks(3020),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 2, 23, 1, 20, 45, 945, DateTimeKind.Local).AddTicks(1913));

            migrationBuilder.AlterColumn<int>(
                name: "Limit",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepositAmount",
                table: "Cars",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 2, 23, 0, 24, 19, 0, DateTimeKind.Local).AddTicks(9831),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 2, 23, 1, 20, 45, 935, DateTimeKind.Local).AddTicks(3384));
        }
    }
}
