using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoKiralama.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ReservationRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_AspNetUsers_AppUserId1",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_AppUserId1",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Reservation");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Reservation",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_AppUserId",
                table: "Reservation",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_AspNetUsers_AppUserId",
                table: "Reservation",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_AspNetUsers_AppUserId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_AppUserId",
                table: "Reservation");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
    }
}
