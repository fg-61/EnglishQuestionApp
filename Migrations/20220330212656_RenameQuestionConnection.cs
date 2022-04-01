using Microsoft.EntityFrameworkCore.Migrations;

namespace EnglishQuestionApp.Migrations
{
    public partial class RenameQuestionConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Questions_QuestinId",
                table: "Options");

            migrationBuilder.RenameColumn(
                name: "QuestinId",
                table: "Options",
                newName: "QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Options_QuestinId",
                table: "Options",
                newName: "IX_Options_QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Questions_QuestionId",
                table: "Options",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Questions_QuestionId",
                table: "Options");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "Options",
                newName: "QuestinId");

            migrationBuilder.RenameIndex(
                name: "IX_Options_QuestionId",
                table: "Options",
                newName: "IX_Options_QuestinId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Questions_QuestinId",
                table: "Options",
                column: "QuestinId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
