using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularApp.Models
{
  public class AuthenticationContext:IdentityDbContext
  {
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public AuthenticationContext(DbContextOptions options):base(options)
    {

    }
  }
}
