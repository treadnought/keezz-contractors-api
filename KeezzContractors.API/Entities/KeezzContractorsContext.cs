using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Entities
{
    public class KeezzContractorsContext : DbContext
    {
        public KeezzContractorsContext(DbContextOptions<KeezzContractorsContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<ContractorInvoice> ContractorInvoices { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}
