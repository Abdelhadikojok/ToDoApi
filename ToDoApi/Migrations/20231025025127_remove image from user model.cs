using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApi.Migrations
{
    /// <inheritdoc />
    public partial class removeimagefromusermodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
