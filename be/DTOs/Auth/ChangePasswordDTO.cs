using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using be.Models;

namespace be.DTOs.Auth
{
    public class ChangePasswordDTO
    {
        public string Username { get; set; } = null!;
        public string OldPassword { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}