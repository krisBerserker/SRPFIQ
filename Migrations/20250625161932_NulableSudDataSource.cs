using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication_SRPFIQ.Migrations
{
    /// <inheritdoc />
    public partial class NulableSudDataSource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdSubDataSource",
                table: "QuestionnaireQuestions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdSubDataSource",
                table: "QuestionnaireQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
