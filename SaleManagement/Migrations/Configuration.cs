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
            context.Admin.Add(new Models.Admin()
            {
                UserName = "minhlv",
                PasswordEncrypted = "brotherMinh",
                Salt = null,
                Status = 0,
            });
        }
    }
}
