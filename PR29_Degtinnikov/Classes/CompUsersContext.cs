using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR29_Degtinnikov.Classes
{
    public class CompUsersContext : DbContext
    {
        public DbSet<Models.Clubs> Clubs { get; set; }
        public DbSet<Models.Users> Users { get; set; }

        public CompUsersContext()
        {
            try
            {
                Database.EnsureCreated();
                Clubs.Load();
                Users.Load();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка инициализации БД: {ex.Message}");
                throw;
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Common.Config.ConnectionConfig, Common.Config.Version);
        }
    }
}
