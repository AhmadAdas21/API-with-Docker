using System.ComponentModel.DataAnnotations;

namespace API_with_DOCKER.dtos
{
    public record AddProduct
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string name { get; init; }
        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string description { get; init; }
        [Required]
        [Range(1, 10000000.00)]
        public decimal price { get; init; }
    }
}
