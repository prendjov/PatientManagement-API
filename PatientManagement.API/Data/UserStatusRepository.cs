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

namespace PatientManagement.API.Data
{
    public class UserStatusRepository : IUserStatusRepository
    {
        private readonly IMapper mapper;

        public UserStatusRepository(IMapper _mapper)
        {
            mapper = _mapper;
        }
        public UserStatusDTO CreateStatus(UserStatusCreate info)
        {
            StoreContext db = new StoreContext();
            
            UserStatus UserStatus = mapper.Map<UserStatus>(info);

            if (UserStatus != null)
            {
                db.UserStatus.Add(UserStatus);
                db.SaveChanges();

                UserStatusDTO response = mapper.Map<UserStatusDTO>(UserStatus);

                return response;
            }
            else return null;
        }
        
    }
}
