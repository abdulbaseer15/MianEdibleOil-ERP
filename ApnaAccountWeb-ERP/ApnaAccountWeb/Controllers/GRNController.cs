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
    public class GRNController : Controller
    {
        public ActionResult GRN()
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
                                   //where  s.AccType == "Detail"
                                  // where s.ParentId.StartsWith("020201") && s.AccType == "Detail"
                                   //where (s.ParentId.StartsWith("020201") || s.ParentId.StartsWith("010203")) && s.AccType == "Detail"
                                   where s.IsParty==true && s.AccType == "Detail"
                                    select s; 
                    ViewBag.AccCodelist = AccountsList.ToList();
                    ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName, rate = x.PurchasePrice??0 }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
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
        [HttpGet]
        public JsonResult SearchPurchaseOrder2(string AccountId, string po)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                var lstDVChilds = db.Database.SqlQuery<POModel1>("SELECT     Gate.GateNo as PO_No, Gate3.ProductID, Gate3.ProductName as ItemDetail, Gate3.Quantity AS QtyDr,(StdWeight-(Select Deduction from BardanaTypes where BTypeID=Gate3.Bardana)*Gate3.Quantity) as WtDr FROM       Gate INNER JOIN Gate3 ON Gate.GateNo = Gate3.GateNo WHERE Gate.IntCol1 = '" + AccountId + "' and Gate.GateNo='" + po + "'").ToList();
               // var lstDVChilds = db.Database.SqlQuery<POModel>("SELECT PO_No, ProductID, ItemDetail, SUM(QtyDr - QtyCr) AS QtyDr, SUM(WtDr - WtCr) AS WtDr,Rate From dbo.qryPurOrderStatus where  AccountID='" + AccountId + "' and PO_No='" + po + "' GROUP BY PO_No,ProductID, ItemDetail,Rate Having (Sum(WtDr - WtCr) > 0)").ToList();
                //var lstDVChilds = db.Database.SqlQuery<POModel>("select InvNo as PO_No,ProductID,ItemDetail,SUM(QtyDr-QtyCr) AS QtyDr,SUM(WtDr-WtCr) as WtDr,Rate from qryPurOrderStatus where AccountId='" + AccountId + "' and PO_No='" + po + "' GROUP BY InvNo,ProductID,ItemDetail,Rate Having SUM( QtyDr-QtyCr) > 0").ToList();
                //var podata = from s in db.PurchaseOrder1
                //             where s.InvNo == po
                //             select new { RateCalc = s.RateCalc, MillType = s.MillType, BrokeryMode = s.BrokeryMode, Brate = s.Brate };
                return Json(lstDVChilds , JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult SearchPurchaseOrder(string AccountId)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                var lstDVChilds = db.Database.SqlQuery<POModel1>("SELECT     Gate.GateNo as PO_No, Gate3.ProductID, Gate3.ProductName as ItemDetail, Gate3.Quantity AS QtyDr,(StdWeight-(Select Deduction from BardanaTypes where BTypeID=Gate3.Bardana)*Gate3.Quantity) as WtDr FROM       Gate INNER JOIN Gate3 ON Gate.GateNo = Gate3.GateNo WHERE Gate.IntCol1 = '" + AccountId + "'  and Gate.GateNo  not in (Select PurAccount from GRN1 where AccountId='" + AccountId + "')").ToList();
                //var lstDVChilds = db.Database.SqlQuery<POModel>("select InvNo as PO_No,ProductID,ItemDetail,SUM(QtyDr-QtyCr) AS QtyDr,SUM(WtDr-WtCr) as WtDr from qryPurOrderStatus where AccountId='" + AccountId + "' GROUP BY InvNo,ProductID,ItemDetail Having SUM( QtyDr-QtyCr) > 0").ToList();
                return Json(lstDVChilds.ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult SearchInvoice(string Date, int VId)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            string invnnn = "GRN-" + VId;
            try
            {
                DateTime date = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                var lstDVChilds = (from t1 in db.GRN1
                                   join t2 in db.GRN2 on t1.InvNo equals t2.InvNo
                                   join t3 in db.Products on t2.ProductID equals t3.ProductID
                                   where t1.InvNo == invnnn
                                   select new { t1.PartyBillNO, t1.PartyBillDate, t1.AccountId,t1.PurAccount, t1.RateCalc, t1.MillType, t1.Brate, t1.BrokeryMode, t2.ProductID, t3.ProductName, t2.QtyDr, t2.WtDr, t2.Rate, t2.Amount, t1.InvTotal }).ToList();
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
                    SqlCommand cmd = new SqlCommand("[SPSW_DeleteInvoiceGRN]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VId", SqlDbType.VarChar).Value ="GRN-"+ DVId;
                    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                    cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "GRN";
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
                return RedirectToAction("GRN", "GRN");
            }
            else
            {
                TempData["message"] = "You do not have permission to delete this.";
                return RedirectToAction("GRN");
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
                cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "GRN";
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
            lock (this)
            {
                try
                {
                    //To Assign new Master ID
                    if (model.isupdate == "False")
                    {
                        string dbcon = Convert.ToString(Session["con"]);
                        DbConection obj = new DbConection();
                        string condb = obj.Connect(dbcon);
                        ApnaAccountEntities db = new ApnaAccountEntities(condb);
                        string dbconsql = Convert.ToString(Session["consql"]);
                        SqlConnection con = new SqlConnection(dbconsql);
                        int model_Vid = Convert.ToInt32(model.vid);
                        var ifExist = (from s in db.GRN1 where s.Sno == model_Vid select s.Sno).FirstOrDefault();
                        if (!string.IsNullOrEmpty(Convert.ToString(ifExist)))
                        {
                            int temp = db.GRN1.OrderByDescending(x => x.Sno).FirstOrDefault().Sno.Value + 1;
                            model.vid = Convert.ToString(temp);
                        }
                    }
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
                            // DateTime date2 = Convert.ToDateTime(model.yourdate);
                            DateTime date2 = DateTime.ParseExact(model.yourdate, "dd/MM/yyyy", null);
                            if (model.detail.Count > 0)
                            {
                                Int16 IsUpdate = Convert.ToInt16((model.isupdate == "False" ? 0 : 1));
                                con.Open();
                                Tran = con.BeginTransaction();
                                Command = new SqlCommand("[SPSW_SaveInvoiceGRN]", con);
                                Command.CommandType = CommandType.StoredProcedure;
                                Command.Parameters.Add("@VId", SqlDbType.BigInt).Value = model.vid;
                                Command.Parameters.Add("@InvNo", SqlDbType.NVarChar).Value = "GRN-" + model.vid;
                                Command.Parameters.Add("@Idate", SqlDbType.Date).Value = date1;
                                Command.Parameters.Add("@PartyBillNO", SqlDbType.NVarChar).Value = model.yourinvoice;
                                Command.Parameters.Add("@PartyBillDate", SqlDbType.Date).Value = date2;
                                Command.Parameters.Add("@AccountId", SqlDbType.NVarChar).Value = model.supid;
                                Command.Parameters.Add("@PurAccount", SqlDbType.NVarChar).Value = model.po;
                                Command.Parameters.Add("@InvTotal", SqlDbType.Decimal).Value = model.nettotal;
                                Command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = 0;
                                Command.Parameters.Add("@SaleTaxAmt", SqlDbType.Decimal).Value = 0;
                                Command.Parameters.Add("@NetTotal", SqlDbType.Decimal).Value = model.nettotal;
                                Command.Parameters.Add("@Sno", SqlDbType.Int).Value = model.vid;
                                Command.Parameters.Add("@IType", SqlDbType.NVarChar).Value = "GRN";
                                Command.Parameters.Add("@BagType", SqlDbType.NVarChar).Value = "";
                                Command.Parameters.Add("@BagQty0", SqlDbType.Decimal).Value = 0;
                                Command.Parameters.Add("@StoreID", SqlDbType.Int).Value = 0;
                                Command.Parameters.Add("@ContractID", SqlDbType.Int).Value = 0;
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
                                if (model.isupdate == "False")
                                    model.vid = Command.ExecuteScalar().ToString();
                                else
                                    Command.ExecuteNonQuery();
                                Command.Dispose();
                                decimal discount = Convert.ToDecimal(model.discount);
                                foreach (var dvchild in model.detail)
                                {
                                    Command = new SqlCommand("[SPSW_SaveInvoiceDetailGRN]", con);
                                    Command.CommandType = CommandType.StoredProcedure;
                                    Command.Parameters.Add("@InvNo", SqlDbType.NVarChar).Value = "GRN-" + model.vid;
                                    Command.Parameters.Add("@ProductID", SqlDbType.Int).Value = dvchild.accCode;
                                    Command.Parameters.Add("@GateNo", SqlDbType.NVarChar).Value = 0;
                                    Command.Parameters.Add("@VehNo", SqlDbType.NVarChar).Value = 0;
                                    Command.Parameters.Add("@BTypeID", SqlDbType.Int).Value = 0;
                                    Command.Parameters.Add("@QtyDr", SqlDbType.Decimal).Value = dvchild.box;
                                    Command.Parameters.Add("@QtyCr", SqlDbType.Decimal).Value = 0;
                                    Command.Parameters.Add("@WtDr", SqlDbType.Decimal).Value = dvchild.piece;
                                    Command.Parameters.Add("@WtCr", SqlDbType.Decimal).Value = 0;
                                    Command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = dvchild.rate;
                                    Command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = dvchild.amount;
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
                catch (Exception ex)
                {
                    Tran.Rollback();
                    return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}
