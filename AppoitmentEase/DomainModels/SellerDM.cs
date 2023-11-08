using EcommereAPI.Helpers;

namespace EcommereAPI.DomainModels
{
    public class SellerDM : UserDM
    {
        //public string? CompanyName { get; set; }
        public string? PhoneNumber { get; set; }
        public SellerType SellerRole { get; set; }
        public ICollection<ProductDM>? Products { get; set; }
        
    }
}
