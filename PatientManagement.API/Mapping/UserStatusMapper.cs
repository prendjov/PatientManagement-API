using AutoMapper;
using PatientManagement.API.Models;
using PatientManagement.API.Security;
using PatientManagementDTO.DTO;
using System;


namespace PatientManagement.API.Mapping
{
    public class UserStatusMapper : Profile
    {
        public UserStatusMapper()
        {
            CreateMap<UserStatusCreate, UserStatus>()
               .ForMember(d => d.DateCreated, opt => opt.MapFrom(a => DateTime.Now));
            CreateMap<UserStatus, UserStatusDTO>();





        }

    }
}
