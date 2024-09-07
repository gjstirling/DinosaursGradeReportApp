using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BadDinosaurCodeTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class DinoType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Dinosaurs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Dinosaurs");
        }
    }
}
