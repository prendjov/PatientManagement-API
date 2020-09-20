using PatientManagementDTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PatientManagement.API.Models;
using Microsoft.VisualBasic;
using PatientManagementDTO.Filter;
using Microsoft.OpenApi.Expressions;
using Microsoft.EntityFrameworkCore;
using PatientManagement.API.Services;

namespace PatientManagement.API.Data
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IMapper mapper;

        public PatientRepository(IMapper _mapper)
        {
            mapper = _mapper;
        }

        public PatientDTO CreatePatient(PatientCreateDTO info)
        {
            StoreContext db = new StoreContext();

            Patient patient = mapper.Map<Patient>(info);

            Patient test = db.Patient.Where(u => u.Idnumber == info.Idnumber).FirstOrDefault();

            if (patient != null && test == null)
            {
                db.Patient.Add(patient);
                db.SaveChanges();

                Patient patientResponse = db.Patient
                    .Where(s => s.PatientId == patient.PatientId)
                    .Include(u => u.User)
                    .Include(b => b.BloodType)
                    .Include(s => s.Status)
                    .FirstOrDefault();

                PatientDTO response = mapper.Map<PatientDTO>(patientResponse);

                return response;
            }

            else return null;
        }
        public PatientDTO UpdatePatient(PatientUpdateDTO info)
        {
            StoreContext db = new StoreContext();

            Patient patient = db.Patient
                .Where(s => s.PatientId == Helper.DecryptInt(info.PatientId))
                .Include(u => u.User)
                .Include(b => b.BloodType)
                .Include(s => s.Status)
                .FirstOrDefault();

            if (patient != null)
            {
                Patient patientInfo = mapper.Map<Patient>(info);
                
                patient.UserId = patientInfo.UserId;
                patient.Idnumber = patientInfo.Idnumber;
                patient.Gender = patientInfo.Gender;
                patient.DateOfBirth = patientInfo.DateOfBirth;
                patient.BloodTypeId = patientInfo.BloodTypeId;
                patient.PhoneNumber = patientInfo.PhoneNumber;
                patient.Address = patientInfo.Address;
                patient.StatusId = patientInfo.StatusId;

                db.SaveChanges();

                PatientDTO response = mapper.Map<PatientDTO>(patient);
                response.DateCreated = patient.DateCreated;

                return response;
            }

            else return null;
        }
        public PatientDTO UpdatePatientStatusByID(string ID, string statusID)
        {
            StoreContext db = new StoreContext();

            Patient patient = db.Patient
                .Where(s => s.PatientId == Helper.DecryptInt(ID))
                .Include(u => u.User)
                .Include(b => b.BloodType)
                .Include(s => s.Status)
                .FirstOrDefault();

            if (patient != null)
            {
                patient.StatusId = Helper.DecryptInt(statusID);

                db.SaveChanges();

                PatientDTO response = mapper.Map<PatientDTO>(patient);

                return response;
            }

            else return null;
        }
        public PatientDTO UpdatePatientStatusByPersonalID(string personalID, string statusID)
        {
            StoreContext db = new StoreContext();

            Patient patient = db.Patient
                .Where(s => s.Idnumber == personalID)
                .Include(u => u.User)
                .Include(b => b.BloodType)
                .Include(s => s.Status)
                .FirstOrDefault();

            if (patient != null)
            {
                patient.StatusId = Helper.DecryptInt(statusID);

                db.SaveChanges();

                PatientDTO response = mapper.Map<PatientDTO>(patient);

                return response;
            }

            else return null;
        }
        public PatientDTO DeletePatientByID(string ID)
        {
            StoreContext db = new StoreContext();

            Patient patient = db.Patient
                .Where(s => s.PatientId == Helper.DecryptInt(ID))
                .Include(u => u.User)
                .Include(b => b.BloodType)
                .Include(s => s.Status)
                .FirstOrDefault();

            if (patient != null)
            {
                PatientDTO response = mapper.Map<PatientDTO>(patient);

                db.Patient.Remove(patient);
                db.SaveChanges();

                return response;
            }

            else return null;
        }
        public PatientDTO FindPatientByID(string ID)
        {
            StoreContext db = new StoreContext();

            if (ID != null)
            {
                Patient patient = db.Patient
                    .Where(s => s.PatientId == Helper.DecryptInt(ID))
                    .Include(u => u.User)
                    .Include(b => b.BloodType)
                    .Include(s => s.Status)
                    .FirstOrDefault();

                if (patient != null)
                {
                    PatientDTO response = mapper.Map<PatientDTO>(patient);

                    return response;
                }
                else return null;
            }
            else return null;
        }

        public PatientDTO FindPatientByPersonalID(string personalID)
        {
            StoreContext db = new StoreContext();

            if (personalID != null)
            {
                Patient patient = db.Patient
                    .Where(s => s.Idnumber == personalID)
                    .Include(u => u.User)
                    .Include(b => b.BloodType)
                    .Include(s => s.Status)
                    .FirstOrDefault();

                if (patient != null)
                {
                    PatientDTO response = mapper.Map<PatientDTO>(patient);

                    return response;
                }

                else return null;
            }

            else return null;
        }
        public List<PatientDTO> ListPatientsByFilter(PatientFilter filter)
        {
            StoreContext db = new StoreContext();

            List<Patient> patients = db.Patient
                .Include(u => u.User)
                .Include(b => b.BloodType)
                .Include(s => s.Status)
                .ToList();


            if (!string.IsNullOrEmpty(filter.FirstName) && filter.FirstName != "string")
            {
                patients = db.Patient.Where(u => u.User.FirstName == filter.FirstName)
                .Include(u => u.User)
                .Include(b => b.BloodType)
                .Include(s => s.Status)
                .ToList();
            }
            if (!string.IsNullOrEmpty(filter.LastName) && filter.LastName != "string")
            {
                patients = db.Patient.Where(u => u.User.LastName == filter.LastName)
                .Include(u => u.User)
                .Include(b => b.BloodType)
                .Include(s => s.Status)
                .ToList();
            }
            if (!string.IsNullOrEmpty(filter.Gender) && filter.Gender != "string")
            {
                patients = db.Patient.Where(u => u.Gender == filter.Gender)
                .Include(u => u.User)
                .Include(b => b.BloodType)
                .Include(s => s.Status)
                .ToList();
            }
            if (string.IsNullOrEmpty(filter.BloodGroupId) && filter.BloodGroupId != "string" && Helper.DecryptInt(filter.BloodGroupId) != 0)
            {
                patients = db.Patient.Where(u => u.BloodType.BloodTypeId == Helper.DecryptInt(filter.BloodGroupId))
                .Include(u => u.User)
                .Include(b => b.BloodType)
                .Include(s => s.Status)
                .ToList();

            }
            if (string.IsNullOrEmpty(filter.StatusId) && filter.StatusId != "string" && Helper.DecryptInt(filter.StatusId) != 0)
            {
                patients = db.Patient.Where(u => u.StatusId == Helper.DecryptInt(filter.StatusId))
                .Include(u => u.User)
                .Include(b => b.BloodType)
                .Include(s => s.Status)
                .ToList();
            }

                patients = patients
                .Take(filter.PageSize)
                .ToList();

            List<PatientDTO> response = mapper.Map<List<PatientDTO>>(patients);

            return response;
        }
        public PatientShowAppointments ShowPatientAppointmentsByID(string ID)
        {
            StoreContext db = new StoreContext();

            PatientShowAppointments response = new PatientShowAppointments();
            Patient patient = db.Patient
                .Where(s => s.PatientId == Helper.DecryptInt(ID))
                .Include(u => u.User)
                .Include(b => b.BloodType)
                .Include(s => s.Status)
                .FirstOrDefault();

            if (patient != null)
            {
                response.Patient = mapper.Map<PatientDTO>(patient);

                List<AppointmentDetails> appointments = db.AppointmentDetails
                    .Where(p => p.PatientId == patient.PatientId)
                    .Include(a => a.Appointment)
                    .Include(p => p.Patient)
                    .ThenInclude(u => u.User)
                    .Include(u => u.Appointment)
                    .ThenInclude(d => d.Doctor)
                    .Include(u => u.Appointment)
                    .ThenInclude(c => c.CreatedBy)
                    .Include(u => u.Appointment)
                    .ThenInclude(s => s.Status)
                    .ToList();

                if (appointments != null)
                {
                    response.Appointments = mapper.Map<List<PatientAppointmentsDTO>>(appointments);

                    return response;
                }

                else return null;
            }

            else return null;
        }
        public PatientShowAppointments ShowPatientAppointmentsByPersonalID(string personalID)
        {
            StoreContext db = new StoreContext();

            PatientShowAppointments response = new PatientShowAppointments();
            Patient patient = db.Patient
                .Where(s => s.Idnumber == personalID)
                .Include(u => u.User)
                .Include(b => b.BloodType)
                .Include(s => s.Status)
                .FirstOrDefault();

            if (patient != null)
            {
                response.Patient = mapper.Map<PatientDTO>(patient);

                List<AppointmentDetails> appointments = db.AppointmentDetails
                    .Where(p => p.PatientId == patient.PatientId)
                    .Include(a => a.Appointment)
                    .Include(p => p.Patient)
                    .ThenInclude(u => u.User)
                    .Include(u => u.Appointment)
                    .ThenInclude(d => d.Doctor)
                    .Include(u => u.Appointment)
                    .ThenInclude(c => c.CreatedBy)
                    .Include(u => u.Appointment)
                    .ThenInclude(s => s.Status)
                    .ToList();

                if (appointments != null)
                {
                    response.Appointments = mapper.Map<List<PatientAppointmentsDTO>>(appointments);

                    return response;
                }

                else return null;
            }

            else return null;
        }
        public async Task<bool> SendEmailToPatient(string appointmentId, string patientEmail)
        {
            StoreContext db = new StoreContext();
            Appointment appointment = db.Appointment.Where(a => a.AppointmentId == Helper.DecryptInt(appointmentId))
                .Include(a => a.Doctor)
                .ThenInclude(a => a.User)
                .FirstOrDefault();

            if (appointment != null)
            {
                bool response = await EmailSender.SendEmailToPatient(mapper.Map<AppointmentInfoDTO>(appointment), patientEmail);

                return response;
            }

            return true;
        }
        public PatientStatusDTO CreatePatientStatus(PatientStatusCreateDTO info)
        {
            StoreContext db = new StoreContext();

            if (!string.IsNullOrEmpty(info.Name) || info.Name != "string")
            {
                PatientStatus patientStatus = mapper.Map<PatientStatus>(info);

                db.PatientStatus.Add(patientStatus);
                db.SaveChanges();

                PatientStatusDTO response = mapper.Map<PatientStatusDTO>(patientStatus);

                return response;
            }

            else return null;
        }
        public PatientStatusDTO UpdatePatientStatus(PatientStatusUpdateDTO info)
        {
            StoreContext db = new StoreContext();

            PatientStatus patientStatus = db.PatientStatus.Where(a => a.Id == Helper.DecryptInt(info.Id)).FirstOrDefault();

            if (patientStatus != null)
            {
                patientStatus.Name = info.Name;
                patientStatus.IsActive = info.IsActive;

                db.SaveChanges();

                PatientStatusDTO response = mapper.Map<PatientStatusDTO>(patientStatus);

                return response;
            }

            else return null;
        }

        public PatientStatusDTO DeletePatientStatus(string ID)
        {
            StoreContext db = new StoreContext();

            PatientStatus patientStatus = db.PatientStatus.Where(a => a.Id == Helper.DecryptInt(ID)).FirstOrDefault();

            if (patientStatus != null)
            {
                PatientStatusDTO response = mapper.Map<PatientStatusDTO>(patientStatus);

                db.PatientStatus.Remove(patientStatus);
                db.SaveChanges();

                return response;
            }

            else return null;
        }
        public List<PatientStatusDTO> ListPatientStatuses(PaginationFilter filter)
        {
            StoreContext db = new StoreContext();

            List<PatientStatus> patientStatuses = db.PatientStatus
                .Take(filter.PageSize)
                .ToList();

            List<PatientStatusDTO> response = mapper.Map<List<PatientStatusDTO>>(patientStatuses);

            return response;

        }

    }
}
