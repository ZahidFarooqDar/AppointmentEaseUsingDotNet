using AppointmentEaseAPI.DomainModels;
using AppointmentEaseAPI.Process;
using AppointmentEaseAPI.ServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentEaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "DOCTOR, PATIENT")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentProcess _appointmentProcess;

        public AppointmentController(IAppointmentProcess appointmentProcess)
        {
            _appointmentProcess = appointmentProcess;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = _appointmentProcess.GetAllAppointments();
            if (appointments == null || appointments.Count == 0)
            {
                return NotFound("Appointments Not Found");
            }
            return Ok(appointments);
        }
        [HttpGet("app-count-doctor/{doctorId}")]
        public async Task<IActionResult> GetTotalAppointmentsOfDoctor(int doctorId/*DateTime dateTime*/)
        {
            DateTime specificDate = new DateTime(2023, 11, 27);
            var appointments = _appointmentProcess.GetTotalAppointmentsOfDoctor(doctorId,specificDate);
            if (appointments == null)
            {
                return NotFound("No Appointments For You...");
            }
            return Ok(appointments);
        }
        [HttpPost()]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequest request)
        {
            try
            {
                // Assuming CreateAppointmentRequest is a DTO or ViewModel for the input data
                var appointment = await _appointmentProcess.CreateAppointment(request.PatientId, request.DoctorId, request.AppointmentDate);
                return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.AppointmentId }, appointment);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var appointment = await _appointmentProcess.GetAppointmentByIdAsync(id);
            if(appointment == null)
            {
                return null;
            }
            return Ok(appointment); 
        }
    }
}
