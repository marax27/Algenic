using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Algenic.Data.Migrations
{
    public partial class DomainModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "CompilationResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SolutionId = table.Column<int>(nullable: false),
                    Output = table.Column<string>(nullable: true),
                    CpuTime = table.Column<string>(nullable: true),
                    MemoryUsage = table.Column<string>(nullable: true),
                    ExecutionSuccessful = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompilationResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScorePolicies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScorePolicies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScoreRules",
                columns: table => new
                {
                    ScorePolicyId = table.Column<int>(nullable: false),
                    Threshold = table.Column<decimal>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreRules", x => new { x.ScorePolicyId, x.Threshold, x.Score });
                    table.ForeignKey(
                        name: "FK_ScoreRules_ScorePolicies_ScorePolicyId",
                        column: x => x.ScorePolicyId,
                        principalTable: "ScorePolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContestId = table.Column<int>(nullable: false),
                    ScorePolicyId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_ScorePolicies_ScorePolicyId",
                        column: x => x.ScorePolicyId,
                        principalTable: "ScorePolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Solutions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false),
                    CompilationResultId = table.Column<int>(nullable: false),
                    SourceCode = table.Column<string>(nullable: true),
                    PointCount = table.Column<decimal>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    IdentityUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solutions_CompilationResults_CompilationResultId",
                        column: x => x.CompilationResultId,
                        principalTable: "CompilationResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Solutions_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solutions_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaskId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Input = table.Column<string>(nullable: true),
                    ExpectedOutput = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tests_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "227cfeba-55dd-4aa8-9332-b91d3c6929f5", "e5568362-527e-48d1-aed7-19e81ee5fa58", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7b3885e0-9fb6-4797-9dde-0f3c59c11db1", "34764608-00d3-400e-9721-358dfc255864", "Examiner", "EXAMINER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6b081e0e-0a2b-4152-abef-ce967fc1c289", "1203a216-a69e-4a88-8f11-df509a4ebedf", "Regular", "REGULAR" });

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_CompilationResultId",
                table: "Solutions",
                column: "CompilationResultId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_IdentityUserId",
                table: "Solutions",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_TaskId",
                table: "Solutions",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ContestId",
                table: "Tasks",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ScorePolicyId",
                table: "Tasks",
                column: "ScorePolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TaskId",
                table: "Tests",
                column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoreRules");

            migrationBuilder.DropTable(
                name: "Solutions");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "CompilationResults");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Contests");

            migrationBuilder.DropTable(
                name: "ScorePolicies");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "227cfeba-55dd-4aa8-9332-b91d3c6929f5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b081e0e-0a2b-4152-abef-ce967fc1c289");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b3885e0-9fb6-4797-9dde-0f3c59c11db1");

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
    }
}
