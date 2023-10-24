using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApi.Migrations
{
    /// <inheritdoc />
    public partial class renametasktable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Categories_CategoryId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "TasksCard");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_UserId",
                table: "TasksCard",
                newName: "IX_TasksCard_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_CategoryId",
                table: "TasksCard",
                newName: "IX_TasksCard_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TasksCard",
                table: "TasksCard",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TasksCard_Categories_CategoryId",
                table: "TasksCard",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TasksCard_Users_UserId",
                table: "TasksCard",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TasksCard_Categories_CategoryId",
                table: "TasksCard");

            migrationBuilder.DropForeignKey(
                name: "FK_TasksCard_Users_UserId",
                table: "TasksCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TasksCard",
                table: "TasksCard");

            migrationBuilder.RenameTable(
                name: "TasksCard",
                newName: "Tasks");

            migrationBuilder.RenameIndex(
                name: "IX_TasksCard_UserId",
                table: "Tasks",
                newName: "IX_Tasks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TasksCard_CategoryId",
                table: "Tasks",
                newName: "IX_Tasks_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Categories_CategoryId",
                table: "Tasks",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
