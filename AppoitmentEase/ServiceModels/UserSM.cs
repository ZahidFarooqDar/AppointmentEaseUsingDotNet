using EcommereAPI.DomainModels;
using EcommereAPI.Helpers;
using System.Net;

namespace EcommereAPI.ServiceModels
{
    public class UserSM
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public RoleType Role { get; set; }
        public ICollection<AddressSM>? Addresses { get; set; }
        public List<UserProductOrderSM>? UserProductOrders { get; set; }
        

    }
}
