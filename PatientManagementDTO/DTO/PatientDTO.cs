using PatientManagementDTO.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientManagementDTO.DTO
{
    public class PatientCreateDTO
    {
        public string UserId { get; set; }
        public string StatusId { get; set; }
        public string Idnumber { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BloodTypeId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
    public class PatientUpdateDTO : PatientCreateDTO
    {
        public string PatientId { get; set; }
        public string StatusId { get; set; }
    }
    public class PatientDTO
    {
        public string PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Idnumber { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string BloodGroup { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class PatientAppointmentsDTO
    {
        public string AppointmentId { get; set; }
        public string CreatedBy { get; set; }
        public string Doctor { get; set; }
        public string Patient { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Status { get; set; }
        public int TotalPerson { get; set; }
        public decimal TotalAmount { get; set; }
        public string PatientNote { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public bool Paid { get; set; }
        public string CreatedById { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public string StatusId { get; set; }
    }
    public class PatientShowAppointments
    {
        public PatientDTO Patient { get; set; }
        public List<PatientAppointmentsDTO> Appointments { get; set; }
    }
    public class AppointmentInfoDTO
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Doctor { get; set; }
    }
    public class PatientFilter : PaginationFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string BloodGroupId { get; set; }
        public string StatusId { get; set; }
    }
    public class PatientStatusDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class PatientStatusUpdateDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
    public class PatientStatusCreateDTO
    {
        public string Name { get; set; }
    }
}
