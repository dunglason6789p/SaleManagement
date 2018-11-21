namespace SaleManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigModel1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        PasswordEncrypted = c.String(),
                        Salt = c.String(),
                        Status = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        RoleCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserName)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: false)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Point = c.Int(nullable: false),
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
                "dbo.DetailSaleBill",
                c => new
                    {
                        SaleBillID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        WareHouseReceiptBill_ID = c.Int(),
                    })
                .PrimaryKey(t => new { t.SaleBillID, t.ProductID })
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: false)
                .ForeignKey("dbo.SaleBill", t => t.SaleBillID, cascadeDelete: false)
                .ForeignKey("dbo.WareHouseReceiptBill", t => t.WareHouseReceiptBill_ID)
                .Index(t => t.SaleBillID)
                .Index(t => t.ProductID)
                .Index(t => t.WareHouseReceiptBill_ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UnitName = c.String(),
                        Price = c.Int(nullable: false),
                        AverageCost = c.String(),
                        DiscountRate = c.Int(nullable: false),
                        Description = c.String(),
                        Origin = c.String(),
                        Brand = c.String(),
                        Availability = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SaleBill",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        TotalValue = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DiscountValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: false)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.DetailWareHouseReceiptBill",
                c => new
                    {
                        WareHouseReceiptBillID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        SupplierID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WareHouseReceiptBillID, t.ProductID })
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: false)
                .ForeignKey("dbo.Supplier", t => t.SupplierID, cascadeDelete: false)
                .ForeignKey("dbo.WareHouseReceiptBill", t => t.WareHouseReceiptBillID, cascadeDelete: false)
                .Index(t => t.WareHouseReceiptBillID)
                .Index(t => t.ProductID)
                .Index(t => t.SupplierID);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.WareHouseReceiptBill",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SupplierID = c.Int(nullable: false),
                        TotalValue = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DiscountValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Supplier", t => t.SupplierID, cascadeDelete: false)
                .Index(t => t.SupplierID);
            
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Staff", t => t.StaffID, cascadeDelete: false)
                .ForeignKey("dbo.Supplier", t => t.SupplierID, cascadeDelete: false)
                .Index(t => t.StaffID)
                .Index(t => t.SupplierID);
            
            CreateTable(
                "dbo.Staff",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
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
                "dbo.ProductGroup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Supplier", t => t.CustomerID, cascadeDelete: false)
                .ForeignKey("dbo.Staff", t => t.StaffID, cascadeDelete: false)
                .Index(t => t.StaffID)
                .Index(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReceiptVoucher", "StaffID", "dbo.Staff");
            DropForeignKey("dbo.ReceiptVoucher", "CustomerID", "dbo.Supplier");
            DropForeignKey("dbo.PaymentVoucher", "SupplierID", "dbo.Supplier");
            DropForeignKey("dbo.PaymentVoucher", "StaffID", "dbo.Staff");
            DropForeignKey("dbo.DetailWareHouseReceiptBill", "WareHouseReceiptBillID", "dbo.WareHouseReceiptBill");
            DropForeignKey("dbo.WareHouseReceiptBill", "SupplierID", "dbo.Supplier");
            DropForeignKey("dbo.DetailSaleBill", "WareHouseReceiptBill_ID", "dbo.WareHouseReceiptBill");
            DropForeignKey("dbo.DetailWareHouseReceiptBill", "SupplierID", "dbo.Supplier");
            DropForeignKey("dbo.DetailWareHouseReceiptBill", "ProductID", "dbo.Product");
            DropForeignKey("dbo.DetailSaleBill", "SaleBillID", "dbo.SaleBill");
            DropForeignKey("dbo.SaleBill", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.DetailSaleBill", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Account", "CustomerID", "dbo.Customer");
            DropIndex("dbo.ReceiptVoucher", new[] { "CustomerID" });
            DropIndex("dbo.ReceiptVoucher", new[] { "StaffID" });
            DropIndex("dbo.PaymentVoucher", new[] { "SupplierID" });
            DropIndex("dbo.PaymentVoucher", new[] { "StaffID" });
            DropIndex("dbo.WareHouseReceiptBill", new[] { "SupplierID" });
            DropIndex("dbo.DetailWareHouseReceiptBill", new[] { "SupplierID" });
            DropIndex("dbo.DetailWareHouseReceiptBill", new[] { "ProductID" });
            DropIndex("dbo.DetailWareHouseReceiptBill", new[] { "WareHouseReceiptBillID" });
            DropIndex("dbo.SaleBill", new[] { "CustomerID" });
            DropIndex("dbo.DetailSaleBill", new[] { "WareHouseReceiptBill_ID" });
            DropIndex("dbo.DetailSaleBill", new[] { "ProductID" });
            DropIndex("dbo.DetailSaleBill", new[] { "SaleBillID" });
            DropIndex("dbo.Account", new[] { "CustomerID" });
            DropTable("dbo.ReceiptVoucher");
            DropTable("dbo.ProductGroup");
            DropTable("dbo.Staff");
            DropTable("dbo.PaymentVoucher");
            DropTable("dbo.WareHouseReceiptBill");
            DropTable("dbo.Supplier");
            DropTable("dbo.DetailWareHouseReceiptBill");
            DropTable("dbo.SaleBill");
            DropTable("dbo.Product");
            DropTable("dbo.DetailSaleBill");
            DropTable("dbo.Customer");
            DropTable("dbo.Account");
        }
    }
}
