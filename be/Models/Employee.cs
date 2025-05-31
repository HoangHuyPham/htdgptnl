using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace be.Models
{
    public class Employee
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Type { get; set; } = null!;
        public string? FullName { get; set; } = null!;
        public float? BellCurveScore { get; set; } = 0;
        public Guid? GradeId { get; set; }
        public Grade? Grade { get; set; }
        public Guid? PositionId { get; set; }
        public Position? Position { get; set; }
        public Guid? PlantId { get; set; }
        public Plant? Plant { get; set; }
        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public Guid? ProcessId { get; set; }
        public Process? Process { get; set; }
        public Guid? OperationId { get; set; }
        public Operation? Operation { get; set; }
        public Guid? EmployeeDetailId { get; set; }
        public EmployeeDetail? EmployeeDetail { get; set; }
        public Guid? WorkingDetailId { get; set; }
        public WorkingDetail? WorkingDetail { get; }
        public Guid? GroupId { get; set; }
        public Group? Group { get; set; }
        public Guid? EmployeeId { get; set; }
        public Employee? Supervisor { get; set; }
        [JsonIgnore]
        public ICollection<Employee>? Employees { get; set; } = [];

        [JsonIgnore]
        public User? User { get; set; }
    }
}