using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public String Name { get; set; } = null!;
        public String Description { get; set; } = null!;
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public Guid? EvaluationScheduleId { get; set; }
        public EvaluationSchedule? EvaluationSchedule{ get; set; }
    }
}