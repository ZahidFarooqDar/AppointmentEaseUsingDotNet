using AutoMapper;
using EcommereAPI.Data;
using EcommereAPI.DomainModels;
using EcommereAPI.Helpers;
using EcommereAPI.ServiceModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EcommereAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ProjectEcommerceContext _context;
        private readonly IMapper _mapper;

        public UserController(ProjectEcommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
           
            var users = await _context.UserDM.Include(a => a.Addresses).ToListAsync();
            var _userModels = users.Select(u => new UserDM
            {
                UserId = u.UserId,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Password = u.Password,
                ConfirmPassword = u.ConfirmPassword,
                Role = u.Role,
                Addresses = u.Addresses.Select(add => new AddressDM
                {
                    AddressId = add.AddressId,
                    Street = add.Street,
                    City = add.City,
                    Country = add.Country,
                    ZipCode = add.ZipCode,
                    State = add.State

                }).ToList()
            }).ToList();
            return Ok(_userModels);
        }
       /* #region DEMO JSON FOR USER with existing Address
        *//*{
  "firstName": "John",
  "lastName": "Doe",
  "userName": "johndoe",
  "email": "johndoe@example.com",
  "password": "password123",
  "confirmPassword": "password123",
  "role": 0,
  "addresses": [
    { "addressId": 1 },
    { "addressId": 2 }
  ],
   
  "userProductOrders": []
}
*//*
        #endregion
        //It assigns user with the addresses present and ignores the addresses which are not present
        [HttpPost("withMultipleAddresses")]
        public async Task<IActionResult> CreateProductWithExistingAddress([FromBody] UserSM userModel)
        {
            var existingAddress = await _context.AddressDM.FindAsync(userModel.Addresses.First().AddressId);
            if (existingAddress == null)
            {
                // Handle the case where the category does not exist, return an error or perform other actions.
                return BadRequest("The specified Address does not exist.");
            }
            var newUser = new UserDM
            {
                UserId = userModel.UserId,
                UserName = userModel.UserName,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                Password = userModel.Password,
                ConfirmPassword = userModel.ConfirmPassword,
                Role = userModel.Role,
                Addresses = new List<AddressDM> { existingAddress }

            };

            _context.UserDM.Add(newUser);
            await _context.SaveChangesAsync();
            //var product = _mapper.Map<ProductDM>(productModel);
            return CreatedAtAction("GetUsers", new { id = userModel.UserId }, newUser);

        }*/

        /*//It checks whether provided address Ids are present or not Not
        [HttpPost("withMultipleAddress")]
        public async Task<IActionResult> CreateUserWithMultipleExistingAddresses([FromBody] UserSM userModel)
        {
            var existingAddresses = new List<AddressDM>();

            var nonExistentAddressIds = new List<int>();  // To store non-existent address IDs

            // Loop through the AddressIds provided in userModel.Addresses
            foreach (var addressId in userModel.Addresses.Select(a => a.AddressId))
            {
                var existingAddress = await _context.AddressDM.FindAsync(addressId);

                // If an address with the provided AddressId exists, add it to the list
                if (existingAddress != null)
                {
                    existingAddresses.Add(existingAddress);
                }
                else
                {
                    nonExistentAddressIds.Add((int)addressId); // Add non-existent address IDs to the list
                }
            }

            // Check if there are any non-existent addresses
            if (nonExistentAddressIds.Any())
            {
                // Return a BadRequest with a message indicating non-existent addresses
                return BadRequest($"The following AddressIds do not exist: {string.Join(", ", nonExistentAddressIds)}");
            }

            var newUser = new UserDM
            {
                UserId = userModel.UserId,
                UserName = userModel.UserName,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                Password = userModel.Password,
                ConfirmPassword = userModel.ConfirmPassword,
                Role = userModel.Role,
                Addresses = existingAddresses  // Associate the user with multiple existing addresses
            };

            _context.UserDM.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = userModel.UserId }, newUser);
        }
*/


        // GET: api/User/{id}
        [HttpGet("byId")]
        public async Task<IActionResult> GetUser([FromQuery] int userId)
        {
            var user = await _context.UserDM.Include(a => a.Addresses).SingleOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                return NotFound($"User with Id {userId} is not available....Try different UserID");
            }

            var userModel = _mapper.Map<UserSM>(user);
            return Ok(userModel);
        }

        // POST: Post user with corresponding Address
        [HttpPost]
        #region Demo JSON for adding User With Address
        /* {
   "firstName": "John",
   "lastName": "Doe",
   "userName": "johndoe",
   "email": "johndoe@example.com",
   "password": "password123",
   "confirmPassword": "password123",
   "role": 0,
   "addresses": [
     {
       "street": "123 Main Street",
       "city": "Example City",
       "zipCode": "12345",
       "state": "Sample State",
       "country": "Sample Country",
       "user": null
     }
   ],
   "userProductOrders": []
 }*/
        #endregion
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userModel)
        {
            
            var user = _mapper.Map<UserDM>(userModel);
            _context.UserDM.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.UserId }, userModel);
        }
        /*[HttpPost("CreateNewly")]
        public async Task<IActionResult> CreateUser2([FromBody] BuyerDTO userModel)
        {

            var user = _mapper.Map<UserDM>(userModel);
            _context.UserDM.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.UserId }, userModel);
        }*/

        // PUT: api/User/{id}
        #region DEMO JSON for Update USER
        /* {
              "userName": "newUsername",
              "firstName": "UpdatedFirstName",
              "lastName": "UpdatedLastName",
              "email": "updatedemail@example.com",
              "password": "newPassword123",
              "confirmPassword": "newPassword123",
              "role": 1,
              "userProductOrders": []
         }*/

        #endregion
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser([FromQuery] int id, [FromBody] UserDTO userModel)
        {
           
            var existingUser = await _context.UserDM.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            existingUser.UserName = userModel.UserName;
            existingUser.FirstName = userModel.FirstName;
            existingUser.LastName = userModel.LastName;
            existingUser.Email = userModel.Email;
            existingUser.Password = userModel.Password;
            existingUser.ConfirmPassword = userModel.ConfirmPassword;
            existingUser.Role = userModel.Role;
            //We are not supposed to use mapper here coz it will provide error on basis of primary key
            await _context.SaveChangesAsync();
            return Ok(existingUser);
        }
       /* #region Demo JSON FOR Updating Address associated with User
        *//* {

              "addressId": 1,
              "street": "updatedStreetName",
              "city": "updatedCity",
              "zipCode": "123456",
              "state": "UpdatedState",
              "country": "UpdatedCountry"
         }
 *//*
        #endregion
        [HttpPut("{userId}/update-address")]
        public async Task<IActionResult> UpdateUserAddresses([FromRoute] int userId, [FromBody] AddressSM updatedAddress)
        {
            var existingUser = await _context.UserDM.Include(u => u.Addresses).FirstOrDefaultAsync(u => u.UserId == userId);

            if (existingUser == null)
            {
                return NotFound();
            }

            var existingAddress = existingUser.Addresses.FirstOrDefault(a => a.AddressId == updatedAddress.AddressId);
            if (existingAddress != null)
            {
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
            }

            await _context.SaveChangesAsync();

            return Ok(existingUser);
        }*/
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




        // DELETE: api/User/{id}
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromQuery] int userId)
        {
            var user = await _context.UserDM.Include(u => u.Addresses).FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound($"User with ID: {userId} is not Available Try Another ID...");
            }
            _context.AddressDM.RemoveRange(user.Addresses);

            _context.UserDM.Remove(user);
            await _context.SaveChangesAsync();
            return Ok($"User with ID: {userId} and associated addresses deleted successfully.");
        }
    }
}
