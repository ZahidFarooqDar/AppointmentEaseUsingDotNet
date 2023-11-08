using EcommereAPI.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EcommereAPI.DomainModels
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
        public RoleType Role { get; set;}
        public ICollection<OrderDM>? Orders { get; set; }
        public ICollection<AddressDM>? Addresses { get; set; }
    }
}
