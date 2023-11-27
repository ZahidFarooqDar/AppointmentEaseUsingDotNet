using System.ComponentModel.DataAnnotations;

namespace AppointmentEaseAPI.ServiceModels
{
    public class CreateAppointmentRequest
    {
        [Required(ErrorMessage = "PatientId is required")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "DoctorId is required")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "AppointmentDate is required")]
        public DateTime AppointmentDate { get; set; }
    }
}
