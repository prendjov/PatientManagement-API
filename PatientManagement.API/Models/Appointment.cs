using System;
using System.Collections.Generic;

namespace PatientManagement.API.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            AppointmentDetails = new HashSet<AppointmentDetails>();
        }

        public int AppointmentId { get; set; }
        public int CreatedById { get; set; }
        public int DoctorId { get; set; }
        public int StatusId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Note { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual Staff Doctor { get; set; }
        public virtual AppointmentStatus Status { get; set; }
        public virtual ICollection<AppointmentDetails> AppointmentDetails { get; set; }
    }
}
