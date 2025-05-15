using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public float? BellCurveScore { get; set; } = 0;
        public Guid? GradeId{ get; set; }
        public Guid? PositionId{ get; set; }
        public Guid? PlantId{ get; set; }
        public Guid? DepartmentId{ get; set; }
        public Guid? ProcessId{ get; set; }
        public Guid? OperationId{ get; set; }
        public Guid? EmployeeDetailId{ get; set; }
        public Guid? WorkingDetailId{ get; set; }
        public User? User{ get; set; }
        public ICollection<EvaluateScore> EvaluateScores { get; set; } = [];
    }
}