using AutoMapper;
using Azure.Identity;
using EcommereAPI.Data;
using EcommereAPI.DomainModels;
using EcommereAPI.Helpers;
using EcommereAPI.ServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommereAPI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<EcommerceUser> _userManager;
        private readonly ProjectEcommerceContext _projectEcommerceContext;
        private readonly SignInManager<EcommerceUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountRepository(
            UserManager<EcommerceUser> userManager,
            SignInManager<EcommerceUser> signInManager,
            IConfiguration configuration,
            ProjectEcommerceContext projectEcommerceContext,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _projectEcommerceContext = projectEcommerceContext;
            _mapper = mapper;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            // Check if a user with the same email address already exists
            var existingUser = await _userManager.FindByEmailAsync(signUpModel.Email);

            if (existingUser != null)
            {
                // A user with the same email already exists, return a custom IdentityResult
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserAlreadyExists",
                    Description = "A user with this email address already exists. Please sign in instead."
                });
            }

            // Check if the provided RoleType is valid (ADMIN or CUSTOMER)
            if (signUpModel.Role != RoleType.SELLER && signUpModel.Role != RoleType.CUSTOMER)
            {
                // Invalid RoleType provided
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "InvalidRoleType",
                    Description = "Invalid RoleType. RoleType must be 0 for SELLER or 1 for CUSTOMER."
                });
            }
           
            // We need to create a new user using UserManager
            var user = new EcommerceUser()
            {
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Email = signUpModel.Email,
                UserName = signUpModel.Email, // Assigning the email as the username
                Role = signUpModel.Role, // Convert RoleType to string and assign
            };
            var _userDM = new UserDM()
            {
                // Map user properties to UserDM properties
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.Email,
                Password = signUpModel.Password,
                ConfirmPassword = signUpModel.ConfirmPassword,
                Role = user.Role,
            };
            

            var result = await _userManager.CreateAsync(user, signUpModel.Password);
            if (result.Succeeded)
            {
                await _projectEcommerceContext.UserDM.AddAsync(_userDM);

                await _projectEcommerceContext.SaveChangesAsync();
            }

            return result;
        }

        public async Task<string> LoginAsync(SignInModel signInModel)
        {
            // Check if the provided email and password are correct for the user
            var user = await _userManager.FindByEmailAsync(signInModel.Email);
            if (user != null)
            {
                // Check if the user's RoleType matches the desired RoleType
                if (user.Role == signInModel.Role)
                {
                    var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, false, false);

                    if (result.Succeeded)
                    {
                        // User is authenticated, generate and return the token

                        // Create claims for the JWT token
                        var authClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, signInModel.Email),
                            new Claim(ClaimTypes.Role, signInModel.Role.ToString()), // Include user's role as a claim
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        };

                        // Generate a key from your secret
                        var authSignInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

                        // Create a new JWT token
                        var token = new JwtSecurityToken(
                            issuer: _configuration["JWT:ValidIssuer"],
                            audience: _configuration["JWT:ValidAudience"],
                            expires: DateTime.Now.AddDays(1), // Token expiration time
                            claims: authClaims,
                            signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256Signature)
                        );

                        // Return the token as a string
                        return new JwtSecurityTokenHandler().WriteToken(token);
                    }
                }
            } 
            return null;         // Authentication failed
        }
    }
}
