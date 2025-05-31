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
        public DbSet<PositionE> PositionEs { get; set; }
        public DbSet<BalanceScore> BalanceScores { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<EvaluationScore> EvaluationScores { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProofImage> ProofImages { get; set; }
        public DbSet<AchievementItem> AchievementItems { get; set; }
        // public DbSet<RoleEvaluationSchedule> RoleEvaluationSchedules { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<RoleSchedule> RoleSchedules { get; set; }
        public DbSet<RoleType> RoleTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EvaluationScore>().HasOne(x => x.Source).WithMany(x => x.EvaluationScoreSources).HasForeignKey(x => x.SourceId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EvaluationScore>().HasOne(x => x.Target).WithMany(x => x.EvaluationScoreTargets).HasForeignKey(x => x.TargetId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EvaluationScore>().HasOne(x => x.SourceRoleType).WithMany(x => x.EvaluationScores).HasForeignKey(x => x.SourceRoleTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EvaluationSchedule>().HasOne(e => e.Schedule).WithOne(e => e.EvaluationSchedule).HasForeignKey<Schedule>(e => e.EvaluationScheduleId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RoleSchedule>().HasOne(e => e.Schedule).WithOne(e => e.RoleSchedule).HasForeignKey<Schedule>(e => e.RoleScheduleId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Employee>().HasOne(e => e.EmployeeDetail).WithOne(e => e.Employee).HasForeignKey<EmployeeDetail>(e => e.EmployeeId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PerformanceEvaluation>().HasOne(e => e.EvaluationSchedule).WithOne(e => e.PerformanceEvaluation).HasForeignKey<EvaluationSchedule>(e => e.PerformanceEvaluationId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Employee>().HasMany(e => e.Employees).WithOne(e => e.Supervisor).HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Restrict);

            // Init Role
            Role AdminRole = new() { Id = Guid.Parse("c26b7fcb-9e16-47aa-893e-3ef148de9714"), Name = "Admin" };
            Role StaffRole = new() { Id = Guid.Parse("f80eee5a-eefe-49c6-9a11-2e5b3804a71c"), Name = "Staff" };
            Role LineManagerRole = new() { Id = Guid.Parse("b8010dc0-70b9-43ad-a39b-e68d383ad990"), Name = "Line Manager" };
            Role DirectorRole = new() { Id = Guid.Parse("e1f3912a-c92d-4447-b4d7-c7ebff0880fe"), Name = "Director" };
            modelBuilder.Entity<Role>(x => x.HasData([AdminRole, StaffRole, LineManagerRole, DirectorRole]));

            // Init RoleType
            RoleType RoleTypeSelf = new() { Name = "Self" };
            RoleType RoleTypeNext = new() { Name = "Next" };
            RoleType RoleTypeLast = new() { Name = "Last" };
            modelBuilder.Entity<RoleType>(x => x.HasData([RoleTypeSelf, RoleTypeNext, RoleTypeLast]));

            // Init Plant
            Plant plant1 = new() { Name = "Plant 1" };
            Plant plant2 = new() { Name = "Plant 2" };
            modelBuilder.Entity<Plant>(x => x.HasData([plant1, plant2]));

            // Init Plant
            Grade Grade1 = new() { Name = "AS0" };
            Grade Grade2 = new() { Name = "AS1" };
            modelBuilder.Entity<Grade>(x => x.HasData([Grade1, Grade2]));

            // Init Department
            Operation Operation1 = new() { Name = "Creative" };
            Operation Operation2 = new() { Name = "Engineering" };
            modelBuilder.Entity<Operation>(x => x.HasData([Operation1, Operation2]));

            // Init Department
            Department Department1 = new() { Name = "Creative" };
            Department Department2 = new() { Name = "Engineering" };
            modelBuilder.Entity<Department>(x => x.HasData([Department1, Department2]));

            // Init Process
            Process Process1 = new() { Name = "Creative" };
            Process Process2 = new() { Name = "HS Engineering" };
            modelBuilder.Entity<Process>(x => x.HasData([Process1, Process2]));

            // Init Group
            Group Group1 = new() { Name = "Creative" };
            Group Group2 = new() { Name = "Engineering" };
            modelBuilder.Entity<Group>(x => x.HasData([Group1, Group2]));

            // Init Position
            Position StaffPosition = new() { Name = "Staff" };
            Position SpecialistPosition = new() { Name = "Specialist" };
            Position SupervisorPosition = new() { Name = "Supervisor" };
            Position SeniorSupervisorPosition = new() { Name = "Senior SuperVisor" };
            Position ManagerPosition = new() { Name = "Manager" };
            Position SeniorManagerPosition = new() { Name = "Senior Manager" };
            modelBuilder.Entity<Position>(x => x.HasData([StaffPosition, SupervisorPosition, SeniorSupervisorPosition, ManagerPosition, SeniorManagerPosition]));

            // Init Operation
            PositionE PositionE1 = new() { Name = "3D Renderer", PositionId = StaffPosition.Id };
            PositionE PositionE2 = new() { Name = "Lead Photographer" };
            modelBuilder.Entity<PositionE>(x => x.HasData([PositionE1, PositionE2]));

            // Init PerformanceEvaluation form
            PerformanceEvaluation PerformanceEvaluation1 = new() { Name = "Đánh giá tháng 6", CreatedAt = DateTime.Parse("2025-01-06") };
            modelBuilder.Entity<PerformanceEvaluation>(x => x.HasData([PerformanceEvaluation1]));

            // Init Evaluation Schedule
            EvaluationSchedule EvaluationSchedule1 = new() { PerformanceEvaluationId = PerformanceEvaluation1.Id };
            modelBuilder.Entity<EvaluationSchedule>(x => x.HasData([EvaluationSchedule1]));

            // Init RoleSchedule
            RoleSchedule RoleScheduleStaff1 = new() { RoleId = StaffRole.Id, RoleTypeId = RoleTypeSelf.Id, EvaluationScheduleId = EvaluationSchedule1.Id };
            RoleSchedule RoleScheduleLineManager1 = new() { RoleId = LineManagerRole.Id, RoleTypeId = RoleTypeNext.Id, EvaluationScheduleId = EvaluationSchedule1.Id };
            RoleSchedule RoleScheduleDirector1 = new() { RoleId = DirectorRole.Id, RoleTypeId = RoleTypeLast.Id, EvaluationScheduleId = EvaluationSchedule1.Id };
            modelBuilder.Entity<RoleSchedule>(x => x.HasData([RoleScheduleStaff1, RoleScheduleLineManager1, RoleScheduleDirector1]));

            // Init Schedule
            Schedule Schedule1 = new() { Start = DateTime.UtcNow, End = DateTime.UtcNow.AddDays(7), EvaluationScheduleId = EvaluationSchedule1.Id };
            Schedule Schedule2 = new() { Start = DateTime.UtcNow, End = DateTime.UtcNow.AddDays(2), RoleScheduleId = RoleScheduleStaff1.Id };
            Schedule Schedule3 = new() { Start = DateTime.UtcNow, End = DateTime.UtcNow.AddDays(2), RoleScheduleId = RoleScheduleLineManager1.Id };
            Schedule Schedule4 = new() { Start = DateTime.UtcNow, End = DateTime.UtcNow.AddDays(3), RoleScheduleId = RoleScheduleDirector1.Id };

            modelBuilder.Entity<Schedule>(x => x.HasData([Schedule1, Schedule2, Schedule3, Schedule4]));



            // Init RoleEvaluationSchedule
            // RoleEvaluationSchedule roleEvaluationSchedule1 = new() { RoleId = StaffRole.Id, EvaluationScheduleId = evaluationSchedule1.Id };
            // modelBuilder.Entity<RoleEvaluationSchedule>(x => x.HasData([roleEvaluationSchedule1]));

            // Init Achievement
            Achievement achievement1 = new() { Name = "Core Value", PerformanceEvaluationId = PerformanceEvaluation1.Id, TotalWeight = 100, Threshold = 80, Target = 100, Stretch = 120, };
            modelBuilder.Entity<Achievement>(x => x.HasData([achievement1]));
            // Init AchievementItem
            AchievementItem achievementItemCare = new() { Name = "Care", AchievementId = achievement1.Id, Weight = 25 };
            AchievementItem achievementItemAccountability = new() { Name = "Accountability", AchievementId = achievement1.Id, Weight = 25 };
            AchievementItem achievementItemResilience = new() { Name = "Resilience", AchievementId = achievement1.Id, Weight = 25 };
            AchievementItem achievementItemElevating = new() { Name = "Elevating", AchievementId = achievement1.Id, Weight = 25 };
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

            // Init Employee
            Employee EmployeeSupervisor1 = new() { FullName = "Nguyen Van B", DepartmentId = Department1.Id, GradeId = Grade1.Id, OperationId = Operation1.Id, GroupId = Group1.Id, PlantId = plant1.Id, PositionId = SupervisorPosition.Id };
            modelBuilder.Entity<Employee>(x => x.HasData([EmployeeSupervisor1]));

            Employee Employee1 = new() { FullName = "Nguyen Van A", DepartmentId = Department1.Id, GradeId = Grade1.Id, OperationId = Operation1.Id, GroupId = Group1.Id, PlantId = plant1.Id, PositionId = StaffPosition.Id, EmployeeId = EmployeeSupervisor1.Id };
            modelBuilder.Entity<Employee>(x => x.HasData([Employee1]));

            // Init User
            User Nhanvien1 = new() { Username = "nhanvien1", Password = "$2a$11$N.PTgKTrSRYtvrCpoQXn9u6GDI01nj/kERqthvErJGWloy8L45roK", Email = "nhanvien1@gmail.com", Phone = "123456789", RoleId = StaffRole.Id, EmployeeId = Employee1.Id };
            modelBuilder.Entity<User>(x => x.HasData([Nhanvien1]));

            User QuanLy1 = new() { Username = "quanly1", Password = "$2a$11$N.PTgKTrSRYtvrCpoQXn9u6GDI01nj/kERqthvErJGWloy8L45roK", Email = "quanly1@gmail.com", Phone = "123456789", RoleId = LineManagerRole.Id, EmployeeId=EmployeeSupervisor1.Id};
            modelBuilder.Entity<User>(x => x.HasData([QuanLy1]));
        }
    }
}
