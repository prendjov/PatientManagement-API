using System;
using System.Collections.Generic;

namespace PatientManagement.API.Models
{
    public partial class UserStatus
    {
        public UserStatus()
        {
            User = new HashSet<User>();
        }

        public int StatusId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
