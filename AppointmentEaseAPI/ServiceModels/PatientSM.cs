using System.ComponentModel.DataAnnotations;

namespace AppointmentEaseAPI.ServiceModels
{
    public class PatientSM
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
