using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Models;

namespace be.DTOs.User
{
    public class CreateUserDTO
    {
        public string Username {get; set;} = null!;
        public string Password {get; set;} = null!;
        public string? Email {get; set;} = null!;
        public string? Phone {get; set;} = null!;
        public Guid? RoleId {get; set;} = null!;
        public Guid? EmployeeId {get; set;}
    }
}