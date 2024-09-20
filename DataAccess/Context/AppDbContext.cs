using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MuhasebeProgramı.Data.Context
{
    public class AppDbContext : IdentityDbContext
    {
        protected AppDbContext()
        {
        }

        public AppDbContext( DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Saler> Salers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Buy> Buys { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
