using DenemeCoreAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DenemeCoreAPI.Data
{
    public class ApplicationDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=apideneme;user Id=sa;Password=123");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Product> Products { get; set; }
    }
}