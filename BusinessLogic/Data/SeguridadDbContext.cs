using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Data
{
    public class SeguridadDbContext : IdentityDbContext<Usuario>
    {
        public SeguridadDbContext(DbContextOptions<SeguridadDbContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        


    }

}
