using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectGuard.Migrations
{
    public partial class VerificationProjectId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Verifications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Verifications_ProjectId",
                table: "Verifications",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Verifications_Projects_ProjectId",
                table: "Verifications",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Verifications_Projects_ProjectId",
                table: "Verifications");

            migrationBuilder.DropIndex(
                name: "IX_Verifications_ProjectId",
                table: "Verifications");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Verifications");
        }
    }
}
