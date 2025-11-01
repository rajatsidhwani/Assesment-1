using AvalphaTechnologies.CommissionCalculator.Contracts;
using AvalphaTechnologies.CommissionCalculator.Models;

namespace AvalphaTechnologies.CommissionCalculator.Services
{
    public class CommissionCalculatorService  : ICommisionService
    {
        private const decimal AvalphaLocalCommissionPct = 0.20m;         // 20%
        private const decimal AvalphaForeignCommissionPct = 0.35m;       // 35%
        private const decimal CompetitorLocalCommissionPct = 0.02m;      // 2%
        private const decimal CompetitorForeignCommissionPct = 0.0755m;  // 7.55%

        public CommissionCalculationResponse Calculate(CommissionCalculationRequest calculationRequest)
        {
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


            return new CommissionCalculationResponse()
            {
                AvalphaTechnologiesCommissionAmount = avalphaTotal,
                CompetitorCommissionAmount = competitorTotal
            };
        }

    }
}
