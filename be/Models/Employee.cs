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
        public Grade? Grade{ get; set; }
        public Position? Position{ get; set; }
        public Plant? Plant{ get; set; }
        public Department? Department{ get; set; }
        public Process? Process{ get; set; }
        public User? User{ get; set; }
        public Operation? Operation{ get; set; }
        public BellCurveScore? BellCurveScore { get; set; }
        public EmployeeDetail? EmployeeDetail{ get; set; }
        public WorkingDetail? WorkingDetail{ get; set; }
        public ICollection<EvaluateScore> EvaluateScores { get; set; } = [];
    }
}