using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Helpers
{
    public class PaginationQuery
    {
        public int Total { get; set; } = 0;
        public int Page { get; set;} = 1;
        public int Limit { get; set; } = 20;
        public string Sort { get; set; } = String.Empty;
    }
}