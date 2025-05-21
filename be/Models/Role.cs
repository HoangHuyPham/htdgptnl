using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace be.Models
{
    public class Role
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public string? Name { get; set; } = Guid.NewGuid().ToString();
        public string? Description { get; set; } = null!;
        [JsonIgnore]
        public ICollection<User>? Users { get; set; } = [];
        [JsonIgnore]
        public ICollection<RoleEvaluationSchedule>? RoleEvaluationSchedules { get; set; } = [];
    }
}