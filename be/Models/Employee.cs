using Newtonsoft.Json;

namespace be.Models
{
    public class Employee
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public EmployeeDetail? Detail { get; set; }
        public Guid? SupervisorId { get; set; }
        public Employee? Supervisor { get; set; }
        [JsonIgnore]
        public ICollection<Employee> Employees { get; set; } = [];
        public Guid? UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
    }
}