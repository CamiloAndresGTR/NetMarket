using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class MarketDbContext : DbContext
    {
        //Para que la clase MarketDbContext pueda leer dinamicamente el valor del dbcontext,
        ////le indicamos que reciba un parametro options
        public MarketDbContext(DbContextOptions<MarketDbContext> options) : base(options)
        {
        }
        //Defino las clases que se convertiran en entidades en la DB
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<TradeMark> TradeMark { get; set; }

        //Sobre-escribo el metodo Modelbuilder para que tome los contraints que creé en  ProductConfig
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
