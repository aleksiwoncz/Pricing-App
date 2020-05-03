using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Aleksandra_Iwończ_Pricing_App.Models
{
    public class ApplicationUser: DbContext
    {
        public ApplicationUser(DbContextOptions<ApplicationUser> options):base(options){}

        public DbSet<User> UserReg { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<Technology> Technology { get; set; }
        public DbSet<Type> Type { get; set; }

    }
}
