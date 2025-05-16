using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackathonWebsite.DAL.Migrations
{
    /// <inheritdoc />
    public partial class HTRatedByPropertyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherIdRatedBy",
                table: "HackathonTasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherIdRatedBy",
                table: "HackathonTasks");
        }
    }
}
