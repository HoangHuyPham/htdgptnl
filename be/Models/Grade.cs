using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace be.Models
{
    public class Grade
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = String.Empty;
        [JsonIgnore]
        public ICollection<EmployeeDetail> EmployeeDetails { get; set; } = [];
    }
}