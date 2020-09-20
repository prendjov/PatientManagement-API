using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientManagement.API.Data;
using PatientManagement.API.Models;
using PatientManagementDTO.DTO;

namespace PatientManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ISetupRepository repository;
        public SetupController(IMapper _mapper, ISetupRepository _repository)
        {
            mapper = _mapper;
            repository = _repository;
        }

        [HttpPost("bloodtype/Create")]
        public IActionResult CreatePatient([FromBody] BloodTypeDTO info)
        {
            BloodTypeDTO response = repository.CreateSetup(info);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new BadRequestObjectResult("ERROR 500");
        }

        [HttpGet("bloodtype/list")]
        public IActionResult GetAllBloodTypes()
        {

            List<BloodTypeDTO> response = repository.GetAllBloodType();

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new BadRequestObjectResult("ERROR 500");
        }
        
    }
}
