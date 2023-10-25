using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApi.Migrations
{
    /// <inheritdoc />
    public partial class addestimatetype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstimateDate",
                table: "TasksCard",
                newName: "EstimateDatenumber");

            migrationBuilder.AddColumn<string>(
                name: "EstimateDateUnit",
                table: "TasksCard",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimateDateUnit",
                table: "TasksCard");

            migrationBuilder.RenameColumn(
                name: "EstimateDatenumber",
                table: "TasksCard",
                newName: "EstimateDate");
        }
    }
}
