using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientManagement.API.Models;
using PatientManagementDTO.DTO;
using PatientManagement.API.Security;

namespace PatientManagement.API.Mapping
{
    public class SetupMapper:Profile
    {
        public SetupMapper()
        {
            CreateMap<BloodTypeDTO, BloodType>();
            CreateMap<BloodType, BloodTypeDTO>();
        }
    }
}
