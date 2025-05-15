using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class Grade
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = Guid.NewGuid().ToString();
        public ICollection<Employee>? Employees { get; set; } = [];
    }
}