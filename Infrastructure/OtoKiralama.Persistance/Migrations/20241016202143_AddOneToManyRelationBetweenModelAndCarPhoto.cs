using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoKiralama.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddOneToManyRelationBetweenModelAndCarPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CarPhotos_ModelId",
                table: "CarPhotos");

            migrationBuilder.CreateIndex(
                name: "IX_CarPhotos_ModelId",
                table: "CarPhotos",
                column: "ModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CarPhotos_ModelId",
                table: "CarPhotos");

            migrationBuilder.CreateIndex(
                name: "IX_CarPhotos_ModelId",
                table: "CarPhotos",
                column: "ModelId",
                unique: true);
        }
    }
}
