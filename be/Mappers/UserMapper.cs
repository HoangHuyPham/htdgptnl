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
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role,
                Employee = user.Employee
            };
        }
        
    }
}