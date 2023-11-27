using AppointmentEaseAPI.Data;
using AppointmentEaseAPI.DomainModels;
using AppointmentEaseAPI.ServiceModels;
using AutoMapper;

namespace AppointmentEaseAPI.Process
{
    public class PatientProcess : IPatientProcess
    {
        private readonly AppointmentEaseContext _dbContext;
        
        private readonly IMapper _mapper;

        public PatientProcess(AppointmentEaseContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<PatientSM> GetAllPatients()
        {
            var patientDM = _dbContext.Patients.ToList();
            if(patientDM == null || patientDM.Count == 0 )
            {
                return null;
            }
            return _mapper.Map<List<PatientSM>>(patientDM);
        }

        public PatientSM GetPatientById(int patientId)
        {
             var patient = _dbContext.Patients.Find(patientId);
                if (patient != null)
                {
                    return _mapper.Map<PatientSM>(patient);
                }
            return null;
        }

        public async Task<PatientSM> CreatePatient(PatientSM patient)
        {
            if(patient == null)
            {
                return null;
            }
            var patientDM = _mapper.Map<PatientDM>(patient);
            _dbContext.Patients.Add(patientDM);
            if(_dbContext.SaveChanges() > 0)
            {
                return patient;
            }
            return null;
        }

        public PatientSM UpdatePatient(int Id, PatientSM patient)
        {
            if (Id == null)
                return null;

            try
            {
                var prod = _dbContext.Doctors.FindAsync(Id);
                if (prod != null)
                {
                    patient.PatientId = Id;
                    var patientDM = _mapper.Map<PatientDM>(patient);
                    _dbContext.Patients.Update(patientDM);
                    if (_dbContext.SaveChanges() > 0)
                        return patient;
                }
            }
            catch
            {
                throw;
            }
            return null;
        }
        public string DeletePatient(int patientId)
        {
            var patient = _dbContext.Patients.Find(patientId);
            if (patient != null)
            {
                _dbContext.Patients.Remove(patient);
                if(_dbContext.SaveChanges() > 0)
                {
                    return "Deleted Succesfully...";
                }
                
            }
            return "Patient does not exist or error in deleting Patient";
        }
    }
}

