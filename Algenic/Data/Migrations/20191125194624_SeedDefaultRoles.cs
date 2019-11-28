using Microsoft.EntityFrameworkCore.Migrations;

namespace Algenic.Data.Migrations
{
    public partial class SeedDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3cba363c-950c-4105-a03f-f57bd25065e6", "dbc338c1-c85d-419b-a4dc-bee4c766aaa9", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f7ec9271-a741-4fae-a059-158756346279", "b7719396-2b36-446e-9d63-bd6fa5a2ede0", "Examiner", "EXAMINER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1a3cd665-b368-4452-ac3b-cabb8538a1fd", "4c7fe0c1-e2e5-41dc-b441-9f4bce824542", "Regular", "REGULAR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a3cd665-b368-4452-ac3b-cabb8538a1fd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cba363c-950c-4105-a03f-f57bd25065e6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7ec9271-a741-4fae-a059-158756346279");
        }
    }
}
