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
   public  interface IUserStatusRepository
    {
        public UserStatusDTO CreateStatus (UserStatusCreate info);
        //public UserStatusDTO UpdateStatus (UserStatusCreate info);


    }
}
