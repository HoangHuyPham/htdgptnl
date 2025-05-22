using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.DTOs.User;
using be.Models;

namespace be.Mappers
{
    public static class UserMapper
    {
        public static UserInfoDTO getUserInfoDTO(this User user){
            return new UserInfoDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                EmployeeId = user.EmployeeId,
                RoleId = user.RoleId,
            };
        }
    }
}