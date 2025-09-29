using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR29_Degtinnikov.Classes
{
    public class ClubsContext: DbContext
    {
        public DbSet<Models.Clubs> Clubs { get; set; }
        public ClubsContext() 
        {
            Database.EnsureCreated();
            Clubs.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Common.Config.ConnectionConfig, Common.Config.Version);
        }
    }
}
