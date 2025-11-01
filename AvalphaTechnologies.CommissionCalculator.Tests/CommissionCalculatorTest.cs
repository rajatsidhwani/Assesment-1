using AvalphaTechnologies.CommissionCalculator.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AvalphaTechnologies.CommissionCalculator.Tests
{
    public class CommissionCalculatorTests
    {
        private readonly CommisionController _controller;

        public CommissionCalculatorTests()
        {
            _controller = new CommisionController();
        }

        [Fact]
        public void Calculate_ValidInput_ReturnsExpectedCommissionAmounts()
        {
            // Arrange
            var request = new CommissionCalculationRequest
            {
                LocalSalesCount = 10,
                ForeignSalesCount = 5,
                AverageSaleAmount = 1000m
            };

            // Act
            var result = _controller.Calculate(request) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var response = Assert.IsType<CommissionCalculationResponse>(result.Value);
            Assert.NotNull(response);

            // Expected calculations
            decimal expectedAvalpha =
                (request.LocalSalesCount * request.AverageSaleAmount * 0.20m) +
                (request.ForeignSalesCount * request.AverageSaleAmount * 0.35m);

            decimal expectedCompetitor =
                (request.LocalSalesCount * request.AverageSaleAmount * 0.02m) +
                (request.ForeignSalesCount * request.AverageSaleAmount * 0.0755m);

            Assert.Equal(expectedAvalpha, response.AvalphaTechnologiesCommissionAmount);
            Assert.Equal(expectedCompetitor, response.CompetitorCommissionAmount);
        }

        [Theory]
        [InlineData(-1, 5, 1000)]
        [InlineData(10, -5, 1000)]
        [InlineData(10, 5, -1000)]
        public void Calculate_InvalidInput_ThrowsArgumentOutOfRangeException(
            int localSales, int foreignSales, decimal avgSale)
        {
            // Arrange
            var request = new CommissionCalculationRequest
            {
                LocalSalesCount = localSales,
                ForeignSalesCount = foreignSales,
                AverageSaleAmount = avgSale
            };

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _controller.Calculate(request));
        }

        [Fact]
        public void Calculate_ZeroSales_ReturnsZeroCommissions()
        {
            // Arrange
            var request = new CommissionCalculationRequest
            {
                LocalSalesCount = 0,
                ForeignSalesCount = 0,
                AverageSaleAmount = 1000m
            };

            // Act
            var result = _controller.Calculate(request) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var response = Assert.IsType<CommissionCalculationResponse>(result.Value);

            Assert.Equal(0m, response.AvalphaTechnologiesCommissionAmount);
            Assert.Equal(0m, response.CompetitorCommissionAmount);
        }
    }
}