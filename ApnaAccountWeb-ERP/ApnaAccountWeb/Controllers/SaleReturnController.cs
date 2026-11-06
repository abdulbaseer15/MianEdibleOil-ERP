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
    public class SaleReturnController : Controller
    {
        //
        // GET: /SaleReturn/
        public ActionResult SaleReturn()
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
               // ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName, rate = x.SalePrice ?? 0 }).ToList();
                ViewBag.AccCodeDetails = db.Products.Where(x => x.PG_ID == 37).Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName, rate = x.SalePrice ?? 0 }).ToList();
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
        [HttpGet]
        public JsonResult SearchInvoiceIGP(string Date, string VId)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                //DateTime date = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                //DateTime date = Convert.ToDateTime(Date), "dd/MM/yyyy", null);
                var lstDVChilds = (from t1 in db.Sale1
                                   join t2 in db.Sale2 on t1.InvNo equals t2.InvNo
                                   join t3 in db.Products on t2.ProductID equals t3.ProductID
                                   join t4 in db.Accounts on t1.AccountId equals t4.AccountId
                                   where t1.InvNo == VId
                                   select new { t1.InvNo, t1.Idate,t1.DispatchNo, t1.AccountId, t1.ArrivalType, t2.ProductID, t3.ProductName,t1.Sno,t2.VehNo, t2.BTypeID,t2.QtyCr, t2.WtCr, t2.Rate,t4.SalesDiscount, t1.Discount, t2.Amount, t3.RateCalc1 }).ToList();
                return Json(new { lstDVChilds = lstDVChilds.ToList() }, JsonRequestBehavior.AllowGet);
                // return Json(lstDVChilds.ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult getInvoicesList(string Date)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                DateTime date = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                var lstDVChilds = (from t1 in db.Sale1
                                   join t2 in db.Sale2 on t1.InvNo equals t2.InvNo
                                   join t3 in db.Accounts on t1.AccountId equals t3.AccountId
                                   where t1.IType == "FSR"
                                   select new { t1.Sno, t1.Idate, t3.Title, t1.NetTotal }).ToList();
                return Json(lstDVChilds.ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult SearchInvoice2(string Date, int VId)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            string invnnn = "FS-" + VId;
            var sd = db.Database.SqlQuery<int>("select TOP 1 VId from VouchersDetail where RefNo='" + invnnn + "'").ToList();
            int VoucherID = sd.FirstOrDefault();
            try
            {
                //DateTime date = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                //DateTime date = Convert.ToDateTime(Date), "dd/MM/yyyy", null);
                var lstDVChilds = (from t1 in db.Sale1
                                   join t2 in db.Sale2 on t1.InvNo equals t2.InvNo
                                   join t3 in db.Products on t2.ProductID equals t3.ProductID
                                   where t1.InvNo == invnnn
                                   select new { t1.InvNo, t1.Idate, t1.YourBillNo, t1.YourBillDate, t1.AccountId, t1.SaleAccount, t1.InvTotal, t1.Discount, t1.SaleTaxAmt, t1.Sno, t1.IType, t1.StoreID, t1.ContractID, t1.BrockerID, t1.BrockerageAccount, t1.ArrivalType, t1.brate, t1.DispatchNo, t1.Freight, t1.BagRate, t1.BagAmount, t1.BrokeryMode, t1.BrokeryAmount, t1.NetTotal, t1.StrCol1, t1.StrCol2, t1.StrCol3, t1.RateCalc, t1.ActualBagQty, t1.MarketComAccount, t1.MarketComAmount, t1.SO_No, t1.FarmID, t1.NumCol1, t1.NumCol2, t1.NumCol3, t1.BitCol1, t2.ProductID, t3.ProductName, t2.QtyCr, t2.WtCr, t2.Rate, t2.Amount, t2.GateNo, t2.VehNo, t2.BTypeID }).ToList();
                return Json(new { lstDVChilds = lstDVChilds.ToList(), DVId1 = VoucherID }, JsonRequestBehavior.AllowGet);
                // return Json(lstDVChilds.ToList(), JsonRequestBehavior.AllowGet);
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
            string invnnn = "FSR-" + VId;
            var sd = db.Database.SqlQuery<int>("select TOP 1 VId from VouchersDetail where RefNo='" + invnnn + "'").ToList();
            int VoucherID = sd.FirstOrDefault();
            try
            {
               // DateTime date = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                //DateTime date = Convert.ToDateTime(Date);
                var lstDVChilds = (from t1 in db.Sale1
                                   join t2 in db.Sale2 on t1.InvNo equals t2.InvNo
                                   join t3 in db.Products on t2.ProductID equals t3.ProductID
                                   where t1.InvNo == invnnn
                                   select new { t1.InvNo, t1.Idate, t1.YourBillNo, t1.YourBillDate, t1.AccountId, t1.SaleAccount, t1.InvTotal, t1.Discount, t1.SaleTaxAmt, t1.Sno, t1.IType, t1.StoreID, t1.ContractID, t1.BrockerID, t1.BrockerageAccount, t1.ArrivalType, t1.brate, t1.DispatchNo, t1.Freight, t1.BagRate, t1.BagAmount, t1.BrokeryMode, t1.BrokeryAmount, t1.NetTotal, t1.StrCol1, t1.StrCol2, t1.StrCol3, t1.RateCalc, t1.ActualBagQty, t1.MarketComAccount, t1.MarketComAmount, t1.SO_No, t1.FarmID, t1.NumCol1, t1.NumCol2, t1.NumCol3, t1.BitCol1, t2.ProductID, t3.ProductName, t2.QtyDr, t2.WtDr, t2.Rate, t2.Amount, t2.GateNo, t2.VehNo, t2.BTypeID }).ToList();
                return Json(new { lstDVChilds = lstDVChilds.ToList(), DVId1 = VoucherID }, JsonRequestBehavior.AllowGet);
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
                    SqlCommand cmd = new SqlCommand("[SPSW_DeleteSaleInvoice]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VId", SqlDbType.VarChar).Value = "FSR-" + DVId;
                    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                    cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "FSR";
                    cmd.Transaction = Tran;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    string refer = "'FSR-" + DVId + "'";
                    var sd = db.Database.SqlQuery<int>("select TOP 1 VId from VouchersDetail where RefNo=" + refer + "").ToList();
                    int VoucherID = sd.FirstOrDefault();
                    SqlCommand cmd2 = new SqlCommand("[SPSW_DeleteVoucher]", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add("@VId", SqlDbType.VarChar).Value = VoucherID;
                    cmd2.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                    cmd2.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
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
                return RedirectToAction("SaleReturn", "SaleReturn");
            }
            else
            {
                TempData["message"] = "You do not have permission to delete this.";
                return RedirectToAction("SaleReturn");
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
                SqlCommand cmd = new SqlCommand("Select Max(isnull(Sno,0))+1 from Sale1 where Itype ='FSR'", con);
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
            string voucherno = null;
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
                   // DateTime date1 = Convert.ToDateTime(model.comdate);
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
                        Command = new SqlCommand("[SPSW_SaveSaleInvoice]", con);
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add("@VId", SqlDbType.BigInt).Value = model.vid;
                        Command.Parameters.Add("@InvNo", SqlDbType.NVarChar).Value = "FSR-" + model.vid;
                        Command.Parameters.Add("@Idate", SqlDbType.Date).Value = date1;
                        Command.Parameters.Add("@PartyBillNO", SqlDbType.NVarChar).Value = model.yourinvoice;
                        Command.Parameters.Add("@PartyBillDate", SqlDbType.Date).Value = date2;
                        Command.Parameters.Add("@AccountId", SqlDbType.NVarChar).Value = model.supid;
                        Command.Parameters.Add("@PurAccount", SqlDbType.NVarChar).Value = "";
                        Command.Parameters.Add("@InvTotal", SqlDbType.Decimal).Value = model.nettotal;
                        Command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = model.discount;
                        Command.Parameters.Add("@SaleTaxAmt", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@NetTotal", SqlDbType.Decimal).Value = model.NetAmount;
                        Command.Parameters.Add("@Sno", SqlDbType.Int).Value = model.vid;
                        Command.Parameters.Add("@IType", SqlDbType.NVarChar).Value = "FSR";
                        Command.Parameters.Add("@BagType", SqlDbType.NVarChar).Value = "";
                        Command.Parameters.Add("@BagQty0", SqlDbType.Decimal).Value = 0;
                        //Command.Parameters.Add("@StoreID", SqlDbType.Int).Value = model.storeid;
                        Command.Parameters.Add("@StoreID", SqlDbType.Int).Value = 0;
                        //Command.Parameters.Add("@ContractID", SqlDbType.Int).Value = model.ContractID;
                        Command.Parameters.Add("@ContractID", SqlDbType.Int).Value = 0;
                        Command.Parameters.Add("@BrockerID", SqlDbType.NVarChar).Value = model.BrockerID;
                        Command.Parameters.Add("@BrockerageAccount", SqlDbType.NVarChar).Value = model.BrockerageAccount;
                        Command.Parameters.Add("@Brate", SqlDbType.Decimal).Value = model.BrockeyRate;
                        Command.Parameters.Add("@BrokeryMode", SqlDbType.NVarChar).Value = model.BrockeryMode;
                        Command.Parameters.Add("@CalcFactor", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@ArrivalType", SqlDbType.Int).Value = model.VehcileType;
                        Command.Parameters.Add("@ArrivalID", SqlDbType.Int).Value = model.VehcileType;
                        Command.Parameters.Add("@Freight", SqlDbType.Decimal).Value = model.freight;
                        Command.Parameters.Add("@BagRate", SqlDbType.Decimal).Value = model.BagRate;
                        Command.Parameters.Add("@BagAmount", SqlDbType.Decimal).Value = model.bagAmount;
                        Command.Parameters.Add("@BrokeryAmount", SqlDbType.Decimal).Value = brockeyAmount;
                        Command.Parameters.Add("@ActualBagQty", SqlDbType.Decimal).Value = model.totalquantity;
                        Command.Parameters.Add("@MarketComAccount", SqlDbType.NVarChar).Value = model.MarketComAccount;
                        Command.Parameters.Add("@MarketComAmount", SqlDbType.Decimal).Value = model.SutliAmount;
                        Command.Parameters.Add("@RateCalc", SqlDbType.NVarChar).Value = model.RateCalc;
                        Command.Parameters.Add("@MillType", SqlDbType.NVarChar).Value = 0;
                        Command.Parameters.Add("@FarmID", SqlDbType.Int).Value = model.FarmId;
                        Command.Parameters.Add("@So_No", SqlDbType.NVarChar).Value = model.SO_NO;
                        Command.Parameters.Add("@FreightAc", SqlDbType.NVarChar).Value = model.FreightAc;
                        Command.Parameters.Add("@BardanaAc", SqlDbType.NVarChar).Value = model.BardanaAc;
                        Command.Parameters.Add("@LabourAc", SqlDbType.NVarChar).Value = model.LabourAc;
                        Command.Parameters.Add("@LabourRate", SqlDbType.Decimal).Value = model.LabourRate;
                        Command.Parameters.Add("@LabourAmt", SqlDbType.Decimal).Value = model.labourAmount;
                        Command.Parameters.Add("@DiscPercent", SqlDbType.Decimal).Value = model.DiscPercent;
                        var labCalOnWt = 0;
                        if (model.LabCalOnWt == "True")
                        {
                            labCalOnWt = 1;
                        }
                        Command.Parameters.Add("@LabCalOnWt", SqlDbType.Bit).Value = labCalOnWt;
                        Command.Parameters.Add("@IsUpdate", SqlDbType.Bit).Value = IsUpdate;
                        Command.Parameters.Add("@DispatchNo", SqlDbType.Decimal).Value = model.DispatchNo;
                        Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                        Command.Dispose();
                        string vehNo = " ";
                        decimal discount = Convert.ToDecimal(model.discount);
                        string ProductSaleAcc = "";
                        string ProductDisAcc = "";
                        foreach (var dvchild in model.detail)
                        {
                            Command = new SqlCommand("Select strCol2 from Products where ProductID=" + Convert.ToInt32(dvchild.accCode) + "", con);
                            Command.CommandType = CommandType.Text;
                            Command.Transaction = Tran;
                            ProductSaleAcc = Convert.ToString(Command.ExecuteScalar());
                            Command.Dispose();
                            if (string.IsNullOrEmpty(ProductSaleAcc))
                            {
                                Tran.Rollback();
                                return Json(new { responseText = "Please Add Sale Account for the Product first" }, JsonRequestBehavior.AllowGet);
                            }
                            vehNo = vehNo + dvchild.VehNo + ",";
                            vNarattion = vNarattion + dvchild.title + " Qty: " + dvchild.box + "Weight :" + dvchild.piece + " Rate:" + dvchild.rate;
                            Command = new SqlCommand("[SPSW_SaveSaleInvoiceDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@InvNo", SqlDbType.NVarChar).Value = "FSR-" + model.vid;
                            Command.Parameters.Add("@ProductID", SqlDbType.Int).Value = dvchild.accCode;
                            Command.Parameters.Add("@GateNo", SqlDbType.NVarChar).Value = dvchild.gateNo;
                            Command.Parameters.Add("@VehNo", SqlDbType.NVarChar).Value = dvchild.VehNo;
                            Command.Parameters.Add("@BTypeID", SqlDbType.Int).Value = dvchild.BTypeID;
                            Command.Parameters.Add("@QtyCr", SqlDbType.Decimal).Value = 0;
                            Command.Parameters.Add("@QtyDr", SqlDbType.Decimal).Value = dvchild.box;
                            Command.Parameters.Add("@WtCr", SqlDbType.Decimal).Value = 0;
                            Command.Parameters.Add("@WtDr", SqlDbType.Decimal).Value = dvchild.piece;
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
                            string refer = "'FSR-" + model.vid + "'";
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
                            cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
                            cmd.Transaction = Tran;
                            DVId = cmd.ExecuteScalar().ToString();
                            string[] a = DVId.Split(',');
                            DVId1 = Convert.ToInt32(a[0]);
                            VoucherID = Convert.ToInt32(a[1]);
                            cmd.Dispose();
                        }
                        var Billnarration = "Sale Return Bill " + "FSR-" + model.vid + " Bilty #:" + model.yourinvoice + " WB #:" + model.DispatchNo + "DC #:" + model.ContractID + " SO #:" + model.SO_NO + "B Rate: " + model.BrockeyRate + "OGP: " + " VehNo: " + vehNo;
                        Command = new SqlCommand("[SPSW_SaveVoucher]", con);
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add("@VId", SqlDbType.Int).Value = DVId1;
                        Command.Parameters.Add("@VoucherID", SqlDbType.Int).Value = VoucherID;
                        Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
                        Command.Parameters.Add("@IsUpdate", SqlDbType.Bit).Value = IsUpdate;
                        Command.Parameters.Add("@LastUpdate", SqlDbType.DateTime).Value = DateTime.Now;
                        //Addition
                        Command.Parameters.Add("@User", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                        Command.Transaction = Tran;
                        VoucherID = Convert.ToInt32(Command.ExecuteScalar().ToString());
                        Command.Dispose();
                        foreach (var dvchild in model.detail)
                        {
                            Command = new SqlCommand("Select strCol2 from Products where ProductID=" + Convert.ToInt32(dvchild.accCode) + "", con);
                            Command.CommandType = CommandType.Text;
                            Command.Transaction = Tran;
                            ProductSaleAcc = Convert.ToString(Command.ExecuteScalar());
                            if (string.IsNullOrEmpty(ProductSaleAcc))
                            {
                                Tran.Rollback();
                                return Json(new { responseText = "Please Add Sale Account for the Product first" }, JsonRequestBehavior.AllowGet);
                            }
                            var ProductSaleAccTitle = (from s in db.Accounts where s.AccountId == ProductSaleAcc select s.Title).FirstOrDefault();
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.supid;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = InvoiceBill;
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FSR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
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
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = ProductSaleAcc;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = InvoiceBill + discount;
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = ProductSaleAccTitle;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FSR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                            Command = new SqlCommand("Select Isnull(strCol3,0) from Products where ProductID=" + Convert.ToInt32(dvchild.accCode) + "", con);
                            Command.CommandType = CommandType.Text;
                            Command.Transaction = Tran;
                            ProductDisAcc = Convert.ToString(Command.ExecuteScalar());
                            if (string.IsNullOrEmpty(ProductDisAcc))
                            {
                                Tran.Rollback();
                                return Json(new { responseText = "Please Add Discount Account for the Product first" }, JsonRequestBehavior.AllowGet);
                            }
                            var ProductDisAccTitle = (from s in db.Accounts where s.AccountId == ProductDisAcc select s.Title).FirstOrDefault();
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = ProductDisAcc;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = discount;
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Discount :" + Billnarration + vNarattion;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = ProductDisAccTitle;
                            Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = "FSR-" + model.vid;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "SJV";
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                        }
                        voucherno = Convert.ToString(VoucherID);
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
                return Json(new { success = true, responseText = "Saved",voucherno=voucherno  }, JsonRequestBehavior.AllowGet);
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
