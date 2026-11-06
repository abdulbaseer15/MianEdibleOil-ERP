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
    public class SaleGSTInvoiceController : Controller
    {
        //
        // GET: /SaleGSTInvoice/
        public ActionResult SaleGSTInvoice()
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
                ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName, rate = x.SalePrice ?? 0,SMW=x.SMW??0,GSTRate=x.GstRate??0 }).ToList();
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
                var FarmList = from s in db.Farms
                               orderby s.FarmID
                               select s;
                ViewBag.FarmsList = FarmList.ToList();
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
            string invnnn = "FGSTS-" + VId;
            try
            {
                DateTime date = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                var lstDVChilds = (from t1 in db.GstInvoice1
                                   join t2 in db.GstInvoice2 on t1.InvNo equals t2.InvNo
                                   join t3 in db.Products on t2.ProductID equals t3.ProductID
                                   where t1.InvNo == invnnn
                                   select new { t1.InvNo, t1.Idate, t1.YourBillNo, t1.YourBillDate, t1.AccountId, t1.SaleAccount, t1.Total_Net, t1.Total_Dis, t1.Total_GSt, t1.Sno, t1.IType, t1.StoreID, t1.ContractID, t1.BrockerID, t1.BrockerageAccount, t1.GateNo, t1.BRate, t1.Total_Qty, t1.Total_Kgs,  t2.ProductID, t3.ProductName, t2.QtyCr, t2.WtCr, t2.Rate, t2.Net_Amount, t2.Gst_Rate, t2.Gross_Amount, t2.Dis,t2.Gst }).ToList();
                return Json(lstDVChilds.ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult SearchFarms(string Date, string AccountId)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                DateTime date = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                var lstDVChilds = (from t1 in db.Farms
                                   where t1.AccountID == AccountId
                                   select new { t1.FarmID, t1.Title, t1.AccountID }).ToList();
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
                var lstDVChilds = (from t1 in db.qrySaleOrderStatus
                                   where t1.AccountId == AccountId
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
                    SqlCommand cmd = new SqlCommand("[SPSW_DeleteSaleGstInvoice]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VId", SqlDbType.VarChar).Value = "FGSTS-" + DVId;
                    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                    cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "FGSTS";
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
                return RedirectToAction("SaleGSTInvoice", "SaleGSTInvoice");
            }
            else
            {
                TempData["message"] = "You do not have permission to delete this.";
                return RedirectToAction("SaleGSTInvoice");
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
                SqlCommand cmd = new SqlCommand("Select Max(isnull(Sno,0))+1 from GstInvoice1 where Itype ='FGSTS'", con);
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
                    DateTime date2 = DateTime.ParseExact(model.yourdate, "dd/MM/yyyy", null);
                    // DateTime date2 = DateTime.ParseExact(model.yourdate, "dd/MM/yyyy", null);
                    string vNarattion = "Product :";
                    if (model.detail.Count > 0)
                    {
                        decimal brockeyAmount = 0;
                        if (model.BrockeryMode == "TRUCK")
                        {
                            brockeyAmount = GetCalculationFactor(model.ContractID, (Convert.ToDecimal(model.VehcileType) * Convert.ToDecimal(model.BrockeyRate)).ToString());
                            if (brockeyAmount == 0 && Convert.ToDecimal(model.ContractID) == 0)
                            {
                                brockeyAmount = Convert.ToDecimal(model.BrockeyRate) * model.detail.Count;
                            }
                        }
                        else if (model.BrockeryMode == "BAG")
                        {
                            brockeyAmount = Convert.ToDecimal(model.totalquantity) * Convert.ToDecimal(model.BrockeyRate);
                        }
                        else if (model.BrockeryMode == "MOND")
                        {
                            brockeyAmount = Convert.ToDecimal(model.TotalWeight) * Convert.ToDecimal(model.BrockeyRate);
                        }
                        else if (model.BrockeryMode == "AMOUNT")
                        {
                            brockeyAmount = (Convert.ToDecimal(model.nettotal) * Convert.ToDecimal(model.BrockeyRate)) / 100;
                        }
                        else
                        {
                            brockeyAmount = GetCalculationFactor(model.ContractID, (Convert.ToDecimal(model.VehcileType) * Convert.ToDecimal(model.BrockeyRate)).ToString());
                        }
                        decimal InvoiceBill = Convert.ToDecimal(model.nettotal) - Convert.ToDecimal(model.discount);
                        decimal Discount = Convert.ToDecimal(model.discount);
                        decimal BardanaAmount = Convert.ToDecimal(model.bagAmount);
                        decimal LabourAmount = Convert.ToDecimal(model.labourAmount);
                        decimal FreightAmount = Convert.ToDecimal(model.freight);
                        decimal SutliAmount = Convert.ToDecimal(model.SutliAmount);
                        Int16 IsUpdate = Convert.ToInt16((model.isupdate == "False" ? 0 : 1));
                        con.Open();
                        Tran = con.BeginTransaction();
                        Command = new SqlCommand("[SPSW_SaveGSTSaleInvoice]", con);
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add("@VId", SqlDbType.BigInt).Value = model.vid;
                        Command.Parameters.Add("@InvNo", SqlDbType.NVarChar).Value = "FGSTS-" + model.vid;
                        Command.Parameters.Add("@Idate", SqlDbType.Date).Value = date1;
                        Command.Parameters.Add("@PartyBillNO", SqlDbType.NVarChar).Value = model.yourinvoice;
                        Command.Parameters.Add("@PartyBillDate", SqlDbType.Date).Value = date2;
                        Command.Parameters.Add("@AccountId", SqlDbType.NVarChar).Value = model.supid;
                        Command.Parameters.Add("@PurAccount", SqlDbType.NVarChar).Value = "";
                        Command.Parameters.Add("@InvTotal", SqlDbType.Decimal).Value = model.nettotal;
                        Command.Parameters.Add("@Total_Dis", SqlDbType.Decimal).Value = model.discount;
                        Command.Parameters.Add("@SaleTaxAmt", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@NetTotal", SqlDbType.Decimal).Value = model.NetAmount;
                        Command.Parameters.Add("@Sno", SqlDbType.Int).Value = model.vid;
                        Command.Parameters.Add("@IType", SqlDbType.NVarChar).Value = "FGSTS";
                        Command.Parameters.Add("@BagType", SqlDbType.NVarChar).Value = "";
                        Command.Parameters.Add("@BagQty0", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@StoreID", SqlDbType.Int).Value = model.storeid;
                        Command.Parameters.Add("@ContractID", SqlDbType.Int).Value = 0;
                        Command.Parameters.Add("@BrockerID", SqlDbType.NVarChar).Value = model.BrockerID;
                        Command.Parameters.Add("@BrockerageAccount", SqlDbType.NVarChar).Value = model.BrockerageAccount;
                        Command.Parameters.Add("@Brate", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@Total_GSt", SqlDbType.Decimal).Value = model.TotalWeight;
                        Command.Parameters.Add("@CalcFactor", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@ArrivalType", SqlDbType.Int).Value = 0;
                        Command.Parameters.Add("@ArrivalID", SqlDbType.Int).Value = 0;
                        Command.Parameters.Add("@Freight", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@BagRate", SqlDbType.Decimal).Value =0;
                        Command.Parameters.Add("@Total_Kgs", SqlDbType.Decimal).Value = model.bagAmount;
                        Command.Parameters.Add("@Total_Gross", SqlDbType.Decimal).Value = model.NetAmount;
                        Command.Parameters.Add("@Total_Qty", SqlDbType.Decimal).Value = model.totalquantity;
                        Command.Parameters.Add("@MarketComAccount", SqlDbType.NVarChar).Value = model.MarketComAccount;
                        Command.Parameters.Add("@MarketComAmount", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@RateCalc", SqlDbType.NVarChar).Value = model.RateCalc;
                        Command.Parameters.Add("@MillType", SqlDbType.NVarChar).Value = 0;
                        Command.Parameters.Add("@FarmID", SqlDbType.Int).Value = model.FarmId;
                        Command.Parameters.Add("@So_No", SqlDbType.NVarChar).Value = model.SO_NO;
                        Command.Parameters.Add("@GateNo", SqlDbType.NVarChar).Value = model.GateNo;
                        Command.Parameters.Add("@BardanaAc", SqlDbType.NVarChar).Value = model.BardanaAc;
                        Command.Parameters.Add("@LabourAc", SqlDbType.NVarChar).Value = model.LabourAc;
                        Command.Parameters.Add("@LabourRate", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@LabourAmt", SqlDbType.Decimal).Value =0;
                        Command.Parameters.Add("@DiscPercent", SqlDbType.Decimal).Value =0;
                        var labCalOnWt = 0;
                        if (model.LabCalOnWt == "True")
                        {
                            labCalOnWt = 1;
                        }
                        Command.Parameters.Add("@LabCalOnWt", SqlDbType.Bit).Value = labCalOnWt;
                        Command.Parameters.Add("@IsUpdate", SqlDbType.Bit).Value = IsUpdate;
                        Command.Transaction = Tran;
                        Command.ExecuteNonQuery();
                        Command.Dispose();
                        string vehNo = " ";
                        decimal discount = Convert.ToDecimal(model.discount);
                        foreach (var dvchild in model.detail)
                        {
                            vehNo = vehNo + dvchild.VehNo + ",";
                            vNarattion = vNarattion + dvchild.title + " Qty: " + dvchild.box + "Weight :" + dvchild.piece + " Rate:" + dvchild.rate;
                            Command = new SqlCommand("[SPSW_SaveGstSaleInvoiceDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@InvNo", SqlDbType.NVarChar).Value = "FGSTS-" + model.vid;
                            Command.Parameters.Add("@ProductID", SqlDbType.Int).Value = dvchild.accCode;
                            Command.Parameters.Add("@Gross_Amount", SqlDbType.Decimal).Value = dvchild.gateNo;
                            Command.Parameters.Add("@GstRate", SqlDbType.NVarChar).Value = dvchild.VehNo;
                            Command.Parameters.Add("@Gst", SqlDbType.NVarChar).Value = dvchild.debit;
                            Command.Parameters.Add("@Dis", SqlDbType.Decimal).Value = dvchild.BTypeID;
                            Command.Parameters.Add("@QtyCr", SqlDbType.Decimal).Value = dvchild.box;
                            Command.Parameters.Add("@QtyDr", SqlDbType.Decimal).Value = 0;
                            Command.Parameters.Add("@WtCr", SqlDbType.Decimal).Value = dvchild.piece;
                            Command.Parameters.Add("@WtDr", SqlDbType.Decimal).Value = 0;
                            Command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = dvchild.rate;
                            Command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = dvchild.amount;
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                        }
                        //string Qry = @" exec SP_NVNo @Dated='" + date1 + "',@VchType='SJV',@snotype='M'";
                        //if (model.isupdate == "True")
                        //{
                        //    Qry = "Select VoucherId as vid,VoucherNo as vno from Vouchers1 where Reference='FGSTS-" + model.vid + "'";
                        //}
                        //SqlDataAdapter da = new SqlDataAdapter(Qry, dbconsql);
                        //DataTable dt = new DataTable();
                        //da.Fill(dt);
                        //var results = from myRow in dt.AsEnumerable()
                        //              select myRow;
                        //string VoucherNo = "";
                        //int VoucherID = 0;
                        //foreach (var dr in results)
                        //{
                        //    VoucherNo = dr["vno"].ToString();
                        //    VoucherID = Convert.ToInt32(dr["vid"].ToString());
                        //}
                        //var Billnarration = "Sale Bill " + "FGSTS-" + model.vid + " Bilty #:" + model.yourinvoice + " WB #:" + model.DispatchNo + "DC #:" + model.ContractID + " SO #:" + model.SO_NO + "B Rate: " + model.BrockeyRate + "OGP: " + " VehNo: " + vehNo;
                        //Command = new SqlCommand("[SPSW_SaveVoucherVB]", con);
                        //Command.CommandType = CommandType.StoredProcedure;
                        //Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //Command.Parameters.Add("@VchNo", SqlDbType.VarChar).Value = VoucherNo;
                        //Command.Parameters.Add("@vDate", SqlDbType.Date).Value = date1;
                        //Command.Parameters.Add("@isPending", SqlDbType.VarChar).Value = 0;
                        //Command.Parameters.Add("@IsUpdate", SqlDbType.Bit).Value = IsUpdate;
                        //Command.Parameters.Add("@Reference", SqlDbType.VarChar).Value = "FGSTS-" + model.vid;
                        //Command.Transaction = Tran;
                        ////string a = "";
                        ////if (model.isupdate == "False")
                        ////    a = Command.ExecuteScalar().ToString();
                        ////else
                        //Command.ExecuteNonQuery();
                        //Command.Dispose();
                        //Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                        //Command.CommandType = CommandType.StoredProcedure;
                        //Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        //Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.supid;
                        //Command.Parameters.Add("@Debit", SqlDbType.Money).Value = InvoiceBill;
                        //Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                        //Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration + vNarattion;
                        //Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                        //Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
                        //Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                        //Command.Transaction = Tran;
                        //Command.ExecuteNonQuery();
                        //Command.Dispose();
                        //Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                        //Command.CommandType = CommandType.StoredProcedure;
                        //Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        //Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = "411111";
                        //Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                        //Command.Parameters.Add("@Credit", SqlDbType.Money).Value = InvoiceBill + discount;
                        //Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration + vNarattion;
                        //Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                        //Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
                        //Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                        //Command.Transaction = Tran;
                        //Command.ExecuteNonQuery();
                        //Command.Dispose();
                        //Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                        //Command.CommandType = CommandType.StoredProcedure;
                        //Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = "411111";
                        //Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        //Command.Parameters.Add("@Debit", SqlDbType.Money).Value = discount;
                        //Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                        //Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Discount :" + Billnarration + vNarattion;
                        //Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                        //Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
                        //Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                        //Command.Transaction = Tran;
                        //Command.ExecuteNonQuery();
                        //Command.Dispose();
                        ////InvoiceBill
                        ////Discount
                        ////BardanaAmount
                        ////LabourAmount
                        ////FreightAmount
                        ////SutliAmount
                        //Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                        //Command.CommandType = CommandType.StoredProcedure;
                        //Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.supid;
                        //Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        //Command.Parameters.Add("@Debit", SqlDbType.Money).Value = BardanaAmount;
                        //Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                        //Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Bardana :" + Billnarration + vNarattion;
                        //Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                        //Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
                        //Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                        //Command.Transaction = Tran;
                        //Command.ExecuteNonQuery();
                        //Command.Dispose();
                        //Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                        //Command.CommandType = CommandType.StoredProcedure;
                        //Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.BardanaAc;
                        //Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        //Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                        //Command.Parameters.Add("@Credit", SqlDbType.Money).Value = BardanaAmount;
                        //Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Bardana :" + Billnarration + vNarattion;
                        //Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                        //Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
                        //Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                        //Command.Transaction = Tran;
                        //Command.ExecuteNonQuery();
                        //Command.Dispose();
                        //Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                        //Command.CommandType = CommandType.StoredProcedure;
                        //Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.supid;
                        //Command.Parameters.Add("@Debit", SqlDbType.Money).Value = LabourAmount;
                        //Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                        //Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Labour :" + Billnarration + vNarattion;
                        //Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                        //Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        //Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
                        //Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                        //Command.Transaction = Tran;
                        //Command.ExecuteNonQuery();
                        //Command.Dispose();
                        //Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                        //Command.CommandType = CommandType.StoredProcedure;
                        //Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.LabourAc;
                        //Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                        //Command.Parameters.Add("@Credit", SqlDbType.Money).Value = LabourAmount;
                        //Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Labour :" + Billnarration + vNarattion;
                        //Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                        //Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        //Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
                        //Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                        //Command.Transaction = Tran;
                        //Command.ExecuteNonQuery();
                        //Command.Dispose();
                        //Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                        //Command.CommandType = CommandType.StoredProcedure;
                        //Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.supid;
                        //Command.Parameters.Add("@Debit", SqlDbType.Money).Value = FreightAmount;
                        //Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                        //Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Freight :" + Billnarration + vNarattion;
                        //Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                        //Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        //Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
                        //Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                        //Command.Transaction = Tran;
                        //Command.ExecuteNonQuery();
                        //Command.Dispose();
                        //Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                        //Command.CommandType = CommandType.StoredProcedure;
                        //Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.FreightAc;
                        //Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                        //Command.Parameters.Add("@Credit", SqlDbType.Money).Value = FreightAmount;
                        //Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Freight :" + Billnarration + vNarattion;
                        //Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                        //Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        //Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
                        //Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                        //Command.Transaction = Tran;
                        //Command.ExecuteNonQuery();
                        //Command.Dispose();
                        //Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                        //Command.CommandType = CommandType.StoredProcedure;
                        //Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.supid;
                        //Command.Parameters.Add("@Debit", SqlDbType.Money).Value = SutliAmount;
                        //Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                        //Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Sutli : " + Billnarration + vNarattion;
                        //Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                        //Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        //Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
                        //Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                        //Command.Transaction = Tran;
                        //Command.ExecuteNonQuery();
                        //Command.Dispose();
                        //Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                        //Command.CommandType = CommandType.StoredProcedure;
                        //Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.MarketComAccount;
                        //Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                        //Command.Parameters.Add("@Credit", SqlDbType.Money).Value = SutliAmount;
                        //Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Sutli : " + Billnarration + vNarattion;
                        //Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                        //Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        //Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
                        //Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                        //Command.Transaction = Tran;
                        //Command.ExecuteNonQuery();
                        //Command.Dispose();
                        //Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                        //Command.CommandType = CommandType.StoredProcedure;
                        //Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.BrockerID;
                        //Command.Parameters.Add("@Debit", SqlDbType.Money).Value = brockeyAmount;
                        //Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                        //Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Brockerage :" + Billnarration + vNarattion;
                        //Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                        //Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        //Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
                        //Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                        //Command.Transaction = Tran;
                        //Command.ExecuteNonQuery();
                        //Command.Dispose();
                        //Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                        //Command.CommandType = CommandType.StoredProcedure;
                        //Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.BrockerageAccount;
                        //Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                        //Command.Parameters.Add("@Credit", SqlDbType.Money).Value = brockeyAmount;
                        //Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Brockerage :" + Billnarration + vNarattion;
                        //Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                        //Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        //Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
                        //Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                        //Command.Transaction = Tran;
                        //Command.ExecuteNonQuery();
                        //Command.Dispose();
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
        private decimal GetCalculationFactor(string ContractId, string vehiclType)
        {
            decimal calFactor = 0;
            if (!string.IsNullOrEmpty(ContractId) && !string.IsNullOrEmpty(vehiclType))
            {
                string qry = "SELECT Mode From Contracts WHERE (cType = 'Sale') AND (ContractID =" + ContractId + ")";
                string dbconsql = Convert.ToString(Session["consql"]);
                SqlConnection con = new SqlConnection(dbconsql);
                con.Open();
                SqlCommand cmd = new SqlCommand(qry, con);
                var Mode = Convert.ToString(cmd.ExecuteScalar());
                if (string.IsNullOrEmpty(Mode))
                {
                    return calFactor;
                }
                else
                {
                    qry = "SELECT ContractsModeCalculation.Qty, ArrivalTypes.ArrivalType FROM  ContractsModeCalculation INNER JOIN ArrivalTypes ON ContractsModeCalculation.Mode_Title = ArrivalTypes.ArrID WHERE (ContractsModeCalculation.Mode_Title ='" + vehiclType + "') AND (ContractsModeCalculation.Calc_Mode ='" + Mode + "')";
                    cmd = new SqlCommand(qry, con);
                    calFactor = Convert.ToDecimal(cmd.ExecuteScalar());
                    return calFactor;
                }
            }
            return calFactor;
        }
        [HttpPost]
        public JsonResult GetBardana(string BagId)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                int id = Convert.ToInt32(BagId);
                var lstDVChilds = db.BardanaTypes.Where(x => x.BTypeID == id).Select(x => x.Deduction).FirstOrDefault();
                return Json(new { responseText = lstDVChilds }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
