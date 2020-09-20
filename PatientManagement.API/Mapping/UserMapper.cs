using AutoMapper;
using PatientManagement.API.Models;
using PatientManagement.API.Security;
using PatientManagementDTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientManagement.API.Mapping
{
    public class UserMapper:Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDTO>()
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => EncryptionHelper.Encrypt(s.UserId.ToString())))
                .ForMember(u => u.RoleId, opt => opt.MapFrom(e => EncryptionHelper.Encrypt(e.RoleId.ToString())))
                .ForMember(x => x.StatusId, opt => opt.MapFrom(a => EncryptionHelper.Encrypt(a.StatusId.ToString())))
                .ForMember(e => e.Password, opt => opt.MapFrom(z => EncryptionHelper.Encrypt(z.Password.ToString())))
                .ForMember(t => t.Email, opt => opt.MapFrom(s => EncryptionHelper.Encrypt( s.Email.ToString())));
            CreateMap<UserCreateDTO, User>()
                .ForMember(s => s.Email, opt => opt.MapFrom(c => EncryptionHelper.Encrypt(c.Email)))
                .ForMember(t => t.Password, opt => opt.MapFrom(e => EncryptionHelper.Encrypt(e.Password)))
                .ForMember(r=>r.RoleId,opt=>opt.MapFrom(o=>EncryptionHelper.Decrypt(o.RoleId)))
                .ForMember(a => a.StatusId, opt => opt.MapFrom(x => EncryptionHelper.Decrypt(x.StatusId)))
                //.ForMember(t => t.Password, opt => opt.Ignore())
                .ForMember(s => s.DateCreated, opt => opt.MapFrom(d => DateTime.Now));

            CreateMap<UserStatus, User>()
                .ForMember(x => x.Status, opt => opt.MapFrom(a => EncryptionHelper.Encrypt(a.StatusId.ToString())));
        }
    }
}
