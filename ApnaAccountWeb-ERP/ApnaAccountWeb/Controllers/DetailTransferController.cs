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
    public class DetailTransferController : Controller
    {
        //
        // GET: /DetailTransfer/
        SqlTransaction Tran;
        SqlCommand Command;
        public ActionResult DetailTransfer()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        [HttpPost]
        public JsonResult UpdateAccount(string From, string To)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            string dbconsql = Convert.ToString(Session["consql"]);
            SqlConnection con = new SqlConnection(dbconsql);
            con.Open();
            try
            {
                Tran = con.BeginTransaction();
                string query = "Update AccountsOpening Set AccountId='" + To + "' where AccountId='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update Farms Set AccountID='" + To + "' where AccountID='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update Gate Set IntCol1='" + To + "' where IntCol1='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update GRN1 Set AccountId='" + To + "' where AccountId='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update GstInvoice1 Set AccountId='" + To + "' where AccountId='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update GstInvoice1 Set BrockerID='" + To + "' where BrockerID='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update InOutWord Set PartyID='" + To + "' where PartyID='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update InvoiceMasterDelivaryChallan Set CustomerId='" + To + "' where CustomerId='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update Products Set StrCol1='" + To + "' where StrCol1='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update Products Set StrCol2='" + To + "' where StrCol2='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update Products Set StrCol3='" + To + "' where StrCol3='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update Purchase1 Set AccountId='" + To + "' where AccountId='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update Purchase1 Set BrockerID='" + To + "' where BrockerID='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update Purchase1 Set BrockerageAccount='" + To + "' where BrockerageAccount='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update PurchaseOrder1 Set AccountId='" + To + "' where AccountId='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update Sale1 Set AccountId='" + To + "' where AccountId='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update Sale1 Set BrockerID='" + To + "' where BrockerID='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update Sale1 Set BrockerageAccount='" + To + "' where BrockerageAccount='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update SaleOrder1 Set AccountId='" + To + "' where AccountId='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                query = "Update VouchersDetail Set AccountId='" + To + "' where AccountId='" + From + "'";
                Command = new SqlCommand(query, con);
                Command.CommandType = CommandType.Text;
                Command.Transaction = Tran;
                Command.ExecuteNonQuery();
                Command.Dispose();
                Tran.Commit();
                return Json(new { success = true, responseText = "Successfully Updated." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
