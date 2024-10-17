using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoKiralama.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddCarPhotoRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarPhotoId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarPhoto_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarPhotoId",
                table: "Cars",
                column: "CarPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarPhoto_ModelId",
                table: "CarPhoto",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarPhoto_CarPhotoId",
                table: "Cars",
                column: "CarPhotoId",
                principalTable: "CarPhoto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarPhoto_CarPhotoId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarPhoto");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarPhotoId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarPhotoId",
                table: "Cars");
        }
    }
}
