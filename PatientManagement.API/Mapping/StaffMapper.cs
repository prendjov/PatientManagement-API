using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using PatientManagement.API.Models;
using PatientManagement.API.Security;
using PatientManagementDTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;
using PatientManagement.API.Services;

namespace PatientManagement.API.Mapping
{
    public class StaffMapper:Profile
    {
        public StaffMapper()
        {
            CreateMap<Staff, StaffDTO>()
                .ForMember(d => d.StaffId, opt => opt.MapFrom(s => Helper.EncyptString(s.StaffId)))
                .ForMember(d=>d.UserId,opt=>opt.MapFrom(s=>Helper.EncyptString(s.UserId)))
                
                .ForMember(d => d.Salary, opt => opt.MapFrom(s => EncryptionHelper.Encrypt(s.Salary.ToString())))
                .ForMember(d => d.FirstName, opt => {
                    opt.Condition(s => s.User != null);
                    opt.MapFrom(s => s.User.FirstName);

                })
                .ForMember(d => d.LastName, opt => {
                    opt.Condition(s => s.User != null);
                    opt.MapFrom(s => s.User.LastName);

                })
                .ForMember(d => d.StatusId, opt => {
                    opt.Condition(s => s.User != null);
                    opt.MapFrom(s => Helper.EncyptString(s.User.StatusId));

                })
                .ForMember(d => d.StatusName, opt => {
                    opt.Condition(s => s.User != null && s.User.Status != null);
                    opt.MapFrom(s=> s.User.Status.Name);

                });








            CreateMap<StaffDTO, Staff>()
                .ForMember(s => s.StaffCode, opt => opt.MapFrom(e => EncryptionHelper.Encrypt(e.StaffCode)));
                
                
               


           


            CreateMap<StaffCreateDTO, Staff>()
                .ForMember(u => u.UserId, opt => opt.MapFrom(u => EncryptionHelper.Encrypt(u.UserId.ToString())));


            CreateMap<DeleteStaffDTO, Staff>();
                
                
          
               
                

          




            
               
                



        }
    }
}
