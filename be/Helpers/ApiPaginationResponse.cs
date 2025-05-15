using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Helpers
{
    public class ApiPaginationResponse<T> : ApiResponse<T>
    {
        public PaginationMetadata Pagination { get; set; } = new PaginationMetadata();
    }
}