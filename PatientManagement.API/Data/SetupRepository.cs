using AutoMapper;
using PatientManagement.API.Models;
using PatientManagementDTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientManagement.API.Models;

namespace PatientManagement.API.Data
{
    public class SetupRepository : ISetupRepository
    {
        private readonly IMapper mapper;

        public SetupRepository(IMapper _mapper)
        {
            mapper = _mapper;
        }

        public BloodTypeDTO CreateSetup(BloodTypeDTO _Setup)
        {
            var db = new StoreContext();
            BloodType Setup = mapper.Map<BloodType>(_Setup);

            db.Add(Setup);
            db.SaveChanges();

            return mapper.Map<BloodTypeDTO>(Setup);

        }

        public List<BloodTypeDTO> GetAllBloodType()

        {
            var db = new StoreContext();
            List<BloodType> list = db.BloodType.ToList();

            List<BloodTypeDTO> types = mapper.Map<List<BloodTypeDTO>>(list);

            return types;

        }
    }
}

