using ApnaAccountWeb.Models;
using ApnaAccountWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ApnaAccountWeb.Controllers
{
    public class ProductsOpeningController : Controller
    {
        public ActionResult OpeningStock()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                //var sd = db.Database.SqlQuery<OpStock>("SELECT ProductID, ProductName,isnull(OpeningQty,0) as UnitQty , isnull(PurchasePrice,0) as UnitRate,(isnull(OpeningQty,0)*isnull(PurchasePrice,0)) as Amounts  FROM Products ").ToList();
                //ViewBag.AccCodeDetails = sd;
                ViewBag.pgroupDetails = db.ProductGroups.Select(x => new DetailList { Code = x.PG_Id, Title = x.Title }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        [HttpGet]
        public JsonResult SearchProducts(int VId)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                int PGID = Convert.ToInt32(VId);
                var lstDVChilds = db.Database.SqlQuery<OpStock>("SELECT ProductID as Productid, ProductName,isnull(OpeningQty,0) as UnitQty,isnull(OpeningWt,0) as UnitWt, isnull(OpeningRate,0) as UnitRate,(isnull(OpeningWt,0)*isnull(OpeningRate,0)) as Amounts  FROM Products where PG_ID=" + PGID + " ").ToList();
                return Json(lstDVChilds, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult SaveOpeningStock(List<OpStock> detail)
        {
            try
            {
                string usertype = Convert.ToString(Session["usertype"]);
                string dbcon = Convert.ToString(Session["con"]);
                DbConection obj = new DbConection();
                string condb = obj.Connect(dbcon);
                ApnaAccountEntities db = new ApnaAccountEntities(condb);
                if (!string.IsNullOrEmpty(usertype) && (usertype == "User"))
                {
                    return Json(new { responseText = "You do not have permission to edit this." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    foreach (var item in detail)
                    {
                        db.Database.ExecuteSqlCommand("update Products set OpeningQty=@OpeningQty,OpeningRate=@OpeningRate,OpeningWt=@OpeningWt where Productid=@Productid", new System.Data.SqlClient.SqlParameter("OpeningQty", item.UnitQty), new System.Data.SqlClient.SqlParameter("OpeningRate", item.UnitRate), new System.Data.SqlClient.SqlParameter("Productid", item.Productid), new System.Data.SqlClient.SqlParameter("OpeningWt", item.UnitWt));
                    }
                    return Json(new { success = true, responseText = "Saved" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
