namespace SaleManagement.Migrations
{
    using SaleManagement.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SaleManagement.Controllers.Utilities;
    using SaleManagement.Models.DAL;
    using System.Diagnostics;

    internal sealed class Configuration : DbMigrationsConfiguration<SaleManagement.Models.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }       

        protected override void Seed(SaleManagement.Models.DAL.ApplicationDbContext context)
        {
            
        }
    }

    
}
