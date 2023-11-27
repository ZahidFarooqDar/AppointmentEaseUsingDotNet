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
    [Authorize(Roles = "DOCTOR, PATIENT")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorProcess _doctorProcess;

        public DoctorController(IDoctorProcess doctorProcess)
        {
            _doctorProcess = doctorProcess;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = _doctorProcess.GetAllDoctors();
            if(doctors == null || doctors.Count == 0)
            {
                return NotFound("Doctors Not Found");
            }
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = _doctorProcess.GetDoctorById(id);
            if (doctor == null)
            {
                return NotFound("Doctors Not Found");
            }
            return Ok(doctor);
        }

        [HttpPost]
        public IActionResult CreateDoctor([FromBody] DoctorSM doctor)
        {
            if (doctor == null)
            {
                return BadRequest("Invalid data");
            }

            _doctorProcess.CreateDoctor(doctor);
            return CreatedAtAction("GetDoctorById", new { id = doctor.DoctorId }, doctor);
        }

        [HttpPut("{doctorId}")]
        public async Task<IActionResult> UpdateDoctor(int doctorId, [FromBody] DoctorSM doctor)
        {
            if (doctor == null || doctorId == null)
            {
                return BadRequest("Invalid data");
            }
            var updatedDoctor =  _doctorProcess.UpdateDoctor(doctorId,doctor);
            return Ok(updatedDoctor);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            var deletedAction = _doctorProcess.DeleteDoctor(id);

            return Ok(deletedAction);
        }
    } 
}
