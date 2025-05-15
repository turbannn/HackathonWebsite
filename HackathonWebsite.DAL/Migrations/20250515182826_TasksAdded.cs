using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackathonWebsite.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TasksAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "HackathonTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HackathonTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HackathonTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HackathonTasks_UserId",
                table: "HackathonTasks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HackathonTasks");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
