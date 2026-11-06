using ApnaAccountWeb.Models;
using ApnaAccountWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ApnaAccountWeb.Controllers
{
    public class VouchersVerificationController : Controller
    {
        public ActionResult VoucherVerify()
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
                ViewBag.Cities = db.Cities.ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult _Verify()
        {
             string usertype = Convert.ToString(Session["usertype"]);
             if (!string.IsNullOrEmpty(usertype) && (usertype == "Admin" || usertype == "Manager"))
             {
                 string dbcon = Convert.ToString(Session["con"]);
                 string dbconsql = Convert.ToString(Session["consql"]);
                 if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
                 {
                     return RedirectToAction("Login", "User");
                 }
                 return PartialView();
             }
             else
             {
                 TempData["message"] = "You do not have permission to view this.";
                 return PartialView();
             }
        }
        [HttpGet]
        public JsonResult SearchVoucher(string Narration)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                //var AccountsList = "Select  AccountId,Debit,Credit,Narration ,VId  ,VDate,Vtype,Title,RefNo from VouchersDetail  where Narration like '" + Narration+"' ".ToList(); 
                var AccountsList = db.Database.SqlQuery<VouchersVerification>("SELECT VD.AccountId, VD.Debit, VD.Credit, VD.Narration, VD.VId, VD.VDate, VD.Vtype, VD.Title, VD.RefNo, V.VId as VId2 FROM VouchersDetail VD INNER JOIN Vouchers V ON VD.VId = V.VoucherId WHERE Narration LIKE @Narration", new SqlParameter("Narration", "%" + Narration + "%")).ToList();
                // Execute the query and retrieve the results
                ViewBag.AccCodelist = AccountsList;
                    return Json(AccountsList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult SearchVoucher2(string Vid,string Type)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                //DateTime date = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                var AccountsList = db.Database.SqlQuery<VouchersVerification>("SELECT VId ,Vtype, Title, Debit, Credit, Narration FROM VouchersDetail WHERE VId = '" + Vid + "' and Vtype= '" + Type + "'").ToList();
                ViewBag.AccCodelist = AccountsList;
                return Json(AccountsList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult SearchVoucher3(string Date)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                DateTime date = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                var AccountsList = db.Database.SqlQuery<VouchersVerification>(
                "select distinct Vid2, Vid, VDate, VType, TtlDebit, City from qryUnApprovedVoucher").ToList<VouchersVerification>(); ;
                ViewBag.AccCodelist = AccountsList;
                return Json(AccountsList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult SaveVoucher(List<VouchersVerification> detail)
        {
            string dbcon = Convert.ToString(Session["con"]);
            string user = Convert.ToString(Session["username"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                if (detail.Count > 0)
                {
                    foreach (var dvchild in detail)
                    {
                        if (dvchild.ischecked.Equals("True"))
                        {
                             //db.VouchersDetails
                             //     .Where(x => x.VId==dvchild.Vid && x.VType==dvchild.Vtype)
                             //     .ToList()
                             //     .ForEach(a => a.IsApproved = true );
                             //db.VouchersDetails
                             //      .Where(x => x.VId == dvchild.Vid && x.VType == dvchild.Vtype)
                             //      .ToList()
                             //      .ForEach(a => a.ApproveDate = DateTime.Now);
                             //db.VouchersDetails
                             //       .Where(x => x.VId == dvchild.Vid && x.VType == dvchild.Vtype)
                             //       .ToList()
                             //       .ForEach(a => a.ApprovedBy=user);
                             //db.SaveChanges();
                            //    db.SaveChanges();
                            //VouchersDetail dt = db.VouchersDetails.Where(v => v.VId == dvchild.Vid && v.VType == dvchild.Vtype).FirstOrDefault();
                            //dt.IsApproved = true;
                            //db.SaveChanges();
                            //using (var db = new ApnaAccountEntities(dbcon))
                            //{
                            //    db.VouchersDetails
                            //      .Where(x => x.VId==dvchild.Vid && x.VType==dvchild.Vtype)
                            //      .ToList()
                            //      .ForEach(a => a.IsApproved = true);
                            //    db.SaveChanges();
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            finally
            {
            }
            return Json(new { success = true, responseText = "Successfully Saved" }, JsonRequestBehavior.AllowGet);
        }
    }
}
