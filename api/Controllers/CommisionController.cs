using AvalphaTechnologies.CommissionCalculator.Contracts;
using AvalphaTechnologies.CommissionCalculator.Models;
using Microsoft.AspNetCore.Mvc;

namespace AvalphaTechnologies.CommissionCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommisionController : ControllerBase
    {
        private readonly ICommisionService _commisionService;
        public CommisionController(ICommisionService commisionService)
        {
            _commisionService = commisionService;
        }


        [ProducesResponseType(typeof(CommissionCalculationResponse), 200)]
        [HttpPost]
        public IActionResult Calculate(CommissionCalculationRequest calculationRequest)
        {
            CommissionCalculationResponse response = _commisionService.Calculate(calculationRequest);
            return Ok(response);
        }
    }
}
