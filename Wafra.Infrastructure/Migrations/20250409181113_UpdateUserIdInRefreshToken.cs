using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wafra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserIdInRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UsersId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_UsersId",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "RefreshTokens");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_userId",
                table: "RefreshTokens",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_userId",
                table: "RefreshTokens",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_userId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_userId",
                table: "RefreshTokens");

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "RefreshTokens",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UsersId",
                table: "RefreshTokens",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UsersId",
                table: "RefreshTokens",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
