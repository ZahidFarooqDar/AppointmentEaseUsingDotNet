using EcommereAPI.ServiceModels;
using System.ComponentModel.DataAnnotations;

namespace EcommereAPI.DomainModels
{
    public class PaymentDetailsDM
    {
        [Key]
        public int PaymentDetailsId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public bool PaymentStatus { get; set; }
        public bool IsRefunded { get; set; }
      
    }
}
