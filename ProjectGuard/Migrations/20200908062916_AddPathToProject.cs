using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectGuard.Migrations
{
    public partial class AddPathToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Projects");
        }
    }
}
