using AppointmentEaseAPI.Data;
using AppointmentEaseAPI.DomainModels;
using AppointmentEaseAPI.ServiceModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace AppointmentEaseAPI.Process
{
    public class DoctorProcess : IDoctorProcess
    {
        private readonly AppointmentEaseContext _dbContext;

        private readonly IMapper _mapper;

        public DoctorProcess(AppointmentEaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<DoctorSM> GetAllDoctors()
        {
            var doctorDM = _dbContext.Doctors.ToList();
            if (doctorDM == null || doctorDM.Count == 0)
            {
                return null;
            }
            return _mapper.Map<List<DoctorSM>>(doctorDM);
        }

        public DoctorSM GetDoctorById(int doctorId)
        {
            var doctor = _dbContext.Doctors.Find(doctorId);
            if (doctor != null)
            {
                return _mapper.Map<DoctorSM>(doctor);
            }
            return null;
        }

        public async Task<DoctorSM> CreateDoctor(DoctorSM doctor)
        {
            if (doctor == null)
            {
                return null;
            }
            var doctorDM = _mapper.Map<DoctorDM>(doctor);
            _dbContext.Doctors.Add(doctorDM);
            if (_dbContext.SaveChanges() > 0)
            {
                return doctor;
            }
            return null;
        }
        public DoctorSM UpdateDoctor(int Id, DoctorSM doctor)
        {
            if (Id == null)
                return null;

            try
            {
                var prod = _dbContext.Doctors.FindAsync(Id);
                if (prod != null)
                {
                    doctor.DoctorId = Id;
                    var doctorDM = _mapper.Map<DoctorDM>(doctor);
                    _dbContext.Doctors.Update(doctorDM);
                    if (_dbContext.SaveChanges() > 0)
                        return doctor;
                }
            }
            catch
            {
                throw;
            }
            return null;
        }

        public string DeleteDoctor(int doctorId)
        {
            var doctor = _dbContext.Doctors.Find(doctorId);
            if (doctor != null)
            {
                _dbContext.Doctors.Remove(doctor);
                if (_dbContext.SaveChanges() > 0)
                {
                    return "Deleted Succesfully...";
                }

            }
            return "doctor does not exist or error in deleting doctor";
        }
    }
}

