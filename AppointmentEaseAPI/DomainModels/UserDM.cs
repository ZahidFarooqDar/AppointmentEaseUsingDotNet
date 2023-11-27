using AppointmentEaseAPI.DomainModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace AppointmentEaseAPI.DomainModels
{
    public class UserDM
    {

        [Key]
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public RoleType Role { get; set; }
        
    }
}
