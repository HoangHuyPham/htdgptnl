using Newtonsoft.Json;

namespace be.Models
{
    public class Department
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = String.Empty;
        [JsonIgnore]
        public ICollection<EmployeeDetail> EmployeeDetails { get; set; } = [];
    }
}