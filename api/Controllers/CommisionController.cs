using Microsoft.AspNetCore.Mvc;

namespace AvalphaTechnologies.CommissionCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommisionController : ControllerBase
    {
        [ProducesResponseType(typeof(CommissionCalculationResponse), 200)]
        [HttpPost]
        public IActionResult Calculate(CommissionCalculationRequest calculationRequest)
        {
            // Constants
            const decimal AvalphaLocalCommissionPct = 0.20m;
            const decimal AvalphaForeignCommissionPct = 0.35m;
            const decimal CompetitorLocalCommissionPct = 0.02m;
            const decimal CompetitorForeignCommissionPct = 0.0755m;

            // Input validation
            if (calculationRequest.LocalSalesCount < 0) throw new ArgumentOutOfRangeException(nameof(calculationRequest.LocalSalesCount));
            if (calculationRequest.ForeignSalesCount < 0) throw new ArgumentOutOfRangeException(nameof(calculationRequest.ForeignSalesCount));
            if (calculationRequest.AverageSaleAmount < 0) throw new ArgumentOutOfRangeException(nameof(calculationRequest.AverageSaleAmount));

            decimal localTotalSales = calculationRequest.LocalSalesCount * calculationRequest.AverageSaleAmount;
            decimal foreignTotalSales = calculationRequest.ForeignSalesCount * calculationRequest.AverageSaleAmount;

            decimal avalphaLocal = decimal.Round(AvalphaLocalCommissionPct * localTotalSales, 2, MidpointRounding.AwayFromZero);
            decimal avalphaForeign = decimal.Round(AvalphaForeignCommissionPct * foreignTotalSales, 2, MidpointRounding.AwayFromZero);
            decimal avalphaTotal = avalphaLocal + avalphaForeign;

            decimal competitorLocal = decimal.Round(CompetitorLocalCommissionPct * localTotalSales, 2, MidpointRounding.AwayFromZero);
            decimal competitorForeign = decimal.Round(CompetitorForeignCommissionPct * foreignTotalSales, 2, MidpointRounding.AwayFromZero);
            decimal competitorTotal = competitorLocal + competitorForeign;


            return Ok(new CommissionCalculationResponse()
            {
                AvalphaTechnologiesCommissionAmount = avalphaTotal,
                CompetitorCommissionAmount = competitorTotal
            });
        }
    }

    public class CommissionCalculationRequest
    {
        public int LocalSalesCount { get; set; }
        public int ForeignSalesCount { get; set; }
        public decimal AverageSaleAmount { get; set; }
    }

    public class CommissionCalculationResponse
    {
        public decimal AvalphaTechnologiesCommissionAmount { get; set; }

        public decimal CompetitorCommissionAmount { get; set; }
    }
}
