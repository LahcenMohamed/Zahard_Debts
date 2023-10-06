using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zahard_Debts
{
    public class ZDDBEntities : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = @"C:\Zahard_DebtsData\ZDDB.db";
            optionsBuilder.UseSqlite($"Data Source = {path}");
        }

        public DbSet<Admins> admins { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Debts> Debts { get; set; }
    }
}
