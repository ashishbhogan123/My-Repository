using Microsoft.EntityFrameworkCore;
using Demo.Models;

namespace Demo.dal

{
    public class CustomerDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // step 2 :- mapping
            modelBuilder.Entity<Customer>().ToTable("tblorder");
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlServer(@"Data Source=ASHISH1234\MSBI;Initial Catalog=SGMCOE;Integrated Security=True");
        }
        public DbSet<Customer> Customers { get; set; }
        

    }
}
