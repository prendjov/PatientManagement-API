using System;
using System.Collections.Generic;

namespace PatientManagement.API.Models
{
    public partial class User
    {
        public User()
        {
            Appointment = new HashSet<Appointment>();
            Patient = new HashSet<Patient>();
            Staff = new HashSet<Staff>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Role Role { get; set; }
        public virtual UserStatus Status { get; set; }
        public virtual ICollection<Appointment> Appointment { get; set; }
        public virtual ICollection<Patient> Patient { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
    }
}
