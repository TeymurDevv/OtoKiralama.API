using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoKiralama.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationWithModelAndBrandAndCars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Models",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Models_BrandId",
                table: "Models",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_BrandId",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Models");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
