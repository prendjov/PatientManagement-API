using System;
using System.Collections.Generic;

namespace PatientManagement.API.Models
{
    public partial class Staff
    {
        public Staff()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int StaffId { get; set; }
        public int UserId { get; set; }
        public string WorkingDays { get; set; }
        public string Experience { get; set; }
        public string StaffCode { get; set; }
        public decimal? Salary { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Appointment> Appointment { get; set; }
    }
}
