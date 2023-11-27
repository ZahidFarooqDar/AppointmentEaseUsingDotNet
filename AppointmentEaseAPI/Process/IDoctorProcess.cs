using AppointmentEaseAPI.DomainModels;
using AppointmentEaseAPI.ServiceModels;

namespace AppointmentEaseAPI.Process
{
    public interface IDoctorProcess
    {
        List<DoctorSM> GetAllDoctors();
        DoctorSM GetDoctorById(int doctorId);
        Task<DoctorSM> CreateDoctor(DoctorSM doctor);
        DoctorSM UpdateDoctor(int Id, DoctorSM doctor);
        string DeleteDoctor(int doctorId);
    }
}
