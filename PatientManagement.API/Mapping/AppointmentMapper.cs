using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PatientManagement.API.Models;
using PatientManagementDTO.DTO;
using PatientManagement.API.Security;
using PatientManagement.API.Services;

namespace PatientManagement.API.Mapping
{
    public class AppointmentMapper : Profile
    {
        public AppointmentMapper()
        {
            var db = new StoreContext();

            CreateMap<Appointment, AppointmentDTO>();
            CreateMap<AppointmentDTO, Appointment>()
                .ForMember(d => d.AppointmentId, opt => opt.Ignore())
                .ForMember(d => d.DoctorId, opt => opt.MapFrom(s => Helper.DecryptInt(s.DoctorId)))
                .ForMember(d => d.CreatedById, opt => opt.MapFrom(s => Helper.DecryptInt(s.CreatedById)))
                .ForMember(d => d.DateCreated, opt => opt.MapFrom(s => DateTime.Now))
                .ForMember(d => d.Time, opt => opt.MapFrom(s => TimeSpan.Parse(s.Time)))
                .ForMember(d => d.StatusId, opt => opt.MapFrom(s => db.AppointmentStatus.Where(x => x.Name == "New").First().StatusId));


            CreateMap<AppointmentDetails, AppointmentDetailsDTO>();
            CreateMap<AppointmentDetailsDTO, AppointmentDetails>();
        }
    }
}
