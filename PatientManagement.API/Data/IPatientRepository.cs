using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientManagementDTO.DTO;
using PatientManagementDTO.Filter;

namespace PatientManagement.API.Data
{
    public interface IPatientRepository
    {
        public PatientDTO CreatePatient(PatientCreateDTO info);
        public PatientDTO UpdatePatient(PatientUpdateDTO info);
        public PatientDTO UpdatePatientStatusByID(string ID, string statusID);
        public PatientDTO UpdatePatientStatusByPersonalID(string personalID, string statusID);
        public PatientDTO DeletePatientByID(string ID);
        public PatientDTO FindPatientByID(string ID);
        public PatientDTO FindPatientByPersonalID(string personalID);
        public List<PatientDTO> ListPatientsByFilter(PatientFilter filter);
        public PatientShowAppointments ShowPatientAppointmentsByID(string ID);
        public PatientShowAppointments ShowPatientAppointmentsByPersonalID(string personalID);
        public Task<bool> SendEmailToPatient(string appointmentId, string patientEmail);
        public PatientStatusDTO CreatePatientStatus(PatientStatusCreateDTO info);
        public PatientStatusDTO UpdatePatientStatus(PatientStatusUpdateDTO info);
        public PatientStatusDTO DeletePatientStatus(string ID);
        public List<PatientStatusDTO> ListPatientStatuses(PaginationFilter filter);
    }
}
