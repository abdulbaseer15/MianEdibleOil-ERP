using ApnaAccountWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApnaAccountWeb.ViewModels;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace ApnaAccountWeb.Controllers
{
    public class FOCVoucherController : Controller
    {
        string currentVoucherId = "";
        public ActionResult FOCVoucher()
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
                ViewBag.AccCodeDetails = db.Accounts.Where(x => x.AccType.Trim() == "Detail").Select(x => new DetailList { AccountId = x.AccountId, Title = x.Title }).ToList();
                ViewBag.AccCodeDetails2 = db.Vouchers.Where(x => x.VType == "FOCRV").Select(x => new DetailList { Code = x.VId, Pdate = x.VDate }).OrderBy(x => x.Code).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
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
                vouchid = (from s in db.Vouchers where s.VId == VId && s.VType == "FOCRV" && s.VDate == date select s.VoucherID).FirstOrDefault();
                var lstDVChilds = db.VouchersDetails.Where(x => x.VId == vouchid && x.VType == "FOCRV" && x.Credit > 0);
                return Json(lstDVChilds, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
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
                cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "FOCRV";
                cmd.Parameters.Add("@VoucherClosing", SqlDbType.VarChar).Value = voucherClosing;
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
        SqlTransaction Tran;
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
                            System.Threading.Thread.Sleep(delay);
                            con.Open();
                            Tran = con.BeginTransaction();
                            Command = new SqlCommand("[SPSW_SaveVoucher]", con);
                            Command.CommandType = CommandType.StoredProcedure;
                            Command.Parameters.Add("@VId", SqlDbType.Int).Value = model.vid;
                            Command.Parameters.Add("@VoucherID", SqlDbType.Int).Value = model.VoucherId;
                            Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                            Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "FOCRV";
                            Command.Parameters.Add("@IsUpdate", SqlDbType.Bit).Value = IsUpdate;
                            Command.Parameters.Add("@LastUpdate", SqlDbType.DateTime).Value = DateTime.Now;
                            //Addition
                            Command.Parameters.Add("@User", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                            Command.Transaction = Tran;
                            // model.vid = Command.ExecuteScalar().ToString();
                            //if (model.isupdate == "False")
                            model.VoucherId = Command.ExecuteScalar().ToString();
                            //else
                            //Command.ExecuteNonQuery();
                            Command.Dispose();
                            //}
                            //else//update
                            //{
                            //con.Open();
                            //Tran = con.BeginTransaction();
                            //Command = new SqlCommand("delete from VouchersDetail where VID=" + model.vid + " and VType='CV'", con);
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
                                Command.Parameters.Add("@Credit", SqlDbType.Money).Value = dvchild.debit;
                                Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = dvchild.naration;
                                Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = dvchild.title;
                                Command.Parameters.Add("@VId", SqlDbType.Int).Value = model.VoucherId;
                                Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                                Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                                Command.Parameters.Add("@Mode", SqlDbType.VarChar).Value = model.CashType;
                                Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "FOCRV";
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
                string username = Convert.ToString(Session["username"]);
                SqlConnection con = new SqlConnection(dbconsql);
                try
                {
                    DateTime date1 = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("[SPSW_DeleteVoucher]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VId", SqlDbType.VarChar).Value = DVId;
                    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                    cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "FOCRV";
                    //Addition
                    cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = username;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    TempData["message"] = "Successfully deleted";
                }
                catch (Exception ex)
                {
                    TempData["message"] = ex.Message;
                }
                finally
                {
                    con.Close();
                }
                return RedirectToAction("FOCVoucher", "FOCVoucher");
            }
            else
            {
                TempData["message"] = "You do not have permission to delete this.";
                return RedirectToAction("FOCVoucher");
            }
        }
    }
}
