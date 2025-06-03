using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Models.Enums;

namespace be.DTOs.Role
{
    public class CreateUserDTO
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; } = String.Empty;
        public string? Phone { get; set; } = String.Empty;
        public Guid? RoleId { get; set; }
    }
}