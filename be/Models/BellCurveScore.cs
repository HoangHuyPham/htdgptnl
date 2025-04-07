using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class BellCurveScore
    {
        public Guid Id { get; set; }
        public double Score { get; set; } = 0;
        public Guid? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}