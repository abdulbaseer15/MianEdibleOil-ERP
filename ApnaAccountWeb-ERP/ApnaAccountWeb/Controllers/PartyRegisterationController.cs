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
    public class PartyRegisterationController : Controller
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
                var AccountsList = from s in db.Accounts1
                                   where s.ParentID.StartsWith("22") && s.Type == "Group"
                                   select s;
                ViewBag.AccCodelist = AccountsList.ToList();
                var AccountsList2 = from s in db.Accounts1
                                    where s.ParentID.StartsWith("122") && s.Type == "Group"
                                   select s;
                ViewBag.AccCodelist2 = AccountsList2.ToList();
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
                return PartialView(db.Accounts1.ToList());
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
                string dbconsql = Convert.ToString(Session["consql"]);
                SqlConnection Connection = new SqlConnection(dbconsql);
                Connection.Open();
                SqlCommand Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                string Qry = @" exec SPSW_GetAccountIdvb @AccountId='" + strAccountId + "'";
                Command.Connection = Connection;
                SqlDataAdapter da = new SqlDataAdapter(Qry, dbconsql);
                DataTable dt = new DataTable();
                da.Fill(dt);
                var results = from myRow in dt.AsEnumerable()
                           select myRow;
                string accountid = "";
                string level = "";
                string nature = "";
                string type = "";
                foreach (var dr in results)
                {
                    accountid = dr["AccountId"].ToString();
                    level = dr["Lvl"].ToString();
                    nature = dr["Nature"].ToString();
                    type = dr["Type"].ToString();
                }
              if (string.IsNullOrEmpty(accountid))
              {
                  return Json(new { success = true, id = "02", responseText = "Something went wrong!" }, JsonRequestBehavior.AllowGet);
              }
             else {
                 return Json(new { success = true, accountid = accountid, lvl = level,type=type }, JsonRequestBehavior.AllowGet);
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
                     if (ModelState.IsValid)
                     {
                         if(!string.IsNullOrEmpty(Convert.ToString(account.IsParty)))
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
                         string nature = parent.Substring(0, 1);
                         //bool IsUpdate = (account.IsUpdate == "true" ? true : false);
                         var data = db.Database.SqlQuery<string>(@" declare @num nvarchar(100)
exec  @num = SPSW_SaveAccountvb  @AccountId={0},@Title={1},@AccType={2},@ParentId={3},@LevelKey={4},@Address={5},@PhoneNo={6},@ContactPerson={7},@SalesTaxNo={8},@FaxNo={9},@CityId={10},@Nature={11},@IsUpdate={12},@IsParty={13}", account.AccountId, account.Title, account.AccType, parent, account.LevelKey, account.Address, account.PhoneNo, account.ContactPerson, account.SalesTaxNo, account.FaxNo, account.CityId, nature, account.IsUpdate,account.IsParty);
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
                         else if (outName.ToString() == "3")
                         {
                             TempData["message"] = "Change Account type and create again.";
                         }
                         else if (outName.ToString() == "4")
                         {
                             TempData["message"] = "Sorry only 9 groups are allowed in a group.";
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
                Accounts1 account = db.Accounts1.Find((strAccountId).Trim());
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
                   Accounts1 account = db.Accounts1.Find((strAccountId).Trim());
                   if (account.ParentID != "0")
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
    }
}
