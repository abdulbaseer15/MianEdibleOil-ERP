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
    public class PurchaseRInvoiceController : Controller
    {
        public ActionResult PurchaseRInvoice()
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
                ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName, rate = x.PurchasePrice ?? 0 }).ToList();
                var AccountsListbardana = from s in db.Accounts
                                          where s.AccType == "Detail"
                                          orderby s.Title
                                          select s;
                ViewBag.AccountsListbardana = AccountsListbardana.ToList();
                var AccountsListbrocker = from s in db.Accounts
                                          where s.AccType == "Detail" && s.IsParty == true
                                          orderby s.Title
                                          select s;
                ViewBag.AccountsListbrocker = AccountsListbrocker.ToList();
                var AccountsListvehicles = from s in db.VehicleTypes
                                           orderby s.VType
                                           select s;
                ViewBag.AccountsListvehicles = AccountsListvehicles.ToList();
                var AccountsListStores = from s in db.Stores
                                         orderby s.Title
                                         select s;
                ViewBag.AccountsListStores = AccountsListStores.ToList();
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
                int id = Convert.ToInt32(code);
                var lstDVChilds = db.Products.Where(x => x.ProductID == id).Select(x => x.PurchasePrice).FirstOrDefault();
                return Json(new { responseText = lstDVChilds }, JsonRequestBehavior.AllowGet);
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
            string invnnn = "FPR-" + VId;
            var sd = db.Database.SqlQuery<int>("select TOP 1 VId from VouchersDetail where RefNo='" + invnnn + "'").ToList();
            int VoucherID = sd.FirstOrDefault();
            try
            {
                DateTime date = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                var lstDVChilds = (from t1 in db.Purchase1
                                   join t2 in db.Purchase2 on t1.InvNo equals t2.InvNo
                                   join t3 in db.Products on t2.ProductID equals t3.ProductID
                                   where t1.InvNo == invnnn
                                   select new { t1.PartyBillNO, t1.PartyBillDate, t1.ArrivalID, t1.StrCol1, t1.StrCol2, t1.StrCol3, t1.ContractID, t1.PO_No, t1.AccountId, t1.StoreID, t1.ArrivalType, t1.MarketComAccount, t1.PurAccount, t1.BitCol3, t1.BrockerID, t1.BrockerageAccount, t1.Brate, t1.BrokeryMode, t1.RateCalc, t2.ProductID, t3.ProductName, t2.GateNo, t2.VehNo, t2.QtyDr, t2.WtDr, NumCol22 = t2.NumCol2, t2.NumCol3, t2.Bcut, t2.Qcut, t2.Rate, t2.Amount, t1.InvTotal, t1.Discount, t2.QtyCr, t2.WtCr, t1.SaleTaxAmt, t1.ActualBagQty, t1.BagRate, t1.BagAmount, t1.NumCol1, t1.NumCol2, t1.BitCol1, t1.Freight, t1.MarketComAmount }).ToList();
                return Json(new { lstDVChilds = lstDVChilds.ToList(), DVId1 = VoucherID }, JsonRequestBehavior.AllowGet);
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
                string dbcon = Convert.ToString(Session["con"]);
                DbConection obj = new DbConection();
                string condb = obj.Connect(dbcon);
                ApnaAccountEntities db = new ApnaAccountEntities(condb);
                string dbconsql = Convert.ToString(Session["consql"]);
                SqlConnection con = new SqlConnection(dbconsql);
                try
                {
                    DateTime date1 = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                    con.Open();
                    Tran = con.BeginTransaction();
                    SqlCommand cmd = new SqlCommand("[SPSW_DeleteInvoiceFP]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VId", SqlDbType.VarChar).Value = "FPR-" + DVId;
                    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                    cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "FPR";
                    cmd.Transaction = Tran;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    string refer = "'FPR-" + DVId + "'";
                    var sd = db.Database.SqlQuery<int>("select TOP 1 VId from VouchersDetail where RefNo=" + refer + "").ToList();
                    int VoucherID = sd.FirstOrDefault();
                    SqlCommand cmd2 = new SqlCommand("[SPSW_DeleteVoucher]", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add("@VId", SqlDbType.VarChar).Value = VoucherID;
                    cmd2.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                    cmd2.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                    //Addition
                    Command.Parameters.Add("@User", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                    cmd2.Transaction = Tran;
                    cmd2.ExecuteNonQuery();
                    cmd2.Dispose();
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
                return RedirectToAction("PurchaseInvoice", "PurchaseInvoice");
            }
            else
            {
                TempData["message"] = "You do not have permission to delete this.";
                return RedirectToAction("PurchaseInvoice");
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
                cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "FPR";
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
                string dbcon = Convert.ToString(Session["con"]);
                DbConection obj = new DbConection();
                string condb = obj.Connect(dbcon);
                ApnaAccountEntities db = new ApnaAccountEntities(condb);
                string dbconsql = Convert.ToString(Session["consql"]);
                SqlConnection con = new SqlConnection(dbconsql);
                try
                {
                    DateTime date1 = DateTime.ParseExact(model.comdate, "dd/MM/yyyy", null);
                    DateTime date2 = DateTime.ParseExact(model.yourdate, "dd/MM/yyyy", null); ;
                    // DateTime date2 = DateTime.ParseExact(model.yourdate, "dd/MM/yyyy", null);
                    string vNarattion = "Product :";
                    if (model.detail.Count > 0)
                    {
                        Int16 IsUpdate = Convert.ToInt16((model.isupdate == "False" ? 0 : 1));
                        con.Open();
                        Tran = con.BeginTransaction();
                        Command = new SqlCommand("[SPSW_SavePInvoice]", con);
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add("@VId", SqlDbType.BigInt).Value = model.vid;
                        Command.Parameters.Add("@InvNo", SqlDbType.NVarChar).Value = "FPR-" + model.vid;
                        Command.Parameters.Add("@Idate", SqlDbType.Date).Value = date1;
                        Command.Parameters.Add("@PartyBillNO", SqlDbType.NVarChar).Value = model.yourinvoice;
                        Command.Parameters.Add("@PartyBillDate", SqlDbType.Date).Value = date2;
                        Command.Parameters.Add("@AccountId", SqlDbType.NVarChar).Value = model.supid;
                        Command.Parameters.Add("@PurAccount", SqlDbType.NVarChar).Value = model.narration;
                        Command.Parameters.Add("@InvTotal", SqlDbType.Decimal).Value = model.nettotal;
                        Command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = model.discount;
                        Command.Parameters.Add("@SaleTaxAmt", SqlDbType.Decimal).Value = model.gst;
                        Command.Parameters.Add("@NetTotal", SqlDbType.Decimal).Value = model.nettotal;
                        Command.Parameters.Add("@Sno", SqlDbType.Int).Value = model.vid;
                        Command.Parameters.Add("@IType", SqlDbType.NVarChar).Value = "FPR";
                        Command.Parameters.Add("@BagType", SqlDbType.NVarChar).Value = model.po;
                        Command.Parameters.Add("@BagQty0", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@StoreID", SqlDbType.Int).Value = model.store;
                        Command.Parameters.Add("@ContractID", SqlDbType.Int).Value = model.grnno;
                        Command.Parameters.Add("@BrockerID", SqlDbType.NVarChar).Value = model.brokid;
                        Command.Parameters.Add("@BrockerageAccount", SqlDbType.NVarChar).Value = model.brokageid;
                        Command.Parameters.Add("@Brate", SqlDbType.Decimal).Value = model.brorate;
                        Command.Parameters.Add("@BrokeryMode", SqlDbType.NVarChar).Value = model.mode;
                        Command.Parameters.Add("@CalcFactor", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@ArrivalType", SqlDbType.Int).Value = model.veh;
                        Command.Parameters.Add("@ArrivalID", SqlDbType.Int).Value = model.wbno;
                        Command.Parameters.Add("@Freight", SqlDbType.Decimal).Value = model.freight;
                        Command.Parameters.Add("@BagRate", SqlDbType.Decimal).Value = model.BagRate;
                        Command.Parameters.Add("@LabourRate", SqlDbType.Decimal).Value = model.LabourRate;
                        Command.Parameters.Add("@LabourAmt", SqlDbType.Decimal).Value = model.labourAmount;
                        Command.Parameters.Add("@LabCalOnWt", SqlDbType.Bit).Value = model.LabCalOnWt;
                        Command.Parameters.Add("@BagAmount", SqlDbType.Decimal).Value = model.bagAmount;
                        Command.Parameters.Add("@BrokeryAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(model.totalquantity) * Convert.ToDecimal(model.brorate);
                        Command.Parameters.Add("@ActualBagQty", SqlDbType.Decimal).Value = model.Bags;
                        Command.Parameters.Add("@MarketComAccount", SqlDbType.NVarChar).Value = model.sutid;
                        Command.Parameters.Add("@MarketComAmount", SqlDbType.Decimal).Value = model.SutliAmount;
                        Command.Parameters.Add("@FreightAc", SqlDbType.NVarChar).Value = model.freid;
                        Command.Parameters.Add("@BardanaAc", SqlDbType.NVarChar).Value = model.barid;
                        Command.Parameters.Add("@LabourAc", SqlDbType.NVarChar).Value = model.labid;
                        Command.Parameters.Add("@RateCalc", SqlDbType.NVarChar).Value = model.RateCalc;
                        Command.Parameters.Add("@IsUpdate", SqlDbType.Bit).Value = IsUpdate;
                        Command.Transaction = Tran;
                        if (model.isupdate == "False")
                            Command.ExecuteScalar();
                        else
                            Command.ExecuteNonQuery();
                        Command.Dispose();
                        string vehNo = " ";
                        decimal discount = Convert.ToDecimal(model.discount);
                        foreach (var dvchild in model.detail)
                        {
                            vehNo = vehNo + dvchild.VehNo + ",";
                            vNarattion = vNarattion + dvchild.title + " Qty: " + dvchild.quantity + "Weight :" + dvchild.weight + " Rate:" + dvchild.rate;
                            Command = new SqlCommand("[SPSW_SavePInvoiceDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@InvNo", SqlDbType.NVarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@ProductID", SqlDbType.Int).Value = dvchild.accCode;
                            Command.Parameters.Add("@GateNo", SqlDbType.NVarChar).Value = dvchild.gateNo;
                            Command.Parameters.Add("@VehNo", SqlDbType.NVarChar).Value = dvchild.VehNo;
                            Command.Parameters.Add("@BTypeID", SqlDbType.Int).Value = 0;
                            Command.Parameters.Add("@QtyDr", SqlDbType.Decimal).Value = 0;
                            Command.Parameters.Add("@QtyCr", SqlDbType.Decimal).Value = dvchild.quantity;
                            Command.Parameters.Add("@WtDr", SqlDbType.Decimal).Value = 0;
                            Command.Parameters.Add("@WtCr", SqlDbType.Decimal).Value = dvchild.weight;
                            Command.Parameters.Add("@MillWeight", SqlDbType.Decimal).Value = dvchild.millweight;
                            Command.Parameters.Add("@SupWeight", SqlDbType.Decimal).Value = dvchild.supweight;
                            Command.Parameters.Add("@QCut", SqlDbType.Decimal).Value = dvchild.qcut;
                            Command.Parameters.Add("@BrdCut", SqlDbType.Decimal).Value = dvchild.brdcut;
                            Command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = dvchild.rate;
                            Command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = dvchild.amount;
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                        }
                        string voucherClosing = Convert.ToString(Session["Vouchertype"]);
                        int VoucherID = 0;
                        int DVId1 = 0;
                        string DVId = "";
                        if (IsUpdate == 1)
                        {
                            string refer = "'FPR-" + model.vid + "'";
                            var sd = db.Database.SqlQuery<int>("select TOP 1 VId from VouchersDetail where RefNo=" + refer + "").ToList();
                            VoucherID = sd.FirstOrDefault();
                            DVId1 = (from s in db.Vouchers where s.VoucherID == VoucherID select s.VId).FirstOrDefault();
                        }
                        else
                        {
                            SqlCommand cmd = new SqlCommand("[SPSW_GetVId]", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            cmd.Parameters.Add("@VoucherClosing", SqlDbType.VarChar).Value = voucherClosing;
                            cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            cmd.Transaction = Tran;
                            DVId = cmd.ExecuteScalar().ToString();
                            string[] a = DVId.Split(',');
                            DVId1 = Convert.ToInt32(a[0]);
                            VoucherID = Convert.ToInt32(a[1]);
                            cmd.Dispose();
                        }
                        var Billnarration = "Purchase Bill " + "FPR-" + model.vid + " Bilty #:" + model.yourinvoice + " WB #:" + model.wbno + "GRN #:" + model.grnno + " PO #:" + model.po + " VehNo: " + model.veh;
                        Random rnd = new Random();
                        int delay = rnd.Next(1, 50);
                        System.Threading.Thread.Sleep(delay);
                        Command = new SqlCommand("[SPSW_SaveVoucher]", con);
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add("@VId", SqlDbType.Int).Value = DVId1;
                        Command.Parameters.Add("@VoucherID", SqlDbType.Int).Value = VoucherID;
                        Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                        Command.Parameters.Add("@IsUpdate", SqlDbType.Bit).Value = IsUpdate;
                        Command.Parameters.Add("@LastUpdate", SqlDbType.DateTime).Value = DateTime.Now;
                        //Addition
                        Command.Parameters.Add("@User", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                        Command.Transaction = Tran;
                        VoucherID = Convert.ToInt32(Command.ExecuteScalar().ToString());
                        Command.Dispose();
                        foreach (var dvchild in model.detail)
                        {
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.supid;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = Convert.ToDecimal(dvchild.amount);
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                            int proCode = Convert.ToInt32(dvchild.accCode);
                            var dataproduct = (from t1 in db.Products
                                               join t2 in db.Accounts on t1.StrCol1 equals t2.AccountId
                                               select new { t1.StrCol1, t2.Title }).FirstOrDefault();
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = dataproduct.StrCol1;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = Convert.ToDecimal(dvchild.amount);
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = dataproduct.Title;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                        }
                        decimal finalbagAmount = Convert.ToDecimal(model.bagAmount);
                        if (finalbagAmount > 0)
                        {
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.supid;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = Convert.ToDecimal(model.bagAmount);
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Bardana :" + Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.barid;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = Convert.ToDecimal(model.bagAmount);
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Bardana :" + Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.barname;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                        }
                        else
                        {
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.supid;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = Convert.ToDecimal(model.bagAmount);
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Bardana :" + Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.barid;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = Convert.ToDecimal(model.bagAmount);
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Bardana :" + Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.barname;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                        }
                        decimal finallabourAmount = Convert.ToDecimal(model.labourAmount);
                        if (finallabourAmount > 0)
                        {
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.supid;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = Convert.ToDecimal(model.labourAmount);
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.labid;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = Convert.ToDecimal(model.labourAmount);
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.labname;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                        }
                        else
                        {
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.supid;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = Convert.ToDecimal(model.labourAmount);
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.labid;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = Convert.ToDecimal(model.labourAmount);
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.labname;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                        }
                        decimal finalfreight = Convert.ToDecimal(model.freight);
                        if (finalfreight > 0)
                        {
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.supid;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = Convert.ToDecimal(model.freight);
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.freid;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = Convert.ToDecimal(model.freight);
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.frename;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                        }
                        else
                        {
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.supid;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = Convert.ToDecimal(model.freight);
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.freid;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = Convert.ToDecimal(model.freight);
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.frename;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                        }
                        decimal finalSutliAmount = Convert.ToDecimal(model.SutliAmount);
                        if (finalSutliAmount > 0)
                        {
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.supid;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = Convert.ToDecimal(model.SutliAmount);
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.sutid;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = Convert.ToDecimal(model.SutliAmount);
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.sutname;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                        }
                        else
                        {
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.supid;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = Convert.ToDecimal(model.SutliAmount);
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.sutid;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = Convert.ToDecimal(model.SutliAmount);
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.sutname;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                        }
                        Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.brokageid;
                        Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                        Command.Parameters.Add("@Credit", SqlDbType.Money).Value = Convert.ToDecimal(model.brorate) * Convert.ToDecimal(model.Bags);
                        Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Brockery and brockage :" + Billnarration + vNarattion;
                        Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.brokagename;
                        Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                        Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                        Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                        Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                        //Addition
                        Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                        Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                        //end
                        Command.Transaction = Tran;
                        Command.ExecuteNonQuery();
                        Command.Dispose();
                        Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.brokid;
                        Command.Parameters.Add("@Debit", SqlDbType.Money).Value = Convert.ToDecimal(model.brorate) * Convert.ToDecimal(model.Bags);
                        Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                        Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Brockery and brockage :" + Billnarration + vNarattion;
                        Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.brokname;
                        Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FPR-" + model.vid;
                        Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                        Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                        Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                        //Addition
                        Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                        Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                        //end
                        Command.Transaction = Tran;
                        Command.ExecuteNonQuery();
                        Command.Dispose();
                        voucherno2 = Convert.ToString(VoucherID);
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
                return Json(new { success = true, responseText = "Saved", voucherNo2 = voucherno2 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
