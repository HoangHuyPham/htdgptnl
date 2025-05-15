using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Helpers
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; } = string.Empty;
    }
}