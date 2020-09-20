using System;
using System.Collections.Generic;

namespace PatientManagement.API.Models
{
    public partial class Patient
    {
        public Patient()
        {
            AppointmentDetails = new HashSet<AppointmentDetails>();
        }

        public int PatientId { get; set; }
        public int UserId { get; set; }
        public string Idnumber { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int BloodTypeId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int? StatusId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }

        public virtual BloodType BloodType { get; set; }
        public virtual PatientStatus Status { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<AppointmentDetails> AppointmentDetails { get; set; }
    }
}
