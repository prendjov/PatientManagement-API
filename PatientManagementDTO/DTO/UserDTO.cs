using System;
using System.Collections.Generic;
using System.Text;

namespace PatientManagementDTO.DTO
{
   public class UserCreateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleId { get; set; }
        public string StatusId { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class UserDTO : UserCreateDTO
    {
        public string UserId { get; set; }
        
    }
    public class UserLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ULoginResponseDTO
    {
        public UserDTO user { get; set; }
        public string AuthToken { get; set; }
    }
}
