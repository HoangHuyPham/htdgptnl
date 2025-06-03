using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class Employee
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public EmployeeDetail? Detail { get; set; }
        public Guid? SupervisorId { get; set; }
        public Employee? Supervisor { get; set; }
        public ICollection<Employee> Employees { get; set; } = [];
        public Guid? UserId { get; set; }
        public User? User{ get; set; }
    }
}