using AutoMapper;
using PatientManagement.API.Models;
using PatientManagement.API.Services;
using PatientManagement.API.Wrappers;
using PatientManagementDTO.DTO;
using PatientManagementDTO.Filter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PatientManagement.API.Data
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IMapper mapper;

        public AppointmentRepository(IMapper _mapper)
        {
            mapper = _mapper;
        }

        public string AddAppointment(AppointmentDTO appointment)
        {
            StoreContext db = new StoreContext();
            Appointment existingAppointment;

            if (string.IsNullOrEmpty(appointment.AppointmentId))
            {
                TimeSpan time = TimeSpan.Parse(appointment.Time);

                existingAppointment = db.Appointment.Where(x => x.Date == appointment.Date && (x.Time == time ||
                                                                x.Time == time.Add(TimeSpan.FromMinutes(-30))
                                                              || x.Time == time.Add(TimeSpan.FromMinutes(30)))).FirstOrDefault();

                    if (existingAppointment == null)
                    {
                        Appointment newAppointment = new Appointment();
                        newAppointment.CreatedById = Helper.DecryptInt(appointment.CreatedById);
                        newAppointment.DoctorId = Helper.DecryptInt(appointment.DoctorId);
                        newAppointment.StatusId = db.AppointmentStatus.Where(x => x.Name == "New").First().StatusId;
                        newAppointment.Date = appointment.Date;
                        newAppointment.Time = TimeSpan.Parse(appointment.Time);
                        newAppointment.FirstName = appointment.FirstName;
                        newAppointment.LastName = appointment.LastName;
                        newAppointment.Note = appointment.Note;
                        db.Add(newAppointment);
                        db.SaveChanges();
                        return "The appointment is successfully created!";
                    }
                    else
                    {
                        return "Another appointment has been scheduled close to this appointment!";
                    }
            }
            else
            {
                int appointmentId = Helper.DecryptInt(appointment.AppointmentId);

                existingAppointment = db.Appointment.Where(x => x.AppointmentId == appointmentId).FirstOrDefault();

                if(existingAppointment != null)
                {
                    existingAppointment.DoctorId = Helper.DecryptInt(appointment.DoctorId);
                    existingAppointment.Date = appointment.Date;
                    existingAppointment.Time = TimeSpan.Parse(appointment.Time);
                    existingAppointment.FirstName = appointment.FirstName;
                    existingAppointment.LastName = appointment.LastName;
                    existingAppointment.Note = appointment.Note;
                    db.SaveChanges();
                }
            }
            return null;
        }


        public List<AppointmentDetailsDTO> AppointmentsFilterListing(AppointmentFilter filter)
        {
            StoreContext db = new StoreContext();
            List<AppointmentDetails> list = db.AppointmentDetails.ToList();

            #region Sorting
            if (filter.SortDirection.ToLower() == "asc")
            {
                switch (filter.SortColumn.ToLower())
                {
                    case "DetailsId":
                        list = list.OrderBy(b => b.DetailsId).ToList();
                        break;
                    case "Appointmentid":
                        list = list.OrderBy(b => b.AppointmentId).ToList();
                        break;
                    case "PatientId":
                        list = list.OrderBy(b => b.PatientId).ToList();
                        break;
                    case "Diagnosis":
                        list = list.OrderBy(b => b.Diagnosis).ToList();
                        break;
                    case "Treatment":
                        list = list.OrderBy(b => b.Treatment).ToList();
                        break;
                    case "TotalAmount":
                        list = list.OrderBy(b => b.TotalAmount).ToList();
                        break;
                    case "Paid":
                        list = list.OrderBy(b => b.Paid).ToList();
                        break;

                }
            }
            else
            {
                switch (filter.SortColumn.ToLower())
                {
                    case "DetailsId":
                        list = list.OrderByDescending(b => b.DetailsId).ToList();
                        break;
                    case "Appointmentid":
                        list = list.OrderByDescending(b => b.AppointmentId).ToList();
                        break;
                    case "PatientId":
                        list = list.OrderByDescending(b => b.PatientId).ToList();
                        break;
                    case "Diagnosis":
                        list = list.OrderByDescending(b => b.Diagnosis).ToList();
                        break;
                    case "Treatment":
                        list = list.OrderByDescending(b => b.Treatment).ToList();
                        break;
                    case "TotalAmount":
                        list = list.OrderByDescending(b => b.TotalAmount).ToList();
                        break;
                    case "Paid":
                        list = list.OrderByDescending(b => b.Paid).ToList();
                        break;
                }
            }

            #endregion
            List<AppointmentDetailsDTO> appointmentDetails = mapper.Map<List<AppointmentDetailsDTO>>(list);
            int totalCount = db.AppointmentDetails.Count();
            appointmentDetails = appointmentDetails.Skip((filter.PageNumber - 1) * filter.PageSize)
                         .Take(filter.PageSize)
                         .ToList();


            List<AppointmentDetailsDTO> response = mapper.Map<List<AppointmentDetailsDTO>>(appointmentDetails);

            return response;
        }

        public List<AppointmentDTO> GetAllAppointments(PaginationFilter filter)
        {
            StoreContext db = new StoreContext();

            List<Appointment> appointments = db.Appointment
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            List<AppointmentDTO> response = mapper.Map<List<AppointmentDTO>>(appointments);

            return response;
        }

        public AppointmentDTO GetById(int id)
        {
            StoreContext db = new StoreContext();

            Appointment appointment = db.Appointment.Where(x => x.AppointmentId == id).FirstOrDefault();

            if (appointment != null)
            {
                AppointmentDTO result = mapper.Map<AppointmentDTO>(appointment);

                return result;
            }
            else
            {
                return null;
            }

        }
    }

}

