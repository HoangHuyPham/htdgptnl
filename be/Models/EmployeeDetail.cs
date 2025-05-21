using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class EmployeeDetail
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public string Code { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string StartDate { get; set; } = null!;
        public bool IsEligible { get; set; } = false;
        public Guid? EmployeeId { get; set; }
        public Employee? Employee{ get; set; }
    }
}