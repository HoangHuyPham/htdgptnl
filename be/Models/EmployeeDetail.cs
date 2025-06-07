using Newtonsoft.Json;

namespace be.Models
{
    public class EmployeeDetail
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public string FullName { get; set; } = String.Empty;
        public long StartDate { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        public bool Eligible { get; set; } = true;
        
        public WorkingDetail? WorkingDetail { get; set; }
        
        public Guid? EmployeeId { get; set; }
        [JsonIgnore]
        public Employee? Employee { get; set; }

        public Guid? GradeId { get; set; }
        public Grade? Grade { get; set; }

        public Guid? PositionEId { get; set; }
        public PositionE? PositionE { get; set; }

        public Guid? PlantId { get; set; }
        public Plant? Plant { get; set; }

        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public Guid? ProcessId { get; set; }
        public Process? Process { get; set; }

        public Guid? OperationId { get; set; }
        public Operation? Operation { get; set; }

        public Guid? GroupId { get; set; }
        public Group? Group { get; set; }
    }
}