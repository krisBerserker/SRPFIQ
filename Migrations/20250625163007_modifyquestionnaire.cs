using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication_SRPFIQ.Migrations
{
    /// <inheritdoc />
    public partial class modifyquestionnaire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Questionnaires",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Questionnaires");
        }
    }
}
