using PatientManagement.API.Models;
using PatientManagement.API.Wrappers;
using PatientManagementDTO.DTO;
using PatientManagementDTO.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientManagement.API.Data
{
    public interface IUserRepository
    {
        public PagedResponse<List<UserDTO>> GetAllUsers(PaginationFilter filter);

        UserDTO GetUserById(string id);

        UserDTO AddUser(UserCreateDTO userCreate);
        bool DeleteUser(string id);
        bool UpdateUser(UserDTO request);
        public List<UserDTO> Active();
        ULoginResponseDTO Login(UserLoginDTO user);
    }
}
