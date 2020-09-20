using PatientManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientManagement.API.Data
{
   public interface IAuthRepository
    {
        public string CreateToken(User users);
    }
}
