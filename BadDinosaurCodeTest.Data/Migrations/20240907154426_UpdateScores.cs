using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BadDinosaurCodeTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateScores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_DinoClass_DinoClassId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_DinoClassId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "DinoClassId",
                table: "Scores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DinoClassId",
                table: "Scores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_DinoClassId",
                table: "Scores",
                column: "DinoClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_DinoClass_DinoClassId",
                table: "Scores",
                column: "DinoClassId",
                principalTable: "DinoClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
