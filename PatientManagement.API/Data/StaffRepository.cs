using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PatientManagement.API.Models;
using PatientManagement.API.Services;
using PatientManagement.API.Wrappers;
using PatientManagementDTO.DTO;
using PatientManagementDTO.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientManagement.API.Data
{
    public class StaffRepository:IStaffRepository
    {
        private readonly IMapper mapper;
        

        public StaffRepository(IMapper _mapper)
        {
            mapper = _mapper;
            
        }
        
        public StaffDTO CreateStaff(StaffCreateDTO _staff)
        {
            StoreContext db = new StoreContext();
            Staff newStaff = mapper.Map<Staff>(_staff);

            if (newStaff != null)
            {
                db.Add(newStaff);
                db.SaveChanges();

                
            }

            

            return  mapper.Map<StaffDTO>(newStaff);

        }

        public StaffDTO UpdateStaff(StaffDTO _staff)
        {
            StoreContext db = new StoreContext();
            Staff newStaff = db.Staff.Where(s => s.StaffId == Helper.DecryptInt(_staff.StaffId)).FirstOrDefault();

            if (newStaff != null)
            {
                newStaff.UserId = Helper.DecryptInt(_staff.UserId);
                newStaff.WorkingDays = _staff.WorkingDays;
                newStaff.Experience = _staff.Experience;
                newStaff.StaffCode = _staff.StaffCode;
                newStaff.Salary = decimal.Parse(_staff.Salary);
            }

            db.SaveChanges();

            return mapper.Map<StaffDTO>(newStaff);

        }
        public StaffDTO DeleteStaff([FromQuery]DeleteStaffDTO id)
        {
            StoreContext db = new StoreContext();
            Staff staff = db.Staff.Where(s => s.StaffId == id.StaffId).FirstOrDefault();
            

            db.Staff.Remove(staff);
            db.SaveChanges();
            StaffDTO deletedStaff = mapper.Map<StaffDTO>(staff);

            return deletedStaff;


            
        }

        public StaffDTO GetById(string staffId)
        {
            StoreContext db = new StoreContext();

            int sid = Helper.DecryptInt(staffId);

            Staff staff = db.Staff.Where(s => s.StaffId == sid).Include(u => u.User).ThenInclude(s => s.Status).FirstOrDefault();

            StaffDTO _staff = mapper.Map<StaffDTO>(staff);

            return _staff;
        }

        public PagedResponse<List<StaffDTO>> GetAllStaff(PaginationFilter filter)
        {
            StoreContext db = new StoreContext();
            List<Staff> staff = db.Staff
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Include(u=>u.User.Status)
                .ThenInclude(s=>s.User)
                .ToList();
               

            List<StaffDTO> list = mapper.Map<List<StaffDTO>>(staff);

            int totalCount = db.Staff.Count();

            PagedResponse<List<StaffDTO>> response = new PagedResponse<List<StaffDTO>>()
            {
                Data = list,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalRecords = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (decimal)filter.PageSize)
            };

            return response;

        }
        

    }
}
