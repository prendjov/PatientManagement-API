using System;
using System.Collections.Generic;

namespace PatientManagement.API.Models
{
    public partial class AppointmentDetails
    {
        public int DetailsId { get; set; }
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string DoctorNote { get; set; }
        public string PatientNote { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public decimal TotalAmount { get; set; }
        public bool Paid { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
