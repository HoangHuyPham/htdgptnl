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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start = table.Column<long>(type: "bigint", nullable: false),
                    End = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PositionEs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionEs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Processs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Threshold = table.Column<float>(type: "real", nullable: false),
                    Target = table.Column<float>(type: "real", nullable: false),
                    Stretch = table.Column<float>(type: "real", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    PerformanceEvaluationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievements_PerformanceEvaluations_PerformanceEvaluationId",
                        column: x => x.PerformanceEvaluationId,
                        principalTable: "PerformanceEvaluations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSelfEvalution = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start = table.Column<long>(type: "bigint", nullable: false),
                    End = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    PerformanceEvaluationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationSchedules_PerformanceEvaluations_PerformanceEvaluationId",
                        column: x => x.PerformanceEvaluationId,
                        principalTable: "PerformanceEvaluations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluationSchedules_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupervisorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Criterias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvidenceRequired = table.Column<bool>(type: "bit", nullable: false),
                    AchievementItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Criterias_AchievementItems_AchievementItemId",
                        column: x => x.AchievementItemId,
                        principalTable: "AchievementItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<long>(type: "bigint", nullable: false),
                    Eligible = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PositionEId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProcessId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDetails_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeDetails_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeDetails_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeDetails_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeDetails_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeDetails_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeDetails_PositionEs_PositionEId",
                        column: x => x.PositionEId,
                        principalTable: "PositionEs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeDetails_Processs_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EvaluationScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<float>(type: "real", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    SourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SourceId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TargetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CriteriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationScores_Criterias_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "Criterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluationScores_Users_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EvaluationScores_Users_SourceId1",
                        column: x => x.SourceId1,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkingDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StandardWorkingDay = table.Column<int>(type: "int", nullable: false),
                    ActualWorkingDay = table.Column<int>(type: "int", nullable: false),
                    Hoic = table.Column<int>(type: "int", nullable: false),
                    Pv = table.Column<int>(type: "int", nullable: false),
                    Np = table.Column<int>(type: "int", nullable: false),
                    Suspension = table.Column<int>(type: "int", nullable: false),
                    Written = table.Column<int>(type: "int", nullable: false),
                    Verbal = table.Column<int>(type: "int", nullable: false),
                    Maternity = table.Column<int>(type: "int", nullable: false),
                    EmployeeDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingDetails_EmployeeDetails_EmployeeDetailId",
                        column: x => x.EmployeeDetailId,
                        principalTable: "EmployeeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evidences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvidenceRequired = table.Column<bool>(type: "bit", nullable: false),
                    EvaluationScoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evidences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evidences_EvaluationScores_EvaluationScoreId",
                        column: x => x.EvaluationScoreId,
                        principalTable: "EvaluationScores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<float>(type: "real", nullable: false),
                    EvidenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Evidences_EvidenceId",
                        column: x => x.EvidenceId,
                        principalTable: "Evidences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("8622af63-705d-43b1-9ccd-76e26fef6cc2"), "Office" },
                    { new Guid("a0e46464-bfd9-4948-bac4-7e6cbf31bf9c"), "Engineer" }
                });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("6e6ec5e3-8495-4786-a44a-cf14c936afd6"), "as2" },
                    { new Guid("7d7d6134-39c8-4a7e-a1c4-1667eaec30c8"), "as1" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5c607477-eed2-4018-938d-b9e2ce44f019"), "Costing" },
                    { new Guid("d2f6bf3a-ecea-4754-911a-19fdffc89132"), "Engineer" }
                });

            migrationBuilder.InsertData(
                table: "Operations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("cfba6725-9026-418b-b531-804a0f694de9"), "Engineer" },
                    { new Guid("e0421e47-b754-4722-a212-414af122dff4"), "Prototype" }
                });

            migrationBuilder.InsertData(
                table: "PerformanceEvaluations",
                columns: new[] { "Id", "CreatedAt", "End", "Name", "Start" },
                values: new object[] { new Guid("0c237d95-af63-4d9c-a9c1-78b8285aa79c"), 1748761480L, 1749366280L, "Don danh gia nhan vien 2025 (Behavior)", 1748761480L });

            migrationBuilder.InsertData(
                table: "Plants",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("919d0deb-95d2-4889-a805-9a9e998c5618"), "plant 1" },
                    { new Guid("c1b4d1b8-c087-4f3a-8a5f-a1f9b401434b"), "plant 2" }
                });

            migrationBuilder.InsertData(
                table: "PositionEs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("081dc95b-1662-480b-851d-1426d89a43bf"), "Casegoods Drafter" },
                    { new Guid("984280b5-38f2-4b0f-8f4c-a5ec8fe41da6"), "Casegoods Drafter Team Leader" }
                });

            migrationBuilder.InsertData(
                table: "Processs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("17e774f9-62ae-4f1d-9f1d-750f2163f2b7"), "Engineer" },
                    { new Guid("4942fab8-ba6c-4e1d-9682-7f854a261c5e"), "Prototype" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Level", "Name" },
                values: new object[,]
                {
                    { new Guid("30863c40-98e3-4813-a166-1dcfdb1b6b3c"), "No description", 0, "Staff" },
                    { new Guid("50fa98a4-2c0c-4f1f-b23f-2790009d5612"), "No description", 2, "Director" },
                    { new Guid("6d3b56e1-821b-43c9-83f2-af1a1566d097"), "No description", 3, "Admin" },
                    { new Guid("756e37d5-f7ee-403d-bd82-f3265d3e0c91"), "No description", 1, "LineManager" }
                });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "Name", "PerformanceEvaluationId", "Stretch", "Target", "Threshold", "Weight" },
                values: new object[] { new Guid("b425352a-670d-49d5-a1b9-58be75e0c6b9"), "Core Value", new Guid("0c237d95-af63-4d9c-a9c1-78b8285aa79c"), 120f, 100f, 80f, 20f });

            migrationBuilder.InsertData(
                table: "EvaluationSchedules",
                columns: new[] { "Id", "CreatedAt", "Description", "End", "IsSelfEvalution", "PerformanceEvaluationId", "RoleId", "Start" },
                values: new object[,]
                {
                    { new Guid("1b66012c-92ee-47ab-977c-6ebcd53d5932"), 1748761480L, "Vui lòng đánh giá trước hạn, đánh giá của bạn sẽ bị vô hiệu nếu quá hạn", 1749366280L, false, new Guid("0c237d95-af63-4d9c-a9c1-78b8285aa79c"), new Guid("50fa98a4-2c0c-4f1f-b23f-2790009d5612"), 1749193480L },
                    { new Guid("98bafd56-2f3a-427c-be42-d188fb275e83"), 1748761480L, "Vui lòng đánh giá trước hạn, đánh giá của bạn sẽ bị vô hiệu nếu quá hạn", 1749020680L, true, new Guid("0c237d95-af63-4d9c-a9c1-78b8285aa79c"), new Guid("30863c40-98e3-4813-a166-1dcfdb1b6b3c"), 1748761480L },
                    { new Guid("c8a0d712-2844-451e-8f25-33d3ec2a3cd3"), 1748761480L, "Vui lòng đánh giá trước hạn, đánh giá của bạn sẽ bị vô hiệu nếu quá hạn", 1749193480L, false, new Guid("0c237d95-af63-4d9c-a9c1-78b8285aa79c"), new Guid("756e37d5-f7ee-403d-bd82-f3265d3e0c91"), 1749020680L }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Phone", "RoleId", "UserName" },
                values: new object[,]
                {
                    { new Guid("29b7f60c-249e-4fbb-abaf-b6d13fa1adf2"), "quanly1@gmail.com", "$2a$11$yfVIdTsqxArm4nx7suWMKunV3BzXGrc3T1k9A88ZUriktZeLnaQ6W", "123456789", new Guid("756e37d5-f7ee-403d-bd82-f3265d3e0c91"), "quanly1" },
                    { new Guid("34e07b8a-648b-4380-8872-dca827bc9524"), "giamdoc1@gmail.com", "$2a$11$JUZYOYbCRcdHLUohlvDZVezgS8HDfCFHKQqQn5zkGDKnMnrXKfdR.", "123456789", new Guid("50fa98a4-2c0c-4f1f-b23f-2790009d5612"), "giamdoc1" },
                    { new Guid("ce7e4268-aaa1-48e4-8dc7-ef268ce8c93c"), "nhanvien1@gmail.com", "$2a$11$Fym7WajUkdmJ31SAIj6UeO/TadSkRXXmoHwT/xKC/JhtrWSnpsfxW", "123456789", new Guid("30863c40-98e3-4813-a166-1dcfdb1b6b3c"), "nhanvien1" }
                });

            migrationBuilder.InsertData(
                table: "AchievementItems",
                columns: new[] { "Id", "AchievementId", "Name", "Stretch", "Target", "Threshold", "Weight" },
                values: new object[,]
                {
                    { new Guid("0fe22e23-be23-4290-94d3-33214dac7f6a"), new Guid("b425352a-670d-49d5-a1b9-58be75e0c6b9"), "Resilience", 120f, 100f, 80f, 25f },
                    { new Guid("48ded0fd-4850-4bfa-b084-5726017535ed"), new Guid("b425352a-670d-49d5-a1b9-58be75e0c6b9"), "Accountability", 120f, 100f, 80f, 25f },
                    { new Guid("ca16db53-40cb-44c8-8d6d-04c0afe20c06"), new Guid("b425352a-670d-49d5-a1b9-58be75e0c6b9"), "Care", 120f, 100f, 80f, 25f },
                    { new Guid("d34e1c45-3eff-4f95-8556-09ec73b13a5a"), new Guid("b425352a-670d-49d5-a1b9-58be75e0c6b9"), "Elevating", 120f, 100f, 80f, 25f }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "SupervisorId", "UserId" },
                values: new object[] { new Guid("0f2824fd-2992-411a-84ca-8506a8ed4439"), null, new Guid("34e07b8a-648b-4380-8872-dca827bc9524") });

            migrationBuilder.InsertData(
                table: "Criterias",
                columns: new[] { "Id", "AchievementItemId", "Content", "EvidenceRequired" },
                values: new object[,]
                {
                    { new Guid("04685700-23a3-4efa-8e46-b52f917f6b72"), new Guid("ca16db53-40cb-44c8-8d6d-04c0afe20c06"), "We don't take ourselves too seriously and always follow the 'Golden Rule' of treating others like how you like to be treated;", false },
                    { new Guid("0d629639-e01c-4f0f-94d6-ef33e6c615f8"), new Guid("ca16db53-40cb-44c8-8d6d-04c0afe20c06"), "We believe that fundamentally, we are here to look after one another", false },
                    { new Guid("2a115a54-dbcb-4402-9cb0-711eaad0d6de"), new Guid("ca16db53-40cb-44c8-8d6d-04c0afe20c06"), "We believe in taking action every day, to help someone else.", false },
                    { new Guid("4d4a5d3e-c354-4136-8268-b6927a95db88"), new Guid("48ded0fd-4850-4bfa-b084-5726017535ed"), "We believe that whatever is rightly done, however humble, is noble", false },
                    { new Guid("824d2d5a-6856-46fa-8e4d-91028a781e0f"), new Guid("d34e1c45-3eff-4f95-8556-09ec73b13a5a"), "We are a meritocracy that believes in competency-based progression.", false },
                    { new Guid("8805eb52-693d-4794-8d67-77e8a57e3f29"), new Guid("48ded0fd-4850-4bfa-b084-5726017535ed"), "We do what we say we'll do", false },
                    { new Guid("a530ecea-77b7-4b5b-89fc-36331ed3aae6"), new Guid("0fe22e23-be23-4290-94d3-33214dac7f6a"), "We believe that together, we are stronger", false },
                    { new Guid("b9e84832-7e7e-4cba-85a5-3431c0919c82"), new Guid("0fe22e23-be23-4290-94d3-33214dac7f6a"), "We don't lose., we only win or learn", false },
                    { new Guid("c774c619-e1f3-42ae-93f2-9fc87e4cea10"), new Guid("48ded0fd-4850-4bfa-b084-5726017535ed"), "We take responsibility for the impact we have & take small steps for a better world", false },
                    { new Guid("d120d2ca-8c89-4509-b09a-f57bfedbd271"), new Guid("d34e1c45-3eff-4f95-8556-09ec73b13a5a"), "We believe in the direct link between developing our people, our community & our business", false },
                    { new Guid("d347adf2-3ffe-4c3f-92e9-4974ee5da399"), new Guid("0fe22e23-be23-4290-94d3-33214dac7f6a"), "When times are tough, we have the courage to step up", false },
                    { new Guid("e2ebd762-ba04-4d6d-861f-92ed4ff1be56"), new Guid("d34e1c45-3eff-4f95-8556-09ec73b13a5a"), "We believe in making things better and the continuous pursuit of knowledge", false }
                });

            migrationBuilder.InsertData(
                table: "EmployeeDetails",
                columns: new[] { "Id", "Code", "DepartmentId", "Eligible", "EmployeeId", "FullName", "GradeId", "GroupId", "OperationId", "PlantId", "PositionEId", "ProcessId", "StartDate", "Type" },
                values: new object[] { new Guid("e65b8dda-4164-4ea9-9c11-e6971ad183cf"), "12347", new Guid("a0e46464-bfd9-4948-bac4-7e6cbf31bf9c"), true, new Guid("0f2824fd-2992-411a-84ca-8506a8ed4439"), "", null, null, new Guid("cfba6725-9026-418b-b531-804a0f694de9"), new Guid("919d0deb-95d2-4889-a805-9a9e998c5618"), new Guid("984280b5-38f2-4b0f-8f4c-a5ec8fe41da6"), new Guid("17e774f9-62ae-4f1d-9f1d-750f2163f2b7"), 1714669200L, "IDL" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "SupervisorId", "UserId" },
                values: new object[] { new Guid("4b756510-4c4a-47ab-9e7a-e996ef549b93"), new Guid("0f2824fd-2992-411a-84ca-8506a8ed4439"), new Guid("29b7f60c-249e-4fbb-abaf-b6d13fa1adf2") });

            migrationBuilder.InsertData(
                table: "EmployeeDetails",
                columns: new[] { "Id", "Code", "DepartmentId", "Eligible", "EmployeeId", "FullName", "GradeId", "GroupId", "OperationId", "PlantId", "PositionEId", "ProcessId", "StartDate", "Type" },
                values: new object[] { new Guid("17b29b37-a852-4062-a512-a546ba7ebd4d"), "12346", new Guid("a0e46464-bfd9-4948-bac4-7e6cbf31bf9c"), true, new Guid("4b756510-4c4a-47ab-9e7a-e996ef549b93"), "", null, null, new Guid("cfba6725-9026-418b-b531-804a0f694de9"), new Guid("c1b4d1b8-c087-4f3a-8a5f-a1f9b401434b"), new Guid("081dc95b-1662-480b-851d-1426d89a43bf"), new Guid("17e774f9-62ae-4f1d-9f1d-750f2163f2b7"), 1695229200L, "IDL" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "SupervisorId", "UserId" },
                values: new object[] { new Guid("f3f133bf-d083-43a3-adc5-ad4edc6a7d6f"), new Guid("4b756510-4c4a-47ab-9e7a-e996ef549b93"), new Guid("ce7e4268-aaa1-48e4-8dc7-ef268ce8c93c") });

            migrationBuilder.InsertData(
                table: "EmployeeDetails",
                columns: new[] { "Id", "Code", "DepartmentId", "Eligible", "EmployeeId", "FullName", "GradeId", "GroupId", "OperationId", "PlantId", "PositionEId", "ProcessId", "StartDate", "Type" },
                values: new object[] { new Guid("7c1eb0cf-110c-4547-850b-b6f08b3c0821"), "12345", new Guid("a0e46464-bfd9-4948-bac4-7e6cbf31bf9c"), true, new Guid("f3f133bf-d083-43a3-adc5-ad4edc6a7d6f"), "", null, null, new Guid("cfba6725-9026-418b-b531-804a0f694de9"), new Guid("919d0deb-95d2-4889-a805-9a9e998c5618"), new Guid("081dc95b-1662-480b-851d-1426d89a43bf"), new Guid("17e774f9-62ae-4f1d-9f1d-750f2163f2b7"), 1655830800L, "IDL" });

            migrationBuilder.CreateIndex(
                name: "IX_AchievementItems_AchievementId",
                table: "AchievementItems",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_PerformanceEvaluationId",
                table: "Achievements",
                column: "PerformanceEvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_Criterias_AchievementItemId",
                table: "Criterias",
                column: "AchievementItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_DepartmentId",
                table: "EmployeeDetails",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_EmployeeId",
                table: "EmployeeDetails",
                column: "EmployeeId",
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_GradeId",
                table: "EmployeeDetails",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_GroupId",
                table: "EmployeeDetails",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_OperationId",
                table: "EmployeeDetails",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_PlantId",
                table: "EmployeeDetails",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_PositionEId",
                table: "EmployeeDetails",
                column: "PositionEId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_ProcessId",
                table: "EmployeeDetails",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SupervisorId",
                table: "Employees",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationSchedules_PerformanceEvaluationId",
                table: "EvaluationSchedules",
                column: "PerformanceEvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationSchedules_RoleId",
                table: "EvaluationSchedules",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationScores_CriteriaId",
                table: "EvaluationScores",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationScores_SourceId",
                table: "EvaluationScores",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationScores_SourceId1",
                table: "EvaluationScores",
                column: "SourceId1");

            migrationBuilder.CreateIndex(
                name: "IX_Evidences_EvaluationScoreId",
                table: "Evidences",
                column: "EvaluationScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_EvidenceId",
                table: "Images",
                column: "EvidenceId",
                unique: true,
                filter: "[EvidenceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDetails_EmployeeDetailId",
                table: "WorkingDetails",
                column: "EmployeeDetailId",
                unique: true,
                filter: "[EmployeeDetailId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationSchedules");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "WorkingDetails");

            migrationBuilder.DropTable(
                name: "Evidences");

            migrationBuilder.DropTable(
                name: "EmployeeDetails");

            migrationBuilder.DropTable(
                name: "EvaluationScores");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "PositionEs");

            migrationBuilder.DropTable(
                name: "Processs");

            migrationBuilder.DropTable(
                name: "Criterias");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AchievementItems");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "PerformanceEvaluations");
        }
    }
}
