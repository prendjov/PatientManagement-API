using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientManagement.API.Models;
using PatientManagement.API.Security;
using PatientManagement.API.Wrappers;
using PatientManagementDTO.DTO;
using PatientManagementDTO.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientManagement.API.Data
{
    public class UserRepository:IUserRepository
    {
        private readonly IMapper mapper;
        private readonly IAuthRepository authUserRepository;
        private readonly StoreContext db;
        public UserRepository(IMapper _mapper, IAuthRepository _authUserRepository)
        {
            mapper = _mapper;
            authUserRepository = _authUserRepository;
            // db = _db;
        }
        public UserDTO AddUser(UserCreateDTO user)
        {
            StoreContext db = new StoreContext();
            User newUser = mapper.Map<User>(user);
            if (db.User.Where(x => x.Email == newUser.Email).Count() == 0)
            {
                db.User.Add(newUser);
                db.SaveChanges();
                UserDTO result = mapper.Map<UserDTO>(newUser);
                return result;
            }
            else
                return null;
            
        }
        public bool UpdateUser(UserDTO request)
        {
            StoreContext db = new StoreContext();
            int userId = Convert.ToInt32(EncryptionHelper.Decrypt(request.UserId));

            User updateUser = db.User.Where(s => s.UserId == userId).FirstOrDefault();
            if (updateUser != null)
            {
                updateUser.FirstName = request.FirstName;
                updateUser.LastName = request.LastName;
                updateUser.Email = request.Email;
                updateUser.RoleId = Convert.ToInt32(EncryptionHelper.Decrypt(request.RoleId));
                updateUser.StatusId = Convert.ToInt32(EncryptionHelper.Decrypt(request.StatusId));
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public PagedResponse<List<UserDTO>> GetAllUsers(PaginationFilter filter)
        {
            StoreContext db = new StoreContext();
            List<User> list = db.User
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();
            var users = mapper.Map<List<UserDTO>>(list);
            int totalCount = db.User.Count();
            PagedResponse<List<UserDTO>> response = new PagedResponse<List<UserDTO>>()
            {
                Data = users,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalRecords = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (decimal)filter.PageSize)
            };
            return response;
        }
        public UserDTO GetUserById(string id)
        {
            StoreContext db = new StoreContext();
            int uId = Convert.ToInt32(EncryptionHelper.Decrypt(id));
            User user = db.User.Where(s => s.UserId == uId).FirstOrDefault();

            if (user != null)
            {
                UserDTO result = mapper.Map<UserDTO>(user);
                return result;
            }
            else
            {
                return null;
            }
        }
        public bool DeleteUser(string id)
        {
            StoreContext db = new StoreContext();
            int userId = Convert.ToInt32(EncryptionHelper.Decrypt(id));
            User users = db.User.Where(s => s.UserId == userId).FirstOrDefault();
            if (users != null)
            {
                db.User.Remove(users);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<UserDTO> Active()
        {
            var db = new StoreContext();


            UserStatus status = db.UserStatus.Where(x => x.Name == "Active").First();

            List<User> list = db.User.Where(x => x.StatusId == status.StatusId).ToList();
           List<UserDTO> users = mapper.Map<List<UserDTO>>(list);
            return users;
        }
        public ULoginResponseDTO Login(UserLoginDTO user)
        {
            StoreContext db = new StoreContext();

            string encryptedEmail = EncryptionHelper.Encrypt(user.Email);
            string encryptedPassword = EncryptionHelper.Encrypt(user.Password);

            User userss = db.User.Where(x => x.Email == encryptedEmail
                                                    && x.Password == encryptedPassword).FirstOrDefault();

            if (userss != null)
            {
                return new ULoginResponseDTO()
                {
                    user = mapper.Map<UserDTO>(userss),
                    AuthToken = authUserRepository.CreateToken(userss)
                };
            }
            else
            {
                return null;
            }

        }
    }
}
