using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace be.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Processes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkingDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Standard = table.Column<int>(type: "int", nullable: false),
                    Actual = table.Column<int>(type: "int", nullable: false),
                    Hoic = table.Column<int>(type: "int", nullable: false),
                    Pv = table.Column<int>(type: "int", nullable: false),
                    Np = table.Column<int>(type: "int", nullable: false),
                    Suspension = table.Column<int>(type: "int", nullable: false),
                    Written = table.Column<int>(type: "int", nullable: false),
                    Verbal = table.Column<int>(type: "int", nullable: false),
                    Maternity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceEvaluations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvaluationScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEvaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceEvaluations_EvaluationSchedules_EvaluationScheduleId",
                        column: x => x.EvaluationScheduleId,
                        principalTable: "EvaluationSchedules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvaluationScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_EvaluationSchedules_EvaluationScheduleId",
                        column: x => x.EvaluationScheduleId,
                        principalTable: "EvaluationSchedules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BalanceScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WhatScale = table.Column<double>(type: "float", nullable: false),
                    HowScale = table.Column<double>(type: "float", nullable: false),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BalanceScores_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PositionEss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionEss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PositionEss_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BellCurveScore = table.Column<float>(type: "real", nullable: true),
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProcessId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorkingDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_WorkingDetails_WorkingDetailId",
                        column: x => x.WorkingDetailId,
                        principalTable: "WorkingDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalWeight = table.Column<float>(type: "real", nullable: true),
                    PerformanceEvaluationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievements_PerformanceEvaluations_PerformanceEvaluationId",
                        column: x => x.PerformanceEvaluationId,
                        principalTable: "PerformanceEvaluations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleEvaluationSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EvaluationScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleEvaluationSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleEvaluationSchedules_EvaluationSchedules_EvaluationScheduleId",
                        column: x => x.EvaluationScheduleId,
                        principalTable: "EvaluationSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleEvaluationSchedules_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BellCurveScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BellCurveScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BellCurveScores_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEligible = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDetails_Employees_EmployeeId1",
                        column: x => x.EmployeeId1,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AchievementItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Threshold = table.Column<float>(type: "real", nullable: false),
                    Target = table.Column<float>(type: "real", nullable: false),
                    Stretch = table.Column<float>(type: "real", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    AchievementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AchievementItems_Achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Criterias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProofRequired = table.Column<bool>(type: "bit", nullable: true),
                    AchievementItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Criterias_AchievementItems_AchievementItemId",
                        column: x => x.AchievementItemId,
                        principalTable: "AchievementItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EvaluateScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<float>(type: "real", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CriteriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluateScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluateScores_Criterias_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "Criterias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EvaluateScores_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProofImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EvaluateScoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProofImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProofImages_EvaluateScores_EvaluateScoreId",
                        column: x => x.EvaluateScoreId,
                        principalTable: "EvaluateScores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProofImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "EvaluationSchedules",
                columns: new[] { "Id", "Description", "End", "Start", "Status" },
                values: new object[] { new Guid("306c98a6-c520-4ca8-898e-583991a15e0c"), "Lich danh gia nhan vien", new DateTime(2025, 5, 24, 10, 41, 59, 682, DateTimeKind.Local).AddTicks(3994), new DateTime(2025, 5, 21, 10, 41, 59, 682, DateTimeKind.Local).AddTicks(3976), "active" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "EvaluationScheduleId", "Name" },
                values: new object[,]
                {
                    { new Guid("2de0a741-b6bd-4b3c-8ab1-76cd380cfcb5"), null, null, "Manager" },
                    { new Guid("c26b7fcb-9e16-47aa-893e-3ef148de9714"), null, null, "Admin" },
                    { new Guid("c36d9d97-8a11-4c8e-b498-289df49982da"), null, null, "Director" },
                    { new Guid("f80eee5a-eefe-49c6-9a11-2e5b3804a71c"), null, null, "Employee" }
                });

            migrationBuilder.InsertData(
                table: "PerformanceEvaluations",
                columns: new[] { "Id", "CreatedAt", "EvaluationScheduleId", "Name" },
                values: new object[] { new Guid("849571c5-0826-4785-b178-82c286f6740c"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("306c98a6-c520-4ca8-898e-583991a15e0c"), "Đánh giá tháng 6" });

            migrationBuilder.InsertData(
                table: "RoleEvaluationSchedules",
                columns: new[] { "Id", "EvaluationScheduleId", "RoleId" },
                values: new object[] { new Guid("25db27de-c006-4be9-a876-4369db3c3642"), new Guid("306c98a6-c520-4ca8-898e-583991a15e0c"), new Guid("f80eee5a-eefe-49c6-9a11-2e5b3804a71c") });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "Name", "PerformanceEvaluationId", "TotalWeight" },
                values: new object[] { new Guid("865726e2-7cf6-4746-8e30-1fb5cd382c80"), "Core Value", new Guid("849571c5-0826-4785-b178-82c286f6740c"), 100f });

            migrationBuilder.InsertData(
                table: "AchievementItems",
                columns: new[] { "Id", "AchievementId", "Name", "Stretch", "Target", "Threshold", "Weight" },
                values: new object[,]
                {
                    { new Guid("46cd9dac-e4a1-4486-8178-e5e191d5c66b"), new Guid("865726e2-7cf6-4746-8e30-1fb5cd382c80"), "Care", 120f, 100f, 80f, 25f },
                    { new Guid("706dec6e-c701-45f6-8e10-0300ec6f37eb"), new Guid("865726e2-7cf6-4746-8e30-1fb5cd382c80"), "Accountability", 120f, 100f, 80f, 25f },
                    { new Guid("a8304762-04da-4f03-9ca1-fb90eb1c8f22"), new Guid("865726e2-7cf6-4746-8e30-1fb5cd382c80"), "Resilience", 120f, 100f, 80f, 25f },
                    { new Guid("b511664d-3c06-4809-967f-d3d5e9e0a79f"), new Guid("865726e2-7cf6-4746-8e30-1fb5cd382c80"), "Elevating", 120f, 100f, 80f, 25f }
                });

            migrationBuilder.InsertData(
                table: "Criterias",
                columns: new[] { "Id", "AchievementItemId", "Content", "ProofRequired" },
                values: new object[,]
                {
                    { new Guid("0046e287-a51b-4176-93e3-159c480b69be"), new Guid("a8304762-04da-4f03-9ca1-fb90eb1c8f22"), "We believe that together...", false },
                    { new Guid("1a8c7b7f-4e6a-4918-b2d5-e5bd8b8912b9"), new Guid("b511664d-3c06-4809-967f-d3d5e9e0a79f"), "We are a meritocracy...", false },
                    { new Guid("360f3a8b-b2be-46f3-9a7f-dcd1d8ae709a"), new Guid("706dec6e-c701-45f6-8e10-0300ec6f37eb"), "We take responsibility...", false },
                    { new Guid("6138e65d-6262-40da-bff3-25cf72523b20"), new Guid("706dec6e-c701-45f6-8e10-0300ec6f37eb"), "We believe that whatever is...", false },
                    { new Guid("8dff4616-cb11-40d5-b7e0-183860a35cae"), new Guid("46cd9dac-e4a1-4486-8178-e5e191d5c66b"), "We believe in taking at action...", false },
                    { new Guid("aa26abb5-3a8e-442a-a713-194b198c51b7"), new Guid("46cd9dac-e4a1-4486-8178-e5e191d5c66b"), "We believe that fundamentally,...", false },
                    { new Guid("ac1b63e0-e666-4f92-9ead-55c96d6daafe"), new Guid("a8304762-04da-4f03-9ca1-fb90eb1c8f22"), "We don't lose, we only...", false },
                    { new Guid("b81d7540-a13c-438a-a3e2-a72d70a8a696"), new Guid("706dec6e-c701-45f6-8e10-0300ec6f37eb"), "We do what we say...", false },
                    { new Guid("ca2652b1-7f4b-40af-96f0-9fd51a031888"), new Guid("b511664d-3c06-4809-967f-d3d5e9e0a79f"), "We believe in make things better...", false },
                    { new Guid("dce125aa-ddd0-425a-9c14-0066048e1289"), new Guid("a8304762-04da-4f03-9ca1-fb90eb1c8f22"), "When times are tough, we have the...", false },
                    { new Guid("de520adf-c93e-4de0-b9d9-10fdcd670a31"), new Guid("46cd9dac-e4a1-4486-8178-e5e191d5c66b"), "We don't take ourselves...", false },
                    { new Guid("fdd18028-6d7b-4457-877a-dfba83e7fb56"), new Guid("b511664d-3c06-4809-967f-d3d5e9e0a79f"), "We believe in the direct link...", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AchievementItems_AchievementId",
                table: "AchievementItems",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_PerformanceEvaluationId",
                table: "Achievements",
                column: "PerformanceEvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceScores_PositionId",
                table: "BalanceScores",
                column: "PositionId",
                unique: true,
                filter: "[PositionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BellCurveScores_EmployeeId",
                table: "BellCurveScores",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Criterias_AchievementItemId",
                table: "Criterias",
                column: "AchievementItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_EmployeeId1",
                table: "EmployeeDetails",
                column: "EmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GradeId",
                table: "Employees",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_OperationId",
                table: "Employees",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PlantId",
                table: "Employees",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ProcessId",
                table: "Employees",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_WorkingDetailId",
                table: "Employees",
                column: "WorkingDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluateScores_CriteriaId",
                table: "EvaluateScores",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluateScores_EmployeeId",
                table: "EvaluateScores",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEvaluations_EvaluationScheduleId",
                table: "PerformanceEvaluations",
                column: "EvaluationScheduleId",
                unique: true,
                filter: "[EvaluationScheduleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PositionEss_PositionId",
                table: "PositionEss",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProofImages_EvaluateScoreId",
                table: "ProofImages",
                column: "EvaluateScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ProofImages_ImageId",
                table: "ProofImages",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleEvaluationSchedules_EvaluationScheduleId",
                table: "RoleEvaluationSchedules",
                column: "EvaluationScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleEvaluationSchedules_RoleId",
                table: "RoleEvaluationSchedules",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_EvaluationScheduleId",
                table: "Roles",
                column: "EvaluationScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeId",
                table: "Users",
                column: "EmployeeId",
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceScores");

            migrationBuilder.DropTable(
                name: "BellCurveScores");

            migrationBuilder.DropTable(
                name: "EmployeeDetails");

            migrationBuilder.DropTable(
                name: "PositionEss");

            migrationBuilder.DropTable(
                name: "ProofImages");

            migrationBuilder.DropTable(
                name: "RoleEvaluationSchedules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "EvaluateScores");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Criterias");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "AchievementItems");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Processes");

            migrationBuilder.DropTable(
                name: "WorkingDetails");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "PerformanceEvaluations");

            migrationBuilder.DropTable(
                name: "EvaluationSchedules");
        }
    }
}
