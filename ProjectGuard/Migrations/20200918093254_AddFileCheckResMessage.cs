using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectGuard.Migrations
{
    public partial class AddFileCheckResMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "FileCheckResults",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "FileCheckResults");
        }
    }
}
