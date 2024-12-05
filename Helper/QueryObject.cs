using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn_api_c_sharp.Helper
{
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set;} = null;
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
    }
}