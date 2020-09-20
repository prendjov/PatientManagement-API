using PatientManagementDTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientManagement.API.Data
{
    public interface ISetupRepository
    {
        public BloodTypeDTO CreateSetup(BloodTypeDTO _staff);
        List<BloodTypeDTO> GetAllBloodType();
    }
}
