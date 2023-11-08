using System.ComponentModel.DataAnnotations;

namespace EcommereAPI.DomainModels
{
    public class TrackerDM
    {
        [Key]
        public string? TrackerId { get; set; }
        public string? TrackingNumber { get; set; }
        public bool IsSubscribed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
