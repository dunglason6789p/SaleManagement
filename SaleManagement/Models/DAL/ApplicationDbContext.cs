using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SaleManagement.Models.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\mydatabase.mdf;Integrated Security=True") //Connection string.
        {

        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<DetailSaleBill> DetailSaleBill { get; set; }
        public DbSet<DetailWareHouseReceiptBill> DetailWareHouseReceiptBill { get; set; }
        public DbSet<PaymentVoucher> PaymentVoucher { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductGroup> ProductGroup { get; set; }
        public DbSet<ReceiptVoucher> ReceiptVoucher { get; set; }
        public DbSet<SaleBill> SaleBill { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<WareHouseReceiptBill> WareHouseReceiptBill { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            /*
            modelBuilder.Entity<Customer>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Customer");
            });

            modelBuilder.Entity<Staff>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Staff");
            });
            */
        }
    }
}