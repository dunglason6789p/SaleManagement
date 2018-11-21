namespace SaleManagement.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SaleManagement.Models.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SaleManagement.Models.DAL.ApplicationDbContext context)
        {
            context.Customer.Add(new Models.Customer()
            {
                Point = 0,
                Gender = true,
                BirthDay = DateTime.Now,
                Phone = "0363503879",
                Email = "minhlv1509@gmail.com",
                Address = "128C Đại La, Hà Nội",
                FamilyName = "Lê",
                LastName = "Minh",
                FullName = "Lê Văn Minh"
            });
            context.Account.Add(new Models.Account()
            {
                UserName = "minhlv",
                PasswordEncrypted = "brotherMinh",
                Salt = null,
                Status = 0,
                CustomerID = 0,
                RoleCode = 0
            });
        }
    }
}
