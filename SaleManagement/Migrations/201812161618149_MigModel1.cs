namespace SaleManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigModel1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SaleBill", "CustomerName");
            DropColumn("dbo.SaleBill", "StaffName");
            DropColumn("dbo.SaleBill", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SaleBill", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.SaleBill", "StaffName", c => c.String());
            AddColumn("dbo.SaleBill", "CustomerName", c => c.String());
        }
    }
}
