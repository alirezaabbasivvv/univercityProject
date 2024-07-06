using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using univercityProject.Models.Context;
using univercityProject.Models.DBModel;
using univercityProject.Models.Dtos;

namespace univercityProject.Services
{
    public interface IUserservices
    {
        int ValidateUser(string username, string password);
        IEnumerable<UserDto> GetAllUsers();
        bool AddUser(UserDto userinfo);
        bool RemoveUser(int id);
        bool EditUser(UserDto userinfo);
        UserDto GetUserById(int id);
    }

    public class UserServices : IUserservices
    {
        private UNDBContext _context { get; set; }

        public UserServices(UNDBContext context)
        {
            _context = context;
        }

        public bool AddUser(UserDto userinfo)
        {
            if (userinfo == null || !ValidateUserDto(userinfo))
                return false;

            try
            {
                var user = new User
                {
                    FirstName = userinfo.FirstName,
                    LastName = userinfo.LastName,
                    IsRemoved = false,
                    Rule = userinfo.Rule,
                    PhoneNumber = userinfo.PhoneNumber,
                    Password = userinfo.Password
                };

                _context.users.Add(user);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EditUser(UserDto userinfo)
        {
            if (userinfo == null || !ValidateUserDto(userinfo))
                return false;

            try
            {
                var user = _context.users.FirstOrDefault(u => u.ID == userinfo.ID && !u.IsRemoved);
                if (user == null)
                    return false;

                user.FirstName = userinfo.FirstName;
                user.LastName = userinfo.LastName;
                user.PhoneNumber = userinfo.PhoneNumber;
                user.Rule = userinfo.Rule;
                user.Password = userinfo.Password;

                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public UserDto GetUserById(int id)
        {
            try
            {
                var user = _context.users.FirstOrDefault(u => u.ID == id && !u.IsRemoved);
                if (user == null)
                    return null;

                return new UserDto
                {
                    ID = user.ID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Rule = user.Rule
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool RemoveUser(int id)
        {
            try
            {
                var user = _context.users.FirstOrDefault(u => u.ID == id && !u.IsRemoved);
                if (user == null)
                    return false;

                user.IsRemoved = true;
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool ValidateUserDto(UserDto userinfo)
        {
            if (string.IsNullOrWhiteSpace(userinfo.FirstName) || string.IsNullOrWhiteSpace(userinfo.LastName))
                return false;

            if (string.IsNullOrWhiteSpace(userinfo.PhoneNumber) || userinfo.PhoneNumber.Length != 10)
                return false;

            if (!Enum.IsDefined(typeof(Rules), userinfo.Rule))
                return false;

            return true;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            try
            {
                return _context.users.Select(p => new UserDto { FirstName = p.FirstName, ID = p.ID, LastName = p.LastName, PhoneNumber = p.PhoneNumber, Rule = p.Rule }).ToList();
            }
            catch (Exception ex)
            {

                return new List<UserDto>();
            }
        }
        public int ValidateUser(string username, string password)
        {
            var user = _context.users.FirstOrDefault(u => u.PhoneNumber == username && u.Password == password);
            if (user == null)
                return 0;


            return user.ID;
        }
    }
}
