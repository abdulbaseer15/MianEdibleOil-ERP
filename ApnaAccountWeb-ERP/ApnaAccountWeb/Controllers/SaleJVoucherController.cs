using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ApnaAccountWeb.Models;
using ApnaAccountWeb.ViewModels;
namespace ApnaAccountWeb.Controllers
{
    public class SaleJVoucherController : Controller
    {
        //
        // GET: /SaleJVoucher/
        SqlTransaction Tran;
        public ActionResult SaleJVoucher()
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
                var Ctrl = (from s in db.ControllAccounts select s.SaleCtrl).FirstOrDefault();
                if (Ctrl != null)
                {
                    var AccountsList = db.Database.SqlQuery<GroupAccounts>(
                "exec SPSW_GetCtrlGrpDetailAccounts @AccountId='" + Ctrl.ToString() + "'").ToList<GroupAccounts>();
                    ViewBag.AccCodelist = AccountsList;
                    // ViewBag.PurchaeAccountsList = db.Accounts.Where(x => x.AccType.Trim() == "Detail").Select(x => new DetailList { AccountId = x.AccountId, Title = x.Title }).ToList();
                    ViewBag.AccCodeDetails2 = db.Vouchers.Where(x => x.VType == "S-JV").Select(x => new DetailList { Code = x.VId, Pdate = x.VDate }).OrderBy(x => x.Code).ToList();
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        [HttpPost]
        public JsonResult GetVoucherID(string date)
        {
            string dbconsql = Convert.ToString(Session["consql"]);
            string voucherClosing = Convert.ToString(Session["Vouchertype"]);
            SqlConnection con = new SqlConnection(dbconsql);
            try
            {
                DateTime date1 = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                con.Open();
                SqlCommand cmd = new SqlCommand("[SPSW_GetVId]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                cmd.Parameters.Add("@VoucherClosing", SqlDbType.VarChar).Value = voucherClosing;
                cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "S-JV";
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
        [HttpGet]
        public JsonResult SearchVoucher(string Date, int VId)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                DateTime date = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                var lstDVChilds = db.VouchersDetails.Where(x => x.VId == VId && x.VType == "S-JV");
                return Json(lstDVChilds, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        SqlCommand Command;
        [HttpPost]
        public JsonResult SaveVoucher(VoucherViewModel model)
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
                    DateTime date1 = DateTime.ParseExact(model.date, "dd/MM/yyyy", null);
                    if (model.detail.Count > 0)
                    {
                        Int16 IsUpdate = Convert.ToInt16((model.isupdate == "False" ? 0 : 1));
                        //if (model.isupdate != "1")//new entry
                        //{
                        Random rnd = new Random();
                        int delay = rnd.Next(1, 50);
                        System.Threading.Thread.Sleep(delay);
                        con.Open();
                        Tran = con.BeginTransaction();
                        Command = new SqlCommand("[SPSW_SaveVoucher]", con);
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add("@VId", SqlDbType.Int).Value = model.vid;
                        Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "S-JV";
                        Command.Parameters.Add("@IsUpdate", SqlDbType.Bit).Value = IsUpdate;
                        Command.Parameters.Add("@LastUpdate", SqlDbType.DateTime).Value = DateTime.Now;
                        //Addition
                        Command.Parameters.Add("@User", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                        Command.Transaction = Tran;
                        //  model.vid = Command.ExecuteScalar().ToString();
                        //if (model.isupdate == "False")
                            model.VoucherId = Command.ExecuteScalar().ToString();
                        //else
                        //    Command.ExecuteNonQuery();
                        Command.Dispose();
                        // }
                        //else//update
                        //{
                        //    con.Open();
                        //    Tran = con.BeginTransaction();
                        //Command = new SqlCommand("delete from VouchersDetail where VID=" + model.vid + " and VType='S-JV'", con);
                        //Command.CommandType = CommandType.Text;
                        //Command.Transaction = Tran;
                        //Command.ExecuteNonQuery();
                        //Command.Dispose();
                        // }
                        foreach (var dvchild in model.detail)
                        {
                            Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = dvchild.accCode;
                            Command.Parameters.Add("@Debit", SqlDbType.Money).Value = dvchild.debit;
                            Command.Parameters.Add("@Credit", SqlDbType.Money).Value = dvchild.credit;
                            Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = dvchild.naration;
                            Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = dvchild.title;
                            //else 
                            //{
                            //    cmd.Parameters.Add("@JVId", SqlDbType.Int).Value = jvchild.JVId;
                            //}
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = Int32.Parse(model.VoucherId);
                            Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = dvchild.isctrlacc;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "S-JV";
                            //Addition
                            Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                            //end
                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                        }
                        Tran.Commit();
                    }
                    // return RedirectToAction("DVoucher", "DVoucher");
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
        public ActionResult DeleteVoucher(string Date, int DVId)
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
                SqlCommand cmd = new SqlCommand("[SPSW_DeleteVoucher]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@VId", SqlDbType.VarChar).Value = DVId;
                cmd.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "S-JV";
                    //Addition
                    cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return RedirectToAction("SaleJVoucher", "SaleJVoucher");
            }
            else
            {
                TempData["message"] = "You do not have permission to delete this.";
                return RedirectToAction("SaleJVoucher");
            }
        }
    }
}
