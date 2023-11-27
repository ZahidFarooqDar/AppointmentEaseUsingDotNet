using AppointmentEaseAPI.Data;
using AppointmentEaseAPI.DomainModels;
using AppointmentEaseAPI.ServiceModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace AppointmentEaseAPI.Process
{
    public class AppointmentProcess : IAppointmentProcess
    {
        private readonly AppointmentEaseContext _dbContext;
        
        private readonly IMapper _mapper;

        public AppointmentProcess(AppointmentEaseContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<AppointmentSM> GetAllAppointments()
        {
            var appointmentsDM = _dbContext.Appointments.ToList();
            if(appointmentsDM == null || appointmentsDM.Count == 0 )
            {
                return null;
            }
            return _mapper.Map<List<AppointmentSM>>(appointmentsDM);
        }
        public async Task<AppointmentSM> CreateAppointment(int patientId, int doctorId, DateTime appointmentDate)
        {
            // Validate that the patient and doctor exist
            var patient = await _dbContext.Patients.FindAsync(patientId);
            if (patient == null)
            {
                throw new Exception("Patient Not Found");
            }

            var doctor = await _dbContext.Doctors.FindAsync(doctorId);
            if (doctor == null)
            {
                // Handle case when the doctor does not exist
                throw new Exception("Doctor Not Found");
            }
            DateTime dateOnly = appointmentDate.Date;
            var appointmentCount = await _dbContext.Appointments
                .Where(a => a.DoctorId == doctorId && EF.Functions.DateDiffDay(a.AppointmentDate, dateOnly) == 0)
                .CountAsync();
            if( appointmentCount > 9)
            {
                throw new Exception("Sorry! Appointment cannot be booked, Limit exceed...Book for another day");
            } 

            // Create the appointment
            var appointment = new AppointmentDM
            {
                PatientId = patientId,
                DoctorId = doctorId,
                AppointmentDate = appointmentDate.Date
            };

            // Add the appointment to the context and save changes
            _dbContext.Appointments.Add(appointment);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<AppointmentSM>(appointment);
        }
        public async Task<AppointmentSM> GetAppointmentByIdAsync(int appointmentId)
        {
            var appointment = await _dbContext.Appointments.FindAsync(appointmentId);

            if (appointment == null)
            {
                // Handle case when appointment is not found
                throw new Exception("Appointment not found");
            }

            // Map AppointmentDM to AppointmentSM using AutoMapper or manual mapping
            return _mapper.Map<AppointmentSM>(appointment);  
        }
        public async Task<int> GetTotalAppointmentsOfDoctor(int doctorId, DateTime appointmentDate)
        {
            // Extract only the date part from the appointmentDate
            //DateTime appointmentDate = DateTime.Now;
            DateTime dateOnly = appointmentDate.Date;

            // Filter appointments based on doctorId and date part of AppointmentDate
            var appointmentCount = await _dbContext.Appointments
                .Where(a => a.DoctorId == doctorId && EF.Functions.DateDiffDay(a.AppointmentDate, dateOnly) == 0)
                .CountAsync();

            return appointmentCount;
        }
    }
}

