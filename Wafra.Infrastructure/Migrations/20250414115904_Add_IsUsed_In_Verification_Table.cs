using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wafra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_IsUsed_In_Verification_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "Verifications",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "Verifications");
        }
    }
}
