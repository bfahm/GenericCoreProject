using GenericCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCore.Persistance.DbContexts
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        // pass the parameter (the options) that will enter the constructor function to the base constructor
        // of the parent class
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // Call base class original function that we're overriding to prevent errors
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
