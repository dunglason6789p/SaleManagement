using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleManagement.Controllers
{
    public class ImportController : Controller
    {
        public JsonResult GetImportBill_JSON(string code, string dateFrom, string dateTo, string customerID, string orderBy, int? pageToGo)
        {
            return Json(CRUD.ImportBillCRUD.GetImportBillList(this, code, dateFrom, dateTo, customerID, orderBy, pageToGo));
        }
    }
}