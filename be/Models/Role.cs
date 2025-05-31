using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace be.Models
{
    public class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; } = Guid.NewGuid().ToString();
        public string? Description { get; set; } = null!;
        public int? Level { get; set;} = 0;
        [JsonIgnore]
        public ICollection<User>? Users { get; set; } = [];
        [JsonIgnore]
        public ICollection<RoleSchedule>? RoleSchedules { get; set; } = [];
    }
}