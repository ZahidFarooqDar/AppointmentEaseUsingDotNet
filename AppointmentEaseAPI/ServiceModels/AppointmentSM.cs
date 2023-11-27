using AppointmentEaseAPI.DomainModels;
using System.ComponentModel.DataAnnotations;

namespace AppointmentEaseAPI.ServiceModels
{
    public class AppointmentSM
    { 
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
