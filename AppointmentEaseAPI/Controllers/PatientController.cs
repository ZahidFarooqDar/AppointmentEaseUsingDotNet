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
    public class PatientController : ControllerBase
    {
        private readonly IPatientProcess _patientProcess;

        public PatientController(IPatientProcess patientProcess)
        {
            _patientProcess = patientProcess;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = _patientProcess.GetAllPatients();
            if(patients == null || patients.Count == 0)
            {
                return NotFound("Patients Not Found");
            }
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = _patientProcess.GetPatientById(id);
            if (patient == null)
            {
                return NotFound("Patients Not Found");
            }
            return Ok(patient);
        }

        [HttpPost]
        public IActionResult CreatePatient([FromBody] PatientSM patient)
        {
            if (patient == null)
            {
                return BadRequest("Invalid data");
            }

            _patientProcess.CreatePatient(patient);
            return CreatedAtAction("GetPatientById", new { id = patient.PatientId }, patient);
        }

        [HttpPut("{patientId}")]
        public async Task<IActionResult> UpdatePatient(int patientId, [FromBody] PatientSM patient)
        {
            if (patient == null || patientId == null)
            {
                return BadRequest("Invalid data");
            }
            var updatedPatient =  _patientProcess.UpdatePatient(patientId,patient);
            return Ok(updatedPatient);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            var deletedAction = _patientProcess.DeletePatient(id);

            return Ok(deletedAction);
        }
    } 
}
