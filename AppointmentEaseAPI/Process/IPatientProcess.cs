using AppointmentEaseAPI.DomainModels;
using AppointmentEaseAPI.ServiceModels;

namespace AppointmentEaseAPI.Process
{
    public interface IPatientProcess
    {
        List<PatientSM> GetAllPatients();
        PatientSM GetPatientById(int patientId);
        Task<PatientSM> CreatePatient(PatientSM patient);
        PatientSM UpdatePatient(int Id, PatientSM patient);
        string DeletePatient(int patientId);
    }
}
