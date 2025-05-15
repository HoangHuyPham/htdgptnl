using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using be.Models;

namespace be.DTOs.Criteria
{
    public class PutCriteriaDTO
    {
        public string? Content { get; set; } = null!;
        public bool? ProofRequired { get; set; } = false;
    }
}