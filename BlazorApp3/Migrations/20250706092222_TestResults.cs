using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace BlazorApp3.Migrations
{
    /// <inheritdoc />
    public partial class TestResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestResults",
                columns: table => new
                {
                    TestResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "longtext", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    TopicTitle = table.Column<string>(type: "longtext", nullable: false),
                    Difficulty = table.Column<string>(type: "longtext", nullable: false),
                    NumberOfQuestions = table.Column<int>(type: "int", nullable: false),
                    NumberOfCorrectAnswers = table.Column<int>(type: "int", nullable: false),
                    NumberOfQuestionsAttempeted = table.Column<int>(type: "int", nullable: false),
                    ScorePercentage = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResults", x => x.TestResultId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestResults");
        }
    }
}
