using System;
using System.Collections.Generic;
using System.Text;

namespace PatientManagementDTO.Filter
{
    public class AppointmentFilter : PaginationFilter
    {
        public int DetailsId { get; set; }
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string Diagnosis { get; set; }
        public bool Paid { get; set; }

    }
}
