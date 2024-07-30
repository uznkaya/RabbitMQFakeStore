using Microsoft.EntityFrameworkCore;
using Shared.RequestResponseMessageModel.Product;
using Shared.RequestResponseMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=localhost,1433; Database=RabbitMQFakeStoreDb; User Id=SA; Password=reallyStrongPwd123;TrustServerCertificate=True;MultiSubnetFailover=True");

            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RabbitMQFakeStoreDb;Trusted_Connection=True;MultiSubnetFailover=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Product>().Property(p=> p.Price).HasColumnType("decimal(18,2)");
        }

        public DbSet<Product> Products { get; set; }
    }
}
