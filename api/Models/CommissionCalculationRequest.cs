using System.ComponentModel.DataAnnotations;

namespace AvalphaTechnologies.CommissionCalculator.Models
{
    public class CommissionCalculationRequest
    {
        [Required]
        [Range(0, 1000000, ErrorMessage = "LocalSalesCount must be between 0 and 1,000,000.")]
        public int LocalSalesCount { get; set; }

        [Required]
        [Range(0, 1000000, ErrorMessage = "ForeignSalesCount must be between 0 and 1,000,000.")]
        public int ForeignSalesCount { get; set; }

        [Required]
        [Range(0, 1_000_000_000, ErrorMessage = "AverageSaleAmount must be between 0 and 1,000,000,000.")]
        public decimal AverageSaleAmount { get; set; }
    }
}
