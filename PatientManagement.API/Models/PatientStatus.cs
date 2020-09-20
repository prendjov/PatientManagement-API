using System;
using System.Collections.Generic;

namespace PatientManagement.API.Models
{
    public partial class PatientStatus
    {
        public PatientStatus()
        {
            Patient = new HashSet<Patient>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<Patient> Patient { get; set; }
    }
}
