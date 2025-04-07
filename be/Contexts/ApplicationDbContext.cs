
using be.Models;
using Microsoft.EntityFrameworkCore;

namespace be.Controllers;
class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options) {
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<EvaluationSchedule> EvaluationSchedules { get; set;}
    DbSet<Employee> Employees { get; set; }
    DbSet<EmployeeDetail> EmployeeDetails { get; set; }
    DbSet<WorkingDetail> WorkingDetails { get; set; }
    DbSet<Grade> Grades { get; set; }
    DbSet<Position> Positions { get; set; }
    DbSet<Plant> Plants { get; set; }
    DbSet<Department> Departments { get; set; }
    DbSet<Process> Processes { get; set; }
    DbSet<Operation> Operations{ get; set; }
    DbSet<PerformanceEvaluation> PerformanceEvaluations{ get; set; }
    DbSet<BellCurveScore> BellCurveScores{ get; set;}
    DbSet<PositionEs> PositionEss{ get; set; }
    DbSet<BalanceScore> BalanceScores{ get; set; }
    DbSet<ArchivementCriteria> ArchivementCriterias{ get; set; }
    DbSet<Archievement> Archievements{ get; set; }
    DbSet<Criteria> Criterias{ get; set; }
}