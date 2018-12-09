using SaleManagement.Controllers.Utilities;
using SaleManagement.Models;
using SaleManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleManagement.Controllers.DBCreating
{
    public class DBCreateController : Controller
    {
        // GET: DBCreate
        public ActionResult Index()
        {
            GlobalHelper_.CURRENT_STORE_ID = 0;

            string salt = EncryptionHelper.GetSalt();
            GlobalHelper_.context.Admin.Add(new Models.Admin()
            {
                UserName = "ntson",
                PasswordEncrypted = EncryptionHelper.GetHash("123"+ salt),
                Salt = salt,
                Status = 0,
            });
            GlobalHelper_.context.SaveChanges();

            Store store = new Store();
            store.AdminID = 0;
            store.Name = "Store0";
            GlobalHelper_.context.Store.Add(store);
            GlobalHelper_.context.SaveChanges();

            #region INIT ALL FIRST-COME DATA
            StaffHelper_.CreateRandomStaffsToDB();
            ProductHelper_.Init();
            SupplierHelper_.CreateSuppliersToDB();
            #endregion

            List<DateTime> days = DateHelper_.GetDefaultDateList();
            foreach (var day in days)
            {
                ImportBillHelper_.NewImport(day);
            }

            return View();
        }
    }


    public static class GlobalHelper_
    {
        public static int CURRENT_STORE_ID;

        public static readonly Random randomizer = new Random();

        public static ApplicationDbContext context = new ApplicationDbContext();

        private static T GetRandomItem<T>(List<T> list)
        {
            return list[GlobalHelper_.randomizer.Next(list.Count)];
        }
    }

    public static class HumanNameHelper_
    {
        private static List<string> Ho_ = new List<string> { "Nguyễn", "Nguyễn", "Nguyễn", "Nguyễn", "Trần", "Bùi", "Lê", "Đinh", "Đặng" };
        private static List<string> Dem_Nam = new List<string> { "Văn", "Văn", "Văn", "Văn", "Thành", "Công", "Tuấn", "Hữu", "Việt", "Chí" };
        private static List<string> Dem_Nu = new List<string> { "Thị", "Thị", "Thị", "Thị", "Mai", "Thu", "Kiều", "Thanh" };
        private static List<string> Ten_Nam = new List<string> { "Cường", "Bách", "Dũng", "Giang", "Hùng", "Hiếu", "Mạnh", "Phát", "Quyết", "Sơn", "Thắng" };
        private static List<string> Ten_Nu = new List<string> { "Châu", "Dung", "Giang", "Hương", "Linh", "Liên", "Lan", "Nhung", "Phương", "Quỳnh", "Trang", "Thu" };

        private static List<string> Que_ = new List<string> { "Hà Nội", "Hải Phòng", "Nam Định", "Thanh Hóa", "Thái Nguyên", "Hà Giang", "Hưng Yên", "Quảng Ninh" };
        private static List<string> Quan_ = new List<string> { "Thanh Xuân", "Hoàng Mai", "Đống Đa", "Ba Đình", "Hai Bà Trưng" };

        public static T GetRandomItem<T>(List<T> list)
        {
            return list[GlobalHelper_.randomizer.Next(list.Count)];
        }

        public static string GetRandomHoDemNam()
        {
            string ho = GetRandomItem<string>(Ho_);
            string dem = GetRandomItem<string>(Dem_Nam);
            string result = ho + " " + dem;
            return result;
        }

        public static string GetRandomHoDemNu()
        {
            string ho = GetRandomItem<string>(Ho_);
            string dem = GetRandomItem<string>(Dem_Nu);
            string result = ho + " " + dem;
            return result;
        }

        public static string GetRandomFullName_Nam()
        {
            return GetRandomHoDemNam() + " " + GetRandomItem(Ten_Nam);
        }

        public static string GetRandomFullName_Nu()
        {
            return GetRandomHoDemNu() + " " + GetRandomItem(Ten_Nu);
        }
    }

    public static class PhoneHelper_
    {
        public static string GetRandomSoDienThoai()
        {
            string sdt = "0";
            for (int i = 0; i < 10; i++)
            {
                sdt += (GlobalHelper_.randomizer.Next(10));
            }
            return sdt;
        }
    }

    public static class StaffHelper_
    {
        public static List<Staff> staffs; //PURPOSE : Access these data without query in DB.

        public static void CreateRandomStaffsToDB() //MUST BE CALLED IMMEDIATELY WHEN START Seed().
        {
            staffs = new List<Staff>();
            for (int i = 0; i <= 20; i++)
            {
                bool isBoy = GlobalHelper_.randomizer.Next(2) == 0 ? true : false;

                Staff staff = new Staff();
                staff.Code = "NV000" + (i < 10 ? "0" + i.ToString() : i.ToString());
                staff.UserName = staff.Code;
                staff.Salt = EncryptionHelper.GetSalt();
                staff.PasswordEncrypted = EncryptionHelper.GetHash("123" + staff.Salt);
                staff.Status = 0;
                staff.StoreID = GlobalHelper_.CURRENT_STORE_ID;

                staff.Gender = isBoy ? true : false;
                staff.BirthDay = DateTime.Now;
                staff.Phone = PhoneHelper_.GetRandomSoDienThoai();
                staff.Email = "example@abc.com";
                staff.Address = "Hà Nội";
                staff.FullName = isBoy ? HumanNameHelper_.GetRandomFullName_Nam() : HumanNameHelper_.GetRandomFullName_Nu();

                staffs.Add(staff);
            }

            GlobalHelper_.context.Staff.AddRange(staffs);
            GlobalHelper_.context.SaveChanges();
        }
    }

    public static class CustomerHelper_
    {
        public static List<Customer> customers; //PURPOSE : Access these data without query in DB.

        public static void CreateRandomStaffsToDB() //MUST BE CALLED IMMEDIATELY WHEN START Seed().
        {
            customers = new List<Customer>();
            for (int i = 0; i <= 20; i++)
            {
                bool isBoy = GlobalHelper_.randomizer.Next(2) == 0 ? true : false;

                Customer staff = new Customer();
                
                staff.StoreID = GlobalHelper_.CURRENT_STORE_ID;

                staff.Gender = isBoy ? true : false;
                staff.BirthDay = DateTime.Now;
                staff.Phone = PhoneHelper_.GetRandomSoDienThoai();
                staff.Email = "example@abc.com";
                staff.Address = "Hà Nội";
                staff.FullName = isBoy ? HumanNameHelper_.GetRandomFullName_Nam() : HumanNameHelper_.GetRandomFullName_Nu();

                customers.Add(staff);
            }

            GlobalHelper_.context.Customer.AddRange(customers);
            GlobalHelper_.context.SaveChanges();
        }
    }

    public static class DateHelper_
    {
        private static DateTime dateShouldStart = new DateTime(2018, 11, 1);
        private static DateTime dateShouldEnd = new DateTime(2018, 12, 1);
        public static List<DateTime> GetDefaultDateList()
        {
            return GetDateList(dateShouldStart, dateShouldEnd);
        }
        public static List<DateTime> GetDateList(DateTime from, DateTime thru)
        {
            List<DateTime> dateTimes = new List<DateTime>();
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                dateTimes.Add(day);
            return dateTimes;
        }
    }

    public static class PriceHelper_
    {
        public static int PriceFlutuate(this int price)
        {
            if (GlobalHelper_.randomizer.Next() == 0)
            {
                return price + 1000;
            }
            else
            {
                if (price > 1000) return price - 1000;
                else return price;
            }
        }
    }

    public static class ProductHelper_
    {
        private static bool Called;
        public static List<Product> soulProducts; //Linh hồn của sản phẩm.
        public static List<bool> added;

        public static void Init() //Create a product list. NOT inserted to DB yet ! //MUST BE CALLED IMMEDIATELY WHEN START Seed().
        {
            soulProducts = new List<Product>();

            soulProducts.Add(new Product() { Code = "	SP000025	", Name = "	Mật ong nguyên chất Tây Nguyên	", UnitName = "	Hộp	", RetailPrice = 75000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 71200, DiscountRate = 0, Availability = 10, AmountSold = 0, CategoryName = "	Thực phẩm tự nhiên từ động vật	", Origin = "	Việt Nam	", BrandName = "	Thảo Thảo Pharm	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/25.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000024	", Name = "	Mật ong rừng nguyên chất	", UnitName = "	Hộp	", RetailPrice = 250000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 237500, DiscountRate = 0, Availability = 18, AmountSold = 0, CategoryName = "	Thực phẩm tự nhiên từ động vật	", Origin = "	Việt Nam	", BrandName = "	Thảo Thảo Pharm	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/24.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000023	", Name = "	Mật ong Breitsamer 340g	", UnitName = "	Hộp	", RetailPrice = 140000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 133000, DiscountRate = 0, Availability = 12, AmountSold = 0, CategoryName = "	Thực phẩm tự nhiên từ động vật	", Origin = "	Việt Nam	", BrandName = "	Thảo Thảo Pharm	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/23.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000022	", Name = "	Mật ong Breitsamer 500g	", UnitName = "	Hộp	", RetailPrice = 192500, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 182800, DiscountRate = 0, Availability = 10, AmountSold = 0, CategoryName = "	Thực phẩm tự nhiên từ động vật	", Origin = "	Việt Nam	", BrandName = "	Thảo Thảo Pharm	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/22.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000021	", Name = "	Mật ong Breitsamer 250g	", UnitName = "	Hộp	", RetailPrice = 105000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 99000, DiscountRate = 0, Availability = 4, AmountSold = 0, CategoryName = "	Thực phẩm tự nhiên từ động vật	", Origin = "	Việt Nam	", BrandName = "	Thảo Thảo Pharm	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/21.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000020	", Name = "	Yến mạch nguyên chất ăn liền	", UnitName = "	Hộp	", RetailPrice = 102000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 96900, DiscountRate = 0, Availability = 0, AmountSold = 0, CategoryName = "	Ngũ cốc, sản phẩm từ ngũ cốc	", Origin = "	Việt Nam	", BrandName = "	Quế Lân INC	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/20.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000019	", Name = "	Yến mạch nguyên chất Captain Oats	", UnitName = "	Hộp	", RetailPrice = 75000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 71200, DiscountRate = 0, Availability = 4, AmountSold = 0, CategoryName = "	Ngũ cốc, sản phẩm từ ngũ cốc	", Origin = "	Việt Nam	", BrandName = "	Quế Lân INC	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/19.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000018	", Name = "	Yến mạch Captain Oát - Instant Oatmeal 500g	", UnitName = "	Hộp	", RetailPrice = 80000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 76000, DiscountRate = 0, Availability = 4, AmountSold = 0, CategoryName = "	Ngũ cốc, sản phẩm từ ngũ cốc	", Origin = "	Việt Nam	", BrandName = "	Quế Lân INC	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/18.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000017	", Name = "	Ngũ cốc dinh dưỡng canxi	", UnitName = "	Hộp	", RetailPrice = 47000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 44600, DiscountRate = 0, Availability = 8, AmountSold = 0, CategoryName = "	Ngũ cốc, sản phẩm từ ngũ cốc	", Origin = "	Việt Nam	", BrandName = "	Quế Lân INC	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/17.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000016	", Name = "	Yến mạch Captain Oats - Quick Cook 1kg	", UnitName = "	Hộp	", RetailPrice = 150000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 142500, DiscountRate = 0, Availability = 4, AmountSold = 0, CategoryName = "	Ngũ cốc, sản phẩm từ ngũ cốc	", Origin = "	Việt Nam	", BrandName = "	Quế Lân INC	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/16.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000015	", Name = "	Mắt cá Ngừ đại dương	", UnitName = "	Hộp	", RetailPrice = 16000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 15200, DiscountRate = 0, Availability = 6, AmountSold = 0, CategoryName = "	Đặc sản	", Origin = "	Việt Nam	", BrandName = "	Thanh Bình Corp	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/15.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000014	", Name = "	Tré Bình Định	", UnitName = "	Hộp	", RetailPrice = 45000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 42700, DiscountRate = 0, Availability = 8, AmountSold = 0, CategoryName = "	Đặc sản	", Origin = "	Việt Nam	", BrandName = "	Thanh Bình Corp	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/14.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000013	", Name = "	Tỏi Lý Sơn 500gr	", UnitName = "	Hộp	", RetailPrice = 65000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 61700, DiscountRate = 0, Availability = 12, AmountSold = 0, CategoryName = "	Đặc sản	", Origin = "	Việt Nam	", BrandName = "	Thanh Bình Corp	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/13.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000012	", Name = "	Mật ong rừng U Minh 500ml	", UnitName = "	Hộp	", RetailPrice = 190000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 180500, DiscountRate = 0, Availability = 8, AmountSold = 0, CategoryName = "	Đặc sản	", Origin = "	Việt Nam	", BrandName = "	Thanh Bình Corp	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/12.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000011	", Name = "	Miến Dong	", UnitName = "	Hộp	", RetailPrice = 63000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 59800, DiscountRate = 0, Availability = 12, AmountSold = 0, CategoryName = "	Đặc sản	", Origin = "	Việt Nam	", BrandName = "	Thanh Bình Corp	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/11.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000010	", Name = "	Đậu phộng rang Tuệ Giác	", UnitName = "	Hộp	", RetailPrice = 60000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 57000, DiscountRate = 0, Availability = 6, AmountSold = 0, CategoryName = "	Thực phẩm chín, qua chế biến	", Origin = "	Việt Nam	", BrandName = "	Thảo Thảo Pharm	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/10.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000009	", Name = "	Mực một nắng Thanh Ký	", UnitName = "	Hộp	", RetailPrice = 280000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 266000, DiscountRate = 0, Availability = 8, AmountSold = 0, CategoryName = "	Thực phẩm chín, qua chế biến	", Origin = "	Việt Nam	", BrandName = "	Thảo Thảo Pharm	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/9.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000008	", Name = "	Heo một nắng Thanh Ký	", UnitName = "	Hộp	", RetailPrice = 139000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 132000, DiscountRate = 0, Availability = 8, AmountSold = 0, CategoryName = "	Thực phẩm chín, qua chế biến	", Origin = "	Việt Nam	", BrandName = "	Thảo Thảo Pharm	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/8.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000007	", Name = "	Yến Thiên sơ chế 100gr	", UnitName = "	Hộp	", RetailPrice = 2800000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 2660000, DiscountRate = 0, Availability = 2, AmountSold = 0, CategoryName = "	Thực phẩm chín, qua chế biến	", Origin = "	Việt Nam	", BrandName = "	Thảo Thảo Pharm	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/7.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000006	", Name = "	Chả bò Đà Nẵng	", UnitName = "	Hộp	", RetailPrice = 154000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 146300, DiscountRate = 0, Availability = 2, AmountSold = 0, CategoryName = "	Thực phẩm chín, qua chế biến	", Origin = "	Việt Nam	", BrandName = "	Thảo Thảo Pharm	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/6.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000005	", Name = "	Cá thu một nắng bịch 500gr	", UnitName = "	Hộp	", RetailPrice = 195000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 185250, DiscountRate = 0, Availability = 8, AmountSold = 0, CategoryName = "	Thực phẩm tươi sống	", Origin = "	Việt Nam	", BrandName = "	Thảo Thảo Pharm	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/5.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000004	", Name = "	Nấm mối làm sẵn bịch 500gr	", UnitName = "	Hộp	", RetailPrice = 145000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 137750, DiscountRate = 0, Availability = 10, AmountSold = 0, CategoryName = "	Thực phẩm tươi sống	", Origin = "	Việt Nam	", BrandName = "	Thảo Thảo Pharm	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/4.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000003	", Name = "	Tôm sú tươi sống	", UnitName = "	Hộp	", RetailPrice = 200000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 190000, DiscountRate = 0, Availability = 6, AmountSold = 0, CategoryName = "	Thực phẩm tươi sống	", Origin = "	Việt Nam	", BrandName = "	Thảo Thảo Pharm	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/3.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000002	", Name = "	Phi lê cá Ngừ đại dương	", UnitName = "	Hộp	", RetailPrice = 230000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 218500, DiscountRate = 0, Availability = 4, AmountSold = 0, CategoryName = "	Thực phẩm tươi sống	", Origin = "	Việt Nam	", BrandName = "	Thảo Thảo Pharm	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/2.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });
            soulProducts.Add(new Product() { Code = "	SP000001	", Name = "	Cá mặt quỷ	", UnitName = "	Hộp	", RetailPrice = 650000, WholesalePrice = 100000, WholesaleMinAmount = 9999999, AverageCost = 617500, DiscountRate = 0, Availability = 10, AmountSold = 0, CategoryName = "	Thực phẩm tươi sống	", Origin = "	Việt Nam	", BrandName = "	Thảo Thảo Pharm	", DateCreated = DateTime.Now, Image = "	/Content/product_images/food/1.jpg	", Description = "	Nội dung mô tả …	", Status = 0, StoreID = GlobalHelper_.CURRENT_STORE_ID });

            added = new List<bool>(new bool[soulProducts.Count]);
        }

        public static double CostFluctuated(this double currentCost)
        {
            /*
            bool fluctYes = GlobalHelper_.randomizer.Next(5) == 0 ? true : false;
            if (fluctYes)
            {
                int percent = GlobalHelper_.randomizer.Next(5, 31);
                double newCost = GlobalHelper_.randomizer.Next(2) == 0 ? currentCost * ((1 + percent) / 100) : currentCost * ((1 - percent) / 100); //INCREASE OR DECREASE COST.
                newCost = Convert.ToInt32(newCost / 100) * 100; //newCost - (newCost % 100); //Làm tròn đến 100 đồng.
                if (newCost >= 1000) return newCost;
                else return 1000;
            }
            else
            {
                return currentCost;
            }
            */

            bool fluctYes = GlobalHelper_.randomizer.Next(4) == 0 ? true : false;
            if (fluctYes)
            {
                int percent = GlobalHelper_.randomizer.Next(1, 10);
                double newCost = GlobalHelper_.randomizer.Next(2) == 0 ? currentCost * ((double)(100 + percent) / 100) : currentCost * ((double)(100 - percent) / 100); //INCREASE OR DECREASE COST.
                newCost = Convert.ToInt32(newCost / 100) * 100; //newCost - (newCost % 100); //Làm tròn đến 100 đồng.
                if (newCost >= 1000) return newCost;
                else return 1000;
            }
            else
            {
                return currentCost;
            }
        }
    }

    public static class SupplierHelper_
    {
        public static List<Supplier> suppliers; //PURPOSE : Access these data without query in DB.
        public static void CreateSuppliersToDB() //MUST BE CALLED IMMEDIATELY WHEN START Seed().
        {
            suppliers = new List<Supplier>();
            suppliers.Add(new Supplier()
            {
                Name = "",
                Phone = "",
                Email = "",
                Description = "",
                StoreID = GlobalHelper_.CURRENT_STORE_ID
            });
            suppliers.Add(new Supplier()
            {
                Name = "Trường Tịnh CORP",
                Phone = "0452656",
                Email = "x@x",
                Description = "sdfdsf",
                StoreID = GlobalHelper_.CURRENT_STORE_ID
            });
            suppliers.Add(new Supplier()
            {
                Name = "Hải Yến",
                Phone = "0452656",
                Email = "x@x",
                Description = "sdfdsf",
                StoreID = GlobalHelper_.CURRENT_STORE_ID
            });
            suppliers.Add(new Supplier()
            {
                Name = "Đức Hà",
                Phone = "0452656",
                Email = "x@x",
                Description = "sdfdsf",
                StoreID = GlobalHelper_.CURRENT_STORE_ID
            });
            suppliers.Add(new Supplier()
            {
                Name = "XingbaoEDM",
                Phone = "0452656",
                Email = "x@x",
                Description = "sdfdsf",
                StoreID = GlobalHelper_.CURRENT_STORE_ID
            });
            suppliers.Add(new Supplier()
            {
                Name = "HanoCom",
                Phone = "0452656",
                Email = "x@x",
                Description = "sdfdsf",
                StoreID = GlobalHelper_.CURRENT_STORE_ID
            });
            suppliers.Add(new Supplier()
            {
                Name = "Quang Nghĩa",
                Phone = "0452656",
                Email = "x@x",
                Description = "sdfdsf",
                StoreID = GlobalHelper_.CURRENT_STORE_ID
            });

            GlobalHelper_.context.Supplier.AddRange(suppliers);
            GlobalHelper_.context.SaveChanges();
        }
    }

    public static class ImportBillHelper_
    {
        public static void NewImport(DateTime date)
        {
            ImportBill importBill = new ImportBill();
            importBill.ImportBillDetails = new List<ImportBillDetail>();

            #region IMPORT BRAND-NEW PRODUCT.
            List<Product> soulProducts = ProductHelper_.soulProducts;
            List<bool> added = ProductHelper_.added;
            int index = GlobalHelper_.randomizer.Next(soulProducts.Count);
            if (added[index] == false)
            {
                Debug.WriteLine("index=" + index + "; soulProducts.Count" + soulProducts.Count);
                soulProducts[index].DateCreated = date;
                GlobalHelper_.context.Product.Add(soulProducts[index]); //ADD TO DB. //WITH INITIAL AMOUNT ADDED !
                GlobalHelper_.context.SaveChanges();
                added[index] = true;

                ImportBillDetail importBillDetail = new ImportBillDetail(); //ADD XONG SẢN PHẨM VÀO DB THÌ PHẢI GHI LẠI TRONG HÓA ĐƠN NHẬP.
                importBillDetail.ProductID = soulProducts[index].ID; //DEBUG HERE ! I EXPECT ID IS GET FROM DB. BUT IT CAN BE NULL IF IM WRONG.
                importBillDetail.Amount = soulProducts[index].Availability > 0 ? soulProducts[index].Availability : 1; //TRONG TRƯỜNG HỢP Ô AVAIL TROG EXCEL == 0 THÌ TA --> THÀNH NHẬP 1 SẢN PHẨM (THAY VÌ NHẬP 0 SP).
                importBillDetail.Price = soulProducts[index].AverageCost > 0 ? 100*Convert.ToInt32(soulProducts[index].AverageCost/100) : 1000; //TRONG TRƯỜNG HỢP Ô AVERAGECOST TROG EXCEL == 0 THÌ TA ...  . //VÀ LÀM TRÒN ĐẾN 100 ĐỒNG.

                importBill.ImportBillDetails.Add(importBillDetail);
            }
            #endregion

            #region INCREASE SOME PRODUCT AMOUNT (IMPORT MORE).
            IQueryable<Product> productsInDB = GlobalHelper_.context.Product.Where(m => true); //SELECT ALL PRODUCTS IN DB (AT THE MOMENT).
            //int numberOfProductsInDB = productsInDB.Count();
            //List<bool> increased = new List<bool>(numberOfProductsInDB);

            List<Product> randomProductsInDB = GlobalHelper_.context.Product.OrderBy(r => Guid.NewGuid()).Take(5).ToList(); //Select random.

            List<ImportBillDetail> importBillDetailToList = importBill.ImportBillDetails.ToList(); //CHUYỂN THÀNH LIST ĐỂ DUYỆT CÁC PHẦN TỬ = FOR.
            for (int i = 0; i < randomProductsInDB.Count; i++) //VỚI MỖI SẢN PHẨM ĐÃ ĐƯỢC LẤY NGẪU NHIÊN TỪ DB.
            {
                //XÉT XEM SẢN PHẨM ĐÃ ĐƯỢC THÊM VÀO HÓA ĐƠN NHẬP Ở TRÊN CHƯA (PHẦN CREATE BRAND-NEW ẤY).
                bool imported = false;
                for (int j = 0; j < importBillDetailToList.Count; j++)
                    if (randomProductsInDB[i].ID == importBillDetailToList[j].ProductID)
                    {
                        imported = true; //TRUE: ĐÃ IMPORT TRONG HÓA ĐƠN HIỆN TẠI RỒI.
                        break;
                    }

                if (!imported) //IF NOT IMPORTED THE SAME PRODUCT BEFORE (IN THE SAME IMPORTBILL).
                {
                    int importedAmount = GlobalHelper_.randomizer.Next(10) + 1;
                    randomProductsInDB[i].Availability += importedAmount;

                    ImportBillDetail importBillDetail = new ImportBillDetail();
                    importBillDetail.ProductID = randomProductsInDB[i].ID; //DEBUG HERE ! I EXPECT ID IS GET FROM DB. BUT IT CAN BE NULL IF IM WRONG.
                    importBillDetail.Amount = importedAmount;
                    importBillDetail.Price = Convert.ToInt32(randomProductsInDB[i].AverageCost.CostFluctuated()); //Average Cost khi áp dụng lên 1 sản phẩm nhập, thì là giá nhập.

                    importBill.ImportBillDetails.Add(importBillDetail);
                }
            }
            #endregion

            importBill.DateCreated = date;
            importBill.SupplierID = SupplierHelper_.suppliers[GlobalHelper_.randomizer.Next(SupplierHelper_.suppliers.Count)].ID;
            importBill.DiscountValue = 0;
            importBill.StoreID = GlobalHelper_.CURRENT_STORE_ID;
            importBill.RefreshTotalValue(); //REFRESH.

            GlobalHelper_.context.ImportBill.Add(importBill);
            GlobalHelper_.context.SaveChanges();

            #region Sau khi lưu vào DB, ta mới có ID (được modify ngay trong importBill object). Giờ ta cập nhật lại importBill.Code qua ID đó.
            importBill.Code = "HD0000" + importBill.ID.ToString();
            GlobalHelper_.context.ImportBill.AddOrUpdate(m => m.ID, importBill);
            GlobalHelper_.context.SaveChanges();
            #endregion

            #region IMPORTBILLDETAIL
            foreach (var impBillDetail in importBill.ImportBillDetails)
            {
                impBillDetail.ImportBillID = importBill.ID;
            }
            GlobalHelper_.context.ImportBillDetail.AddRange(importBill.ImportBillDetails);
            GlobalHelper_.context.SaveChanges();
            #endregion

            #region UPDATE PRODUCT AVAILABILITY AND AVERAGE-COST.
            foreach (var impBillDetail in importBill.ImportBillDetails)
            {
                int productID_temp = impBillDetail.ProductID;
                Product productToUpdate = GlobalHelper_.context.Product.SingleOrDefault(m => m.ID == productID_temp);                
                productToUpdate.AverageCost = (productToUpdate.AverageCost * productToUpdate.Availability + impBillDetail.Price * impBillDetail.Amount) / (productToUpdate.Availability + impBillDetail.Amount);
                productToUpdate.Availability += impBillDetail.Amount;
                GlobalHelper_.context.Product.AddOrUpdate(m => m.ID, productToUpdate);
                GlobalHelper_.context.SaveChanges();
            }
            #endregion
        }
    }

    public static class SaleBillHelper_
    {
        public static void NewSaleBill(DateTime date)
        {
            SaleBill saleBill = new SaleBill();
            saleBill.SaleBillDetails = new List<SaleBillDetail>();            

            #region DECREASE SOME PRODUCT AMOUNT (SELL).
            IQueryable<Product> productsInDB = GlobalHelper_.context.Product.Where(m => true); //SELECT ALL PRODUCTS IN DB (AT THE MOMENT). (QUERYABLE).
            
            List<Product> randomProductsInDB = GlobalHelper_.context.Product.OrderBy(r => Guid.NewGuid()).Take(5).ToList(); //Select random.
                        
            foreach (var randomProduct in randomProductsInDB) //VỚI MỖI SẢN PHẨM ĐÃ ĐƯỢC LẤY NGẪU NHIÊN TỪ DB.
            {
                if (randomProduct.Availability <= 0) continue; //HẾT RỒI THÌ BÁN GÌ NỮA ! SKIP MẶT HÀNG NÀY THÔI !.

                //XÉT XEM SẢN PHẨM ĐÃ ĐƯỢC THÊM VÀO HÓA ĐƠN CHƯA.
                bool inSaleBillAlready = false;
                foreach (var saleBillDetail in saleBill.SaleBillDetails) 
                    if (randomProduct.ID == saleBillDetail.ProductID)
                    {
                        inSaleBillAlready = true; //TRUE: ĐÃ CÓ TRONG HÓA ĐƠN HIỆN TẠI RỒI.
                        break;
                    }

                if (!inSaleBillAlready)
                {
                    int sellAmount = GlobalHelper_.randomizer.Next(randomProduct.Availability); //Vì ở trên kia ta đã loại trường hợp == 0 nên ở đây avail luôn > 0.
                    if (sellAmount == 0) sellAmount = 1; //KHÔNG CẦN XÉT TH avail == 0 nữa.
                    // TA CHƯA randomProduct.Availability -= sellAmount; VỘI, MÀ SẼ CHẠY LỆNH NÀY Ở CUỐI (PHÍA DƯỚI).

                    SaleBillDetail saleBillDetail = new SaleBillDetail();
                    saleBillDetail.ProductID = randomProduct.ID; //DEBUG HERE ! I EXPECT ID IS GET FROM DB. BUT IT CAN BE NULL IF IM WRONG.
                    saleBillDetail.Amount = sellAmount;
                    if (sellAmount >= randomProduct.WholesaleMinAmount) //Nếu số lượng mua >= số lượng để tính giá bán buôn, thì tính theo giá bán buôn.
                    {
                        saleBillDetail.Price = Convert.ToInt32(randomProduct.WholesalePrice); //Giá bán buôn.
                    }
                    else
                    {
                        saleBillDetail.Price = Convert.ToInt32(randomProduct.RetailPrice); //Giá bán lẻ.
                    }

                    saleBill.SaleBillDetails.Add(saleBillDetail);
                }
            }
            #endregion

            saleBill.DateCreated = date;
            saleBill.CustomerID = CustomerHelper_.customers[GlobalHelper_.randomizer.Next(CustomerHelper_.customers.Count)].ID;
            saleBill.DiscountValue = 0;
            saleBill.StoreID = GlobalHelper_.CURRENT_STORE_ID;
            saleBill.RefreshTotalValue(); //REFRESH.

            GlobalHelper_.context.SaleBill.Add(saleBill);
            GlobalHelper_.context.SaveChanges();

            #region Sau khi lưu vào DB, ta mới có ID (được modify ngay trong saleBill object). Giờ ta cập nhật lại "saleBill.Code" qua ID đó.
            saleBill.Code = "HDB0000" + saleBill.ID.ToString();
            GlobalHelper_.context.SaleBill.AddOrUpdate(m => m.ID, saleBill);
            GlobalHelper_.context.SaveChanges();
            #endregion

            #region SALEBILLDETAILs : SET SALEBILLID FOR THEM.
            foreach (var saleBillDetail in saleBill.SaleBillDetails)
            {
                saleBillDetail.SaleBillID = saleBill.ID;
            }
            GlobalHelper_.context.SaleBillDetail.AddRange(saleBill.SaleBillDetails);
            GlobalHelper_.context.SaveChanges();
            #endregion

            #region UPDATE PRODUCT AVAILABILITY AND ...
            foreach (var saleBillDetail in saleBill.SaleBillDetails)
            {
                int productID_temp = saleBillDetail.ProductID;
                Product productToUpdate = GlobalHelper_.context.Product.SingleOrDefault(m => m.ID == productID_temp);
                //productToUpdate.AverageCost = (productToUpdate.AverageCost * productToUpdate.Availability + saleBillDetail.Price * saleBillDetail.Amount) / (productToUpdate.Availability + saleBillDetail.Amount);
                productToUpdate.Availability -= saleBillDetail.Amount;
                productToUpdate.AmountSold += saleBillDetail.Amount;
                GlobalHelper_.context.Product.AddOrUpdate(m => m.ID, productToUpdate);
                GlobalHelper_.context.SaveChanges();
            }
            #endregion
        }
    }
}