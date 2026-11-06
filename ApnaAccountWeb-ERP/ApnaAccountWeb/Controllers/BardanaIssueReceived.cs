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
    public class BardanaIssueReceivedController : Controller
    {
        public ActionResult BardanaIssueReceived()
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
                var AccountsList = from s in db.Accounts
                                   where s.AccType == "Detail" && s.IsParty == true
                                   orderby s.Title
                                   select s;
                ViewBag.AccCodelist = AccountsList.ToList();
                    ViewBag.AccCodelisttype = (from s in db.BardanaTypes 
                                     select new DetailList { Code = s.BTypeID, Title=s.Title }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
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
                var lstDVChilds = (from t1 in db.Bardanas
                                   where t1.BardanaID == VId 
                                   select t1).ToList();
                return Json(lstDVChilds.ToList(), JsonRequestBehavior.AllowGet);
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
                    DateTime date1 = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                    con.Open();
                    Tran = con.BeginTransaction();
                    SqlCommand cmd = new SqlCommand("[SPSW_DeleteInvoiceBard]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VId", SqlDbType.VarChar).Value = DVId;
                    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                    cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "Bar";
                    cmd.Transaction = Tran;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                Tran.Commit();
                    TempData["message"] = "Successfully deleted";
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
                return RedirectToAction("BardanaIssueReceived", "BardanaIssueReceived");
            }
            else
            {
                TempData["message"] = "You do not have permission to delete this.";
                return RedirectToAction("BardanaIssueReceived");
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
                SqlCommand cmd = new SqlCommand("[SPSW_GetIid]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "Bar";
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
            string voucherno2 = null;
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
                    DateTime date2 = Convert.ToDateTime(model.yourdate);
                   // DateTime date2 = DateTime.ParseExact(model.yourdate, "dd/MM/yyyy", null);
                      Int16 IsUpdate = Convert.ToInt16((model.isupdate == "False" ? 0 : 1));
                        con.Open();
                        Tran = con.BeginTransaction();
                        Command = new SqlCommand("[SPSW_SaveInvoiceBardana]", con);
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add("@VId", SqlDbType.BigInt).Value =model.vid;
                        Command.Parameters.Add("@InvNo", SqlDbType.NVarChar).Value = "IF-" + model.vid;
                        Command.Parameters.Add("@Idate", SqlDbType.Date).Value = date1;
                        Command.Parameters.Add("@PartyBillNO", SqlDbType.NVarChar).Value = model.yourinvoice;
                        Command.Parameters.Add("@PartyBillDate", SqlDbType.Date).Value = date2;
                        Command.Parameters.Add("@AccountId", SqlDbType.NVarChar).Value =model.supid;
                        Command.Parameters.Add("@PurAccount", SqlDbType.NVarChar).Value = model.supname;
                        Command.Parameters.Add("@InvTotal", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@SaleTaxAmt", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@NetTotal", SqlDbType.Decimal).Value = model.nettotal;
                        Command.Parameters.Add("@Sno", SqlDbType.Int).Value = model.vid;
                        Command.Parameters.Add("@IType", SqlDbType.NVarChar).Value = "IF";
                        Command.Parameters.Add("@BagType", SqlDbType.NVarChar).Value = model.totalquantity;
                        Command.Parameters.Add("@BagQty0", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@StoreID", SqlDbType.Int).Value =model.store;
                        Command.Parameters.Add("@ContractID", SqlDbType.Int).Value =0;
                        Command.Parameters.Add("@BrockerID", SqlDbType.NVarChar).Value = "";
                        Command.Parameters.Add("@BrockerageAccount", SqlDbType.NVarChar).Value = "";
                        Command.Parameters.Add("@Brate", SqlDbType.Decimal).Value = model.discount;
                        Command.Parameters.Add("@BrokeryMode", SqlDbType.NVarChar).Value = model.store;
                        Command.Parameters.Add("@CalcFactor", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@ArrivalType", SqlDbType.Int).Value = 0;
                        Command.Parameters.Add("@ArrivalID", SqlDbType.Int).Value = 0;
                        Command.Parameters.Add("@Freight", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@BagRate", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@BagAmount", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@BrokeryAmount", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@ActualBagQty", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@MarketComAccount", SqlDbType.NVarChar).Value = "";
                        Command.Parameters.Add("@MarketComAmount", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@RateCalc", SqlDbType.NVarChar).Value = model.purid;
                        Command.Parameters.Add("@MillType", SqlDbType.NVarChar).Value = model.purname;
                        Command.Parameters.Add("@IsUpdate", SqlDbType.Bit).Value = IsUpdate;
                        Command.Transaction = Tran;
                        string tempid = "";
                        if (model.isupdate == "False")
                            tempid = Command.ExecuteScalar().ToString();
                        else
                            Command.ExecuteNonQuery();
                        Command.Dispose();
                        Tran.Commit();
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
