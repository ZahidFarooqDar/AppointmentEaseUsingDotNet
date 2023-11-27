using System.ComponentModel.DataAnnotations;

namespace AppointmentEaseAPI.DomainModels
{
    public class AppointmentDM
    {
        [Key]
        public int AppointmentId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }
        public virtual PatientDM Patient { get; set; }
        public virtual DoctorDM Doctor { get; set; }
    }
}