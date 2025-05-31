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
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
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
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTypes", x => x.Id);
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
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Threshold = table.Column<float>(type: "real", nullable: false),
                    Target = table.Column<float>(type: "real", nullable: false),
                    Stretch = table.Column<float>(type: "real", nullable: false),
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
                name: "EvaluationSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerformanceEvaluationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationSchedules_PerformanceEvaluations_PerformanceEvaluationId",
                        column: x => x.PerformanceEvaluationId,
                        principalTable: "PerformanceEvaluations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "PositionEs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionEs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PositionEs_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BellCurveScore = table.Column<float>(type: "real", nullable: true),
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProcessId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorkingDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                        name: "FK_Employees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
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
                name: "AchievementItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "RoleSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EvaluationScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleSchedules_EvaluationSchedules_EvaluationScheduleId",
                        column: x => x.EvaluationScheduleId,
                        principalTable: "EvaluationSchedules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleSchedules_RoleTypes_RoleTypeId",
                        column: x => x.RoleTypeId,
                        principalTable: "RoleTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleSchedules_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
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
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDetails_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvaluationScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_EvaluationSchedules_EvaluationScheduleId",
                        column: x => x.EvaluationScheduleId,
                        principalTable: "EvaluationSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_RoleSchedules_RoleScheduleId",
                        column: x => x.RoleScheduleId,
                        principalTable: "RoleSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<float>(type: "real", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TargetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CriteriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SourceRoleTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationScores_Criterias_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "Criterias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EvaluationScores_RoleTypes_SourceRoleTypeId",
                        column: x => x.SourceRoleTypeId,
                        principalTable: "RoleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationScores_Users_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationScores_Users_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_ProofImages_EvaluationScores_EvaluateScoreId",
                        column: x => x.EvaluateScoreId,
                        principalTable: "EvaluationScores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProofImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("21084582-636a-4b7d-9a2f-ff2bcdc506fa"), "Engineering" },
                    { new Guid("843a424c-5b43-4083-aa8a-9abfe550482d"), "Creative" }
                });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4a01c3d6-237f-4585-af30-0a580c52dd5a"), "AS1" },
                    { new Guid("f81d53ad-f9e7-48ba-89b0-b4dfb59cb59a"), "AS0" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("6d69a063-8b04-425f-85a9-56fc012d7fee"), "Engineering" },
                    { new Guid("fe37abbf-316f-4319-aec9-d31cdbcc037a"), "Creative" }
                });

            migrationBuilder.InsertData(
                table: "Operations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("07f5552d-540a-4d62-a3cb-df927e2f8878"), "Creative" },
                    { new Guid("6fa5a4df-bb44-4804-8782-6aaa3b6732c9"), "Engineering" }
                });

            migrationBuilder.InsertData(
                table: "PerformanceEvaluations",
                columns: new[] { "Id", "CreatedAt", "EvaluationScheduleId", "Name" },
                values: new object[] { new Guid("6943b635-0a14-480c-b3d2-954efd482bd3"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Đánh giá tháng 6" });

            migrationBuilder.InsertData(
                table: "Plants",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3dc445ae-bbea-4ebb-8681-abee9602ce2c"), "Plant 1" },
                    { new Guid("a65b25d3-4f3e-4ad1-b995-5c1e5ae53361"), "Plant 2" }
                });

            migrationBuilder.InsertData(
                table: "PositionEs",
                columns: new[] { "Id", "Name", "PositionId" },
                values: new object[] { new Guid("a24cb6f6-83f0-41ec-85f4-de750554cbdf"), "Lead Photographer", null });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1f14453a-75c6-4d4d-9bae-5ac494c1e29d"), "Senior Manager" },
                    { new Guid("722d108a-fdcc-41a8-aebb-50f502308340"), "Manager" },
                    { new Guid("a4719c40-9b19-4057-956a-9d7964c2315f"), "Supervisor" },
                    { new Guid("aad0d4b8-b8f6-412e-9611-1ddb7250bc12"), "Staff" },
                    { new Guid("f02218b0-8a44-4ae9-aa11-5fd47021d00b"), "Senior SuperVisor" }
                });

            migrationBuilder.InsertData(
                table: "Processes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0758ef0e-1b4e-413d-948f-22df618862af"), "HS Engineering" },
                    { new Guid("b68557d7-0fb4-4072-aae5-440b32a2e4ad"), "Creative" }
                });

            migrationBuilder.InsertData(
                table: "RoleTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2b0bccd1-8cbb-4ed8-a2b1-34518ae7292a"), "Last" },
                    { new Guid("9d15496d-5596-4590-ac43-06e9ad65e714"), "Self" },
                    { new Guid("df761b61-22a3-4bbd-ab32-fb93e344ae7e"), "Next" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Level", "Name" },
                values: new object[,]
                {
                    { new Guid("b8010dc0-70b9-43ad-a39b-e68d383ad990"), null, 0, "Line Manager" },
                    { new Guid("c26b7fcb-9e16-47aa-893e-3ef148de9714"), null, 0, "Admin" },
                    { new Guid("e1f3912a-c92d-4447-b4d7-c7ebff0880fe"), null, 0, "Director" },
                    { new Guid("f80eee5a-eefe-49c6-9a11-2e5b3804a71c"), null, 0, "Staff" }
                });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "Name", "PerformanceEvaluationId", "Stretch", "Target", "Threshold", "TotalWeight" },
                values: new object[] { new Guid("dbbc3f12-5cf4-420f-93b9-ae79f8748a20"), "Core Value", new Guid("6943b635-0a14-480c-b3d2-954efd482bd3"), 120f, 100f, 80f, 100f });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BellCurveScore", "DepartmentId", "EmployeeDetailId", "EmployeeId", "FullName", "GradeId", "GroupId", "OperationId", "PlantId", "PositionId", "ProcessId", "Type", "WorkingDetailId" },
                values: new object[] { new Guid("b1ab6af7-a60f-49e6-a25a-d8a9f04b4750"), 0f, new Guid("843a424c-5b43-4083-aa8a-9abfe550482d"), null, null, "Nguyen Van B", new Guid("f81d53ad-f9e7-48ba-89b0-b4dfb59cb59a"), new Guid("fe37abbf-316f-4319-aec9-d31cdbcc037a"), new Guid("07f5552d-540a-4d62-a3cb-df927e2f8878"), new Guid("3dc445ae-bbea-4ebb-8681-abee9602ce2c"), new Guid("a4719c40-9b19-4057-956a-9d7964c2315f"), null, null, null });

            migrationBuilder.InsertData(
                table: "EvaluationSchedules",
                columns: new[] { "Id", "PerformanceEvaluationId", "ScheduleId" },
                values: new object[] { new Guid("4bbd3859-15e9-41f3-a191-02c9dbfc99f0"), new Guid("6943b635-0a14-480c-b3d2-954efd482bd3"), null });

            migrationBuilder.InsertData(
                table: "PositionEs",
                columns: new[] { "Id", "Name", "PositionId" },
                values: new object[] { new Guid("a4065b88-ac42-4210-85bd-72d7ebb1ebe4"), "3D Renderer", new Guid("aad0d4b8-b8f6-412e-9611-1ddb7250bc12") });

            migrationBuilder.InsertData(
                table: "AchievementItems",
                columns: new[] { "Id", "AchievementId", "Name", "Weight" },
                values: new object[,]
                {
                    { new Guid("04162c7f-00af-466d-a23c-ab5be4e97923"), new Guid("dbbc3f12-5cf4-420f-93b9-ae79f8748a20"), "Elevating", 25f },
                    { new Guid("c2e9f3b0-eae7-4c93-b001-d9c0525cc46e"), new Guid("dbbc3f12-5cf4-420f-93b9-ae79f8748a20"), "Resilience", 25f },
                    { new Guid("d7f702e1-5d85-4e50-8afd-a670d683e76d"), new Guid("dbbc3f12-5cf4-420f-93b9-ae79f8748a20"), "Care", 25f },
                    { new Guid("f94de203-602b-44ed-ad0a-6e6a544443a7"), new Guid("dbbc3f12-5cf4-420f-93b9-ae79f8748a20"), "Accountability", 25f }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BellCurveScore", "DepartmentId", "EmployeeDetailId", "EmployeeId", "FullName", "GradeId", "GroupId", "OperationId", "PlantId", "PositionId", "ProcessId", "Type", "WorkingDetailId" },
                values: new object[] { new Guid("ea146c09-405b-43e2-8469-cc5d5229e246"), 0f, new Guid("843a424c-5b43-4083-aa8a-9abfe550482d"), null, new Guid("b1ab6af7-a60f-49e6-a25a-d8a9f04b4750"), "Nguyen Van A", new Guid("f81d53ad-f9e7-48ba-89b0-b4dfb59cb59a"), new Guid("fe37abbf-316f-4319-aec9-d31cdbcc037a"), new Guid("07f5552d-540a-4d62-a3cb-df927e2f8878"), new Guid("3dc445ae-bbea-4ebb-8681-abee9602ce2c"), new Guid("aad0d4b8-b8f6-412e-9611-1ddb7250bc12"), null, null, null });

            migrationBuilder.InsertData(
                table: "RoleSchedules",
                columns: new[] { "Id", "EvaluationScheduleId", "RoleId", "RoleTypeId", "ScheduleId" },
                values: new object[,]
                {
                    { new Guid("3eca66a0-f4f4-4362-8cb6-4ccf0b03dd54"), new Guid("4bbd3859-15e9-41f3-a191-02c9dbfc99f0"), new Guid("b8010dc0-70b9-43ad-a39b-e68d383ad990"), new Guid("df761b61-22a3-4bbd-ab32-fb93e344ae7e"), null },
                    { new Guid("850487be-cc0b-4914-bbeb-f0e46796c3e8"), new Guid("4bbd3859-15e9-41f3-a191-02c9dbfc99f0"), new Guid("f80eee5a-eefe-49c6-9a11-2e5b3804a71c"), new Guid("9d15496d-5596-4590-ac43-06e9ad65e714"), null },
                    { new Guid("b9f5ad63-45b5-4340-b5bf-50dc0b25b5cd"), new Guid("4bbd3859-15e9-41f3-a191-02c9dbfc99f0"), new Guid("e1f3912a-c92d-4447-b4d7-c7ebff0880fe"), new Guid("2b0bccd1-8cbb-4ed8-a2b1-34518ae7292a"), null }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "Description", "End", "EvaluationScheduleId", "RoleScheduleId", "Start" },
                values: new object[] { new Guid("3f3b2117-84aa-4af6-be8d-07795892af54"), "", new DateTime(2025, 6, 3, 2, 49, 29, 858, DateTimeKind.Utc).AddTicks(262), new Guid("4bbd3859-15e9-41f3-a191-02c9dbfc99f0"), null, new DateTime(2025, 5, 27, 2, 49, 29, 858, DateTimeKind.Utc).AddTicks(261) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "EmployeeId", "Password", "Phone", "RoleId", "Username" },
                values: new object[] { new Guid("c76aa230-5e83-43d7-9be6-8dd7c9db50c2"), "quanly1@gmail.com", new Guid("b1ab6af7-a60f-49e6-a25a-d8a9f04b4750"), "$2a$11$N.PTgKTrSRYtvrCpoQXn9u6GDI01nj/kERqthvErJGWloy8L45roK", "123456789", new Guid("b8010dc0-70b9-43ad-a39b-e68d383ad990"), "quanly1" });

            migrationBuilder.InsertData(
                table: "Criterias",
                columns: new[] { "Id", "AchievementItemId", "Content", "ProofRequired" },
                values: new object[,]
                {
                    { new Guid("411922a1-3c77-4dbb-8ef7-def47e0bef91"), new Guid("f94de203-602b-44ed-ad0a-6e6a544443a7"), "We take responsibility...", false },
                    { new Guid("534986c8-ef84-4de4-866f-eec29d5cd664"), new Guid("c2e9f3b0-eae7-4c93-b001-d9c0525cc46e"), "We don't lose, we only...", false },
                    { new Guid("54c21b71-5656-42fb-89b8-2e60431d23cd"), new Guid("c2e9f3b0-eae7-4c93-b001-d9c0525cc46e"), "When times are tough, we have the...", false },
                    { new Guid("567edfc2-6394-4161-9646-c0a5af25bfb4"), new Guid("04162c7f-00af-466d-a23c-ab5be4e97923"), "We believe in make things better...", false },
                    { new Guid("5dc0ecb7-ada8-4aa7-8770-af010fc8563c"), new Guid("f94de203-602b-44ed-ad0a-6e6a544443a7"), "We do what we say...", false },
                    { new Guid("5e591a58-c992-4a1d-9bad-f95fef871165"), new Guid("f94de203-602b-44ed-ad0a-6e6a544443a7"), "We believe that whatever is...", false },
                    { new Guid("67fe13a2-9b12-4893-b234-fadff6da9da9"), new Guid("d7f702e1-5d85-4e50-8afd-a670d683e76d"), "We believe that fundamentally,...", false },
                    { new Guid("6b7d25c6-61c6-486b-8365-a2ceb5ca81c6"), new Guid("c2e9f3b0-eae7-4c93-b001-d9c0525cc46e"), "We believe that together...", false },
                    { new Guid("744d4ee2-018d-463d-8a58-5cec1db8937f"), new Guid("04162c7f-00af-466d-a23c-ab5be4e97923"), "We believe in the direct link...", false },
                    { new Guid("969573bb-12f8-4524-a71d-59ce329c7219"), new Guid("d7f702e1-5d85-4e50-8afd-a670d683e76d"), "We believe in taking at action...", false },
                    { new Guid("a85ace94-78d6-4e6c-a2ab-c8b1037acc3d"), new Guid("d7f702e1-5d85-4e50-8afd-a670d683e76d"), "We don't take ourselves...", false },
                    { new Guid("d897ed25-8c26-42fe-8df8-443b00e67ed9"), new Guid("04162c7f-00af-466d-a23c-ab5be4e97923"), "We are a meritocracy...", false }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "Description", "End", "EvaluationScheduleId", "RoleScheduleId", "Start" },
                values: new object[,]
                {
                    { new Guid("6e66db90-eae1-47bb-a041-d7d18f17f1b1"), "", new DateTime(2025, 5, 29, 2, 49, 29, 858, DateTimeKind.Utc).AddTicks(269), null, new Guid("850487be-cc0b-4914-bbeb-f0e46796c3e8"), new DateTime(2025, 5, 27, 2, 49, 29, 858, DateTimeKind.Utc).AddTicks(269) },
                    { new Guid("abba22f8-08a8-48d0-8bf1-3f17da09a8b7"), "", new DateTime(2025, 5, 29, 2, 49, 29, 858, DateTimeKind.Utc).AddTicks(273), null, new Guid("3eca66a0-f4f4-4362-8cb6-4ccf0b03dd54"), new DateTime(2025, 5, 27, 2, 49, 29, 858, DateTimeKind.Utc).AddTicks(273) },
                    { new Guid("b996b0fa-2250-4abb-aedc-8f06aef70e16"), "", new DateTime(2025, 5, 30, 2, 49, 29, 858, DateTimeKind.Utc).AddTicks(276), null, new Guid("b9f5ad63-45b5-4340-b5bf-50dc0b25b5cd"), new DateTime(2025, 5, 27, 2, 49, 29, 858, DateTimeKind.Utc).AddTicks(275) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "EmployeeId", "Password", "Phone", "RoleId", "Username" },
                values: new object[] { new Guid("e5079700-2f29-411a-b815-bf31e8faed0f"), "nhanvien1@gmail.com", new Guid("ea146c09-405b-43e2-8469-cc5d5229e246"), "$2a$11$N.PTgKTrSRYtvrCpoQXn9u6GDI01nj/kERqthvErJGWloy8L45roK", "123456789", new Guid("f80eee5a-eefe-49c6-9a11-2e5b3804a71c"), "nhanvien1" });

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
                name: "IX_EmployeeDetails_EmployeeId",
                table: "EmployeeDetails",
                column: "EmployeeId",
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeId",
                table: "Employees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GradeId",
                table: "Employees",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GroupId",
                table: "Employees",
                column: "GroupId");

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
                name: "IX_EvaluationSchedules_PerformanceEvaluationId",
                table: "EvaluationSchedules",
                column: "PerformanceEvaluationId",
                unique: true,
                filter: "[PerformanceEvaluationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationScores_CriteriaId",
                table: "EvaluationScores",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationScores_SourceId",
                table: "EvaluationScores",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationScores_SourceRoleTypeId",
                table: "EvaluationScores",
                column: "SourceRoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationScores_TargetId",
                table: "EvaluationScores",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionEs_PositionId",
                table: "PositionEs",
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
                name: "IX_RoleSchedules_EvaluationScheduleId",
                table: "RoleSchedules",
                column: "EvaluationScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleSchedules_RoleId",
                table: "RoleSchedules",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleSchedules_RoleTypeId",
                table: "RoleSchedules",
                column: "RoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_EvaluationScheduleId",
                table: "Schedules",
                column: "EvaluationScheduleId",
                unique: true,
                filter: "[EvaluationScheduleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_RoleScheduleId",
                table: "Schedules",
                column: "RoleScheduleId",
                unique: true,
                filter: "[RoleScheduleId] IS NOT NULL");

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
                name: "PositionEs");

            migrationBuilder.DropTable(
                name: "ProofImages");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "EvaluationScores");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "RoleSchedules");

            migrationBuilder.DropTable(
                name: "Criterias");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "EvaluationSchedules");

            migrationBuilder.DropTable(
                name: "RoleTypes");

            migrationBuilder.DropTable(
                name: "AchievementItems");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Groups");

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
                name: "PerformanceEvaluations");
        }
    }
}
