using AvalphaTechnologies.CommissionCalculator.Controllers;
using AvalphaTechnologies.CommissionCalculator.Models;

namespace AvalphaTechnologies.CommissionCalculator.Contracts
{
    public interface ICommisionService
    {
        CommissionCalculationResponse Calculate(CommissionCalculationRequest request);
    }
}
