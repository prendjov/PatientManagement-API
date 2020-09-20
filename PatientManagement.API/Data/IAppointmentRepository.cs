using PatientManagement.API.Wrappers;
using PatientManagementDTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientManagementDTO.Filter;
namespace PatientManagement.API.Data
{
    public interface IAppointmentRepository
    {
        public List<AppointmentDTO> GetAllAppointments(PaginationFilter filter);

        public AppointmentDTO GetById(int id);

        string AddAppointment(AppointmentDTO appointment);

        List<AppointmentDetailsDTO> AppointmentsFilterListing(AppointmentFilter filter);
    }
}
