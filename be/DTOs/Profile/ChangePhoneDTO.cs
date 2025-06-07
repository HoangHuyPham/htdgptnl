using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using be.Models;

namespace be.DTOs.Profile
{
    public class ChangePhoneDTO
    {
        public string NewPhone { get; set; } = null!;
    }
}