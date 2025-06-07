using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.DTOs.Role;
using be.DTOs.User;
using be.Models;

namespace be.Services.Interfaces
{
    public interface IEmailService
    {
        public Task SendChangeEmail(User user, string address, string newEmail);
        public Task SendResetPassword(User user, string address, string newEmail);
    }
}