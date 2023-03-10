using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SehirRehberi.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SehirRehberi.DataAccess
{
    public class Context:DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=SehirRehberDb;integrated security=true;Encrypt=false");
            base.OnConfiguring(optionsBuilder); 
        }
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
