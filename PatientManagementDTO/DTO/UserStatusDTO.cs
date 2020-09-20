using System;
using System.Collections.Generic;
using System.Text;

namespace PatientManagementDTO.DTO
{
   public  class UserStatusCreate

    {
        
        public string Name { get; set; }
        

    }
    public class UserStatusDTO : UserStatusCreate
    {
        public int StatusId { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
    }
}
