using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace learn_api_c_sharp.DTOs.Stock
{
    public class UpdateStockRequest
    {
        [Required]
        [MinLength(3, ErrorMessage = "Symbol must be at least 3 characters")]
        [MaxLength(10, ErrorMessage = "Symbol can be over 10 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Company name must be at least 5 characters")]
        [MaxLength(50, ErrorMessage = "Company name can be over 50 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000000)]
        public decimal Price { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDive { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Industry must be at least 5 characters")]
        [MaxLength(20, ErrorMessage = "Industry can be over 280 characters")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 5000000000)]
        public long MarketCap { get; set; }
    }
}