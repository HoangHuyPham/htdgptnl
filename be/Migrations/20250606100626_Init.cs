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
                name: "ValidationTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidationTokens", x => x.Id);
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
                    { new Guid("24f691c4-5cef-4883-9c19-78d19d8b1c5b"), "Office" },
                    { new Guid("32eb03b8-0f09-4bde-88e0-11bb37e57585"), "Engineer" }
                });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("91cec208-1665-4d2f-ba4a-b479a6a94882"), "as1" },
                    { new Guid("bb7a56a7-396b-4e7a-bffb-e92e8a4c422b"), "as2" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("760b93fe-5555-4eca-a7eb-d394838011f9"), "Costing" },
                    { new Guid("aaa03ed0-4c5d-402d-904a-5faf20b4397f"), "Engineer" }
                });

            migrationBuilder.InsertData(
                table: "Operations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("ea28b7df-6ec2-4f2f-bc1e-c7327b5a538b"), "Prototype" },
                    { new Guid("f9117a2d-3373-4b3f-a933-3ddab7dd6975"), "Engineer" }
                });

            migrationBuilder.InsertData(
                table: "PerformanceEvaluations",
                columns: new[] { "Id", "CreatedAt", "End", "Name", "Start" },
                values: new object[] { new Guid("008000f6-186b-4ae9-a543-8b628a99f0d5"), 1749204385L, 1749809185L, "Don danh gia nhan vien 2025 (Behavior)", 1749204385L });

            migrationBuilder.InsertData(
                table: "Plants",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4423e651-d2fd-4de4-bf0f-5b6bbf15d8d7"), "plant 2" },
                    { new Guid("7836ded2-971e-4637-8b1b-be6b48df9ab7"), "plant 1" }
                });

            migrationBuilder.InsertData(
                table: "PositionEs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5ff0b3e2-7ec5-4bb3-9bba-06e64333e5d9"), "Casegoods Drafter Team Leader" },
                    { new Guid("e8f7a96a-b600-48c5-8894-dbe78d52d7aa"), "Casegoods Drafter" }
                });

            migrationBuilder.InsertData(
                table: "Processs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("09db74b3-11eb-4eeb-88e0-2f2c58d3ce49"), "Prototype" },
                    { new Guid("6a139eaa-4ad2-4c51-a31a-06d01d9dc982"), "Engineer" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Level", "Name" },
                values: new object[,]
                {
                    { new Guid("05cf5ce9-8c60-425f-bebc-4c34841b9136"), "No description", 1, "LineManager" },
                    { new Guid("2a19b00a-8753-4d5f-937e-5f3a7815b554"), "No description", 0, "Staff" },
                    { new Guid("6668daea-8563-4074-8cda-9886c93beecf"), "No description", 2, "Director" },
                    { new Guid("9dc56b9f-5646-4296-bfd8-ab7c9bf9fe86"), "No description", 3, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "Name", "PerformanceEvaluationId", "Stretch", "Target", "Threshold", "Weight" },
                values: new object[] { new Guid("31f75965-3202-43fe-a0ce-722ed32097dc"), "Core Value", new Guid("008000f6-186b-4ae9-a543-8b628a99f0d5"), 120f, 100f, 80f, 20f });

            migrationBuilder.InsertData(
                table: "EvaluationSchedules",
                columns: new[] { "Id", "CreatedAt", "Description", "End", "IsSelfEvalution", "PerformanceEvaluationId", "RoleId", "Start" },
                values: new object[,]
                {
                    { new Guid("0139d531-ab7d-4e5f-9d08-3bfe4d3747e5"), 1749204385L, "Vui lòng đánh giá trước hạn, đánh giá của bạn sẽ bị vô hiệu nếu quá hạn", 1749463585L, true, new Guid("008000f6-186b-4ae9-a543-8b628a99f0d5"), new Guid("2a19b00a-8753-4d5f-937e-5f3a7815b554"), 1749204385L },
                    { new Guid("f25c902f-193c-4cd7-9979-fbb1a326d59d"), 1749204385L, "Vui lòng đánh giá trước hạn, đánh giá của bạn sẽ bị vô hiệu nếu quá hạn", 1749636385L, false, new Guid("008000f6-186b-4ae9-a543-8b628a99f0d5"), new Guid("05cf5ce9-8c60-425f-bebc-4c34841b9136"), 1749463585L },
                    { new Guid("f38ba82c-f562-4b9f-a0a9-1e90e77c8c03"), 1749204385L, "Vui lòng đánh giá trước hạn, đánh giá của bạn sẽ bị vô hiệu nếu quá hạn", 1749809185L, false, new Guid("008000f6-186b-4ae9-a543-8b628a99f0d5"), new Guid("6668daea-8563-4074-8cda-9886c93beecf"), 1749636385L }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Phone", "RoleId", "UserName" },
                values: new object[,]
                {
                    { new Guid("2da351a9-f668-4d35-a057-7f67ad8ac550"), "", "$2a$11$1JY3Q.mRHET2IKG3Squ6reEPEBul8a6N52Rqn5LMDgfQK9TeMJqdq", "123456789", new Guid("2a19b00a-8753-4d5f-937e-5f3a7815b554"), "nhanvien1" },
                    { new Guid("53f6e5c7-ad3f-4d4c-b8d9-4128a4d567bb"), "quanly1@gmail.com", "$2a$11$k9bkfbp88ObCtUCurk.IL.n0ma20D4oMXNCGxnfe7fwcNMWqOm9wq", "123456789", new Guid("05cf5ce9-8c60-425f-bebc-4c34841b9136"), "quanly1" },
                    { new Guid("819984b9-637e-41a5-bad8-970963a21222"), "giamdoc1@gmail.com", "$2a$11$QH2UBrl9S0fw.ytc2y4KIOtIgfDziJuDaaT/p1mJxUD/8nWOpRbE6", "123456789", new Guid("6668daea-8563-4074-8cda-9886c93beecf"), "giamdoc1" }
                });

            migrationBuilder.InsertData(
                table: "AchievementItems",
                columns: new[] { "Id", "AchievementId", "Name", "Stretch", "Target", "Threshold", "Weight" },
                values: new object[,]
                {
                    { new Guid("11d4dc49-5ede-4a3b-97c3-c1d55f52d4ab"), new Guid("31f75965-3202-43fe-a0ce-722ed32097dc"), "Care", 120f, 100f, 80f, 25f },
                    { new Guid("50ca81f1-a54b-4375-aec9-cb1748915e0a"), new Guid("31f75965-3202-43fe-a0ce-722ed32097dc"), "Elevating", 120f, 100f, 80f, 25f },
                    { new Guid("7ff51df4-fb6c-42a1-8ae2-ec95c73a4d9b"), new Guid("31f75965-3202-43fe-a0ce-722ed32097dc"), "Accountability", 120f, 100f, 80f, 25f },
                    { new Guid("a7e52710-6b0e-4f84-8eef-f9ce7c5673ee"), new Guid("31f75965-3202-43fe-a0ce-722ed32097dc"), "Resilience", 120f, 100f, 80f, 25f }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "SupervisorId", "UserId" },
                values: new object[] { new Guid("5b1b28c5-22a4-4052-909e-7d22de65c5da"), null, new Guid("819984b9-637e-41a5-bad8-970963a21222") });

            migrationBuilder.InsertData(
                table: "Criterias",
                columns: new[] { "Id", "AchievementItemId", "Content", "EvidenceRequired" },
                values: new object[,]
                {
                    { new Guid("15aa687e-6b33-467e-85a1-98ccf5f223f0"), new Guid("7ff51df4-fb6c-42a1-8ae2-ec95c73a4d9b"), "We believe that whatever is rightly done, however humble, is noble", false },
                    { new Guid("3439bc1d-9b45-4ce5-ae3e-43f06afcfc50"), new Guid("50ca81f1-a54b-4375-aec9-cb1748915e0a"), "We believe in making things better and the continuous pursuit of knowledge", false },
                    { new Guid("3feb171f-d34f-44cc-9da5-2aa12e7aca9e"), new Guid("11d4dc49-5ede-4a3b-97c3-c1d55f52d4ab"), "We believe in taking action every day, to help someone else.", false },
                    { new Guid("461c0791-a865-446e-bf1d-3747e9124df7"), new Guid("a7e52710-6b0e-4f84-8eef-f9ce7c5673ee"), "When times are tough, we have the courage to step up", false },
                    { new Guid("4f4cf4c8-0f66-47d2-aa9c-7de0192334cb"), new Guid("11d4dc49-5ede-4a3b-97c3-c1d55f52d4ab"), "We believe that fundamentally, we are here to look after one another", false },
                    { new Guid("7b2f3d1e-e943-4124-aa1a-a3b130ffda6f"), new Guid("11d4dc49-5ede-4a3b-97c3-c1d55f52d4ab"), "We don't take ourselves too seriously and always follow the 'Golden Rule' of treating others like how you like to be treated;", false },
                    { new Guid("8ebb7393-978e-4702-a3a8-44d1ff3026b5"), new Guid("a7e52710-6b0e-4f84-8eef-f9ce7c5673ee"), "We believe that together, we are stronger", false },
                    { new Guid("945d2515-a459-409b-8101-70b95d8d8dd3"), new Guid("a7e52710-6b0e-4f84-8eef-f9ce7c5673ee"), "We don't lose., we only win or learn", false },
                    { new Guid("9963f4ee-530a-4b82-9768-555fe0e7e0e8"), new Guid("7ff51df4-fb6c-42a1-8ae2-ec95c73a4d9b"), "We take responsibility for the impact we have & take small steps for a better world", false },
                    { new Guid("a54c5654-bf80-49c8-bf81-5028c606a809"), new Guid("50ca81f1-a54b-4375-aec9-cb1748915e0a"), "We believe in the direct link between developing our people, our community & our business", false },
                    { new Guid("a62c5d56-3685-47cd-939d-bcf097bfaddc"), new Guid("7ff51df4-fb6c-42a1-8ae2-ec95c73a4d9b"), "We do what we say we'll do", false },
                    { new Guid("cc3d4106-c28a-4599-bceb-1234c53dc806"), new Guid("50ca81f1-a54b-4375-aec9-cb1748915e0a"), "We are a meritocracy that believes in competency-based progression.", false }
                });

            migrationBuilder.InsertData(
                table: "EmployeeDetails",
                columns: new[] { "Id", "Code", "DepartmentId", "Eligible", "EmployeeId", "FullName", "GradeId", "GroupId", "OperationId", "PlantId", "PositionEId", "ProcessId", "StartDate", "Type" },
                values: new object[] { new Guid("cec3d403-cdb1-44ae-8513-ca7e6f664135"), "12347", new Guid("32eb03b8-0f09-4bde-88e0-11bb37e57585"), true, new Guid("5b1b28c5-22a4-4052-909e-7d22de65c5da"), "Nguyễn Văn C", new Guid("bb7a56a7-396b-4e7a-bffb-e92e8a4c422b"), new Guid("aaa03ed0-4c5d-402d-904a-5faf20b4397f"), new Guid("f9117a2d-3373-4b3f-a933-3ddab7dd6975"), new Guid("7836ded2-971e-4637-8b1b-be6b48df9ab7"), new Guid("5ff0b3e2-7ec5-4bb3-9bba-06e64333e5d9"), new Guid("6a139eaa-4ad2-4c51-a31a-06d01d9dc982"), 1714669200L, "IDL" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "SupervisorId", "UserId" },
                values: new object[] { new Guid("890172b4-aa4b-4079-8986-4c4269cda2f0"), new Guid("5b1b28c5-22a4-4052-909e-7d22de65c5da"), new Guid("53f6e5c7-ad3f-4d4c-b8d9-4128a4d567bb") });

            migrationBuilder.InsertData(
                table: "EmployeeDetails",
                columns: new[] { "Id", "Code", "DepartmentId", "Eligible", "EmployeeId", "FullName", "GradeId", "GroupId", "OperationId", "PlantId", "PositionEId", "ProcessId", "StartDate", "Type" },
                values: new object[] { new Guid("1f509f21-5bb8-40fa-a697-d268dbd87ea9"), "12346", new Guid("32eb03b8-0f09-4bde-88e0-11bb37e57585"), true, new Guid("890172b4-aa4b-4079-8986-4c4269cda2f0"), "Nguyễn Văn B", new Guid("bb7a56a7-396b-4e7a-bffb-e92e8a4c422b"), new Guid("aaa03ed0-4c5d-402d-904a-5faf20b4397f"), new Guid("f9117a2d-3373-4b3f-a933-3ddab7dd6975"), new Guid("4423e651-d2fd-4de4-bf0f-5b6bbf15d8d7"), new Guid("e8f7a96a-b600-48c5-8894-dbe78d52d7aa"), new Guid("6a139eaa-4ad2-4c51-a31a-06d01d9dc982"), 1695229200L, "IDL" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "SupervisorId", "UserId" },
                values: new object[] { new Guid("25600998-561b-4548-a17e-f12877319593"), new Guid("890172b4-aa4b-4079-8986-4c4269cda2f0"), new Guid("2da351a9-f668-4d35-a057-7f67ad8ac550") });

            migrationBuilder.InsertData(
                table: "EmployeeDetails",
                columns: new[] { "Id", "Code", "DepartmentId", "Eligible", "EmployeeId", "FullName", "GradeId", "GroupId", "OperationId", "PlantId", "PositionEId", "ProcessId", "StartDate", "Type" },
                values: new object[] { new Guid("d01661c3-ccf5-4deb-9d33-e32fa699e65c"), "12345", new Guid("32eb03b8-0f09-4bde-88e0-11bb37e57585"), true, new Guid("25600998-561b-4548-a17e-f12877319593"), "Nguyễn Văn A", new Guid("91cec208-1665-4d2f-ba4a-b479a6a94882"), new Guid("aaa03ed0-4c5d-402d-904a-5faf20b4397f"), new Guid("f9117a2d-3373-4b3f-a933-3ddab7dd6975"), new Guid("7836ded2-971e-4637-8b1b-be6b48df9ab7"), new Guid("e8f7a96a-b600-48c5-8894-dbe78d52d7aa"), new Guid("6a139eaa-4ad2-4c51-a31a-06d01d9dc982"), 1655830800L, "IDL" });

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
                name: "ValidationTokens");

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
