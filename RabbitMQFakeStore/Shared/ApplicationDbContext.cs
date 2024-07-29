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

       public DbSet<Product> Products { get; set; }
    }
}
