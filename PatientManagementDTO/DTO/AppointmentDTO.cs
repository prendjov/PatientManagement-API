using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PatientManagementDTO.DTO
{
    public class AppointmentDTO
    {
        public string AppointmentId { get; set; }
        [Required]
        public string CreatedById { get; set; }
        [Required]
        public string DoctorId { get; set; }
        public string StatusId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Note { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class AppointmentDetailsDTO
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
    }
}
