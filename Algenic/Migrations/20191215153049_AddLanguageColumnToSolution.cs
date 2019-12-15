using Microsoft.EntityFrameworkCore.Migrations;

namespace Algenic.Migrations
{
    public partial class AddLanguageColumnToSolution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d376adc-fea5-4c2a-bae1-dd776226ba84");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ebd9e597-f11c-4e7f-b6f6-f3432c2adffd");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Solutions",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f3682a71-4048-48ce-adb6-5c578497328d", "e0f2d005-a5d0-45ed-a646-aab331c2eb30", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8d892c5b-a796-471b-a316-0e8de3daffb9", "0c65ca5e-6c0a-48f1-8b20-84bc637db6db", "Examiner", "EXAMINER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d892c5b-a796-471b-a316-0e8de3daffb9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3682a71-4048-48ce-adb6-5c578497328d");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Solutions");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3d376adc-fea5-4c2a-bae1-dd776226ba84", "98e4ba06-d683-4f67-9971-973f41a2e069", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ebd9e597-f11c-4e7f-b6f6-f3432c2adffd", "f5ca26a3-d864-445f-9a09-5e23f5585ec6", "Examiner", "EXAMINER" });
        }
    }
}
