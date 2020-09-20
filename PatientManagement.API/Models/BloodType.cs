using System;
using System.Collections.Generic;

namespace PatientManagement.API.Models
{
    public partial class BloodType
    {
        public BloodType()
        {
            Patient = new HashSet<Patient>();
        }

        public int BloodTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Patient> Patient { get; set; }
    }
}
