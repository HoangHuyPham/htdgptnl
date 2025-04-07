using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class Process
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}