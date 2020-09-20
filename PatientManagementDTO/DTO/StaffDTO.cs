using System;
using System.Collections.Generic;
using System.Text;

namespace PatientManagementDTO.DTO
{
    public class StaffDTO
    {
        public string StaffId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StatusId { get; set; }
        public string StatusName { get; set; }

        public string WorkingDays { get; set; }
        public string Experience { get; set; }
        public string StaffCode { get; set; }
        public string Salary { get; set; }
    }

    public class StaffCreateDTO
    {
       
        public int UserId { get; set; }
        public string WorkingDays { get; set; }
        public string Experience { get; set; }
        public string StaffCode { get; set; }
        public string Salary { get; set; }
    }

    public class DeleteStaffDTO
    {
        public int StaffId { get; set; }
    }




}
