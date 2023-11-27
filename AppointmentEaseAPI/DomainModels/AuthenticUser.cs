using AppointmentEaseAPI.DomainModels.Enums;
using Microsoft.AspNetCore.Identity;

namespace AppointmentEaseAPI.DomainModels
{
    public class AuthenticUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public RoleType Role { get; set; }
    }
}
