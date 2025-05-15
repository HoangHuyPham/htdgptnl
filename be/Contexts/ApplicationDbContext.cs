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
            modelBuilder.Entity<EvaluateScore>().HasOne(e=>e.EmployeeEvaluate).WithMany(e=>e.EvaluateScores).HasForeignKey(e=>e.EmployeeId);
        }
    }
}
