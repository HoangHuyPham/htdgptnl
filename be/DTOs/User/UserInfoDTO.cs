using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.DTOs.User
{
    public class UserInfoDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Username { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? Phone { get; set; } = null!;
        public Guid? RoleId { get; set; }
        public Guid? EmployeeId { get; set; }
    }
}