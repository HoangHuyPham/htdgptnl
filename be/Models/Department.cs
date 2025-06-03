using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class Department
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = String.Empty;
        public ICollection<EmployeeDetail> EmployeeDetails { get; set; } = [];
    }
}