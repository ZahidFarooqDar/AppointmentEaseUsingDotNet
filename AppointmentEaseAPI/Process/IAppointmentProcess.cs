using AppointmentEaseAPI.ServiceModels;
using Microsoft.AspNetCore.Identity;

namespace AppointmentEaseAPI.Process
{
    public interface IAppointmentProcess
    {
        List<AppointmentSM> GetAllAppointments();
        Task<AppointmentSM> CreateAppointment(int patientId, int doctorId, DateTime appointmentDate);
        Task<AppointmentSM> GetAppointmentByIdAsync(int appointmentId);
        Task<int> GetTotalAppointmentsOfDoctor(int doctorId,DateTime appointmentDate);
    }
}
