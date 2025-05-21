using be.Models;
using Microsoft.EntityFrameworkCore;

namespace be.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<EvaluationSchedule> EvaluationSchedules { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDetail> EmployeeDetails { get; set; }
        public DbSet<WorkingDetail> WorkingDetails { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<PerformanceEvaluation> PerformanceEvaluations { get; set; }
        public DbSet<BellCurveScore> BellCurveScores { get; set; }
        public DbSet<PositionEs> PositionEss { get; set; }
        public DbSet<BalanceScore> BalanceScores { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<EvaluateScore> EvaluateScores { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProofImage> ProofImages { get; set; }
        public DbSet<AchievementItem> AchievementItems { get; set; }
        public DbSet<RoleEvaluationSchedule> RoleEvaluationSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EvaluateScore>().HasOne(e => e.EmployeeEvaluate).WithMany(e => e.EvaluateScores).HasForeignKey(e => e.EmployeeId);
        
            // Init Role
            Role adminRole = new() { Id = Guid.Parse("c26b7fcb-9e16-47aa-893e-3ef148de9714"), Name = "Admin" };
            Role employeeRole = new() { Id = Guid.Parse("f80eee5a-eefe-49c6-9a11-2e5b3804a71c"), Name = "Employee" };
            Role managerRole = new() { Id = Guid.Parse("2de0a741-b6bd-4b3c-8ab1-76cd380cfcb5"), Name = "Manager" };
            Role directorRole = new() { Id = Guid.Parse("c36d9d97-8a11-4c8e-b498-289df49982da"), Name = "Director" };
            modelBuilder.Entity<Role>(x => x.HasData([adminRole, employeeRole, managerRole, directorRole]));

            // Init EvaluationSchedule
            EvaluationSchedule evaluationSchedule1 = new() { Start = DateTime.Now, End = DateTime.Now.AddDays(3), Description = "Lich danh gia nhan vien", Status = "active" };
            modelBuilder.Entity<EvaluationSchedule>(x => x.HasData([evaluationSchedule1]));

            // Init PerformanceEvaluation form
            PerformanceEvaluation performanceEvaluation1 = new() { Name = "Đánh giá tháng 6", EvaluationScheduleId = evaluationSchedule1.Id, CreatedAt = DateTime.Parse("2025-01-06") };
            modelBuilder.Entity<PerformanceEvaluation>(x => x.HasData([performanceEvaluation1]));
            // Init RoleEvaluationSchedule
            RoleEvaluationSchedule roleEvaluationSchedule1 = new() { RoleId = employeeRole.Id, EvaluationScheduleId = evaluationSchedule1.Id };
            modelBuilder.Entity<RoleEvaluationSchedule>(x => x.HasData([roleEvaluationSchedule1]));
            // Init Achievement
            Achievement achievement1 = new() { Name = "Core Value", PerformanceEvaluationId = performanceEvaluation1.Id, TotalWeight = 100 };
            modelBuilder.Entity<Achievement>(x => x.HasData([achievement1]));
            // Init AchievementItem
            AchievementItem achievementItemCare = new() { Name = "Care", AchievementId = achievement1.Id, Threshold = 80, Target = 100, Stretch = 120, Weight = 25 };
            AchievementItem achievementItemAccountability = new() { Name = "Accountability", AchievementId = achievement1.Id, Threshold = 80, Target = 100, Stretch = 120, Weight = 25 };
            AchievementItem achievementItemResilience = new() { Name = "Resilience", AchievementId = achievement1.Id, Threshold = 80, Target = 100, Stretch = 120, Weight = 25 };
            AchievementItem achievementItemElevating = new() { Name = "Elevating", AchievementId = achievement1.Id, Threshold = 80, Target = 100, Stretch = 120, Weight = 25 };
            modelBuilder.Entity<AchievementItem>(x => x.HasData([achievementItemCare, achievementItemAccountability, achievementItemResilience, achievementItemElevating]));
            // Init Criteria
            Criteria criteria1 = new() { Content = "We believe that fundamentally,...", AchievementItemId = achievementItemCare.Id };
            Criteria criteria2 = new() { Content = "We don't take ourselves...", AchievementItemId = achievementItemCare.Id };
            Criteria criteria3 = new() { Content = "We believe in taking at action...", AchievementItemId = achievementItemCare.Id };

            Criteria criteria4 = new() { Content = "We do what we say...", AchievementItemId = achievementItemAccountability.Id };
            Criteria criteria5 = new() { Content = "We believe that whatever is...", AchievementItemId = achievementItemAccountability.Id };
            Criteria criteria6 = new() { Content = "We take responsibility...", AchievementItemId = achievementItemAccountability.Id };

            Criteria criteria7 = new() { Content = "When times are tough, we have the...", AchievementItemId = achievementItemResilience.Id };
            Criteria criteria8 = new() { Content = "We don't lose, we only...", AchievementItemId = achievementItemResilience.Id };
            Criteria criteria9 = new() { Content = "We believe that together...", AchievementItemId = achievementItemResilience.Id };

            Criteria criteria10 = new() { Content = "We believe in make things better...", AchievementItemId = achievementItemElevating.Id };
            Criteria criteria11 = new() { Content = "We believe in the direct link...", AchievementItemId = achievementItemElevating.Id };
            Criteria criteria12 = new() { Content = "We are a meritocracy...", AchievementItemId = achievementItemElevating.Id };
            modelBuilder.Entity<Criteria>(x => x.HasData([criteria1, criteria2, criteria3, criteria4, criteria5, criteria6, criteria7, criteria8, criteria9, criteria10, criteria11, criteria12]));
        }
    }
}
