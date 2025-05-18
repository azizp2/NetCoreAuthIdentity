using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MinLength(10)]
        public string Symbol { get; set; } = string.Empty;
        [MinLength(10)]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(0, 1000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.0001, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(100)]
        public string Industry { get; set; } = string.Empty;
        [Range(1, 500000000)]
        public long MarketCap { get; set; }
    }
}
