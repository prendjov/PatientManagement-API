using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PatientManagement.API.Models;
using PatientManagement.API.Security;
using PatientManagement.API.Services;
using PatientManagementDTO.DTO;

namespace PatientManagement.API.Mapping
{
    public class PatientMapper : Profile
    {
        public PatientMapper()
        {
            CreateMap<PatientCreateDTO, Patient>()
                .ForMember(p => p.PhoneNumber, opt => opt.MapFrom(p => EncryptionHelper.Encrypt(p.PhoneNumber)))
                .ForMember(a => a.Address, opt => opt.MapFrom(a => EncryptionHelper.Encrypt(a.Address)))
                .ForMember(d => d.DateCreated, opt => opt.MapFrom(a => DateTime.Now))
                .ForMember(p => p.UserId, opt => opt.MapFrom(p => Helper.DecryptInt(p.UserId)))
                .ForMember(p => p.StatusId, opt => opt.MapFrom(p => Helper.DecryptInt(p.StatusId)))
                .ForMember(p => p.BloodTypeId, opt => opt.MapFrom(p => Helper.DecryptInt(p.BloodTypeId)));

        CreateMap<Patient, PatientDTO>()
                .ForMember(p => p.PatientId, opt => opt.MapFrom(a => Helper.EncyptString(a.PatientId)))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => EncryptionHelper.Decrypt(s.PhoneNumber)))
                .ForMember(u => u.Address, opt => opt.MapFrom(e => EncryptionHelper.Decrypt(e.Address)))
                .ForMember(f => f.FirstName, opt => opt.MapFrom(s => s.User.FirstName))
                .ForMember(l => l.LastName, opt => opt.MapFrom(s => s.User.LastName))
                .ForMember(b => b.BloodGroup, opt => opt.MapFrom(g => g.BloodType.Name))
                .ForMember(s => s.Status, opt => opt.MapFrom(s => s.Status.Name));

            CreateMap<PatientUpdateDTO, Patient>()
                .ForMember(p => p.PatientId, opt => opt.MapFrom(a => Helper.DecryptInt(a.PatientId)))
                .ForMember(p => p.UserId, opt => opt.MapFrom(a => Helper.DecryptInt(a.UserId)))
                .ForMember(p => p.BloodTypeId, opt => opt.MapFrom(a => Helper.DecryptInt(a.BloodTypeId)))
                .ForMember(p => p.StatusId, opt => opt.MapFrom(a => Helper.DecryptInt(a.StatusId)))
                .ForMember(p => p.Address, opt => opt.MapFrom(a => EncryptionHelper.Encrypt(a.Address)))
                .ForMember(p => p.PhoneNumber, opt => opt.MapFrom(a => EncryptionHelper.Encrypt(a.PhoneNumber)))
                .ForMember(p => p.DateUpdated, opt => opt.MapFrom(a => DateTime.Now));

            CreateMap<AppointmentDetails, PatientAppointmentsDTO>()
                .ForMember(d => d.AppointmentId, opt => opt.MapFrom(a => Helper.EncyptString(a.Appointment.AppointmentId)))
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(g => g.Appointment.CreatedBy.FirstName + " " + g.Appointment.CreatedBy.LastName))
                .ForMember(d => d.Doctor, opt => opt.MapFrom(s => s.Appointment.Doctor.User.FirstName + " " + s.Appointment.Doctor.User.LastName))
                .ForMember(d => d.Patient, opt => opt.MapFrom(s => s.Patient.User.FirstName + " " + s.Patient.User.LastName))
                .ForMember(d => d.Date, opt => opt.MapFrom(dtr => dtr.Appointment.Date))
                .ForMember(d => d.Status, opt => opt.MapFrom(l => l.Appointment.Status.Name))
                .ForMember(d => d.StatusId, opt => opt.MapFrom(l => l.Appointment.StatusId))
                .ForMember(d => d.Time, opt => opt.MapFrom(f => f.Appointment.Time))
                .ForMember(d => d.CreatedById, opt => opt.MapFrom(h => Helper.EncyptString(h.Appointment.CreatedById)))
                .ForMember(d => d.DoctorId, opt => opt.MapFrom(h => Helper.EncyptString(h.Appointment.DoctorId)))
                .ForMember(d => d.PatientId, opt => opt.MapFrom(h => Helper.EncyptString(h.PatientId)))
                .ForMember(d => d.StatusId, opt => opt.MapFrom(h => Helper.EncyptString(h.Patient.PatientId)));

        CreateMap<Appointment, AppointmentInfoDTO>()
                .ForMember(d => d.Date, opt => opt.MapFrom(a => a.Date))
                .ForMember(d => d.Time, opt => opt.MapFrom(a => a.Time))
                .ForMember(d => d.Doctor, opt => opt.MapFrom(a => a.Doctor.User.FirstName + " " + a.Doctor.User.LastName));

            CreateMap<PatientStatusCreateDTO, PatientStatus>()
                .ForMember(p => p.Name, opt => opt.MapFrom(p => p.Name))
                .ForMember(p => p.IsActive, opt => opt.MapFrom(p => true))
                .ForMember(p => p.DateCreated, opt => opt.MapFrom(a => DateTime.Now));

            CreateMap<PatientStatus, PatientStatusDTO>()
                .ForMember(p => p.Id, opt => opt.MapFrom(p => Helper.EncyptString(p.Id)));
        }
    }
}
