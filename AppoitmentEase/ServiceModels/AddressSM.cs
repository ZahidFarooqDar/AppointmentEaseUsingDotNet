using System.ComponentModel.DataAnnotations;

namespace EcommereAPI.ServiceModels
{
    public class AddressSM
    {
        public int? AddressId { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public int UserId { get; set; }
        public UserSM? UserSM { get; set; }
        //public virtual List<UserSM>? Users { get; set; }
        //public int? CustomerId { get; set; }
        // public Customer Customer { get; set; }
    }
}
