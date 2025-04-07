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
        public bool IsEligible { get; set; } = false;
        public Grade? Grade{ get; set; }
        public Position? Position{ get; set; }
        public Plant? Plant{ get; set; }
        public Department? Department{ get; set; }
        public Process? Process{ get; set; }
        public Operation? Operation{ get; set; }
        public User? User{ get; set; }
        public PerformanceEvaluation? PerformanceEvaluation{ get; set; }
        public BellCurveScore? BellCurveScore { get; set; }
    }
}