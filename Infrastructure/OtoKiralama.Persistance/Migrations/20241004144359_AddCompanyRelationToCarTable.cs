using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoKiralama.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyRelationToCarTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CompanyId",
                table: "Cars",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Companies_CompanyId",
                table: "Cars",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Companies_CompanyId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CompanyId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Cars");
        }
    }
}
