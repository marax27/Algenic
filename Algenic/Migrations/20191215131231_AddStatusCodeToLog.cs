using Microsoft.EntityFrameworkCore.Migrations;

namespace Algenic.Migrations
{
    public partial class AddStatusCodeToLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c99ed10-3a9f-43a9-8476-08cfaf3a1a98");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7d3f46b-610a-43ea-a1c2-fd8b9135171d");

            migrationBuilder.AddColumn<string>(
                name: "StatusCode",
                table: "Logs",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3d376adc-fea5-4c2a-bae1-dd776226ba84", "98e4ba06-d683-4f67-9971-973f41a2e069", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ebd9e597-f11c-4e7f-b6f6-f3432c2adffd", "f5ca26a3-d864-445f-9a09-5e23f5585ec6", "Examiner", "EXAMINER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d376adc-fea5-4c2a-bae1-dd776226ba84");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ebd9e597-f11c-4e7f-b6f6-f3432c2adffd");

            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "Logs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1c99ed10-3a9f-43a9-8476-08cfaf3a1a98", "4559ebf4-6249-473a-a89f-45931a243487", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d7d3f46b-610a-43ea-a1c2-fd8b9135171d", "c7233fbf-9589-4c75-bb90-78760616a211", "Examiner", "EXAMINER" });
        }
    }
}
