using ApnaAccountWeb.Models;
using ApnaAccountWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
namespace ApnaAccountWeb.Controllers
{
    public class BankCJVoucherController : Controller
    {
        //
        // GET: /BankCJVoucher/
        SqlTransaction Tran;
        string currentVoucherId = "";
        public ActionResult BankCJVoucher()
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
                //var PurCtrl = (from s in db.ControllAccounts select s.BankCtrl).FirstOrDefault();
                //if (PurCtrl != null)
                //{
                //    var AccountsList = db.Database.SqlQuery<GroupAccounts>(
                //"exec SPSW_GetCtrlGrpDetailAccounts @AccountId='" + PurCtrl.ToString() + "'").ToList<GroupAccounts>();
                //    ViewBag.AccCodelist = AccountsList;

                //Commented for IsParty
                //var AccountsList = from s in db.Accounts
                //                   where s.AccType == "Detail" && s.IsParty == true
                //                   orderby s.Title
                //                   select s;
                //ViewBag.AccCodelist = AccountsList;

                var AccountsList = from s in db.Accounts
                                   where s.AccType == "Detail"
                                   orderby s.Title
                                   select s;
                ViewBag.AccCodelist = AccountsList;
                // ViewBag.PurchaeAccountsList = db.Accounts.Where(x => x.AccType.Trim() == "Detail").Select(x => new DetailList { AccountId = x.AccountId, Title = x.Title }).ToList();
                ViewBag.AccCodeDetails2 = db.Vouchers.Where(x => x.VType == "BRV").Select(x => new DetailList { Code = x.VId, Pdate = x.VDate }).OrderBy(x => x.Code).ToList();
                //}
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
                cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "BRV";
                string DVId = cmd.ExecuteScalar().ToString();
                string[] a = DVId.Split(',');
                DVId = a[0];
                currentVoucherId = a[1];
                cmd.Dispose();
                if (DVId == null || DVId == "")
                {
                    return Json(new { success = true, responseText = "1" }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { success = true, responseText = DVId, responseVI = currentVoucherId }, JsonRequestBehavior.AllowGet);
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
                var vouchid = 0;
                DateTime date = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                vouchid = (from s in db.Vouchers where s.VId == VId && s.VType == "BRV" && s.VDate == date select s.VoucherID).FirstOrDefault();
                var lstDVChilds = db.VouchersDetails.Where(x => x.VId == vouchid && x.VType == "BRV");
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
            lock (this)
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
                            Thread.Sleep(delay);
                            con.Open();
                            Tran = con.BeginTransaction();
                            Command = new SqlCommand("[SPSW_SaveVoucher]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@VoucherID", SqlDbType.Int).Value = model.VoucherId;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "BRV";
                            Command.Parameters.Add("@IsUpdate", SqlDbType.Bit).Value = IsUpdate;
                            Command.Parameters.Add("@LastUpdate", SqlDbType.DateTime).Value = DateTime.Now;
                            //Addition
                            Command.Parameters.Add("@User", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Transaction = Tran;
                            // model.vid = Command.ExecuteScalar().ToString();
                            //if (model.isupdate == "False")
                            model.VoucherId = Command.ExecuteScalar().ToString();
                            //else
                            //    Command.ExecuteNonQuery();
                            Command.Dispose();
                            //  }
                            //else//update
                            //{
                            //con.Open();
                            //Tran = con.BeginTransaction();
                            //Command = new SqlCommand("delete from VouchersDetail where VID=" + model.vid + " and VType='BC-JV'", con);
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
                                Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                                Command.Parameters.Add("@VId", SqlDbType.Int).Value = model.VoucherId;
                                Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                                Command.Parameters.Add("@Mode", SqlDbType.VarChar).Value = dvchild.mode;
                                Command.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = dvchild.refno;
                                Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = dvchild.isctrlacc;
                                Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "BRV";
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
                    cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "BRV";
                    //Addition
                    string user = Convert.ToString(Session["username"]);
                    cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = user;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    TempData["message"] = "Voucher deleted successfully";
                }
                catch (Exception ex)
                {
                    TempData["message"] = ex.Message;
                }
                finally
                {
                    con.Close();
                }
                return RedirectToAction("BankCJVoucher", "BankCJVoucher");
            }
            else
            {
                TempData["message"] = "You do not have permission to delete this.";
                return RedirectToAction("BankCJVoucher");
            }
        }
    }
}
