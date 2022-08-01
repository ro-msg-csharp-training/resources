using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineOrder.Model;

namespace OnlineOrder.Data
{
    public class OnlineOrderContext : DbContext
    {
        public OnlineOrderContext (DbContextOptions<OnlineOrderContext> options)
            : base(options)
        {
        }

        public DbSet<OnlineOrder.Model.Product> Product { get; set; } = default!;

    }
}
