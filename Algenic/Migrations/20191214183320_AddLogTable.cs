using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Algenic.Migrations
{
    public partial class AddLogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ba2951b-82b2-4ade-97fe-9947f62e4d2d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6148cd4-943e-45d4-9193-14438f2712da");

            migrationBuilder.AddColumn<int>(
                name: "LogId",
                table: "Solutions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SolutionId = table.Column<int>(nullable: false),
                    TestId = table.Column<int>(nullable: false),
                    ErrorMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_Solutions_SolutionId",
                        column: x => x.SolutionId,
                        principalTable: "Solutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logs_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1c99ed10-3a9f-43a9-8476-08cfaf3a1a98", "4559ebf4-6249-473a-a89f-45931a243487", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d7d3f46b-610a-43ea-a1c2-fd8b9135171d", "c7233fbf-9589-4c75-bb90-78760616a211", "Examiner", "EXAMINER" });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_SolutionId",
                table: "Logs",
                column: "SolutionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logs_TestId",
                table: "Logs",
                column: "TestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c99ed10-3a9f-43a9-8476-08cfaf3a1a98");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7d3f46b-610a-43ea-a1c2-fd8b9135171d");

            migrationBuilder.DropColumn(
                name: "LogId",
                table: "Solutions");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9ba2951b-82b2-4ade-97fe-9947f62e4d2d", "b31ca625-210e-4931-ae23-b04490aef67e", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a6148cd4-943e-45d4-9193-14438f2712da", "1b726ceb-aee8-4a51-b541-7b7c6f47b3f1", "Examiner", "EXAMINER" });
        }
    }
}
