using Microsoft.EntityFrameworkCore;
using ProcessPaymentTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPaymentTask
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
          : base(options)
        { }
        public DbSet<CardInformation> CardInformation { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
    }
}
