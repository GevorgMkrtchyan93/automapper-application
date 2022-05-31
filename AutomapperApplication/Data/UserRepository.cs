using AutomapperApplication.Data.Interfaces;
using AutomapperApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AutomapperApplication.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly MApperUsersContext _context;
        public UserRepository(MApperUsersContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Save(User user)
        {
            _context.Users.Update(user);
            _context.SaveChangesAsync();
        }
    }
}
