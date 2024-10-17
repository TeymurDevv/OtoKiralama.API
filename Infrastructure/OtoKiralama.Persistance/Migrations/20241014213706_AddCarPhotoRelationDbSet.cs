using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoKiralama.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddCarPhotoRelationDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarPhoto_Models_ModelId",
                table: "CarPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarPhoto_CarPhotoId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarPhotoId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarPhoto",
                table: "CarPhoto");

            migrationBuilder.DropIndex(
                name: "IX_CarPhoto_ModelId",
                table: "CarPhoto");

            migrationBuilder.DropColumn(
                name: "CarPhotoId",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "CarPhoto",
                newName: "CarPhotos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarPhotos",
                table: "CarPhotos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarPhotos_ModelId",
                table: "CarPhotos",
                column: "ModelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CarPhotos_Models_ModelId",
                table: "CarPhotos",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarPhotos_Models_ModelId",
                table: "CarPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarPhotos",
                table: "CarPhotos");

            migrationBuilder.DropIndex(
                name: "IX_CarPhotos_ModelId",
                table: "CarPhotos");

            migrationBuilder.RenameTable(
                name: "CarPhotos",
                newName: "CarPhoto");

            migrationBuilder.AddColumn<int>(
                name: "CarPhotoId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarPhoto",
                table: "CarPhoto",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarPhotoId",
                table: "Cars",
                column: "CarPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarPhoto_ModelId",
                table: "CarPhoto",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarPhoto_Models_ModelId",
                table: "CarPhoto",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarPhoto_CarPhotoId",
                table: "Cars",
                column: "CarPhotoId",
                principalTable: "CarPhoto",
                principalColumn: "Id");
        }
    }
}
