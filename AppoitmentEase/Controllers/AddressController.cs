using AutoMapper;
using EcommereAPI.Data;
using EcommereAPI.DomainModels;
using EcommereAPI.Helpers;
using EcommereAPI.ServiceModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommereAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ProjectEcommerceContext _context;
        private readonly IMapper _mapper;

        public AddressController(ProjectEcommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
            var addresses = await _context.AddressDM.ToListAsync();
            if(addresses == null || addresses.Count == 0)
            {
                return NotFound("Oops no Address Found...");
            }
            var _addresses = addresses.Select(a=> new AddressDM
            {
                AddressId = a.AddressId,
                Street = a.Street,
                State = a.State,
                City = a.City,
                ZipCode = a.ZipCode,
                Country = a.Country
            }).ToList();
            return Ok(_addresses);
        }
        [HttpGet("byId")]
        public async Task<IActionResult> GetAddressById([FromQuery] int AddressId)
        {
            var address =await _context.AddressDM.SingleOrDefaultAsync(a=> a.AddressId == AddressId);
            if(address == null)
            {
                return NotFound($"Address with ID: {AddressId} is not Available... Try Another ID....");
            }
            return Ok(address);
        }
        #region Demo JSON for Adding Address to User
        /*{
              "street": "newStreetName",
              "city": "newCity",
              "zipCode": "123456",
              "state": "newState",
              "country": "newCountry"
         }*/
        #endregion
        [HttpPost("{userId}/addAddress")]
        public async Task<IActionResult> AddAddressToUser([FromRoute] int userId, [FromBody] AddressDTO newAddress)
        {
            var existingUser = await _context.UserDM.Include(u => u.Addresses).FirstOrDefaultAsync(u => u.UserId == userId);

            if (existingUser == null)
            {
                return NotFound($"User with ID: {userId} not found.");
            }

            // Create a new address from the provided data
            var addressToAdd = new AddressDM
            {
                Street = newAddress.Street,
                City = newAddress.City,
                ZipCode = newAddress.ZipCode,
                State = newAddress.State,
                Country = newAddress.Country
            };

            // Add the new address to the user's addresses
            existingUser.Addresses.Add(addressToAdd);

            await _context.SaveChangesAsync();

            return Ok(existingUser);
        }
        #region Demo JSON FOR Updating Address associated with User
        /* {

              "addressId": 1,
              "street": "updatedStreetName",
              "city": "updatedCity",
              "zipCode": "123456",
              "state": "UpdatedState",
              "country": "UpdatedCountry"
         }
 */
        #endregion
        [HttpPut("{userId}/update-address/{addressId}")]
        public async Task<IActionResult> UpdateUserAddresses([FromRoute] int userId, [FromRoute] int addressId, [FromBody] AddressDTO updatedAddress)
        {
            var existingUser = await _context.UserDM.Include(u => u.Addresses).FirstOrDefaultAsync(u => u.UserId == userId);

            if (existingUser == null)
            {
                return NotFound($"User with ID: {userId} is not available...");
            }

            var existingAddress = existingUser.Addresses.FirstOrDefault(a => a.AddressId == addressId);
            if (existingAddress == null)
            {
                return BadRequest($"Address with ID: {addressId} is Not Assosciated with UserID {userId}...");
            }
            
                // Update only the properties that are not null in updatedAddress
                if (updatedAddress.Street != null)
                {
                    existingAddress.Street = updatedAddress.Street;
                }
                if (updatedAddress.City != null)
                {
                    existingAddress.City = updatedAddress.City;
                }
                if (updatedAddress.ZipCode != null)
                {
                    existingAddress.ZipCode = updatedAddress.ZipCode;
                }
                if (updatedAddress.State != null)
                {
                    existingAddress.State = updatedAddress.State;
                }
                if (updatedAddress.Country != null)
                {
                    existingAddress.Country = updatedAddress.Country;
                }
            

            await _context.SaveChangesAsync();

            return Ok(existingUser);
        }
       /* [HttpPut("{userId}/update-addresss")]
        public async Task<IActionResult> UpdateUserAddresses2([FromRoute] int userId, [FromBody] AddressSM updatedAddress)
        {
            var existingUser = await _context.UserDM.Include(u => u.Addresses).FirstOrDefaultAsync(u => u.UserId == userId);

            if (existingUser == null)
            {
                return NotFound($"User with ID: {userId} is not available...");
            }

            var existingAddress = existingUser.Addresses.FirstOrDefault(a => a.AddressId == updatedAddress.AddressId);
            if (existingAddress == null)
            {
                return NotFound($"Address with ID: {updatedAddress.AddressId} is Not Assosciated with UserID {userId}...");
            }
            
                // Update only the properties that are not null in updatedAddress
                if (updatedAddress.Street != null)
                {
                    existingAddress.Street = updatedAddress.Street;
                }
                if (updatedAddress.City != null)
                {
                    existingAddress.City = updatedAddress.City;
                }
                if (updatedAddress.ZipCode != null)
                {
                    existingAddress.ZipCode = updatedAddress.ZipCode;
                }
                if (updatedAddress.State != null)
                {
                    existingAddress.State = updatedAddress.State;
                }
                if (updatedAddress.Country != null)
                {
                    existingAddress.Country = updatedAddress.Country;
                }
            

            await _context.SaveChangesAsync();

            return Ok(existingUser);
        }*/
        [HttpDelete("DeleteAddress")]
        public async Task<IActionResult> DeleteUserAddress([FromQuery] int userId, [FromQuery] int addressId)
        {
            var user = await _context.UserDM.Include(u => u.Addresses).FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound($"User with ID: {userId} is not available. Try another ID...");
            }

            var addressToDelete = user.Addresses.FirstOrDefault(a => a.AddressId == addressId);

            if (addressToDelete == null)
            {
                return NotFound($"Address with ID: {addressId} is not associated with User ID: {userId}.");
            }

            _context.AddressDM.Remove(addressToDelete);
            await _context.SaveChangesAsync();

            return Ok($"Address with ID: {addressId} associated with User ID: {userId} deleted successfully.");
        }


    }
}
