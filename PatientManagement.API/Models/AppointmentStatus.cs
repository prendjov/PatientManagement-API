using System;
using System.Collections.Generic;

namespace PatientManagement.API.Models
{
    public partial class AppointmentStatus
    {
        public AppointmentStatus()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int StatusId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Appointment> Appointment { get; set; }
    }
}
