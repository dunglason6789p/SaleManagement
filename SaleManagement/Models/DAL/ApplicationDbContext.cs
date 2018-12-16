using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

using SaleManagement.Models.History;

namespace SaleManagement.Models.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\mydatabase.mdf;Integrated Security=True") //Connection string.
        {

        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Customer> Customer { get; set; }        
        public DbSet<ImportBill> ImportBill { get; set; }
        public DbSet<ImportBillDetail> ImportBillDetail { get; set; }
        public DbSet<PaymentVoucher> PaymentVoucher { get; set; }
        public DbSet<Product> Product { get; set; }        
        public DbSet<ReceiptVoucher> ReceiptVoucher { get; set; }
        public DbSet<SaleBill> SaleBill { get; set; }
        public DbSet<SaleBillDetail> SaleBillDetail { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Supplier> Supplier { get; set; }

        //History.
        public DbSet<ProductAvailabilityCheck> ProductAvailabilityCheck { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ApplicationDbContext>(null);
            base.OnModelCreating(modelBuilder);

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

        /*
        public void AddAndSaveChanges<T>(T objectX)
        {
            this.<T>.
        }
        */
    }
}