using AutomapperApplication.Models;
using Microsoft.EntityFrameworkCore;
using AutomapperApplication.ViewModels;

namespace AutomapperApplication.Data
{
    public class MApperUsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<IndexUserView> IndexUserView { get; set; }
        public DbSet<EditUserView> EditUserView { get; set; }

        public MApperUsersContext(DbContextOptions<MApperUsersContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
