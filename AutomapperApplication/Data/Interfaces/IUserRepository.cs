using AutomapperApplication.Models;
using System.Collections.Generic;

namespace AutomapperApplication.Data.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(int id);
        void Create(User user);
        void Update(User user);
        void Save();
        void Delete(int id);
        void Save(User user);
    }
}
