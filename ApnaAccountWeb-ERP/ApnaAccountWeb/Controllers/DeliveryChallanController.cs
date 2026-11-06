using ApnaAccountWeb.Models;
using ApnaAccountWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ApnaAccountWeb.Controllers
{
    public class DeliveryChallanController : Controller
    {
        public ActionResult DeliveryChallan()
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
                ViewBag.AccCodeDetailsAccounts = db.Accounts.Where(x=>x.IsParty==true &&  x.AccType.Trim() == "Detail").Select(x => new DetailList { AccountId = x.AccountId, Title = x.Title }).ToList();
                ViewBag.AccCodeDetailsProducts = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
                ViewBag.AccCodeDetailsFarms = db.Farms.Select(x => new DetailList { Code = x.FarmID, Title = x.Title }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        [HttpGet]
        public JsonResult SearchSaleOrder2(string AccountId, string po)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                var lstDVChilds = db.Database.SqlQuery<SOModel>("SELECT SO_No, ProductID, ItemDetail, SUM(QtyCr- QtyDr) AS QtyCr, SUM(WtCr-WtDr) AS WtCr,Sum(Rate)as Rate,SUM(Farmid) as FarmId From dbo.qrySaleOrderStatus_WDC where  AccountID='" + AccountId + "' and SO_No='" + po + "' GROUP BY SO_No,ProductID, ItemDetail Having (Sum(WtCr - WtDr) > 0)").ToList();
                return Json(lstDVChilds.ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult SearchSaleOrder(string Date, string AccountId)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                DateTime date = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                var lstDVChilds = db.Database.SqlQuery<SOModel>("select InvNo as SO_No,ProductID,ItemDetail,SUM(QtyCr- QtyDr) AS QtyCr,SUM(WtCr-WtDr) AS WtCr from qrySaleOrderStatus_WDC where AccountId='" + AccountId + "' GROUP BY InvNo,ProductID,ItemDetail Having SUM(QtyCr- QtyDr) > 0").ToList();
                //var discountPercent = (from t1 in db.Accounts
                //                       where t1.AccountId == AccountId
                //                       select t1.SalesDiscount).FirstOrDefault();
                return Json(lstDVChilds.ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult GetProductRate(string code)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                int id=Convert.ToInt32(code);
                var lstDVChilds = db.Products.Where(x => x.ProductID ==id).Select(x=>x.PurchasePrice).FirstOrDefault();
                return Json(new { responseText = lstDVChilds }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult GetCustomerBalance(string code)
        {
            string dbconsql = Convert.ToString(Session["consql"]);
            SqlConnection con = new SqlConnection(dbconsql);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("[SPSW_GetCustomerBalance]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Now;
                cmd.Parameters.Add("@Party", SqlDbType.VarChar).Value = code;
                string DVId = cmd.ExecuteScalar().ToString();
                cmd.Dispose();
                if (DVId == null || DVId == "")
                {
                    return Json(new {  responseText = "0" }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { responseText = DVId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                con.Close();
            }
        }
        [HttpGet]
        public JsonResult SearchInvoice(string Date, int VId)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                DateTime date = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                var lstDVChilds = (from t1 in db.InvoiceMasterDelivaryChallans
                                   join t2 in db.InvoiceDetailDelivaryChallans on t1.InvId equals t2.InvId                                  
                                   where t1.InvId == VId
                                   select new { t1.InvId, t1.FarmId, t1.Farm, t1.CustomerId, t1.Customer, t1.Address, t1.Idate,  t2.ItemName, t2.ItemId, t2.Packing, t2.Quantity,t1.SO }).ToList();
                return Json(lstDVChilds, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteInvoice(string Date, int DVId)
        {
           


                string usertype = Convert.ToString(Session["usertype"]);
                if (!string.IsNullOrEmpty(usertype) && (usertype == "Admin"))
                {
                    string dbconsql = Convert.ToString(Session["consql"]);
                    SqlConnection con = new SqlConnection(dbconsql);
                    try
                    {
                    string dbcon = Convert.ToString(Session["con"]);
                    DbConection obj = new DbConection();
                    string condb = obj.Connect(dbcon);
                    ApnaAccountEntities db = new ApnaAccountEntities(condb);

                    string DC = "DC-" + DVId;
                    var ifExist = db.Sale1.Where(x => x.SO_No == DC).FirstOrDefault();

                    if (ifExist == null)
                    {
                        DateTime date1 = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                        con.Open();
                        Tran = con.BeginTransaction();
                        SqlCommand cmd = new SqlCommand("[SPSW_DeleteInvoice]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@VId", SqlDbType.VarChar).Value = DVId;
                        cmd.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "DC";
                        cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                        cmd.Transaction = Tran;
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        Tran.Commit();
                        TempData["message"] = "Successfully deleted";
                    }
                    else
                    {
                        TempData["message"] = "Sale Invoice already created for this Delivery Chalan";
                    }
                    }
                    catch (Exception ex)
                    {
                        Tran.Rollback();
                        TempData["message"] = ex.Message;
                    }
                    finally
                    {
                        con.Close();
                    }
                    return RedirectToAction("DeliveryChallan", "DeliveryChallan");
                }
                else
                {
                    TempData["message"] = "You do not have permission to delete this.";
                    return RedirectToAction("DeliveryChallan");
                }


            
           
        }
        [HttpPost]
        public JsonResult GetInvoiceID(string date)
        {
            string dbconsql = Convert.ToString(Session["consql"]);
            SqlConnection con = new SqlConnection(dbconsql);
            try
            {
                DateTime date1 = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                con.Open();
                SqlCommand cmd = new SqlCommand("[SPSW_GetDCid]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "DC";
                string DVId = cmd.ExecuteScalar().ToString();
                cmd.Dispose();
                if (DVId == null || DVId == "")
                {
                    return Json(new { success = true, responseText = "1" }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { success = true, responseText = DVId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                con.Close();
            }
        }
        SqlTransaction Tran;
        SqlCommand Command;
        [HttpPost]
        public JsonResult SaveInvoice(InvoiceViewModel model)
        {
            string usertype = Convert.ToString(Session["usertype"]);
            if (!string.IsNullOrEmpty(usertype) && (usertype == "User") && model.isupdate == "True")
            {
                return Json(new { responseText = "You do not have permission to edit this." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string dbconsql = Convert.ToString(Session["consql"]);
                SqlConnection con = new SqlConnection(dbconsql);
                try
                {
                    DateTime date1 = DateTime.ParseExact(model.comdate, "dd/MM/yyyy", null);
                    if (model.detail.Count > 0)
                    {
                        Int16 IsUpdate = Convert.ToInt16((model.isupdate == "False" ? 0 : 1));
                        con.Open();
                        Tran = con.BeginTransaction();
                        Command = new SqlCommand("[SPSW_SaveInvoice]", con);
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add("@VId", SqlDbType.Int).Value = model.vid;
                        Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        Command.Parameters.Add("@CustomerId", SqlDbType.VarChar).Value = model.supid;
                        Command.Parameters.Add("@FarmId", SqlDbType.VarChar).Value = model.purid;
                        Command.Parameters.Add("@Customer", SqlDbType.VarChar).Value = model.supname;
                        Command.Parameters.Add("@Farm", SqlDbType.VarChar).Value = model.purname;
                        Command.Parameters.Add("@SO", SqlDbType.VarChar).Value = model.store;
                        Command.Parameters.Add("@Address", SqlDbType.VarChar).Value = model.yourinvoice;
                        Command.Parameters.Add("@IsUpdate", SqlDbType.Bit).Value = IsUpdate;
                        Command.Parameters.Add("@User", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                        Command.Transaction = Tran;
                        if (model.isupdate == "False")
                            model.vid = Command.ExecuteScalar().ToString();
                        else
                            Command.ExecuteNonQuery();
                        Command.Dispose();
                        decimal discount=Convert.ToDecimal(model.discount);
                        foreach (var dvchild in model.detail)
                        {
                            Command = new SqlCommand("[SPSW_SaveInvoiceDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@InvId", SqlDbType.Int).Value = Int32.Parse(model.vid);
                            Command.Parameters.Add("@ItemId", SqlDbType.Int).Value =Int32.Parse(dvchild.accCode);
                            Command.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = dvchild.title;
                            Command.Parameters.Add("@Rate", SqlDbType.Money).Value = dvchild.piece;
                            Command.Parameters.Add("@InvTotal", SqlDbType.Money).Value = dvchild.amount;
                            Command.Parameters.Add("@Itype", SqlDbType.VarChar).Value = "DC";
                            Command.Parameters.Add("@Idate", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@User", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                        }
                        Tran.Commit();
                    }
                }
                catch (Exception ex)
                {
                    Tran.Rollback();
                    return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    Command.Parameters.Clear();
                    con.Close();
                }
                return Json(new { success = true, responseText = "Saved" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
