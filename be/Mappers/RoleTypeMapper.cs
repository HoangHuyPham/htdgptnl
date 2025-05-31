using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.DTOs.Role;
using be.DTOs.RoleType;
using be.Models;

namespace be.Mappers
{
    public static class RoleTypeMapper
    {
        public static RoleTypeDTO getDTO(this RoleType roleType){
            return new RoleTypeDTO{
                Id = roleType.Id,
                Name = roleType.Name!,
            };
        }
    }
}