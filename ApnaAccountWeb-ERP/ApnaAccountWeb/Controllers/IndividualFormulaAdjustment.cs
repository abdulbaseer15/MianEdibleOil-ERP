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
    public class IndividualFormulaAdjustmentController : Controller
    {
        public ActionResult IndividualFormulaAdjustment()
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
                    ViewBag.AccCodelist = (from t1 in db.Products
                                   join t2 in db.ProductGroups on t1.PG_ID equals t2.PG_Id
                                           join t3 in db.ProductSubGroups on t1.PT_ID equals t3.PSG_Id
                                   where t1.PG_ID ==1
                                     select new DetailList { Code = t1.ProductID, Title=t1.ProductName, Type = t3.Title, Group = t2.Title }).ToList();
                    ViewBag.AccCodelistbard = (from t1 in db.Products
                                           join t2 in db.ProductGroups on t1.PG_ID equals t2.PG_Id
                                           join t3 in db.ProductSubGroups on t1.PT_ID equals t3.PSG_Id
                                           where t1.PG_ID == 13
                                           select new DetailList { Code = t1.ProductID, Title = t1.ProductName, Type = t3.Title, Group = t2.Title }).ToList();
                    ViewBag.AccCodelistPro = (from t1 in db.Products
                                               join t2 in db.ProductGroups on t1.PG_ID equals t2.PG_Id
                                              join t3 in db.ProductSubGroups on t1.PT_ID equals t3.PSG_Id
                                               where t1.PG_ID != 1
                                               select new DetailList { Code = t1.ProductID, Title = t1.ProductName, Type = t3.Title, Group = t2.Title }).ToList();
               var AccCodelistPO = from s in db.Process4
                                    orderby s.Sno
                                    select new DetailList { AccountId = s.InvNo, Code = s.Sno??0};
               ViewBag.AccCodelistPO = AccCodelistPO.ToList(); 
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
            string invnnn = "IFA-" + VId;
            try
            {
                DateTime date = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                var lstDVChilds = (from t1 in db.Process4
                                   join t2 in db.Process5 on t1.InvNo equals t2.InvNo
                                   join t3 in db.Products on t2.ProductID equals t3.ProductID
                                   where t1.InvNo == invnnn && t2.wtDr>0
                                   select new { t1.InvNo, t1.Idate, t1.YourBillNo, t1.YourBillDate, t1.InvTotal, t1.ReceiveQty, t1.PlantID, t1.IType, t1.Weight2, t1.NumCol1, t1.IntCol1, t2.ProductID, t3.ProductName, t2.QtyDr, t2.QtyCr, t2.wtDr,t2.WtCr, rate=t2.NumCol1, t2.NumCol2 }).ToList();
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
                    SqlCommand cmd = new SqlCommand("[SPSW_DeleteInvoiceFG]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VId", SqlDbType.VarChar).Value ="IFA-"+ DVId;
                    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                    cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "IFA";
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
                return RedirectToAction("IndividualFormulaAdjustment", "IndividualFormulaAdjustment");
            }
            else
            {
                TempData["message"] = "You do not have permission to delete this.";
                return RedirectToAction("IndividualFormulaAdjustment");
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
                cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "IFA";
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
                    if (model.detail.Count > 0)
                    {
                        Int16 IsUpdate = Convert.ToInt16((model.isupdate == "False" ? 0 : 1));
                        con.Open();
                        Tran = con.BeginTransaction();
                        Command = new SqlCommand("[SPSW_SaveInvoiceFG]", con);
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add("@VId", SqlDbType.BigInt).Value =model.vid;
                        Command.Parameters.Add("@InvNo", SqlDbType.NVarChar).Value = "IFA-" + model.vid;
                        Command.Parameters.Add("@Idate", SqlDbType.Date).Value = date1;
                        Command.Parameters.Add("@PartyBillNO", SqlDbType.NVarChar).Value = model.yourinvoice;
                        Command.Parameters.Add("@PartyBillDate", SqlDbType.Date).Value = date2;
                        Command.Parameters.Add("@AccountId", SqlDbType.NVarChar).Value =model.supid;
                        Command.Parameters.Add("@PurAccount", SqlDbType.NVarChar).Value = model.supname;
                        Command.Parameters.Add("@InvTotal", SqlDbType.Decimal).Value = model.totalquantity;
                        Command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@SaleTaxAmt", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@NetTotal", SqlDbType.Decimal).Value = model.nettotal;
                        Command.Parameters.Add("@Sno", SqlDbType.Int).Value = model.vid;
                        Command.Parameters.Add("@IType", SqlDbType.NVarChar).Value = "IFA";
                        Command.Parameters.Add("@BagType", SqlDbType.NVarChar).Value = "";
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
                        decimal discount=Convert.ToDecimal(model.discount);
                        foreach (var dvchild in model.detail)
                        {
                            Command = new SqlCommand("[SPSW_SaveInvoiceDetailFG]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@InvNo", SqlDbType.NVarChar).Value = "IFA-"+model.vid;
                            Command.Parameters.Add("@ProductID", SqlDbType.Int).Value = dvchild.accCode;
                            Command.Parameters.Add("@GateNo", SqlDbType.NVarChar).Value = dvchild.title;
                            Command.Parameters.Add("@VehNo", SqlDbType.NVarChar).Value = 0;
                            Command.Parameters.Add("@BTypeID", SqlDbType.Int).Value = 0;
                            Command.Parameters.Add("@QtyDr", SqlDbType.Decimal).Value = 0;
                            Command.Parameters.Add("@QtyCr", SqlDbType.Decimal).Value = 0;
                            Command.Parameters.Add("@WtDr", SqlDbType.Decimal).Value = dvchild.piece;
                            Command.Parameters.Add("@WtCr", SqlDbType.Decimal).Value = 0;
                            Command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = dvchild.rate;
                            Command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = dvchild.amount;
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                        }
                        Command = new SqlCommand("[SPSW_SaveInvoiceDetailFG]", con);
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add("@InvNo", SqlDbType.NVarChar).Value = "IFA-" + model.vid;
                        Command.Parameters.Add("@ProductID", SqlDbType.Int).Value = 1;
                        Command.Parameters.Add("@GateNo", SqlDbType.NVarChar).Value = 0;
                        Command.Parameters.Add("@VehNo", SqlDbType.NVarChar).Value = 0;
                        Command.Parameters.Add("@BTypeID", SqlDbType.Int).Value = 0;
                        Command.Parameters.Add("@QtyDr", SqlDbType.Decimal).Value = model.store;
                        Command.Parameters.Add("@QtyCr", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@WtDr", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@WtCr", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = 0;
                        Command.Transaction = Tran;
                        Command.ExecuteNonQuery();
                        Command.Dispose();
                        Command = new SqlCommand("[SPSW_SaveInvoiceDetailFG]", con);
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add("@InvNo", SqlDbType.NVarChar).Value = "IFA-" + model.vid;
                        Command.Parameters.Add("@ProductID", SqlDbType.Int).Value = 20;
                        Command.Parameters.Add("@GateNo", SqlDbType.NVarChar).Value = 0;
                        Command.Parameters.Add("@VehNo", SqlDbType.NVarChar).Value = 0;
                        Command.Parameters.Add("@BTypeID", SqlDbType.Int).Value = 0;
                        Command.Parameters.Add("@QtyDr", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@QtyCr", SqlDbType.Decimal).Value = model.store;
                        Command.Parameters.Add("@WtDr", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@WtCr", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = 0;
                        Command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = 0;
                        Command.Transaction = Tran;
                        Command.ExecuteNonQuery();
                        Command.Dispose();
                        string Qry = @" exec SP_NVNo @Dated='" + date1 + "',@VchType='HJV',@snotype='M'";
                        if (model.isupdate == "True")
                        {
                            Qry = "Select VoucherId as vid,VoucherNo as vno from Vouchers1 where Reference='IFA-" + model.vid + "'";
                        }
                        SqlDataAdapter da = new SqlDataAdapter(Qry, dbconsql);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        var results = from myRow in dt.AsEnumerable()
                                      select myRow;
                        string VoucherNo = "";
                        int VoucherID = 0;
                        foreach (var dr in results)
                        {
                            VoucherNo = dr["vno"].ToString();
                            VoucherID = Convert.ToInt32(dr["vid"].ToString());
                        }
                        Command = new SqlCommand("[SPSW_SaveVoucherVB]", con);
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        Command.Parameters.Add("@VchNo", SqlDbType.VarChar).Value = VoucherNo;
                        Command.Parameters.Add("@vDate", SqlDbType.Date).Value = date1;
                        Command.Parameters.Add("@isPending", SqlDbType.VarChar).Value = 0;
                        Command.Parameters.Add("@IsUpdate", SqlDbType.Bit).Value = IsUpdate;
                        Command.Parameters.Add("@Reference", SqlDbType.VarChar).Value = "IFA-" + model.vid;
                        Command.Transaction = Tran;
                        Command.ExecuteNonQuery();
                        Command.Dispose();
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = "411";
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = model.nettotal;
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value =0;
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Stock Consumed On" + "IFA-" + model.vid + " Batch# " + model.store;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = "Local Sale";
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "HJV";
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
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value ="12431";
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value =0 ;
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = model.nettotal;
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = "Stock Consumed On" + "IFA-" + model.vid + " Batch# " + model.store ;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = "Unknown";
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "HJV";
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
                return Json(new { success = true, responseText = "Saved" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
