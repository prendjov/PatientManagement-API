using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientManagement.API.Data;
using PatientManagement.API.Models;
using PatientManagement.API.Wrappers;
using PatientManagementDTO.DTO;
using PatientManagementDTO.Filter;

namespace PatientManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository repository;

        public AppointmentController(IAppointmentRepository _repository)
        {
            repository = _repository;
        }

        [HttpPost("list")]
        public IActionResult GetAllAppointments([FromBody] PaginationFilter appoinfo)
        {
            List<AppointmentDTO> response = repository.GetAllAppointments(appoinfo);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new NotFoundObjectResult("There aren't any appointments on this page.");
        }
        [HttpPost("create")]
        public IActionResult AddAppointment([FromBody] AppointmentDTO appointment)
        {
            if (ModelState.IsValid)
            {
                string newAppointment = repository.AddAppointment(appointment);
                return new ObjectResult(newAppointment);
            }
            else
            {
                return BadRequest("Request not valid!");
            }
        }

        [HttpGet("single")]
        public IActionResult GetById([FromQuery] int id)
        {
            //throw new DivideByZeroException();
            AppointmentDTO appointment = repository.GetById(id);

            if (appointment != null)
            {
                return new ObjectResult(new Response<AppointmentDTO>(appointment));
            }
            else
            {
                return Content("Appointment with that ID is not found!");
            }
        }
        [HttpPost("details/list")]
        public IActionResult AppointmentsFilterListing([FromBody] AppointmentFilter appoinfo)
        {
            //List<AppointmentDetailsDTO> response = repository.AppointmentsFilterListing(appoinfo);

            //if (response != null)
            //{
            //    return new ObjectResult(response);
            //}

            //else return new NotFoundObjectResult("0 Appointments found.");
            List<AppointmentDetailsDTO> result = repository.AppointmentsFilterListing(appoinfo);
            return new ObjectResult(result);
        }
    }
}

