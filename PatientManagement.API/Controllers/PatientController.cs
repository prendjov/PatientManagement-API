using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientManagement.API.Data;
using PatientManagement.API.Filters;
using PatientManagement.API.Security;
using PatientManagementDTO.DTO;
using PatientManagementDTO.Filter;

namespace PatientManagement.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository repository;
        public PatientController(IPatientRepository _repository)
        {
            repository = _repository;
        }
        /// <summary>
        /// Create a patient.
        /// </summary>
        /// <returns>Patient</returns>
        /// <response code = "200">Returns the created patient.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("Create")]
        public IActionResult CreatePatient([FromBody] PatientCreateDTO info)
        {
            PatientDTO response = repository.CreatePatient(info);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new BadRequestObjectResult("Try again, you missed some of the needed informations.");
        }
        /// <summary>
        /// Find patient by ID.
        /// </summary>
        /// <returns>Patient</returns>
        /// <response code = "200">Returns the patient that matches the inserted ID.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("Find/ByID")]
        public IActionResult FindPatientByID([FromQuery] string ID)
        {
            PatientDTO response = repository.FindPatientByID(ID);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new NotFoundObjectResult("A patient with the inserted ID is not existing.");
        }
        /// <summary>
        /// Find a patient by personal ID.
        /// </summary>
        /// <returns>Patient</returns>
        /// <response code = "200">Returns the patient that matches the inserted personal ID.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("Find/ByPersonalID")]
        public IActionResult FindPatientByPersonalID([FromQuery] string personalID)
        {
            PatientDTO response = repository.FindPatientByPersonalID(personalID);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new NotFoundObjectResult("A patient with the inserted personal ID is not existing.");
        }
        /// <summary>
        /// Update a patient.
        /// </summary>
        /// <returns>Patient</returns>
        /// <response code = "200">Returns the patient with the updated informations.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("Update")]
        public IActionResult UpdatePatient([FromBody] PatientUpdateDTO info)
        {
            PatientDTO response = repository.UpdatePatient(info);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new NotFoundObjectResult("The inserted ID in the body is not matching any patient.");
        }
        /// <summary>
        /// Delete a patient by ID.
        /// </summary>
        /// <returns>Patient</returns>
        /// <response code = "200">Returns the informations of the deleted patient.</response>
        /// <response code = "400">An error occured.</response>
        [HttpDelete("Delete")]
        public IActionResult DeletePatient([FromQuery] string ID)
        {
            PatientDTO response = repository.DeletePatientByID(ID);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new NotFoundObjectResult("A patient with that ID is not existing.");
        }
        /// <summary>
        /// List all patients.
        /// </summary>
        /// <returns>Patient</returns>
        /// <response code = "200">Lists all the patients by custom number.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("List/All")]
        public IActionResult ListAllPatients([FromBody] PatientFilter info)
        {
            List<PatientDTO> response = repository.ListPatientsByFilter(info);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new NotFoundObjectResult("There isn't any patient inserted yet.");
        }
        /// <summary>
        /// Update the patient status by ID.
        /// </summary>
        /// <returns>Patient</returns>
        /// <response code = "200">Shows the informations of the patient.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("Update/Status/ByID")]
        public IActionResult UpdatePatientStatusByID([FromQuery] string ID, string statusID)
        {
            PatientDTO response = repository.UpdatePatientStatusByID(ID, statusID);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new NotFoundObjectResult("There isn't any patient with that ID.");
        }
        /// <summary>
        /// Update the patient status by personal ID.
        /// </summary>
        /// <returns>Patient</returns>
        /// <response code = "200">Shows the informations of the patient.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("Update/Status/ByPersonalID")]
        public IActionResult UpdateDeadPatientByPersonalID([FromQuery] string personalID, string statusID)
        {
            PatientDTO response = repository.UpdatePatientStatusByPersonalID(personalID, statusID);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new NotFoundObjectResult("There isn't any patient with that personal ID.");
        }
        /// <summary>
        /// List all appointments of a patient by ID.
        /// </summary>
        /// <returns>Patient</returns>
        /// <response code = "200">Shows the patient and all of his appointments.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("List/Appointments/ByPatientID")]
        public IActionResult ShowAppointmentsByPatientID([FromQuery] string ID)
        {
            PatientShowAppointments response = repository.ShowPatientAppointmentsByID(ID);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new NotFoundObjectResult("A user by that ID was not found or there aren't any appointments of that user");
        }
        /// <summary>
        /// List all appointments of a patient by personal ID.
        /// </summary>
        /// <returns>Patient</returns>
        /// <response code = "200">Shows the patient and all of his appointments.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("List/Appointments/ByPersonalID")]
        public IActionResult ShowAppointmentsByPatientPersonalID([FromQuery] string personalID)
        {
            PatientShowAppointments response = repository.ShowPatientAppointmentsByPersonalID(personalID);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new NotFoundObjectResult("A user by that personal ID was not found or there aren't any appointments of that user");
        }
        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail([FromQuery] string appointmentId, string patientEmail)
        {
            bool response = await repository.SendEmailToPatient(appointmentId, patientEmail);

            return new ObjectResult(response);
        }
        /// <summary>
        /// Create a patient status.
        /// </summary>
        /// <returns>Patient</returns>
        /// <response code = "200">Shows the created patient status.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("Create/PatientStatus")]
        public IActionResult CreatePatientStatus([FromBody] PatientStatusCreateDTO info)
        {
            PatientStatusDTO response = repository.CreatePatientStatus(info);

            return new ObjectResult(response);
        }
        /// <summary>
        /// Update a patient status.
        /// </summary>
        /// <returns>Patient</returns>
        /// <response code = "200">Shows the updated patient status.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("Update/PatientStatus")]
        public IActionResult UpdatePatientStatus([FromBody] PatientStatusUpdateDTO info)
        {
            PatientStatusDTO response = repository.UpdatePatientStatus(info);

            return new ObjectResult(response);
        }
        /// <summary>
        /// Delete a patient status.
        /// </summary>
        /// <returns>Patient</returns>
        /// <response code = "200">Shows the deleted patient status.</response>
        /// <response code = "400">An error occured.</response>
        [HttpDelete("Delete/PatientStatus")]
        public IActionResult DeletePatientStatus([FromQuery] string ID)
        {
            PatientStatusDTO response = repository.DeletePatientStatus(ID);

            return new ObjectResult(response);
        }
        /// <summary>
        /// Get all a patient statuses.
        /// </summary>
        /// <returns>Patient</returns>
        /// <response code = "200">Shows all of the patient statuses.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("List/All/PatientStatus")]
        public IActionResult ListPatientStatuses([FromBody] PaginationFilter filter)
        {
            List<PatientStatusDTO> response = repository.ListPatientStatuses(filter);

            return new ObjectResult(response);

        }

    }
}
