using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoKiralama.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddModelRelationCarTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "ModelId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "ModelId",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id");
        }
    }
}
