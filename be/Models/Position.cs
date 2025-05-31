using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace be.Models
{
    public class Position
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; } = Guid.NewGuid().ToString();
        [JsonIgnore]
        public ICollection<PositionE>? PositionEs { get; set; } = [];
        public ICollection<Employee>? Employees { get; set; } = [];
        public BalanceScore? BalanceScore { get; set; }
    }
}