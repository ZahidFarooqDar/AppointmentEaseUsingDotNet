using EcommereAPI.ServiceModels;
using System.ComponentModel.DataAnnotations;

namespace EcommereAPI.DomainModels
{
    public class AddressDM
    {
        [Key]
        public int AddressId { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public int UserId { get; set; }
        public UserDM? UserSM { get; set; }
        
    }
}
