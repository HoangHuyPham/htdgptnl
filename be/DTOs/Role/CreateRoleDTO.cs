using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Models;

namespace be.DTOs.Role
{
    public class CreateRoleDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}