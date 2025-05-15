using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.DTOs.Role;
using be.Models;

namespace be.Mappers
{
    public static class RoleMapper
    {
        public static RoleDTO getDTO(this Role role){
            return new RoleDTO{
                Id = role.Id,
                Name = role.Name!,
                Description = role.Description,
            };
        }
    }
}