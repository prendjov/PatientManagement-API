using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientManagement.API.Data;
using PatientManagement.API.Filters;
using PatientManagement.API.Models;
using PatientManagement.API.Wrappers;
using PatientManagementDTO.DTO;
using PatientManagementDTO.Filter;

namespace PatientManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IMapper mapper;
        
        private readonly IStaffRepository repository;
        public StaffController(IMapper _mapper,IStaffRepository _repository)
            
        {
            mapper = _mapper;
            repository = _repository;
        }
        
        //[AuthFilter]
        [HttpPost("create")]

        public IActionResult CreateStaff(StaffCreateDTO _staff)
        {
            
            StaffDTO newStaff = repository.CreateStaff(_staff);

            return new ObjectResult(newStaff);


        }
        
        //[AuthFilter]
        [HttpPost("update")]

        public IActionResult UpdateStaff(StaffDTO _staff)
        {

            StaffDTO staff = repository.UpdateStaff(_staff);

            return new ObjectResult(staff);
        }

        //[AuthFilter]
        [HttpPost("delete")]
        public IActionResult DeleteStaff(DeleteStaffDTO id)
        {
            StaffDTO staff = repository.DeleteStaff(id);

            return new ObjectResult(staff);
        }

        //[AuthFilter]
        [HttpGet("single")]
        public IActionResult GetById([FromQuery]string staffId)
        {
            StaffDTO staff = repository.GetById(staffId);

            return new ObjectResult(staff);
        }

        //[AuthFilter]
        [HttpPost("list")]

        public IActionResult GetAllStaff(PaginationFilter filter)
        {
            PagedResponse<List<StaffDTO>> list = repository.GetAllStaff(filter);

            return new ObjectResult(list);
        }



    }
}
