using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApnaAccountWeb.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using ApnaAccountWeb.ViewModels;
namespace ApnaAccountWeb.Controllers
{
    public class AccountsController : Controller
    {
        public ActionResult AccountCoding()
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
        public PartialViewResult _AccountTree()
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                return PartialView(db.Accounts.ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public JsonResult NewAccount(string strAccountId)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                var data = db.Database.SqlQuery<string>(@"declare @num varchar(15)
exec  @num = spsw_getaccountid @AccountId='"+strAccountId+"'");
                var AccoundiDd = data.First();
                string[] responsedata=AccoundiDd.Split(',');
                if (responsedata[0].Equals("1"))
               {
                   return Json(new { success = true, id = "01", responseText = responsedata[1] }, JsonRequestBehavior.AllowGet);
               }
                else if (responsedata[0].Equals("2"))
               {
                   return Json(new { success = true,id="02", responseText = responsedata[1] }, JsonRequestBehavior.AllowGet);
               }
               else {
                   return Json(new { success = true, responseText = responsedata[1] }, JsonRequestBehavior.AllowGet);
               }
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult AccountCoding(Account account)
        {
             string usertype = Convert.ToString(Session["usertype"]);
             if (!string.IsNullOrEmpty(usertype) && (usertype == "User") && account.IsUpdate == 1)
             {
                 TempData["message"] = "You do not have permission to edit this.";
                 return RedirectToAction("AccountCoding");
             }
             else
             {
                 string dbcon = Convert.ToString(Session["con"]);
                 DbConection obj = new DbConection();
                 string condb = obj.Connect(dbcon);
                 ApnaAccountEntities db = new ApnaAccountEntities(condb);
                 ViewBag.Cities = db.Cities.ToList();
                 try
                 {
                     account.AccType = "Group";
                     if (ModelState.IsValid)
                     {
                         if (!string.IsNullOrEmpty(Convert.ToString(account.IsParty)))
                         {
                             account.IsParty = true;
                         }
                         else
                         {
                             account.IsParty = false;
                         }

                         string parent = account.Nature;
                         if (parent.Contains(' '))
                         {
                             parent = parent.Substring(0, parent.IndexOf(' '));
                         }
                         string nature = parent.Substring(0, 2);
                      
                         //bool IsUpdate = (account.IsUpdate == "true" ? true : false);
                         var data = db.Database.SqlQuery<string>(@" declare @num nvarchar(100)
exec  @num = spsw_saveaccount  @AccountId={0},@Title={1},@AccType={2},@ParentId={3},@LevelKey={4},@Address={5},@PhoneNo={6},@ContactPerson={7},@SalesTaxNo={8},@FaxNo={9},@CityId={10},@Nature={11},@IsUpdate={12},@SalesDiscount={13},@IsParty={14}", account.AccountId, account.Title, account.AccType, parent, account.LevelKey, account.Address, account.PhoneNo, account.ContactPerson, account.SalesTaxNo, account.FaxNo, account.CityId, nature, account.IsUpdate,account.SalesDiscount,account.IsParty);
                         var outName = data.First();
                         if (outName.ToString() == "1")
                         {
                             TempData["message"] = "Account Saved Successfully";
                             ModelState.Clear();
                         }
                         else if (outName.ToString() == "0")
                         {
                             TempData["message"] = "Failed";
                         }
                     }
                     return RedirectToAction("AccountCoding");
                 }
                 catch (Exception ex)
                 {
                     TempData["message"] = ex.Message;
                     return View(account);
                 }
             }
        }
        public ActionResult SearchAccount(string strAccountId)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                Account account = db.Accounts.Find((strAccountId).Trim());
                return Json(account, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("AccountCoding");
            }
        }
        public JsonResult DeleteAccount(string strAccountId)
        {
           string usertype = Convert.ToString(Session["usertype"]);
           if (!string.IsNullOrEmpty(usertype) && (usertype == "Admin"))
           {
               string dbcon = Convert.ToString(Session["con"]);
               DbConection obj = new DbConection();
               string condb = obj.Connect(dbcon);
               ApnaAccountEntities db = new ApnaAccountEntities(condb);
               try
               {
                   db.Configuration.ProxyCreationEnabled = false;
                   Account account = db.Accounts.Find((strAccountId).Trim());
                   if (account.ParentId != "0")
                   {
                       var data = db.Database.SqlQuery<string>(@"declare @num varchar(100)
exec  @num = spsw_deleteaccount @AccountId='" + strAccountId + "'");
                       var AccoundiD = data.First();
                       if (AccoundiD.ToString() == "1")
                       {
                           TempData["message"] = "Account Deleted Successfully";
                       }
                       else
                       {
                           TempData["message"] = AccoundiD;
                       }
                       return Json(new { success = true, responseText = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
                   }
                   else
                   {
                       return Json(new { success = false, responseText = "Mother Accounts Canoot be deleted" }, JsonRequestBehavior.AllowGet);
                   }
               }
               catch (Exception ex)
               {
                   return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
               }
           }
           else
           {
               return Json(new { responseText = "You do not have permission to delete this." }, JsonRequestBehavior.AllowGet);
           }
        }
        public ActionResult DeleteCoding()
        {
             string usertype = Convert.ToString(Session["usertype"]);
             if (!string.IsNullOrEmpty(usertype) && (usertype == "Admin"))
             {
                 string dbcon = Convert.ToString(Session["con"]);
                 string dbconsql = Convert.ToString(Session["consql"]);
                 if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
                 {
                     return RedirectToAction("Login", "User");
                 }
             }
             else
             {
                 TempData["message"] = "You do not have permission to delete this.";
             }
             return View();
        }
        [HttpPost]
        public ActionResult DeleteCoding(string Nature)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                if (ModelState.IsValid)
                {
                    string parent = Nature;
                    if (parent.Contains(' '))
                    {
                        parent = parent.Substring(0, parent.IndexOf(' '));
                    }
                    string nature = parent;
                    var data = db.Database.SqlQuery<string>(@"declare @num varchar(100)
exec  @num = spsw_masterdeletion @AccountId='" + nature + "'");
                    var AccoundiDd = data.First();
                    string[] responsedata = AccoundiDd.Split(',');
                    if (responsedata[0].Equals("1"))
                    {
                        TempData["message"] = "Account Deleted Successfully";
                    }
                    else if (responsedata[0].Equals("2"))
                    {
                        TempData["message"] = responsedata[1];
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult OpeningBalances()
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
                var sd = db.Database.SqlQuery<OBViewModel>("SELECT Accounts.AccountId, Accounts.Title,isnull(AccountsOpening.ODebit,0) as ODebit , isnull(AccountsOpening.OCredit,0) as OCredit FROM Accounts FULL OUTER JOIN AccountsOpening ON Accounts.AccountId = AccountsOpening.AccountId where AccType='Detail'").ToList();
                ViewBag.AccCodeDetails = sd;
                //ViewBag.AccCodeDetails = db.Accounts.Where(x => x.AccType.Trim() == "Detail").Select(x => new DetailList { AccountId = x.AccountId, Title = x.Title }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
       }
        [HttpPost]
        public JsonResult SaveOpening(List<OBViewModel> detail)
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
                    db.Database.ExecuteSqlCommand("truncate table AccountsOpening");
                    foreach (var item in detail)
                    {
                        db.Database.ExecuteSqlCommand("insert into  AccountsOpening (AccountId,ODebit,OCredit)values(@AccountId,@ODebit,@OCredit)", new System.Data.SqlClient.SqlParameter("AccountId", item.AccountId), new System.Data.SqlClient.SqlParameter("ODebit", item.ODebit), new System.Data.SqlClient.SqlParameter("OCredit", item.OCredit));
                    }
                    return Json(new { success = true, responseText = "Saved" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new {responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }   
            }
        public ActionResult CreditLimit()
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
                var sd = db.Database.SqlQuery<OBViewModel2>("SELECT Accounts.AccountId, Accounts.Title,isnull(Clients_Credit_Limit.Credit_Days,0) as ODebit , isnull(Clients_Credit_Limit.Credit_Limit,0) as OCredit FROM Accounts FULL OUTER JOIN Clients_Credit_Limit ON Accounts.AccountId = Clients_Credit_Limit.AccountId where AccType='Detail' and IsParty=1").ToList();
                ViewBag.AccCodeDetails = sd;
                //ViewBag.AccCodeDetails = db.Accounts.Where(x => x.AccType.Trim() == "Detail").Select(x => new DetailList { AccountId = x.AccountId, Title = x.Title }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        [HttpPost]
        public JsonResult SaveCreditLimit(List<OBViewModel2> detail)
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
                    db.Database.ExecuteSqlCommand("truncate table Clients_Credit_Limit");
                    foreach (var item in detail)
                    {
                        db.Database.ExecuteSqlCommand("insert into  Clients_Credit_Limit (AccountID,Remarks,Credit_Days,Credit_Limit,Editor,Edit_Date)values(@AccountId,@Title,@ODebit,@OCredit,1,'2018-11-05 00:00:00.000')", new System.Data.SqlClient.SqlParameter("AccountId", item.AccountId), new System.Data.SqlClient.SqlParameter("Title", item.title), new System.Data.SqlClient.SqlParameter("ODebit", item.ODebit), new System.Data.SqlClient.SqlParameter("OCredit", item.OCredit));
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
