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
    public class InwardGatePassMixController : Controller
    {
        public ActionResult InwardGatePassMix()
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
                    ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName, rate = x.PurchasePrice??0 }).ToList();
                    var AccountsListbardana = from s in db.BardanaTypes                                       
                                              select s;
                    ViewBag.AccountsListbardana = AccountsListbardana.ToList();
                    var AccountsListbrocker = from s in db.Accounts
                                              where s.AccType == "Detail" && s.IsParty==true
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
                    //var AccCodelistPO = from s in db.PurchaseOrder1
                    //                    orderby s.Sno
                    //                    select s.InvNo;
                    //ViewBag.AccCodelistPO = AccCodelistPO.ToList(); 
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        [HttpGet]
        public JsonResult SearchPurchaseOrder2(string AccountId,string po)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                var lstDVChilds = db.Database.SqlQuery<POModel>("SELECT PO_No, ProductID, ItemDetail, SUM(QtyDr - QtyCr) AS QtyDr, SUM(WtDr - WtCr) AS WtDr,Rate From dbo.qryPurOrderStatus where  AccountID='" + AccountId + "' and PO_No='" + po + "' GROUP BY PO_No,ProductID, ItemDetail,Rate Having (Sum(WtDr - WtCr) > 0)").ToList();
                return Json(lstDVChilds.ToList(), JsonRequestBehavior.AllowGet);
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
                var lstDVChilds = db.Database.SqlQuery<qryPurOrderStatu>("select * from qryPurOrderStatus where AccountId='" + AccountId + "' and QtyDr>0").ToList();
                //ViewBag.AccCodeDetails = sd;
                //var lstDVChilds = (from t1 in db.qryPurOrderStatus
                //                   where t1.AccountId == AccountId
                //                   select t1).ToList();
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
        [HttpGet]
        public JsonResult SearchInvoice(string Date, int VId)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            string invnnn = "GPM-" + VId;
            try
            {
                DateTime date = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                var lstDVChilds = (from t1 in db.Gates
                                   join t2 in db.Gate3 on t1.GateNo equals t2.GateNo                                  
                                   where t1.GateNo == invnnn
                                   select new { t1.BuiltyNo, t1.KatlaNo,t1.VehReference,t1.IntCol1,t1.CID,t1.Transportation,t1.Through,t1.Weight1,t1.Weight2,t2.ProductID,t2.ProductName,t2.Bardana,t2.BardanaType,t2.Quantity,t2.CalcWeight,t2.StdWeight }).ToList();
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
                    DateTime date1 = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                    con.Open();
                    Tran = con.BeginTransaction();
                    SqlCommand cmd = new SqlCommand("[SPSW_DeleteGate]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VId", SqlDbType.VarChar).Value ="GPM-"+ DVId;
                    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                    cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "GPM";
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
                return RedirectToAction("InwardGatePassMix", "InwardGatePassMix");
            }
            else
            {
                TempData["message"] = "You do not have permission to delete this.";
                return RedirectToAction("InwardGatePassMix");
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
                cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "IGPMM";
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
                    //To Assign new Master ID if duplicated
                    if (model.isupdate == "False")
                    {
                        string dbcon = Convert.ToString(Session["con"]);
                        DbConection obj = new DbConection();
                        string condb = obj.Connect(dbcon);
                        ApnaAccountEntities db = new ApnaAccountEntities(condb);
                        string dbconsql = Convert.ToString(Session["consql"]);
                        SqlConnection con = new SqlConnection(dbconsql);
                        int model_Vid = Convert.ToInt32(model.vid);
                        var ifExist = (from s in db.Gates where s.Sno == model_Vid select s.Sno).FirstOrDefault();
                        if (!string.IsNullOrEmpty(Convert.ToString(ifExist)))
                        {
                            decimal temp = db.Gates.Where(x => x.Itype == "INM" && x.IType2 == "GPM").OrderByDescending(x => x.Sno).FirstOrDefault().Sno.Value + 1;
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
                        string dbcon = Convert.ToString(Session["con"]);
                        DbConection obj = new DbConection();
                        string condb = obj.Connect(dbcon);
                        ApnaAccountEntities db = new ApnaAccountEntities(condb);
                        string dbconsql = Convert.ToString(Session["consql"]);
                        SqlConnection con = new SqlConnection(dbconsql);
                        try
                        {
                            DateTime date1 = DateTime.ParseExact(model.comdate, "dd/MM/yyyy", null);
                            string vNarattion = "Product :";
                            if (model.detail.Count > 0)
                            {
                                Int16 IsUpdate = Convert.ToInt16((model.isupdate == "False" ? 0 : 1));
                                con.Open();
                                Tran = con.BeginTransaction();
                                Command = new SqlCommand("[SPSW_Save_GateM]", con);
                                Command.CommandType = CommandType.StoredProcedure;
                                Command.Parameters.Add("@VId", SqlDbType.BigInt).Value = model.vid;
                                Command.Parameters.Add("@InvNo", SqlDbType.NVarChar).Value = "GPM-" + model.vid;
                                Command.Parameters.Add("@Idate", SqlDbType.Date).Value = date1;
                                Command.Parameters.Add("@PartyBillNO", SqlDbType.NVarChar).Value = model.wbno;
                                Command.Parameters.Add("@PartyBillDate", SqlDbType.Date).Value = System.DateTime.Now;
                                Command.Parameters.Add("@AccountId", SqlDbType.NVarChar).Value = model.supid;
                                Command.Parameters.Add("@PurAccount", SqlDbType.NVarChar).Value = model.supname;
                                Command.Parameters.Add("@InvTotal", SqlDbType.Decimal).Value = model.nettotal;
                                Command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = model.discount;
                                Command.Parameters.Add("@SaleTaxAmt", SqlDbType.Decimal).Value = model.gst;
                                Command.Parameters.Add("@NetTotal", SqlDbType.Decimal).Value = model.nettotal;
                                Command.Parameters.Add("@Sno", SqlDbType.Int).Value = model.vid;
                                Command.Parameters.Add("@IType", SqlDbType.NVarChar).Value = "";
                                Command.Parameters.Add("@BagType", SqlDbType.NVarChar).Value = model.po;
                                Command.Parameters.Add("@BagQty0", SqlDbType.Decimal).Value = 0;
                                Command.Parameters.Add("@StoreID", SqlDbType.NVarChar).Value = model.store;
                                Command.Parameters.Add("@ContractID", SqlDbType.Int).Value = model.grnno;
                                Command.Parameters.Add("@BrockerID", SqlDbType.NVarChar).Value = model.Milw;
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
                                Command.Parameters.Add("@BagAmount", SqlDbType.Decimal).Value = model.totalquantity;
                                Command.Parameters.Add("@BrokeryAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(model.totalquantity) * Convert.ToDecimal(model.brorate);
                                Command.Parameters.Add("@ActualBagQty", SqlDbType.Decimal).Value = model.Bags;
                                Command.Parameters.Add("@MarketComAccount", SqlDbType.NVarChar).Value = model.sutid;
                                Command.Parameters.Add("@MarketComAmount", SqlDbType.Decimal).Value = model.SutliAmount;
                                Command.Parameters.Add("@FreightAc", SqlDbType.NVarChar).Value = model.narration;
                                Command.Parameters.Add("@BardanaAc", SqlDbType.NVarChar).Value = model.barid;
                                Command.Parameters.Add("@LabourAc", SqlDbType.NVarChar).Value = Convert.ToString(Session["username"]);
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
                                    Command = new SqlCommand("[SPSW_Save_Gate_DetailM]", con);
                                    Command.CommandType = CommandType.StoredProcedure;
                                    Command.Parameters.Add("@InvNo", SqlDbType.NVarChar).Value = "GPM-" + model.vid;
                                    Command.Parameters.Add("@ProductID", SqlDbType.Int).Value = dvchild.accCode;
                                    Command.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = dvchild.title;
                                    Command.Parameters.Add("@GateNo", SqlDbType.NVarChar).Value = dvchild.gateNo;
                                    Command.Parameters.Add("@VehNo", SqlDbType.NVarChar).Value = dvchild.VehNo;
                                    Command.Parameters.Add("@BTypeID", SqlDbType.Int).Value = 0;
                                    Command.Parameters.Add("@QtyDr", SqlDbType.Decimal).Value = dvchild.quantity;
                                    Command.Parameters.Add("@QtyCr", SqlDbType.Decimal).Value = 0;
                                    Command.Parameters.Add("@WtDr", SqlDbType.Decimal).Value = dvchild.weight;
                                    Command.Parameters.Add("@WtCr", SqlDbType.Decimal).Value = 0;
                                    Command.Parameters.Add("@MillWeight", SqlDbType.Decimal).Value = dvchild.millweight;
                                    Command.Parameters.Add("@SupWeight", SqlDbType.Decimal).Value = dvchild.supweight;
                                    Command.Parameters.Add("@QCut", SqlDbType.Decimal).Value = 0;
                                    Command.Parameters.Add("@BrdCut", SqlDbType.Decimal).Value = 0;
                                    Command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = 0;
                                    Command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = 0;
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
