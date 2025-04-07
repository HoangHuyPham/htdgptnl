using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class Position
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<PositionEs>? PositionEss { get; set; }
        public BalanceScore? BalanceScore{ get; set; }
    }
}