using Microsoft.EntityFrameworkCore;
using PR29_Degtinnikov.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR29_Degtinnikov.Classes
{
    public class UserContext:DbContext
    {
        public DbSet<Models.Users> Users { get; set; }
        public UserContext() 
        {
            Database.EnsureCreated();
            Users.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Common.Config.ConnectionConfig, Common.Config.Version);
        }
    }
}
