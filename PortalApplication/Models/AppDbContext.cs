using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortalApplication.ViewModels;

namespace PortalApplication.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

            

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        //public DbSet<PortalApplication.ViewModels.CreateRoleViewModel> CreateRoleViewModel { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //public DbSet<PortalApplication.ViewModels.EditUserViewModel> EditUserViewModel { get; set; }
    }
}
