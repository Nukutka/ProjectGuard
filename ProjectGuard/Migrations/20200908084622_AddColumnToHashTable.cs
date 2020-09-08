using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectGuard.Migrations
{
    public partial class AddColumnToHashTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NeedHash",
                table: "HashValues",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NeedHash",
                table: "HashValues");
        }
    }
}
