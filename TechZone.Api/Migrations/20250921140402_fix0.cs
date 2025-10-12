using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechZone.Migrations
{
    /// <inheritdoc />
    public partial class fix0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "EmailQueues",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailQueues_UserId",
                table: "EmailQueues",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailQueues_AspNetUsers_UserId",
                table: "EmailQueues",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailQueues_AspNetUsers_UserId",
                table: "EmailQueues");

            migrationBuilder.DropIndex(
                name: "IX_EmailQueues_UserId",
                table: "EmailQueues");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "EmailQueues",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
