using EcommereAPI.DomainModels;
using EcommereAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommereAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "SellerPolicy")]
    public class SellerController : ControllerBase
    {
        private readonly ISellerRepository _adminRepository;

        public SellerController(ISellerRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }


        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {

            var allDbUsers = await _adminRepository.GetAllUsers();

            if (allDbUsers == null)
            {
                return NotFound(); // No users found
            }

            return Ok(allDbUsers); // Users found
        }
    }
}
