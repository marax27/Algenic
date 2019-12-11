using Microsoft.EntityFrameworkCore.Migrations;

namespace Algenic.Migrations
{
    public partial class RemoveRegularRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "159be99b-fbec-49c3-b919-585fed505d40");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23b4d357-46d2-4f31-9112-ba48da551022");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36a7bc45-5960-46e6-bf5f-419e0ec63c28");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "039d8609-8fd8-483c-b6d0-23ca925366eb", "1973a643-a830-49c5-9094-8b4d6531bc4f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8d5e3874-77d8-48b6-98b6-fc238f141641", "21f90102-fce5-4076-9cfe-3c45cd136ef5", "Examiner", "EXAMINER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "039d8609-8fd8-483c-b6d0-23ca925366eb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d5e3874-77d8-48b6-98b6-fc238f141641");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "159be99b-fbec-49c3-b919-585fed505d40", "31347c4d-3566-4a65-adaf-cd4f3f111f63", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "23b4d357-46d2-4f31-9112-ba48da551022", "e5937065-4ed8-4132-b8a2-1046b9bd2bae", "Examiner", "EXAMINER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "36a7bc45-5960-46e6-bf5f-419e0ec63c28", "1903238d-f32f-4358-94da-e9a09f2decb0", "Regular", "REGULAR" });
        }
    }
}
