using Microsoft.EntityFrameworkCore.Migrations;

namespace Algenic.Migrations
{
    public partial class OptionalScorePolicy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_ScorePolicies_ScorePolicyId",
                table: "Tasks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "039d8609-8fd8-483c-b6d0-23ca925366eb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d5e3874-77d8-48b6-98b6-fc238f141641");

            migrationBuilder.AlterColumn<int>(
                name: "ScorePolicyId",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9ba2951b-82b2-4ade-97fe-9947f62e4d2d", "b31ca625-210e-4931-ae23-b04490aef67e", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a6148cd4-943e-45d4-9193-14438f2712da", "1b726ceb-aee8-4a51-b541-7b7c6f47b3f1", "Examiner", "EXAMINER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_ScorePolicies_ScorePolicyId",
                table: "Tasks",
                column: "ScorePolicyId",
                principalTable: "ScorePolicies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_ScorePolicies_ScorePolicyId",
                table: "Tasks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ba2951b-82b2-4ade-97fe-9947f62e4d2d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6148cd4-943e-45d4-9193-14438f2712da");

            migrationBuilder.AlterColumn<int>(
                name: "ScorePolicyId",
                table: "Tasks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "039d8609-8fd8-483c-b6d0-23ca925366eb", "1973a643-a830-49c5-9094-8b4d6531bc4f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8d5e3874-77d8-48b6-98b6-fc238f141641", "21f90102-fce5-4076-9cfe-3c45cd136ef5", "Examiner", "EXAMINER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_ScorePolicies_ScorePolicyId",
                table: "Tasks",
                column: "ScorePolicyId",
                principalTable: "ScorePolicies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
