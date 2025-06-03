using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace be.DTOs.User
{
    using be.Models;
    public class UserInfoDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Role? Role { get; set; }
    }
}