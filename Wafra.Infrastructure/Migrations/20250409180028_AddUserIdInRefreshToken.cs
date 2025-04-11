using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wafra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdInRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UsersId",
                table: "RefreshTokens");

            migrationBuilder.AlterColumn<int>(
                name: "UsersId",
                table: "RefreshTokens",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "RefreshTokens",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UsersId",
                table: "RefreshTokens",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UsersId",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "RefreshTokens");

            migrationBuilder.AlterColumn<int>(
                name: "UsersId",
                table: "RefreshTokens",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UsersId",
                table: "RefreshTokens",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
