using BabyLog.Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BabyLog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        // Gets current customer from JWT token
        // Token comes from BabyFællesskab
        [HttpGet("me")]
        public ActionResult<CurrentCustomer> GetCurrentCustomer()
        {
            // Read JWT token from Authorization header
            var authHeader = Request.Headers.Authorization.ToString();

            // Authorization header is missing
            if (string.IsNullOrWhiteSpace(authHeader))
                return Unauthorized();

            // Remove "Bearer " from token
            var token = authHeader.Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();

            // Read JWT token content
            var jwtToken = handler.ReadJwtToken(token);

            // Read CustomerId from token
            var customerIdClaim = jwtToken.Claims
                .FirstOrDefault(c => c.Type == "CustomerId");

            // CustomerId is missing in token
            if (customerIdClaim == null)
                return Unauthorized();

            var customerId = int.Parse(customerIdClaim.Value);

            // Return logged-in customer information
            var customer = new CurrentCustomer
            {
                CustomerId = customerId,
                FirstName = "Customer",
                LastName = $"#{customerId}"
            };

            return Ok(customer);
        }
    }
}