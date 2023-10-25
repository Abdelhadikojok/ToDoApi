using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApi.Migrations
{
    /// <inheritdoc />
    public partial class back2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EstimateDatenumber",
                table: "TasksCard",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,0)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "EstimateDatenumber",
                table: "TasksCard",
                type: "numeric(18,0)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
