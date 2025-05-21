using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class Process
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public string? Name { get; set; } = Guid.NewGuid().ToString();
        public ICollection<Employee>? Employees { get; set; } = [];
    }
}