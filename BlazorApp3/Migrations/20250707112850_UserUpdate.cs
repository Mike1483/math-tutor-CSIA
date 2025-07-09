using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp3.Migrations
{
    /// <inheritdoc />
    public partial class UserUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /* migrationBuilder.RenameColumn(
                name: "NumberOfQuestionsAttempeted",
                table: "TestResults",
                newName: "NumberOfQuestionsAttempted"); */

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");

           /* migrationBuilder.RenameColumn(
                name: "NumberOfQuestionsAttempted",
                table: "TestResults",
                newName: "NumberOfQuestionsAttempeted"); */
        }
    }
}
