using System.ComponentModel.DataAnnotations;

namespace API_with_DOCKER.dtos
{
    public record addProduct
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [Range(1,100000000)]
        public decimal Price { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string description { get; set; }

    }
}
