using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repository;
        private readonly IUserStatusRepository statusRepository;
        public UserController(IUserRepository _repository,
                              IUserStatusRepository _statusRepository)
        {
            repository = _repository;
            statusRepository = _statusRepository;
        }



        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns>All Users</returns>
        /// <response code = "200">Return all Users</response>
        /// <response code = "400">Some error occured</response>
        /// 
        [AuthFilter]
        [HttpPost("list")]
        public IActionResult GetAllUsers(PaginationFilter filter)
        {
            PagedResponse<List<UserDTO>> userList = repository.GetAllUsers(filter);
            return new ObjectResult(userList);
        }
        /// <summary>
        /// Update User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /update/User
        ///         {
        ///             "FirstName": "Tom",
        ///             "LastName" : "Cruze",
        ///             "email" : "somemail@.com",
        ///             "RoleId" : 1
        ///             "StatusId" : 1
        ///             
        ///         }
        ///         
        /// </remarks>
        /// <param name="userUpdate"></param>
        /// <returns>Updated list of users</returns>
        /// <response code = "200">Return updated list of users</response>
        /// <response code = "400">Some error occured</response>
        [AuthFilter]
        [HttpPost("update")]
        public IActionResult UpdateUser([FromBody] UserDTO userCreate)
        {
            if (!string.IsNullOrEmpty(userCreate.UserId))
            {
                bool result = repository.UpdateUser(userCreate);
                return new ObjectResult(result);
            }
            else
            {
                return BadRequest("Missing item: StudentId");
            }
        }
        /// <summary>
        /// Get all Active Users
        /// </summary>
        /// <returns>All Active Users</returns>
        /// <response code = "200">Return All Active Users</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("Active")]
        public IActionResult Active()
        {
            List<UserDTO> users = repository.Active();
            return new ObjectResult(users);
        }
        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /single/User
        ///     {
        ///         "UserId" : 1
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single User</returns>
        /// <response code = "200">Return User by Id</response>
        /// <response code = "400">Some error occured</response>
        [AuthFilter]
        [HttpGet("single")]
        public IActionResult GetUserById([FromQuery] string id)
        {
            UserDTO userr = repository.GetUserById(id);
            if (userr != null)
            {
                return new ObjectResult(userr);
            }
            else
            {
                return Content("User with that ID is not found!");
            }
        }
        /// <summary>
        /// Create a new User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /create/User
        ///         {
        ///             "FirstName": "Tom",
        ///             "LastName" : "Cruze",
        ///             "email" : "somemail@.com",
        ///             "password" : "23@11"
        ///             "RoleId": 1,
        ///             "StatusId":1
        ///         }
        /// </remarks>
        /// <param name="userCreate"></param>
        /// <returns>Updated list of users</returns>
        /// <response code = "200">Return updated list of users</response>
        /// <response code = "400">Some error occured</response>
   //     [AuthFilter]
        [HttpPost("Create")]
        public IActionResult AddUser([FromBody] UserCreateDTO userCreate)
        {
            userCreate.Email = userCreate.Email == "string" ? "" : userCreate.Email;
            if (string.IsNullOrEmpty(userCreate.Email) || string.IsNullOrEmpty(userCreate.Password))
            {
                return BadRequest("Missing items");
            }
            UserCreateDTO newUser = repository.AddUser(userCreate);
            return new ObjectResult(newUser);
        }
        /// <summary>
        /// Delete User by Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Delete /single/User
        ///     {
        ///         "UserId" : 1
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single User</returns>
        /// <response code = "200">Return single User</response>
        /// <response code = "400">Some error occured</response>
        [AuthFilter]
        [HttpGet("Delete")]
        public IActionResult DeleteUser([FromQuery] string id)
        {
            bool result = repository.DeleteUser(id);
            return new ObjectResult(result);
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDTO user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Email and password are required!");
            }
            else
            {
                ULoginResponseDTO response = repository.Login(user);

                if (response != null)

                    return new ObjectResult(response);

                else
                    return NotFound("Wrong Credentials!");
            }
        }
        [AuthFilter]
        [HttpPost("UserStatus/Create")]
        public IActionResult CreateStatus([FromBody] UserStatusDTO info)
        {
            UserStatusDTO response = statusRepository.CreateStatus(info);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new BadRequestObjectResult("Try again, you missed some of the needed informations.");
        }

    }






}
