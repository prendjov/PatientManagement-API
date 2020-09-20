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
  public  interface IStaffRepository
    {
        public StaffDTO CreateStaff(StaffCreateDTO _staff);

        public StaffDTO UpdateStaff(StaffDTO _staff);
        public StaffDTO DeleteStaff(DeleteStaffDTO id);
        public StaffDTO GetById(string staffId);

        public PagedResponse<List<StaffDTO>> GetAllStaff(PaginationFilter filter);




    }

}
