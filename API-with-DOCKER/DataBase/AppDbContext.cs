using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using API_with_DOCKER.models;

namespace API_with_DOCKER.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
