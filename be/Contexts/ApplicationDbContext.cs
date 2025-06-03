using be.Models;
using be.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace be.Contexts
{
    using BCrypt.Net;
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<AchievementItem> AchievementItems { get; set; }
        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDetail> EmployeeDetails { get; set; }
        public DbSet<EvaluationSchedule> EvaluationSchedules { get; set; }
        public DbSet<EvaluationScore> EvaluationScores { get; set; }
        public DbSet<Evidence> Evidences { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<PerformanceEvaluation> PerformanceEvaluations { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<PositionE> PositionEs { get; set; }
        public DbSet<Process> Processs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkingDetail> WorkingDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeDetail>().HasOne(x => x.WorkingDetail).WithOne(x => x.EmployeeDetail).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Employee>().HasOne(x => x.Detail).WithOne(x => x.Employee).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Employee>().HasMany(x => x.Employees).WithOne(x => x.Supervisor).HasForeignKey(x => x.SupervisorId);
            modelBuilder.Entity<User>().HasOne(x => x.Employee).WithOne(x => x.User).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany(x => x.EvaluationScoreSources).WithOne(x => x.Source).HasForeignKey(x => x.SourceId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany(x => x.EvaluationScoreTargets).WithOne(x => x.Target).HasForeignKey(x => x.SourceId).OnDelete(DeleteBehavior.NoAction); // Hạn chế của sql server

            modelBuilder.Entity<PerformanceEvaluation>().HasMany(x => x.Achievements).WithOne(x => x.PerformanceEvaluation).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Achievement>().HasMany(x => x.AchievementItems).WithOne(x => x.Achievement).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<AchievementItem>().HasMany(x => x.Criterias).WithOne(x => x.AchievementItem).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Criteria>().HasMany(x => x.EvaluationScores).WithOne(x => x.Criteria).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<EvaluationScore>().HasMany(x => x.Evidences).WithOne(x => x.EvaluationScore).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Evidence>().HasOne(x => x.Image).WithOne(x => x.Evidence).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PerformanceEvaluation>().HasMany(x => x.EvaluationSchedules).WithOne(x => x.PerformanceEvaluation).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Role>().HasMany(x => x.EvaluationSchedules).WithOne(x => x.Role).OnDelete(DeleteBehavior.Cascade);

            // Data seeder

            // Role
            Role roleStaff = new() { Name = "Staff", Description = "No description", Level = RoleLevel.Staff };
            Role roleLineManager = new() { Name = "LineManager", Description = "No description", Level = RoleLevel.LineManager };
            Role roleDirector = new() { Name = "Director", Description = "No description", Level = RoleLevel.Director };
            Role roleAdmin = new() { Name = "Admin", Description = "No description", Level = RoleLevel.Admin };
            modelBuilder.Entity<Role>().HasData([roleStaff, roleLineManager, roleDirector, roleAdmin]);

            // User
            User user1 = new() { UserName = "nhanvien1", Password = BCrypt.HashPassword("123"), Email = "nhanvien1@gmail.com", Phone = "123456789", RoleId = roleStaff.Id };
            User user2 = new() { UserName = "quanly1", Password = BCrypt.HashPassword("123"), Email = "quanly1@gmail.com", Phone = "123456789", RoleId = roleLineManager.Id };
            User user3 = new() { UserName = "giamdoc1", Password = BCrypt.HashPassword("123"), Email = "giamdoc1@gmail.com", Phone = "123456789", RoleId = roleDirector.Id };
            modelBuilder.Entity<User>().HasData([user1, user2, user3]);

            // Employee
            Employee employee3 = new() { UserId = user3.Id };
            Employee employee2 = new() { UserId = user2.Id, SupervisorId = employee3.Id };
            Employee employee1 = new() { UserId = user1.Id, SupervisorId = employee2.Id };

            modelBuilder.Entity<Employee>().HasData([employee1, employee2, employee3]);

            // Grade
            Grade grade1 = new() { Name = "as1" };
            Grade grade2 = new() { Name = "as2" };
            modelBuilder.Entity<Grade>().HasData([grade1, grade2]);

            // PostionE
            PositionE positionE1 = new() { Name = "Casegoods Drafter" };
            PositionE positionE2 = new() { Name = "Casegoods Drafter Team Leader" };
            modelBuilder.Entity<PositionE>().HasData([positionE1, positionE2]);

            // Plant
            Plant plant1 = new() { Name = "plant 1" };
            Plant plant2 = new() { Name = "plant 2" };
            modelBuilder.Entity<Plant>().HasData([plant1, plant2]);

            // Department
            Department department1 = new() { Name = "Engineer" };
            Department department2 = new() { Name = "Office" };
            modelBuilder.Entity<Department>().HasData([department1, department2]);

            // Process
            Process process1 = new() { Name = "Engineer" };
            Process process2 = new() { Name = "Prototype" };
            modelBuilder.Entity<Process>().HasData([process1, process2]);

            // Operation
            Operation operation1 = new() { Name = "Engineer" };
            Operation operation2 = new() { Name = "Prototype" };
            modelBuilder.Entity<Operation>().HasData([operation1, operation2]);

            // Group
            Group group1 = new() { Name = "Engineer" };
            Group group2 = new() { Name = "Costing" };
            modelBuilder.Entity<Group>().HasData([group1, group2]);

            // EmployeeDetail
            EmployeeDetail employeeDetail1 = new() { Code = "12345", Type = "IDL", DepartmentId = department1.Id, EmployeeId = employee1.Id, PlantId = plant1.Id, OperationId = operation1.Id, ProcessId = process1.Id, PositionEId = positionE1.Id, StartDate = DateTimeOffset.Parse("06-22-2022").ToUnixTimeSeconds() };
            EmployeeDetail employeeDetail2 = new() { Code = "12346", Type = "IDL", DepartmentId = department1.Id, EmployeeId = employee2.Id, PlantId = plant2.Id, OperationId = operation1.Id, ProcessId = process1.Id, PositionEId = positionE1.Id, StartDate = DateTimeOffset.Parse("09-21-2023").ToUnixTimeSeconds() };
            EmployeeDetail employeeDetail3 = new() { Code = "12347", Type = "IDL", DepartmentId = department1.Id, EmployeeId = employee3.Id, PlantId = plant1.Id, OperationId = operation1.Id, ProcessId = process1.Id, PositionEId = positionE2.Id, StartDate = DateTimeOffset.Parse("05-03-2024").ToUnixTimeSeconds() };
            modelBuilder.Entity<EmployeeDetail>().HasData([employeeDetail1, employeeDetail2, employeeDetail3]);

            // PerformanceEvaluation
            PerformanceEvaluation performanceEvaluation1 = new() { Name = "Don danh gia nhan vien 2025 (Behavior)", Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds(), End = DateTimeOffset.UtcNow.AddDays(7).ToUnixTimeSeconds() };
            modelBuilder.Entity<PerformanceEvaluation>().HasData([performanceEvaluation1]);

            // Achievement
            Achievement achievement1 = new() { Name = "Core Value", Weight = 20f, Threshold = 80f, Target = 100f, Stretch = 120f, PerformanceEvaluationId = performanceEvaluation1.Id };
            modelBuilder.Entity<Achievement>().HasData([achievement1]);

            // AchievementItem
            AchievementItem achievementItem1 = new() { Name = "Care", Weight = 25f, Threshold = 80f, Target = 100f, Stretch = 120f, AchievementId = achievement1.Id };
            AchievementItem achievementItem2 = new() { Name = "Accountability", Weight = 25f, Threshold = 80f, Target = 100f, Stretch = 120f, AchievementId = achievement1.Id };
            AchievementItem achievementItem3 = new() { Name = "Resilience", Weight = 25f, Threshold = 80f, Target = 100f, Stretch = 120f, AchievementId = achievement1.Id };
            AchievementItem achievementItem4 = new() { Name = "Elevating", Weight = 25f, Threshold = 80f, Target = 100f, Stretch = 120f, AchievementId = achievement1.Id };
            modelBuilder.Entity<AchievementItem>().HasData([achievementItem1, achievementItem2, achievementItem3, achievementItem4]);

            // Criteria
            Criteria criteria1 = new() { Content = "We believe that fundamentally, we are here to look after one another", AchievementItemId = achievementItem1.Id };
            Criteria criteria2 = new() { Content = "We don't take ourselves too seriously and always follow the 'Golden Rule' of treating others like how you like to be treated;", AchievementItemId = achievementItem1.Id };
            Criteria criteria3 = new() { Content = "We believe in taking action every day, to help someone else.", AchievementItemId = achievementItem1.Id };

            Criteria criteria4 = new() { Content = "We do what we say we'll do", AchievementItemId = achievementItem2.Id };
            Criteria criteria5 = new() { Content = "We believe that whatever is rightly done, however humble, is noble", AchievementItemId = achievementItem2.Id };
            Criteria criteria6 = new() { Content = "We take responsibility for the impact we have & take small steps for a better world", AchievementItemId = achievementItem2.Id };

            Criteria criteria7 = new() { Content = "When times are tough, we have the courage to step up", AchievementItemId = achievementItem3.Id };
            Criteria criteria8 = new() { Content = "We don't lose., we only win or learn", AchievementItemId = achievementItem3.Id };
            Criteria criteria9 = new() { Content = "We believe that together, we are stronger", AchievementItemId = achievementItem3.Id };

            Criteria criteria10 = new() { Content = "We believe in making things better and the continuous pursuit of knowledge", AchievementItemId = achievementItem4.Id };
            Criteria criteria11 = new() { Content = "We believe in the direct link between developing our people, our community & our business", AchievementItemId = achievementItem4.Id };
            Criteria criteria12 = new() { Content = "We are a meritocracy that believes in competency-based progression.", AchievementItemId = achievementItem4.Id };
            modelBuilder.Entity<Criteria>().HasData([criteria1, criteria2, criteria3, criteria4, criteria5, criteria6, criteria7, criteria8, criteria9, criteria10, criteria11, criteria12]);

            // EvaluationSchedule
            EvaluationSchedule evaluationSchedule1 = new() { PerformanceEvaluationId = performanceEvaluation1.Id, IsSelfEvalution = true, RoleId = roleStaff.Id, Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds(), End = DateTimeOffset.UtcNow.AddDays(3).ToUnixTimeSeconds(), Description = "Vui lòng đánh giá trước hạn, đánh giá của bạn sẽ bị vô hiệu nếu quá hạn" };
            EvaluationSchedule evaluationSchedule2 = new() { PerformanceEvaluationId = performanceEvaluation1.Id, RoleId = roleLineManager.Id, Start = DateTimeOffset.UtcNow.AddDays(3).ToUnixTimeSeconds(), End = DateTimeOffset.UtcNow.AddDays(5).ToUnixTimeSeconds(), Description = "Vui lòng đánh giá trước hạn, đánh giá của bạn sẽ bị vô hiệu nếu quá hạn" };
            EvaluationSchedule evaluationSchedule3 = new() { PerformanceEvaluationId = performanceEvaluation1.Id, RoleId = roleDirector.Id, Start = DateTimeOffset.UtcNow.AddDays(5).ToUnixTimeSeconds(), End = DateTimeOffset.UtcNow.AddDays(7).ToUnixTimeSeconds(), Description = "Vui lòng đánh giá trước hạn, đánh giá của bạn sẽ bị vô hiệu nếu quá hạn" };

            modelBuilder.Entity<EvaluationSchedule>().HasData([evaluationSchedule1, evaluationSchedule2, evaluationSchedule3]);
        }
    }
}
