using EcommereAPI.Helpers;
using Microsoft.AspNetCore.Identity;

namespace EcommereAPI.DomainModels
{
    public class EcommerceUser:IdentityUser
    {
        public string? FirstName { get; set; }   //Here we have defined our own properties 
        public string? LastName { get; set; }
        public RoleType Role { get; set; }
    }
}
