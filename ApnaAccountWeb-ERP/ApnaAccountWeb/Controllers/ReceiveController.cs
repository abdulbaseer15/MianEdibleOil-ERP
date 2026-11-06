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
    public class ReceiveController : Controller
    {
        public ActionResult ReceiveInvoice()
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
                var Ctrl = (from s in db.ControllAccounts select s).FirstOrDefault();
                if (Ctrl != null)
                {
                    //     var AccountsList = db.Database.SqlQuery<GroupAccounts>(
                    //"exec SPSW_GetCtrlGrpspDetailAccounts @AccountId='" + Ctrl.PurchaseCtrl.ToString() + "'").ToList<GroupAccounts>();

                    var AccountsList = from s in db.Accounts1
                                       where s.ParentID.StartsWith("21") || s.ParentID.StartsWith("22") && s.Type == "Detail"
                                       select s;

                    ViewBag.AccCodelist = AccountsList;

                    var purchaseAccountsList = from s in db.Accounts1
                                               where s.ParentID.StartsWith("13") && s.Type == "Detail"
                                               select s;
                    ViewBag.PurAccCodelist = purchaseAccountsList;

                    //  ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).Where(x=>x).ToList();

                    ViewBag.AccCodeDetails = db.Products.Where(x => x.PG_ID == 23).Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
                    ViewBag.PlantCoding = db.Plants.Select(x => new DetailList { Code = x.PlantID, Title = x.Title }).ToList();
                    ViewBag.StoreDetails = db.Stores.Select(x => new DetailList { Code = x.StoreID, Title = x.Title }).ToList();
                }
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
                var lstDVChilds = db.Products.Where(x => x.ProductID == id).FirstOrDefault();
                var TempUnit = db.ProductUnits.Where(x => x.PU_Id == lstDVChilds.BagTypeId).FirstOrDefault();
                string unit = "None";
                if (unit == null)
                    unit = "No Unit Selected";
                else
                    unit = TempUnit.Title;
                return Json(new { responseText = lstDVChilds.OpeningWt, unit = unit }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult SearchInvoice(int VId)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                // DateTime date = DateTime.ParseExact(Date, "dd/MM/yyyy", null);

                var lstDVChilds = db.ReceiveMasterProductions.Where(x => x.ReceiveID == VId);
                var lstDVChilds1 = db.ReceiveDetailProductions.Where(x => x.ReceiveID == VId);
                // return Json({lstDVChilds : lstDVChilds, lstDVChilds1 : lstDVChilds1}, JsonRequestBehavior.AllowGet);
                return Json(new { lstDVChilds = lstDVChilds, lstDVChilds1 = lstDVChilds1 }, JsonRequestBehavior.AllowGet);


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
                    SqlCommand cmd = new SqlCommand("[SPSW_DeleteReceiveInvoice_Std]", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReceiveID", SqlDbType.VarChar).Value = DVId;
                    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                    cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "RFP";
                    cmd.Transaction = Tran;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    //string dbcon = Convert.ToString(Session["con"]);
                    //DbConection obj = new DbConection();
                    //string condb = obj.Connect(dbcon);
                    //ApnaAccountEntities db = new ApnaAccountEntities(condb);
                    //string refer = "'PI-" + DVId + "'";
                    //var sd = db.Database.SqlQuery<int>("select TOP 1 VoucherID from Vouchers1 where Reference=" + refer + "").ToList();
                    //int pid = sd.FirstOrDefault();

                    //SqlCommand cmd2 = new SqlCommand("[SPSW_DeleteVoucher]", con);

                    //cmd2.CommandType = CommandType.StoredProcedure;
                    //cmd2.Parameters.Add("@VId", SqlDbType.VarChar).Value = pid;
                    //cmd2.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                    //cmd2.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                    //cmd2.Transaction = Tran;
                    //cmd2.ExecuteNonQuery();
                    //cmd2.Dispose();

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
                return RedirectToAction("ReceiveInvoice", "Receive");
            }
            else
            {
                TempData["message"] = "You do not have permission to delete this.";
                return RedirectToAction("Receive");

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
                cmd.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "RFP";  // Receive From Production
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
            string ItemNarration = "";
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
                    DateTime date2 = DateTime.ParseExact(model.yourdate, "dd/MM/yyyy", null);


                    if (model.detail.Count > 0)
                    {
                        Int16 IsUpdate = Convert.ToInt16((model.isupdate == "False" ? 0 : 1));

                        con.Open();
                        Tran = con.BeginTransaction();
                        Command = new SqlCommand("[SPSW_SaveReceiveInvoice_Std]", con);

                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add("@ReceiveID", SqlDbType.Int).Value = model.vid;
                        Command.Parameters.Add("@ReceiveDate", SqlDbType.Date).Value = date1;
                        Command.Parameters.Add("@RefDate", SqlDbType.Date).Value = date2;
                        Command.Parameters.Add("@RefNo", SqlDbType.NVarChar).Value = model.yourinvoice;
                        Command.Parameters.Add("@StoreID", SqlDbType.Int).Value = model.storeid;
                        Command.Parameters.Add("@Batch", SqlDbType.NVarChar).Value = model.Batch;
                        Command.Parameters.Add("@PlantId", SqlDbType.Int).Value = model.Plant;
                        Command.Parameters.Add("@InsertedBy", SqlDbType.NVarChar).Value = Session["username"].ToString(); ;
                        Command.Parameters.Add("@InsetedTime", SqlDbType.DateTime).Value = DateTime.Now;
                        Command.Parameters.Add("@NetQty", SqlDbType.Decimal).Value = model.totalquantity;
                        Command.Parameters.Add("@NetWeight", SqlDbType.Decimal).Value = model.TotalWeight;
                        Command.Parameters.Add("@IsUpdate", SqlDbType.Bit).Value = IsUpdate;
                        Command.Transaction = Tran;

                        if (model.isupdate == "False")
                            model.vid = Command.ExecuteScalar().ToString();
                        else
                            Command.ExecuteNonQuery();

                        Command.Dispose();

                        decimal count = Convert.ToDecimal(model.totalquantity);
                        //  decimal discount = Convert.ToDecimal(model.discount) / count;
                        foreach (var dvchild in model.detail)
                        {

                            Command = new SqlCommand("[SPSW_SaveReceiveDetail_Std]", con);

                            Command.CommandType = CommandType.StoredProcedure;

                            Command.Parameters.Add("@ReceiveID", SqlDbType.Int).Value = Int32.Parse(model.vid);
                            Command.Parameters.Add("@ProductID", SqlDbType.Int).Value = Int32.Parse(dvchild.accCode);
                            Command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = dvchild.title;
                            Command.Parameters.Add("@qty", SqlDbType.Decimal).Value = dvchild.debit;
                            Command.Parameters.Add("@Weight", SqlDbType.Decimal).Value = dvchild.weight;
                            Command.Parameters.Add("@Itype", SqlDbType.VarChar).Value = "RFP";

                            Command.Transaction = Tran;
                            Command.ExecuteNonQuery();
                            Command.Dispose();
                            //ItemNarration += "Item: " + dvchild.title + " Rate:" + dvchild.credit + " Qty:" + dvchild.debit;
                        }

                        //model.isupdate = "False";
                        //IsUpdate = 0;

                        //if (model.supid != "211")
                        //{


                        //    string Qry = @" exec SP_NVNo @Dated='" + date1 + "',@VchType='PJV',@snotype='S'";

                        //    if (model.isupdate == "True")
                        //    {
                        //        Qry = "Select VoucherId as vid,VoucherNo as vno from Vouchers1 where Reference='PI-" + model.vid + "'";

                        //    }

                        //    SqlDataAdapter da = new SqlDataAdapter(Qry, dbconsql);
                        //    DataTable dt = new DataTable();
                        //    da.Fill(dt);

                        //    var results = from myRow in dt.AsEnumerable()

                        //                  select myRow;

                        //    string VoucherNo = "";
                        //    int VoucherID = 0;

                        //    foreach (var dr in results)
                        //    {
                        //        VoucherNo = dr["vno"].ToString();
                        //        VoucherID = Convert.ToInt32(dr["vid"].ToString());

                        //    }
                        //    var Billnarration = "Purchase bill made vide PI-" + model.vid + "  " + ItemNarration;




                        //    Command = new SqlCommand("[SPSW_SaveVoucherVB]", con);

                        //    Command.CommandType = CommandType.StoredProcedure;
                        //    Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //    Command.Parameters.Add("@VchNo", SqlDbType.VarChar).Value = VoucherNo;
                        //    Command.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = "PJV";
                        //    Command.Parameters.Add("@vDate", SqlDbType.Date).Value = date1;
                        //    Command.Parameters.Add("@isPending", SqlDbType.VarChar).Value = 0;
                        //    Command.Parameters.Add("@IsUpdate", SqlDbType.Bit).Value = IsUpdate;
                        //    Command.Parameters.Add("@Reference", SqlDbType.VarChar).Value = "PI-" + model.vid;
                        //    // Adding Voucher Logs
                        //    string user = Convert.ToString(Session["username"]);
                        //    if (IsUpdate == 1)
                        //    {
                        //        Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                        //        Command.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                        //    }
                        //    else
                        //    {
                        //        Command.Parameters.Add("@InsertedBy", SqlDbType.NVarChar).Value = Convert.ToString(Session["username"]);
                        //    }

                        //    Command.Transaction = Tran;
                        //    if (model.isupdate == "False")
                        //        VoucherID = Convert.ToInt32(Command.ExecuteScalar());
                        //    else
                        //        Command.ExecuteNonQuery();

                        //    Command.Dispose();


                        //    Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);

                        //    Command.CommandType = CommandType.StoredProcedure;
                        //    Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //    Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        //    Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.supid;

                        //    Command.Parameters.Add("@Debit", SqlDbType.Money).Value = 0;
                        //    Command.Parameters.Add("@Credit", SqlDbType.Money).Value = model.nettotal;
                        //    Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration;
                        //    Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.supname;

                        //    Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                        //    Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                        //    //Addition
                        //    Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                        //    Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                        //    Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                        //    //end
                        //    Command.Transaction = Tran;
                        //    Command.ExecuteNonQuery();
                        //    Command.Dispose();

                        //    Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);

                        //    Command.CommandType = CommandType.StoredProcedure;
                        //    Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //    Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        //    Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.purid;

                        //    Command.Parameters.Add("@Debit", SqlDbType.Money).Value = model.nettotal;
                        //    Command.Parameters.Add("@Credit", SqlDbType.Money).Value = 0;
                        //    Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration;
                        //    Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.purname;

                        //    Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "PJV";
                        //    Command.Parameters.Add("@IsCtrlAcc", SqlDbType.VarChar).Value = 0;
                        //    //Addition
                        //    Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                        //    Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                        //    Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                        //    //end
                        //    Command.Transaction = Tran;
                        //    Command.ExecuteNonQuery();
                        //    Command.Dispose();



                        //}

                        //else
                        //{
                        //    string Qry = @" exec SP_NVNo @Dated='" + date1 + "',@VchType='CPV',@snotype='S'";

                        //    if (model.isupdate == "True")
                        //    {
                        //        Qry = "Select VoucherId as vid,VoucherNo as vno from Vouchers1 where Reference='PI-" + model.vid + "'";

                        //    }

                        //    SqlDataAdapter da = new SqlDataAdapter(Qry, dbconsql);
                        //    DataTable dt = new DataTable();
                        //    da.Fill(dt);

                        //    var results = from myRow in dt.AsEnumerable()

                        //                  select myRow;

                        //    string VoucherNo = "";
                        //    int VoucherID = 0;

                        //    foreach (var dr in results)
                        //    {
                        //        VoucherNo = dr["vno"].ToString();
                        //        VoucherID = Convert.ToInt32(dr["vid"].ToString());

                        //    }
                        //    var Billnarration = "Purchase bill made vide PI-" + model.vid + "  " + ItemNarration;




                        //    Command = new SqlCommand("[SPSW_SaveVoucherVB]", con);

                        //    Command.CommandType = CommandType.StoredProcedure;
                        //    Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //    Command.Parameters.Add("@VchNo", SqlDbType.VarChar).Value = VoucherNo;
                        //    Command.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = "CPV";
                        //    Command.Parameters.Add("@vDate", SqlDbType.Date).Value = date1;
                        //    Command.Parameters.Add("@isPending", SqlDbType.VarChar).Value = 0;
                        //    Command.Parameters.Add("@IsUpdate", SqlDbType.Bit).Value = IsUpdate;
                        //    Command.Parameters.Add("@Reference", SqlDbType.VarChar).Value = "PI-" + model.vid;
                        //    // Adding Voucher Logs
                        //    string user = Convert.ToString(Session["username"]);
                        //    if (IsUpdate == 1)
                        //    {
                        //        Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                        //        Command.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                        //    }
                        //    else
                        //    {
                        //        Command.Parameters.Add("@InsertedBy", SqlDbType.NVarChar).Value = Convert.ToString(Session["username"]);
                        //    }

                        //    Command.Transaction = Tran;
                        //    if (model.isupdate == "False")
                        //        VoucherID = Convert.ToInt32(Command.ExecuteScalar());
                        //    else
                        //        Command.ExecuteNonQuery();

                        //    Command.Dispose();



                        //    Command = new SqlCommand("[SPSW_SaveVoucherDetail]", con);
                        //    Command.CommandType = CommandType.StoredProcedure;
                        //    Command.Parameters.Add("@AccCode", SqlDbType.VarChar).Value = model.purid;
                        //    Command.Parameters.Add("@Debit", SqlDbType.Money).Value = model.nettotal;
                        //    Command.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Billnarration;
                        //    Command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = model.purname;
                        //    Command.Parameters.Add("@VId", SqlDbType.Int).Value = VoucherID;
                        //    Command.Parameters.Add("@Date", SqlDbType.Date).Value = date1;
                        //    Command.Parameters.Add("@VoucherType", SqlDbType.VarChar).Value = "CPV";
                        //    //Addition
                        //    Command.Parameters.Add("@VId2", SqlDbType.Int).Value = model.vid;
                        //    Command.Parameters.Add("@AccountId2", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
                        //    Command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = Convert.ToString(Session["Office"]);
                        //    //end
                        //    Command.Transaction = Tran;
                        //    Command.ExecuteNonQuery();
                        //    Command.Dispose();



                        //}




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
