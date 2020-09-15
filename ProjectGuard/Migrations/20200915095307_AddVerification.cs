using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ProjectGuard.Migrations
{
    public partial class AddVerification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Verifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    Result = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileCheckResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    HashValueId = table.Column<int>(nullable: false),
                    Result = table.Column<bool>(nullable: false),
                    VerificationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileCheckResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileCheckResults_HashValues_HashValueId",
                        column: x => x.HashValueId,
                        principalTable: "HashValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileCheckResults_Verifications_VerificationId",
                        column: x => x.VerificationId,
                        principalTable: "Verifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileCheckResults_HashValueId",
                table: "FileCheckResults",
                column: "HashValueId");

            migrationBuilder.CreateIndex(
                name: "IX_FileCheckResults_VerificationId",
                table: "FileCheckResults",
                column: "VerificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileCheckResults");

            migrationBuilder.DropTable(
                name: "Verifications");
        }
    }
}
