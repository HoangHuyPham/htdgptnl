using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class WorkingDetail
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public int Standard { get; set; } = 0;
        public int Actual { get; set; } = 0;
        public int Hoic { get; set; } = 0;
        public int Pv { get; set; } = 0;
        public int Np { get; set; } = 0;
        public int Suspension { get; set; } = 0;
        public int Written { get; set; } = 0;
        public int Verbal { get; set; } = 0;
        public int Maternity { get; set; } = 0;
        public ICollection<Employee>? Employees { get; set; }
    }
}