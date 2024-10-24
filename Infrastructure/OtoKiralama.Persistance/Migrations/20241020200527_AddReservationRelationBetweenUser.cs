using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoKiralama.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationRelationBetweenUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Reservation",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_AppUserId1",
                table: "Reservation",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_AspNetUsers_AppUserId1",
                table: "Reservation",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_AspNetUsers_AppUserId1",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_AppUserId1",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Reservation");
        }
    }
}
