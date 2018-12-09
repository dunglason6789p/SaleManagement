namespace SaleManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigModel2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 450),
                        PasswordEncrypted = c.String(),
                        Salt = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.UserName, unique: true);
            
            CreateTable(
                "dbo.Brand",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ParentCategoryID = c.Int(nullable: false),
                        StoreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Point = c.Int(nullable: false),
                        StoreID = c.Int(nullable: false),
                        Money = c.Int(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        BirthDay = c.DateTime(nullable: false),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        FamilyName = c.String(),
                        LastName = c.String(nullable: false),
                        FullName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ImportBill",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        SupplierID = c.Int(nullable: false),
                        StaffID = c.Int(nullable: false),
                        TotalValue = c.Int(nullable: false),
                        DiscountValue = c.Int(nullable: false),
                        Payment = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        StoreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ImportBillDetail",
                c => new
                    {
                        ImportBillID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        SupplierID = c.Int(nullable: false),
                        StoreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ImportBillID, t.ProductID });
            
            CreateTable(
                "dbo.PaymentVoucher",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StaffID = c.Int(nullable: false),
                        SupplierID = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Value = c.Int(nullable: false),
                        Description = c.String(),
                        StoreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        UnitName = c.String(),
                        RetailPrice = c.Int(nullable: false),
                        WholesalePrice = c.Int(nullable: false),
                        WholesaleMinAmount = c.Int(nullable: false),
                        AverageCost = c.Double(nullable: false),
                        DiscountRate = c.Int(nullable: false),
                        Availability = c.Int(nullable: false),
                        CategoryName = c.String(),
                        Origin = c.String(),
                        BrandName = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Image = c.String(),
                        Description = c.String(),
                        StoreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductAvailabilityCheck",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        AmountExpected = c.Int(nullable: false),
                        AmountChecked = c.Int(nullable: false),
                        Checked = c.Boolean(nullable: false),
                        NumberOfPoorQuality = c.Int(nullable: false),
                        NumberOfLost = c.Int(nullable: false),
                        NumberOfExcess = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductID, t.Date });
            
            CreateTable(
                "dbo.ReceiptVoucher",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StaffID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Value = c.Int(nullable: false),
                        Description = c.String(),
                        StoreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SaleBill",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        CustomerID = c.Int(nullable: false),
                        StaffID = c.Int(nullable: false),
                        TotalValue = c.Int(nullable: false),
                        PaymentBank = c.Int(nullable: false),
                        PaymentCash = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DiscountValue = c.Int(nullable: false),
                        StoreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SaleBillDetail",
                c => new
                    {
                        SaleBillID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        StoreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SaleBillID, t.ProductID });
            
            CreateTable(
                "dbo.Staff",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        UserName = c.String(maxLength: 450),
                        PasswordEncrypted = c.String(),
                        Salt = c.String(),
                        Status = c.Int(nullable: false),
                        StoreID = c.Int(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        BirthDay = c.DateTime(nullable: false),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        FamilyName = c.String(),
                        LastName = c.String(nullable: false),
                        FullName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.UserName, unique: true);
            
            CreateTable(
                "dbo.Store",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AdminID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Description = c.String(),
                        StoreID = c.Int(nullable: false),
                        Money = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Staff", new[] { "UserName" });
            DropIndex("dbo.Admin", new[] { "UserName" });
            DropTable("dbo.Supplier");
            DropTable("dbo.Store");
            DropTable("dbo.Staff");
            DropTable("dbo.SaleBillDetail");
            DropTable("dbo.SaleBill");
            DropTable("dbo.ReceiptVoucher");
            DropTable("dbo.ProductAvailabilityCheck");
            DropTable("dbo.Product");
            DropTable("dbo.PaymentVoucher");
            DropTable("dbo.ImportBillDetail");
            DropTable("dbo.ImportBill");
            DropTable("dbo.Customer");
            DropTable("dbo.Category");
            DropTable("dbo.Brand");
            DropTable("dbo.Admin");
        }
    }
}
