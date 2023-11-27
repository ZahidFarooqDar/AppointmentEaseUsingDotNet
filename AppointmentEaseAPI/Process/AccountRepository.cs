using AppointmentEaseAPI.Data;
using AppointmentEaseAPI.DomainModels;
using AppointmentEaseAPI.DomainModels.Enums;
using AppointmentEaseAPI.ServiceModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;

namespace AppointmentEaseAPI.Process
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AuthenticUser> _userManager;
        private readonly AppointmentEaseContext _dbContext;
        private readonly SignInManager<AuthenticUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AccountRepository(
             UserManager<AuthenticUser> userManager,
             SignInManager<AuthenticUser> signInManager,
             IConfiguration configuration,
             AppointmentEaseContext dbContext,
             IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _dbContext = dbContext;
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

            // Check if the provided RoleType is valid (DOCTOR or PATIENT)
            if (signUpModel.Role != RoleType.DOCTOR && signUpModel.Role != RoleType.PATIENT)
            {
                // Invalid RoleType provided
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "InvalidRoleType",
                    Description = "Invalid RoleType. RoleType must be 1 for DOCTOR or 2 for PATIENT."
                });
            }

            // Create a new user using UserManager
            var user = new AuthenticUser()
            {
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Email = signUpModel.Email,
                UserName = signUpModel.Email, // Assigning the email as the username
                Role = signUpModel.Role,
            };

            // Map user properties to UserDM properties
            var userDM = new UserDM()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.Email,
                Password = signUpModel.Password,
                ConfirmPassword = signUpModel.ConfirmPassword,
                Role = user.Role
            };

            // Add both user and userDM to the respective contexts
            await _userManager.CreateAsync(user, signUpModel.Password);
            await _dbContext.UserDM.AddAsync(userDM);

            // Check if both user and userDM are successfully saved
            var userCreationResult = await _dbContext.SaveChangesAsync();
            if (userCreationResult > 0)
            {
                // If the role is DOCTOR, add a Doctor to the Doctors table
                if (signUpModel.Role == RoleType.DOCTOR)
                {
                    var doctor = new DoctorDM
                    {
                        Name = signUpModel.FirstName,
                        Email = signUpModel.Email,
                        Password = signUpModel.Password,
                        Specialization = signUpModel.Specialization
                    };
                    _dbContext.Doctors.Add(doctor);
                }

                // If the role is PATIENT, add a Patient to the Patients table
                if (signUpModel.Role == RoleType.PATIENT)
                {
                    var patient = new PatientDM
                    {
                        Name = signUpModel.FirstName,
                        Email = signUpModel.Email,
                        Password = signUpModel.Password,
                    };
                    _dbContext.Patients.Add(patient);
                }

                // Save changes to the database
                var roleCreationResult = await _dbContext.SaveChangesAsync();

                // Return the IdentityResult based on the roleCreationResult
                return roleCreationResult > 0 ? IdentityResult.Success : IdentityResult.Failed();
            }

            // If the user or userDM creation fails, return IdentityResult.Failed
            return IdentityResult.Failed();
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

