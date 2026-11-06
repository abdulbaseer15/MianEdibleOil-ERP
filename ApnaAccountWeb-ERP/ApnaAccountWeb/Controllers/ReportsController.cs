using ApnaAccountWeb.DataSets;
using ApnaAccountWeb.Models;
using ApnaAccountWeb.ViewModels;
using CrystalDecisions.CrystalReports.Engine;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.collection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
namespace ApnaAccountWeb.Controllers
{
    public class ReportsController : Controller
    {
        decimal dblOpening = 0;
        decimal dblDrAmount = 0;
        decimal dblCrAmount = 0;
        public ActionResult Report(string rtype, string dtpDatefrom, string dtpDateto, string txtAccCode, string title, string excel, string Opening, string Closing)
        {
            if (rtype == "ChartofAccount")
            {
                ViewBag.rtype = rtype;
                TempData["excel"] = excel;
            }
            if (rtype == "PendingGRN")
            {
                ViewBag.rtype = rtype;
                TempData["excel"] = excel;
            }
            if (rtype == "rptGRNBetweenDate")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                ViewBag.rtype = rtype;
                TempData["excel"] = excel;
            }
            else if (rtype == "rptProfitLoss")
            {
                TempData["dtsupres"] = title;
                TempData["dtDateto"] = dtpDateto;
                ViewBag.rtype = rtype;
                if (Opening == "")
                    Opening = "0";
                if (Closing == "")
                    Closing = "0";
                TempData["Opening"] = Opening;
                TempData["Closing"] = Closing;
                TempData["excel"] = excel;
            }
            else if (rtype == "rptBalanceSheet")
            {
                TempData["dtsupres"] = title;
                TempData["dtDateto"] = dtpDateto;
                ViewBag.rtype = rtype;
                if (Closing == "")
                    Closing = "0";
                TempData["Closing"] = Closing;
                TempData["excel"] = excel;
            }
            else if (rtype == "GatePassEmployee")
            {
                TempData["txtAccCode"] = txtAccCode;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "GatePassParties")
            {
                TempData["txtAccCode"] = txtAccCode;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptAccountReconcilation")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptAccountLedger")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptPartyLedgerSummary")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptReceivablePayable")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptPartyLedger")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptAging")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["supres"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptDeliveryChallan")
            {
                TempData["txtAccCode"] = txtAccCode;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptTrialBalance")
            {
                TempData["trbDatefrom"] = dtpDatefrom;
                TempData["trbsort"] = title;
                ViewBag.rtype = rtype;
                TempData["excel"] = excel;
            }
            else if (rtype == "rptSaleOrder")
            {
                TempData["trbDatefrom"] = dtpDatefrom;
                TempData["dtAccCode"] = txtAccCode;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptPurchaseInvoice")
            {
                TempData["trbDatefrom"] = dtpDatefrom;
                TempData["dtAccCode"] = txtAccCode;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptStoreConsumption")
            {
                ViewBag.rtype = rtype;
                TempData["vrVNo"] = txtAccCode;
            }
            else if (rtype == "rptIssueInvoice")
            {
                ViewBag.rtype = rtype;
                TempData["vrVNo"] = txtAccCode;
            }
            else if (rtype == "rptReceiveInvoice")
            {
                ViewBag.rtype = rtype;
                TempData["vrVNo"] = txtAccCode;
            }
            else if (rtype == "rptPurchaseInvoice2")
            {
                TempData["trbDatefrom"] = dtpDatefrom;
                TempData["dtAccCode"] = txtAccCode;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptSaleInvoice")
            {
                TempData["trbDatefrom"] = dtpDatefrom;
                TempData["dtAccCode"] = txtAccCode;
                TempData["title"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptPurchaseOrder")
            {
                TempData["txtAccCode"] = txtAccCode;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptInwardGatePassRM")
            {
                TempData["txtAccCode"] = txtAccCode;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptInwardGatePassMix")
            {
                TempData["txtAccCode"] = txtAccCode;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptGRN")
            {
                TempData["txtAccCode"] = txtAccCode;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptOutwardGatePass")
            {
                TempData["txtAccCode"] = txtAccCode;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptOutwardGatePassMix")
            {
                TempData["txtAccCode"] = txtAccCode;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptGstInvoice")
            {
                TempData["trbDatefrom"] = dtpDatefrom;
                TempData["dtAccCode"] = txtAccCode;
                TempData["title"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptGstPurchaseInvoice")
            {
                TempData["trbDatefrom"] = dtpDatefrom;
                TempData["dtAccCode"] = txtAccCode;
                TempData["title"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptReturnGstInvoice")
            {
                TempData["trbDatefrom"] = dtpDatefrom;
                TempData["dtAccCode"] = txtAccCode;
                TempData["title"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptSaleReturnInvoice")
            {
                TempData["trbDatefrom"] = dtpDatefrom;
                TempData["dtAccCode"] = txtAccCode;
                TempData["title"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptBardanaParchi")
            {
                TempData["trbDatefrom"] = dtpDatefrom;
                TempData["dtAccCode"] = txtAccCode;
                TempData["title"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptTrialBalanceBetweenDates")
            {
                TempData["trbDateto"] = dtpDateto;
                TempData["trbDatefrom"] = dtpDatefrom;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptCashbook")
            {
                TempData["cbDateto"] = dtpDateto;
                TempData["cbDatefrom"] = dtpDatefrom;
                TempData["excel"] = excel;
                TempData["title"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptDaybook")
            {
                TempData["cbDateto"] = dtpDateto;
                TempData["cbDatefrom"] = dtpDatefrom;
                TempData["title"] = title;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptDayBookInvoiceDetail")
            {
                TempData["cbDateto"] = dtpDateto;
                TempData["cbDatefrom"] = dtpDatefrom;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptDayBookRecovered")
            {
                TempData["cbDateto"] = dtpDateto;
                TempData["cbDatefrom"] = dtpDatefrom;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptJournalBook")
            {
                TempData["jbDateto"] = dtpDateto;
                TempData["jbDatefrom"] = dtpDatefrom;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptDetailTrial")
            {
                TempData["dtsupres"] = title;
                TempData["dtDateto"] = dtpDateto;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptBalanceSheet")
            {
                TempData["dtsupres"] = title;
                TempData["dtDateto"] = dtpDateto;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
                if (Closing == "")
                    Closing = "0";
                TempData["Closing"] = Closing;
            }
            else if (rtype == "rptGroupTrial")
            {
                TempData["dtsupres"] = dtpDatefrom;
                TempData["dtDateto"] = dtpDateto;
                TempData["dtAccCode"] = txtAccCode;
                TempData["dttitle"] = title;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptGroupTrial_WA")
            {
                TempData["dtsupres"] = dtpDatefrom;
                TempData["dtDateto"] = dtpDateto;
                TempData["dtAccCode"] = txtAccCode;
                TempData["dttitle"] = title;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptSixColumnTrial")
            {
                TempData["sctDatefrom"] = dtpDatefrom;
                TempData["sctDateto"] = dtpDateto;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptGeneralReceivable")
            {
                TempData["sctDatefrom"] = dtpDatefrom;
                TempData["sctDateto"] = dtpDateto;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptHODVReceipt" || rtype == "rptFODVReceipt" || rtype == "rptHOCVReceipt" || rtype == "rptFOCVReceipt" || rtype == "rptJVReceipt" || rtype == "rptPJVReceipt" || rtype == "rptPJVReceipt2" || rtype == "rptSJVReceipt" || rtype == "rptBDVReceipt" || rtype == "rptBCVReceipt" || rtype == "rptHOJVReceipt" || rtype == "rptFOJVReceipt")
            {
                ViewBag.rtype = rtype;
                TempData["Datefrom"] = dtpDatefrom;
                TempData["vrVNo"] = txtAccCode;
                TempData["title"] = title;
                TempData["excel"] = excel;
            }
            else if (rtype == "rptCashFlow")
            {
                TempData["cfDateto"] = dtpDateto;
                TempData["cfDatefrom"] = dtpDatefrom;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            //Added for Log Report
            else if (rtype == "rptLog")
            {
                TempData["cbDateto"] = dtpDateto;
                TempData["cbDatefrom"] = dtpDatefrom;
                TempData["excel"] = excel;
                TempData["title"] = title;
                ViewBag.rtype = rtype;
            }
            return View();
        }
        public ActionResult BalanceSheet()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        //Log Report 
        public ActionResult Log()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult ProfitLoss()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
      }
        public ActionResult GRNBetweenDate()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult AccountLedger()
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
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult PartyLedger()
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
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult PartyLedgerSummary()
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
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        [HttpPost]
        public JsonResult GetIfExist(string dtpDateto, string dtpDatefrom)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                DateTime date1 = DateTime.ParseExact(dtpDateto, "dd/MM/yyyy", null);
                DateTime date2 = DateTime.ParseExact(dtpDatefrom, "dd/MM/yyyy", null);
                var lstDVChilds = from s in db.VouchersDetails where s.VDate >= date2 && s.VDate <= date1 && (s.VType == "HOCPV" || s.VType == "HOCRV" || s.VType == "FOCPV" || s.VType == "FOCRV") select s;
                if (lstDVChilds.Count() > 0)
                {
                    return Json(new { responseText = "True" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { responseText = "False" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ReceivablePayable()
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
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult AccountReconciliation()
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
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult Aging()
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
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult Receivable()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult Daybook()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult DayBookRecovered()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult DayBookInvoiceDetail()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult Cashbook()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult CashFlow()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult Journalbook()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        //public ActionResult BalanceSheet()
        //{
        //    string dbcon = Convert.ToString(Session["con"]);
        //    string dbconsql = Convert.ToString(Session["consql"]);
        //    if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
        //    {
        //        return RedirectToAction("Login", "User");
        //    }
        //    return View();
        //}
        public ActionResult TrialBalance()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult TrialBalanceBetweenDates()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult DetailTrial()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult GroupTrial()
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
                //string usertype = Convert.ToString(Session["usertype"]);
                //if (!string.IsNullOrEmpty(usertype) && (usertype != "Admin"))
                //{
                //    ViewBag.AccCodeDetails = db.Accounts1.Where(x =>  x.AccountID == "122" || x.AccountID == "221" || x.ParentID == "122" || x.ParentID == "221").Select(x => new DetailList { AccountId = x.AccountID, Title = x.Title }).ToList();
                //}
                //else
                //{
                ViewBag.AccCodeDetails = db.Accounts.Where(x => x.AccType.Trim() == "Group").Select(x => new DetailList { AccountId = x.AccountId, Title = x.Title }).ToList();
                // ViewBag.AccCodeDetails = db.Accounts.Where(x => x.Type.Trim() == "Group").Select(x => new DetailList { AccountId = x.AccountID, Title = x.Title }).ToList();
                //}
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult GroupTrial_WA()
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
                //string usertype = Convert.ToString(Session["usertype"]);
                //if (!string.IsNullOrEmpty(usertype) && (usertype != "Admin"))
                //{
                //    ViewBag.AccCodeDetails = db.Accounts1.Where(x =>  x.AccountID == "122" || x.AccountID == "221" || x.ParentID == "122" || x.ParentID == "221").Select(x => new DetailList { AccountId = x.AccountID, Title = x.Title }).ToList();
                //}
                //else
                //{
                ViewBag.AccCodeDetails = db.Accounts.Where(x => x.AccType.Trim() == "Group").Select(x => new DetailList { AccountId = x.AccountId, Title = x.Title }).ToList();
                // ViewBag.AccCodeDetails = db.Accounts.Where(x => x.Type.Trim() == "Group").Select(x => new DetailList { AccountId = x.AccountID, Title = x.Title }).ToList();
                //}
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult SixColumnTrial()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        [HttpGet]
        public ActionResult GatePassEmployee()
        {
            int id = Convert.ToInt32(TempData["txtAccCode"]);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptGatePassEmployee.rpt")));
            rd.SetDataSource(GetGatePassEmployee(id));
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);
            rd.SetParameterValue("prmTitle", company);
            rd.SetParameterValue("prmAddress", Address);
            rd.SetParameterValue("prmTitles", "Gate Pass Employees");
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptPurchaseOrder()
        {
            string username = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string OrderID = Convert.ToString(TempData["txtAccCode"]);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptPurOrder.rpt")));
            rd.SetDataSource(GetPurchaseOrder("PO-" + OrderID, "PO"));
            string company = Convert.ToString(Session["Company"]);
            rd.SetParameterValue(0, company);
            rd.SetParameterValue(1, username);
            rd.SetParameterValue(2, "Broker Name");
            rd.SetParameterValue(3, "temp");
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = "Purchase Order ";
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptGRN()
        {
            string username = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string OrderID = Convert.ToString(TempData["txtAccCode"]);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptVoucherReceipt.rpt")));
            rd.SetDataSource(GetPurchaseOrder("GRN-" + OrderID, "GRN"));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue(1, company);
            rd.SetParameterValue("prmUserName", username);
            rd.SetParameterValue("prmBrokerName", "Broker Name");
            rd.SetParameterValue("DestinationParty", "temp");
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = "Goods Receive Note";
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        // ITextSharp to PDF
        //[HttpGet]
        //public ActionResult rptGRN()
        //{
        //    string username = Convert.ToString(Session["username"]);
        //    string dbcon = Convert.ToString(Session["con"]);
        //    string dbconsql = Convert.ToString(Session["consql"]);
        //    if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
        //    {
        //        return RedirectToAction("Login", "User");
        //    }
        //    string OrderID = Convert.ToString(TempData["txtAccCode"]);
        //    // Get the dataset for the report
        //    dsPurchase dataSet = GetPurchaseOrder(OrderID, "GRN");
        //    // Create a MemoryStream to hold the PDF content
        //        // Use iTextSharp to generate the PDF
        //        // Create a MemoryStream to hold the PDF content
        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            // Use iTextSharp to generate the PDF
        //            using (Document document = new Document())
        //            {
        //                PdfWriter writer = PdfWriter.GetInstance(document, stream);
        //                document.Open();
        //                // Add a title to the PDF
        //                document.Add(new Paragraph("Goods Receive Note Report"));
        //                // Add the dataset content to the PDF
        //                AddDataSetToPdf(document, dataSet);
        //            }
        //            // Set the position to the beginning of the stream before returning
        //            stream.Position = 0;
        //            // Return the PDF as a FileStreamResult for download
        //            return new FileStreamResult(stream, "application/pdf")
        //            {
        //                FileDownloadName = "GRN_Report.pdf"
        //            };
        //    }
        //}
        // Helper method to add a DataSet to the PDF
        //private void AddDataSetToPdf(Document document, dsPurchase dataSet)
        //{
        //    foreach (DataRow row in dataSet.Tables[0].Rows)
        //    {
        //         Add a table to the PDF
        //        iText.Layout.Element.Table pdfTable = new iText.Layout.Element.Table(dataSet.Tables[0].Columns.Count);
        //         Add column headers
        //        foreach (DataColumn column in dataSet.Tables[0].Columns)
        //        {
        //            pdfTable.AddHeaderCell(column.ColumnName);
        //        }
        //         Add data rows
        //        foreach (object value in row.ItemArray)
        //        {
        //            pdfTable.AddCell(value.ToString());
        //        }
        //         Add the table to the PDF document
        //        document.Add(pdfTable);
        //    }
        //}
        // Helper method to create a sample DataSet (replace with your data retrieval logic)
        private DataSet GetSampleDataSet()
        {
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable("SampleTable");
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Age", typeof(int));
            table.Rows.Add(1, "John Doe", 30);
            table.Rows.Add(2, "Jane Doe", 25);
            table.Rows.Add(3, "Bob Smith", 40);
            dataSet.Tables.Add(table);
            return dataSet;
        }
        [HttpGet]
        public ActionResult rptInwardGatePassRM()
        {
            string username = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string OrderID = Convert.ToString(TempData["txtAccCode"]);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptGatePassCommercial.rpt")));
            rd.SetDataSource(GetInwardGatePass("GPR-" + OrderID, "GPR"));
            rd.SetParameterValue("prmUserName", username);
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = CompanyInfo.Address;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptInwardGatePassMix()
        {
            string username = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string OrderID = Convert.ToString(TempData["txtAccCode"]);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptInwardMixed.rpt")));
            rd.SetDataSource(GetInwardGatePassMix("GPM-" + OrderID, "GPM"));
            rd.SetParameterValue("prmUserName", username);
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = CompanyInfo.Address;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptOutwardGatePass()
        {
            string username = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string OrderID = Convert.ToString(TempData["txtAccCode"]);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptGatePassFinish.rpt")));
            rd.SetDataSource(GetOutwardGatePass("GOF-" + OrderID, "GOF"));
            rd.SetParameterValue("prmUserName", username);
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = CompanyInfo.Address;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptOutwardGatePassMix()
        {
            string username = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string OrderID = Convert.ToString(TempData["txtAccCode"]);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
           // string reportPath = Server.MapPath("/IReports/rptOutwardMixedNew.rpt");
           // rd.Load(reportPath);
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptOutwardMixedNew.rpt")));
           // rd.Load(Path.Combine(Server.MapPath("~/Reports/rptOutwardGateMixedNew.rpt")));
            rd.SetDataSource(GetOutwardGatePassMix("GOM-" + OrderID, "GOM"));
            rd.SetParameterValue("prmUserName", username);
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = CompanyInfo.Address;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult GatePassParties()
        {
            int id = Convert.ToInt32(TempData["txtAccCode"]);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptGatePassEmployee.rpt")));
            rd.SetDataSource(GetGatePassParties(id));
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);
            rd.SetParameterValue("prmTitle", company);
            rd.SetParameterValue("prmAddress", Address);
            rd.SetParameterValue("prmTitles", "Gate Pass Parties");
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult ChartofAccount()
        {
            //var alldata = (from a1 in db.Accounts
            //               join a2 in db.Accounts on a1.AccountId equals a2.ParentId
            //               into joinds
            //               from gr in joinds.DefaultIfEmpty()
            //               select new
            //               {
            //                   AccountId = a1.AccountId??"0",
            //                   Title = a1.Title ?? "",
            //                   Nature = a1.Nature ?? "",
            //                   AccType = a1.AccType ?? "",
            //                   LevelKey = a1.LevelKey ?? 0,
            //                   Parent=gr.Title?? ""
            //               }).ToList();
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            string excel = Convert.ToString(TempData["excel"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptChartofaccount.rpt")));
            rd.SetDataSource(GetChartofAccount_Data());
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult PendingGRN()
        {
            string username = Convert.ToString(Session["username"]);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptGRNBetweenDate.rpt")));
            rd.SetDataSource(GetPendingGRN());
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue(1, company);
            rd.SetParameterValue("prmUserName", username);
            rd.SetParameterValue("prmBrokerName", "Broker Name");
            rd.SetParameterValue("prmDate", "");
            rd.SetParameterValue("DestinationParty", "temp");
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = "Pending Goods Receive Note";
            //if (excel == "Yes")
            //{
            //    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            //    stream.Seek(0, SeekOrigin.Begin);
            //    rd.Close();
            //    rd.Dispose();
            //    return new FileStreamResult(stream, "application/vnd.ms-excel");
            //}
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptAccountLedger()
        {
            string id = Convert.ToString(TempData["txtAccCode"]);
            string title = Convert.ToString(TempData["title"]);
            string excel = Convert.ToString(TempData["excel"]);
            string TDate = Convert.ToString(TempData["dtpDateto"]);
            string FDate = Convert.ToString(TempData["dtpDatefrom"]);
           
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptAccountLedger.rpt")));
            rd.SetDataSource(GetAccountLedger(id, fdate, tdate));


            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);
            rd.SetParameterValue("prmTitle", company);
            rd.SetParameterValue("prmAddress", Address);
            rd.SetParameterValue("prmTitles", "Account: " + id + " Title: " + title + "   Date From: " + FDate + "    To:" + " " + TDate);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptPartyLedgerSummary()
        {
            //string id = Convert.ToString(TempData["txtAccCode"]);
            //string title = Convert.ToString(TempData["title"]);
            string excel = Convert.ToString(TempData["excel"]);
            string TDate = Convert.ToString(TempData["dtpDateto"]);
            //string FDate = Convert.ToString(TempData["dtpDatefrom"]);
            //DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptPartyLedgerSummary.rpt")));
            rd.SetDataSource(GetPartyLedgersummary(tdate));
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);
            rd.SetParameterValue("prmTitle", company);
            rd.SetParameterValue("prmAddress", Address);
            rd.SetParameterValue("prmTitles", " To:" + " " + TDate);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        public ActionResult rptGRNBetweenDate()
        {
            string username = Convert.ToString(Session["username"]);
            //string id = Convert.ToString(TempData["txtAccCode"]);
            //string title = Convert.ToString(TempData["title"]);
            string excel = Convert.ToString(TempData["excel"]);
            string Fdate = Convert.ToString(TempData["dtpDatefrom"]);
            DateTime fdate = DateTime.ParseExact(Fdate, "dd/MM/yyyy", null);
            string TDate = Convert.ToString(TempData["dtpDateto"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptGRNBetweenDateReport.rpt")));
            rd.SetDataSource(GetGRNBetween(fdate, tdate));
            string company = Convert.ToString(Session["Company"]);
            rd.SetParameterValue(1, company);
            rd.SetParameterValue("prmUserName", username);
            rd.SetParameterValue("prmBrokerName", "Broker Name");
            rd.SetParameterValue("prmDate", "From Date: " + Fdate + "    ToDate:" + " " + TDate);
            rd.SetParameterValue("DestinationParty", "temp");
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = "Inward Gate Pass Raw Materail Between Date";
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptReceivablePayable()
        {
            string excel = Convert.ToString(TempData["excel"]);
            string TDate = Convert.ToString(TempData["dtpDateto"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            string supress = "Yes";
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptGeneralReceivable.rpt")));
            rd.SetDataSource(GetRecPayable_Data(tdate, tdate));
            //rd.SetDataSource(GetPartyLedgersummary(tdate));
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);
            rd.SetParameterValue("Company", company);
            rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("Date", "As on Date: " + tdate.ToString("MMM-dd-yyyy"));
            rd.SetParameterValue("supres", supress);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptPartyLedger()
        {
            string id = Convert.ToString(TempData["txtAccCode"]);
            string title = Convert.ToString(TempData["title"]);
            string excel = Convert.ToString(TempData["excel"]);
            string TDate = Convert.ToString(TempData["dtpDateto"]);
            string FDate = Convert.ToString(TempData["dtpDatefrom"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptPartyLedgerNew.rpt")));
            rd.SetDataSource(GetPartyLedger(id, fdate, tdate));
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);
            rd.SetParameterValue("prmTitle", company);
            rd.SetParameterValue("prmAddress", Address);
            rd.SetParameterValue("prmTitles", "Account: " + id + "  " + title + "           Date From: " + FDate + "    To:" + " " + TDate);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptAccountReconcilation()
        {
            string id = Convert.ToString(TempData["txtAccCode"]);
            string title = Convert.ToString(TempData["title"]);
            string excel = Convert.ToString(TempData["excel"]);
            string TDate = Convert.ToString(TempData["dtpDateto"]);
            string FDate = Convert.ToString(TempData["dtpDatefrom"]);


            string company = Convert.ToString(Session["Company"]);
     
          

            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptAccountsReconciliation.rpt")));
            rd.SetDataSource(GetAccountReconcilation(id, fdate, tdate));
            //rd.SetParameterValue("prmTitle", CompanyInfo.BusinessName);
            //rd.SetParameterValue("prmAddress", CompanyInfo.Address);
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = "Account Reconciliation";
            //rd.SetParameterValue("prmTitles", "Account: " + id + "  " + title + "           Date From: " + FDate + "    To:" + " " + TDate);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptAging()
        {
            string id = Convert.ToString(TempData["txtAccCode"]);
            string title = Convert.ToString(TempData["title"]);
            string supres = Convert.ToString(TempData["supres"]);
            if (supres == "ARO")
            {
                id = "010203";
            }
            if (supres == "APO")
            {
                //id = "221";
                id = "020201";
            }
            string excel = Convert.ToString(TempData["excel"]);
            string TDate = Convert.ToString(TempData["dtpDateto"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptAging.rpt")));
            rd.SetDataSource(GetAgingData(id, title, supres, tdate));
            if (supres == "ARO")
            {
                rd.SummaryInfo.ReportComments = "Aging Receiveable over all";
                rd.SetParameterValue("prmTitles", tdate);
            }
            if (supres == "APO")
            {
                rd.SummaryInfo.ReportComments = "  Aging Payable over all";
                rd.SetParameterValue("prmTitles", tdate);
            }
            if (supres == "ARS")
            {
                rd.SummaryInfo.ReportComments = " Aging Receiveable Selected";
                rd.SetParameterValue("prmTitles", title + "  To:" + " " + tdate);
            }
            if (supres == "APS")
            {
                rd.SummaryInfo.ReportComments = "Aging Payable Selected";
                rd.SetParameterValue("prmTitles", title + "  To:" + " " + tdate);
            }
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);
            rd.SetParameterValue("prmTitle", company);
            rd.SetParameterValue("prmAddress", Address);
            rd.SetParameterValue("TODate", tdate);
            rd.SetParameterValue("prmRecPay", "True");
            rd.SetParameterValue("prmPrintTitle", "True");
            //if (excel == "ARO" || excel == "ARS")
            //    {
            //        rd.SetParameterValue("prmRecPay", "True");
            //        rd.SetParameterValue("prmPrintTitle","True" );
            //    }
            //else
            //{
            //    rd.SetParameterValue("prmRecPay","False");
            //    rd.SetParameterValue("prmPrintTitle", "False");
            //}
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptDeliveryChallan()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptInvoicePur2.rpt")));
            int vno = Convert.ToInt32(TempData["txtAccCode"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            var data = (from s in db.InvoiceMasterDelivaryChallans where s.InvId == vno select s).FirstOrDefault();
            rd.SetDataSource(GetInvoice_Data(vno));
            rd.SetParameterValue("CompanyPhone", "32423523");
            rd.SetParameterValue("CompanyAddredd", CompanyInfo.Address);
            rd.SetParameterValue("prmBal", 1000);
            rd.SetParameterValue("CustId", data.CustomerId);
            rd.SetParameterValue("Cust", data.Customer);
            rd.SetParameterValue("FarmId", data.FarmId);
            rd.SetParameterValue("Farm", data.Farm);
            rd.SetParameterValue("Address", data.Address);
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = "Delivery Challan";
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptBardanaParchi()
        {
            string username = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["trbDatefrom"]);
            string OrderID = Convert.ToString(TempData["dtAccCode"]);
            string Type = Convert.ToString(TempData["title"]);
            DateTime tdate = Convert.ToDateTime(TDate);
            //DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptSaleBardanaPurchi.rpt")));
            if (Type == "Ba")
            {
                rd.SetDataSource(GetBardana(OrderID, "Ba"));
            }
            else
            {
                rd.SetDataSource(GetBardana(OrderID, "Bar"));
            }
            rd.SetParameterValue(0, username);
            //rd.SetParameterValue(1, username);
            //rd.SetParameterValue(2, TDate);
            //rd.SetParameterValue(3, "temp");
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = "Sale Invoice ";
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptSaleReturnInvoice()
        {
            string username = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["trbDatefrom"]);
            string OrderID = Convert.ToString(TempData["dtAccCode"]);
            string VehiclyType = Convert.ToString(TempData["title"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptInvoiceSaleNew.rpt")));
            rd.SetDataSource(GetSaleOrder(OrderID, "SR"));
            rd.SetParameterValue(0, VehiclyType);
            rd.SetParameterValue(1, username);
            rd.SetParameterValue(2, TDate);
            rd.SetParameterValue(3, "temp");
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = "Sale Return Invoice ";
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptGstPurchaseInvoice()
        {
            string username = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["trbDatefrom"]);
            string OrderID = Convert.ToString(TempData["dtAccCode"]);
            string VehiclyType = Convert.ToString(TempData["title"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptGstInvoice.rpt")));
            rd.SetDataSource(GetSaleOrder(OrderID, "PGSTI"));
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = "Purchase GST  Invoice ";
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptReturnGstInvoice()
        {
            string username = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["trbDatefrom"]);
            string OrderID = Convert.ToString(TempData["dtAccCode"]);
            string VehiclyType = Convert.ToString(TempData["title"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptGstInvoice.rpt")));
            rd.SetDataSource(GetSaleOrder(OrderID, "GSIR"));
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = "Sale GST Return Invoice ";
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptGstInvoice()
        {
            string username = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["trbDatefrom"]);
            string OrderID = Convert.ToString(TempData["dtAccCode"]);
            string VehiclyType = Convert.ToString(TempData["title"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptGstInvoice.rpt")));
            rd.SetDataSource(GetSaleOrder(OrderID, "GSI"));
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = "Sale Invoice ";
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptSaleInvoice()
        {
            string username = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["trbDatefrom"]);
            string OrderID = Convert.ToString(TempData["dtAccCode"]);
            string VehiclyType = Convert.ToString(TempData["title"]);

            //DateTime tdate = Convert.ToDateTime(TDate);

            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptInvoiceSaleNew.rpt")));
            //rd.Load(Path.Combine(Server.MapPath("~/IReports/rptInvoiceSaleNew2.rpt")));
            rd.SetDataSource(GetSaleOrder(OrderID, "SI"));
            rd.SetParameterValue(0, VehiclyType);
            rd.SetParameterValue(1, username);
            rd.SetParameterValue(2, TDate);
            rd.SetParameterValue(3, "temp");
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            //rd.SummaryInfo.ReportComments = "Sale Invoice ";
            rd.SummaryInfo.ReportComments = "Sale Bill ";
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptPurchaseInvoice()
        {
            string username = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["trbDatefrom"]);
            string OrderID = Convert.ToString(TempData["dtAccCode"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptInvoiceSaleNew.rpt")));
            rd.SetDataSource(GetSaleOrder("FP-" + OrderID, "PI"));
            rd.SetParameterValue(0, "");
            rd.SetParameterValue(1, username);
            rd.SetParameterValue(2, TDate);
            rd.SetParameterValue(3, "temp");
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = "Purchase Invoice ";
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }


        [HttpGet]
        public ActionResult rptStoreConsumption()
        {

            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptInvoicePur2New.rpt")));
            int vno = Convert.ToInt32(TempData["vrVNo"]);
            rd.SetDataSource(GetInvoiceNew_Data(vno, "SC"));
            rd.SetParameterValue("CompanyPhone", CompanyInfo.Phone);
            rd.SetParameterValue("CompanyAddredd", CompanyInfo.Address);
            rd.SetParameterValue("prmBal", 1000);
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = "STORE CONSUMPTION INVOICE";

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/pdf");
        }



        [HttpGet]
        public ActionResult rptIssueInvoice()
        {

            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptInvoiceProduction.rpt")));
            int vno = Convert.ToInt32(TempData["vrVNo"]);
            rd.SetDataSource(GetInvoiceNew_Data(vno, "IFP"));
            rd.SetParameterValue("CompanyPhone", CompanyInfo.Phone);
            rd.SetParameterValue("CompanyAddredd", CompanyInfo.Address);
            rd.SetParameterValue("prmBal", 1000);
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = "ISSUE FOR PRODUCTION";

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/pdf");
        }

        [HttpGet]
        public ActionResult rptReceiveInvoice()
        {

            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptInvoiceProduction.rpt")));
            int vno = Convert.ToInt32(TempData["vrVNo"]);
            rd.SetDataSource(GetInvoiceNew_Data(vno, "RFP"));
            rd.SetParameterValue("CompanyPhone", CompanyInfo.Phone);
            rd.SetParameterValue("CompanyAddredd", CompanyInfo.Address);
            rd.SetParameterValue("prmBal", 1000);
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = "RECEIVE FROM PRODUCTION";

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/pdf");
        }


        [HttpGet]
        public ActionResult rptPurchaseInvoice2()
        {
            string username = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["trbDatefrom"]);
            string OrderID = Convert.ToString(TempData["dtAccCode"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptInvoiceSaleNew.rpt")));
            rd.SetDataSource(GetSaleOrder("FPR-" + OrderID, "PR"));
            rd.SetParameterValue(0, "");
            rd.SetParameterValue(1, username);
            rd.SetParameterValue(2, TDate);
            rd.SetParameterValue(3, "temp");
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = "Purchase Invoice ";
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptProfitLoss()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["dtDateto"]);
            string supress = Convert.ToString(TempData["dtsupres"]);
            int Opening = Convert.ToInt32(TempData["Opening"]);
            int Closing = Convert.ToInt32(TempData["Closing"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            string excel = Convert.ToString(TempData["excel"]);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            if (supress.Equals("D"))
            {
                rd.Load(Path.Combine(Server.MapPath("~/Reports/rptProfitLoss2.rpt")));
            }
            else
            {
                rd.Load(Path.Combine(Server.MapPath("~/Reports/rptProfitLoss.rpt")));
            }
            rd.SetDataSource(GetProfitLoss_Data(tdate, supress));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("Date", TDate);
            rd.SetParameterValue("supres", "Yes");
            rd.SetParameterValue("OpStock", Opening);
            rd.SetParameterValue("CloStock", Closing);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptGeneralReceivable()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string FDate = Convert.ToString(TempData["sctDatefrom"]);
            string TDate = Convert.ToString(TempData["sctDateto"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            string supress = "No";
            string excel = Convert.ToString(TempData["excel"]);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptGeneralReceivable.rpt")));
            rd.SetDataSource(GetSixColumnTrial_Data2(fdate, tdate));
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);
            rd.SetParameterValue("Company", company);
            rd.SetParameterValue("Address", Address);
            //rd.SetParameterValue("Date", "From " + fdate.ToString("MMM-dd-yyyy") + " to " + tdate.ToString("MMM-dd-yyyy"));
            rd.SetParameterValue("Date", "To Date " + fdate.ToString("MMM-dd-yyyy"));
            rd.SetParameterValue("supres", supress);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptBalanceSheet()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["dtDateto"]);
            string supress = Convert.ToString(TempData["dtsupres"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            int Closing = Convert.ToInt32(TempData["Closing"]);
            string excel = Convert.ToString(TempData["excel"]);
            SqlConnection con = new SqlConnection(dbconsql);
            con.Open();
            SqlCommand cmd = new SqlCommand("[SPRW_GetProfitAndLoss3]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = tdate;
            string profitLoss = Convert.ToString(cmd.ExecuteScalar());
            cmd.Dispose();
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            if (supress.Equals("D"))
            {
                rd.Load(Path.Combine(Server.MapPath("~/Reports/rptBalancesheet2.rpt")));
                //rd.Load(Path.Combine(Server.MapPath("~/Reports/rptBalancesheetN.rpt")));
            }
            else
            {
                rd.Load(Path.Combine(Server.MapPath("~/Reports/rptBalancesheet.rpt")));
            }
            //rd.Load(Path.Combine(Server.MapPath("~/Reports/rptBalancesheet.rpt")));
            rd.SetDataSource(GetBalanceSheet_Data(tdate, supress));
            string company = Convert.ToString(Session["Company"]);
            rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]);
            rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("Date", TDate);
            rd.SetParameterValue("supres", "Yes");
            rd.SetParameterValue("profitloss", Convert.ToDecimal(profitLoss));
            rd.SetParameterValue("CloStock", Closing);
            //rd.SetParameterValue("Phone", CompanyInfo.Phone);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {

                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptSaleOrder()
        {
            string username = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["trbDatefrom"]);
            string OrderID = Convert.ToString(TempData["dtAccCode"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptsaleOrder.rpt")));
            rd.SetDataSource(GetSaleOrder(OrderID, "SO"));
            string company = Convert.ToString(Session["Company"]);
           
            rd.SetParameterValue(0, company);
            rd.SetParameterValue(1, username);
            rd.SetParameterValue(2, TDate);
            rd.SetParameterValue(3, "temp");
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportComments = "Sale Order ";
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptTrialBalance222()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string excel = Convert.ToString(TempData["excel"]);
            string TDate = Convert.ToString(TempData["trbDatefrom"]);
            string sort = Convert.ToString(TempData["trbsort"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptTrialBalance.rpt")));
            rd.SetDataSource(GetTrialBalanceToDate(tdate));
            string company = Convert.ToString(Session["Company"]);
            
            rd.SetParameterValue(0, company);
            rd.SetParameterValue(1, CompanyInfo.Address);
            rd.SetParameterValue(2, TDate);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptTrialBalance()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string excel = Convert.ToString(TempData["excel"]);
            string TDate = Convert.ToString(TempData["trbDatefrom"]);
            string sort = Convert.ToString(TempData["trbsort"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptTrialBalance.rpt")));
            rd.SetDataSource(GetTrialBalance_Data(tdate, sort));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("Date", TDate);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            } 
        }
        //[HttpGet]
        //public ActionResult rptBalanceSheet()
        //{
        //    string dbcon = Convert.ToString(Session["con"]);
        //    string dbconsql = Convert.ToString(Session["consql"]);
        //    if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
        //    {
        //        return RedirectToAction("Login", "User");
        //    }
        //    string excel = Convert.ToString(TempData["excel"]);
        //    string TDate = Convert.ToString(TempData["dtDateto"]);
        //    string sort = Convert.ToString(TempData["trbsort"]);
        //    DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/Reports/rptProfitAndLoss2.rpt")));
        //    rd.SetDataSource(GetBalanceSheetToDate(tdate));
        //    rd.SetParameterValue(0, CompanyInfo.BusinessName);
        //    rd.SetParameterValue(1, CompanyInfo.Address);
        //    rd.SetParameterValue(2, TDate);
        //    if (excel == "Yes")
        //    {
        //        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        rd.Close();
        //        rd.Dispose();
        //        return new FileStreamResult(stream, "application/vnd.ms-excel");
        //    }
        //    else
        //    {
        //        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        rd.Close();
        //        rd.Dispose();
        //        return new FileStreamResult(stream, "application/pdf");
        //    }
        //}
        //[HttpGet]
        //public ActionResult rptTrialBalanceBetweenDates()
        //{
        //    string dbcon = Convert.ToString(Session["con"]);
        //    string dbconsql = Convert.ToString(Session["consql"]);
        //    if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
        //    {
        //        return RedirectToAction("Login", "User");
        //    }
        //    string excel = Convert.ToString(TempData["excel"]);
        //    string fDate = Convert.ToString(TempData["trbDatefrom"]);
        //    string tDate = Convert.ToString(TempData["trbDateto"]);
        //    //string sort = Convert.ToString(TempData["trbsort"]);
        //    DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
        //    DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/Reports/rptTrialBalanceBetween.rpt")));
        //    rd.SetDataSource(GetTrialBalanceBetween_Data(frodate, todate));
        //    string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
        //    string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
        //    rd.SetParameterValue("From", frodate);
        //    rd.SetParameterValue("To", todate);
        //    if (excel == "Yes")
        //    {
        //        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        rd.Close();
        //        rd.Dispose();
        //        return new FileStreamResult(stream, "application/vnd.ms-excel");
        //    }
        //    else
        //    {
        //        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        rd.Close();
        //        rd.Dispose();
        //        return new FileStreamResult(stream, "application/pdf");
        //    }
        //}
        [HttpGet]
        public ActionResult rptTrialBalanceBetweenDates()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string fDate = Convert.ToString(TempData["trbDatefrom"]);
            string tDate = Convert.ToString(TempData["trbDateto"]);
            string excel = Convert.ToString(TempData["excel"]);
            //string sort = Convert.ToString(TempData["trbsort"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptTrialBalanceBetween.rpt")));
            rd.SetDataSource(GetTrialBalanceBetween_Data(frodate, todate));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("From", frodate);
            rd.SetParameterValue("To", todate);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptDaybook()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string excel = Convert.ToString(TempData["excel"]);
            string TDate = Convert.ToString(TempData["cbDateto"]);
            string FDate = Convert.ToString(TempData["cbDatefrom"]);
            string title = Convert.ToString(TempData["title"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            decimal debit = 0;
            decimal credit = 0;
            decimal balance = 0;
            SqlConnection Connection = new SqlConnection(dbconsql);
            Connection.Open();
            SqlCommand cmd = new SqlCommand("SPRW_GetOpening", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FromDate", fdate);
            cmd.Parameters.Add("@ToDate", tdate);
            cmd.Parameters.Add("@Type", title);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    debit = Convert.ToDecimal(reader["Debit"]);
                    credit = Convert.ToDecimal(reader["Credit"]);
                }
            }
            if (debit == 0)
            {
                balance = credit;
            }
            else
            {
                balance = debit;
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptDayBook.rpt")));
            rd.SetDataSource(GetAccountLedger_Data(fdate, tdate, title, false, false, true));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("From", FDate);
            rd.SetParameterValue("To", TDate);
            rd.SetParameterValue("OB", balance);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptCashbook()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string excel = Convert.ToString(TempData["excel"]);
            string TDate = Convert.ToString(TempData["cbDateto"]);
            string FDate = Convert.ToString(TempData["cbDatefrom"]);
            string title = Convert.ToString(TempData["title"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            decimal debit = 0;
            decimal credit = 0;
            decimal balance = 0;
            SqlConnection Connection = new SqlConnection(dbconsql);
            Connection.Open();
            SqlCommand cmd = new SqlCommand("SPRW_GetOpening", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FromDate", fdate);
            cmd.Parameters.Add("@ToDate", tdate);
            cmd.Parameters.Add("@Type", title);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    debit = Convert.ToDecimal(reader["Debit"]);
                    credit = Convert.ToDecimal(reader["Credit"]);
                }
            }
            if (debit == 0)
            {
                balance = credit;
            }
            else
            {
                balance = debit;
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptCashBook.rpt")));
            rd.SetDataSource(GetAccountLedger_Data(fdate, tdate, title, true));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("From", FDate);
            rd.SetParameterValue("To", TDate);
            rd.SetParameterValue("OB", balance);

            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");

            }
        }
        [HttpGet]
        public ActionResult rptDayBookInvoiceDetail()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["cbDateto"]);
            string FDate = Convert.ToString(TempData["cbDatefrom"]);
            string excel = Convert.ToString(TempData["excel"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptDayBookInvDetail.rpt")));
            rd.SetDataSource(GetCashBookDayBookDetail(0, fdate, tdate));
            string company = Convert.ToString(Session["Company"]);
            rd.SetParameterValue(1, company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue(2, Address);
            rd.SetParameterValue("prmTitles", " Date From: " + FDate + "    To:" + " " + TDate);
            //rd.SetParameterValue(3, FDate);
            //rd.SetParameterValue(4, GetOpening(fdate));
            //rd.SetParameterValue(3, CompanyInfo.Address);
            //    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptDayBookRecovered()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["cbDateto"]);
            string FDate = Convert.ToString(TempData["cbDatefrom"]);
            string excel = Convert.ToString(TempData["excel"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptCashBook2.rpt")));
            rd.SetDataSource(GetDayBookRecovered(0, fdate, tdate));
            //rd.SetParameterValue(0, CompanyInfo.Address);
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);
            rd.SetParameterValue("prmTitle", company);
            rd.SetParameterValue("prmAddress", Address);
            rd.SetParameterValue("prmDateRange", " Date From: " + FDate + "    To:" + " " + TDate);
           
            rd.SetParameterValue("prmReportTitle", dblOpening);
            rd.SetParameterValue("prmOpeningBalance", dblOpening);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptJournalbook()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["jbDateto"]);
            string FDate = Convert.ToString(TempData["jbDatefrom"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            string excel = Convert.ToString(TempData["excel"]);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptJournalBook.rpt")));
            rd.SetDataSource(GetAccountLedger_Data(fdate, tdate, "", false, false, false, true));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("From", FDate);
            rd.SetParameterValue("To", TDate);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptCashbook222()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["cbDateto"]);
            string FDate = Convert.ToString(TempData["cbDatefrom"]);
            string excel = Convert.ToString(TempData["excel"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptCashBook.rpt")));
            rd.SetDataSource(GetCashBookDayBook(1, fdate, tdate));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue(1, company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue(2, Address);
            rd.SetParameterValue(3, FDate);
            rd.SetParameterValue(4, GetOpening(fdate));
            rd.SetParameterValue(0, CompanyInfo.Address);
            //    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptCashFlow()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["cfDateto"]);
            string FDate = Convert.ToString(TempData["cfDatefrom"]);
            string excel = Convert.ToString(TempData["excel"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptCashFlow.rpt")));
            //rd.SetDataSource(GetAccountLedger_Data(fdate, tdate, "", false,true));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("From", FDate);
            rd.SetParameterValue("To", TDate);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptDetailTrial()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["dtDateto"]);
            string supress = Convert.ToString(TempData["dtsupres"]);
            string excel = Convert.ToString(TempData["excel"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptDetailTrial.rpt")));
            rd.SetDataSource(GetDetailTrial_Data(tdate));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("Date", TDate);
            rd.SetParameterValue("supres", supress);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptGroupTrial()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string excel = Convert.ToString(TempData["excel"]);
            string id = Convert.ToString(TempData["dtAccCode"]);
            string title = Convert.ToString(TempData["dttitle"]);
            string TDate = Convert.ToString(TempData["dtDateto"]);
            string supp = Convert.ToString(TempData["dtsupres"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptGroupTrial.rpt")));
            rd.SetDataSource(GetGroupTrial_Data(tdate, id));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("Title", id + " " + title);
            rd.SetParameterValue("Date", TDate);
            rd.SetParameterValue("supres", "Yes");
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptGroupTrial_WA()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string excel = Convert.ToString(TempData["excel"]);
            string id = Convert.ToString(TempData["dtAccCode"]);
            string title = Convert.ToString(TempData["dttitle"]);
            int days = -1 * Convert.ToInt32(TempData["dtDateto"]);
            string supp = Convert.ToString(TempData["dtsupres"]);
            DateTime tdate = System.DateTime.Now.AddDays(days);
            DateTime Agdate = System.DateTime.Now;
            // tdate.AddDays(days);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptGroupTrial2.rpt")));
            rd.SetDataSource(GetGroupTrial_Data2(tdate, id, Agdate));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("Title", id + " " + title);
            rd.SetParameterValue("Date", Agdate);
            rd.SetParameterValue("supres", "Yes");
            rd.SetParameterValue("days", days * -1);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptSixColumnTrial()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string FDate = Convert.ToString(TempData["sctDatefrom"]);
            string TDate = Convert.ToString(TempData["sctDateto"]);
            string excel = Convert.ToString(TempData["excel"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptSixColumnTrial.rpt")));
            rd.SetDataSource(GetSixColumnTrial_Data(fdate, tdate));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("FDate", FDate);
            rd.SetParameterValue("TDate", TDate);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        [HttpGet]
        public ActionResult rptJVReceipt()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string preparedby = Convert.ToString(Session["username"]);
            string excel = Convert.ToString(TempData["excel"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            //if(VType=="DV" || VType=="CV")
           // rd.Load(Path.Combine(Server.MapPath("~/Reports/rptVoucherReceipt.rpt")));
            //else if (VType == "JV")
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptJVReceipt.rpt")));
            string TDate = Convert.ToString(TempData["Datefrom"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            int vno = Convert.ToInt32(TempData["vrVNo"]);
            int vid = Convert.ToInt32(TempData["title"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            string vchno = "JJV-" + vid;
            //var vchid = db.qryVoucherBooks.Where(x => x.voucherNo == vchno && x.vDate==tdate).Select(x => x.VoucherID).FirstOrDefault();
            var Posted = db.Database.SqlQuery<DateTime>("select top 1  case   when (SELECT count(*) as vdate FROM VouchersLog WHERE VId = @VoucherID)=0 then (SELECT TOP 1 vdate FROM vouchers WHERE voucherid = @VoucherID)  else (SELECT TOP 1 TransDateTime FROM VouchersLog WHERE VId = @VoucherID ORDER BY TransDateTime DESC) end   from VouchersLog", new SqlParameter("VoucherID", vno)).ToList();
            DateTime PostedDate = Posted.FirstOrDefault();
            rd.SetDataSource(GetVoucherReceipt_Data(Convert.ToString(vno), "JV"));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("vchno", vchno);
            rd.SetParameterValue("pby", preparedby);
            rd.SetParameterValue("PostedDate", PostedDate);
            //if(VType=="DV")
            //rd.SetParameterValue("VType", "Debit Voucher");
            //else if (VType == "CV")
            //    rd.SetParameterValue("VType", "Credit Voucher");
            //else if (VType == "JV")
            rd.SetParameterValue("VType", "Journal Voucher");
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptHOJVReceipt()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string preparedby = Convert.ToString(Session["username"]);
            string excel = Convert.ToString(TempData["excel"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            //if(VType=="DV" || VType=="CV")
            // rd.Load(Path.Combine(Server.MapPath("~/Reports/rptVoucherReceipt.rpt")));
            //else if (VType == "JV")
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptJVReceipt.rpt")));
            string TDate = Convert.ToString(TempData["Datefrom"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            int vno = Convert.ToInt32(TempData["vrVNo"]);
            int vid = Convert.ToInt32(TempData["title"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            string vchno = "HOJJV-" + vid;
            //var vchid = db.qryVoucherBooks.Where(x => x.voucherNo == vchno && x.vDate==tdate).Select(x => x.VoucherID).FirstOrDefault();
            var Posted = db.Database.SqlQuery<DateTime>("select top 1  case   when (SELECT count(*) as vdate FROM VouchersLog WHERE VId = @VoucherID)=0 then (SELECT TOP 1 vdate FROM vouchers WHERE voucherid = @VoucherID)  else (SELECT TOP 1 TransDateTime FROM VouchersLog WHERE VId = @VoucherID ORDER BY TransDateTime DESC) end   from VouchersLog", new SqlParameter("VoucherID", vno)).ToList();
            DateTime PostedDate = Posted.FirstOrDefault();
            rd.SetDataSource(GetVoucherReceipt_Data(Convert.ToString(vno), "HOJV"));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("vchno", vchno);
            rd.SetParameterValue("pby", preparedby);
            rd.SetParameterValue("PostedDate", PostedDate);
            //if(VType=="DV")
            //rd.SetParameterValue("VType", "Debit Voucher");
            //else if (VType == "CV")
            //    rd.SetParameterValue("VType", "Credit Voucher");
            //else if (VType == "JV")
            rd.SetParameterValue("VType", "Journal Voucher");
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptFOJVReceipt()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string preparedby = Convert.ToString(Session["username"]);
            string excel = Convert.ToString(TempData["excel"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            //if(VType=="DV" || VType=="CV")
            // rd.Load(Path.Combine(Server.MapPath("~/Reports/rptVoucherReceipt.rpt")));
            //else if (VType == "JV")
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptJVReceipt.rpt")));
            string TDate = Convert.ToString(TempData["Datefrom"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            int vno = Convert.ToInt32(TempData["vrVNo"]);
            int vid = Convert.ToInt32(TempData["title"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            string vchno = "FOJJV-" + vid;
            //var vchid = db.qryVoucherBooks.Where(x => x.voucherNo == vchno && x.vDate==tdate).Select(x => x.VoucherID).FirstOrDefault();
            var Posted = db.Database.SqlQuery<DateTime>("select top 1  case   when (SELECT count(*) as vdate FROM VouchersLog WHERE VId = @VoucherID)=0 then (SELECT TOP 1 vdate FROM vouchers WHERE voucherid = @VoucherID)  else (SELECT TOP 1 TransDateTime FROM VouchersLog WHERE VId = @VoucherID ORDER BY TransDateTime DESC) end   from VouchersLog", new SqlParameter("VoucherID", vno)).ToList();
            DateTime PostedDate = Posted.FirstOrDefault();
            rd.SetDataSource(GetVoucherReceipt_Data(Convert.ToString(vno), "FOJV"));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("vchno", vchno);
            rd.SetParameterValue("pby", preparedby);
            rd.SetParameterValue("PostedDate", PostedDate);
            //if(VType=="DV")
            //rd.SetParameterValue("VType", "Debit Voucher");
            //else if (VType == "CV")
            //    rd.SetParameterValue("VType", "Credit Voucher");
            //else if (VType == "JV")
            rd.SetParameterValue("VType", "Journal Voucher");
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptHODVReceipt()
        {
            string preparedby = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            //Uncommenting till else if (3 lines) for testing 
            //if (VType == "DV" || VType == "CV")
              //  rd.Load(Path.Combine(Server.MapPath("~/Reports/rptVoucherReceipt.rpt")));
            //else if (VType == "JV")
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptJVReceipt.rpt")));
            string TDate = Convert.ToString(TempData["Datefrom"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            int vno = Convert.ToInt32(TempData["vrVNo"]);
            int vid = Convert.ToInt32(TempData["title"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            var Posted = db.Database.SqlQuery<DateTime>("select top 1  case   when (SELECT count(*) as vdate FROM VouchersLog WHERE VId = @VoucherID)=0 then (SELECT TOP 1 vdate FROM vouchers WHERE voucherid = @VoucherID)  else (SELECT TOP 1 TransDateTime FROM VouchersLog WHERE VId = @VoucherID ORDER BY TransDateTime DESC) end   from VouchersLog", new SqlParameter("VoucherID", vno)).ToList();
            DateTime PostedDate = Posted.FirstOrDefault();
            string vchno = "HOCPV-" + vid;
            //var vchid = db.qryVoucherBooks.Where(x => x.voucherNo == vchno && x.vDate==tdate).Select(x => x.VoucherID).FirstOrDefault();
            rd.SetDataSource(GetVoucherReceipt_Data(Convert.ToString(vno), "HODV"));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("vchno", vchno);
            rd.SetParameterValue("pby", preparedby);
            rd.SetParameterValue("PostedDate", PostedDate);
            //if(VType=="DV")
            //rd.SetParameterValue("VType", "Debit Voucher");
            //else if (VType == "CV")
            //    rd.SetParameterValue("VType", "Credit Voucher");
            //else if (VType == "JV")
            rd.SetParameterValue("VType", "HO Debit Voucher");
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptFODVReceipt()
        {
            string preparedby = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            //Uncommenting till else if (3 lines) for testing 
            //if (VType == "DV" || VType == "CV")
            //  rd.Load(Path.Combine(Server.MapPath("~/Reports/rptVoucherReceipt.rpt")));
            //else if (VType == "JV")
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptJVReceipt.rpt")));
            string TDate = Convert.ToString(TempData["Datefrom"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            int vno = Convert.ToInt32(TempData["vrVNo"]);
            int vid = Convert.ToInt32(TempData["title"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            var Posted = db.Database.SqlQuery<DateTime>("select top 1  case   when (SELECT count(*) as vdate FROM VouchersLog WHERE VId = @VoucherID)=0 then (SELECT TOP 1 vdate FROM vouchers WHERE voucherid = @VoucherID)  else (SELECT TOP 1 TransDateTime FROM VouchersLog WHERE VId = @VoucherID ORDER BY TransDateTime DESC) end   from VouchersLog", new SqlParameter("VoucherID", vno)).ToList();
            DateTime PostedDate = Posted.FirstOrDefault();
            string vchno = "FOCPV-" + vid;
            //var vchid = db.qryVoucherBooks.Where(x => x.voucherNo == vchno && x.vDate==tdate).Select(x => x.VoucherID).FirstOrDefault();
            rd.SetDataSource(GetVoucherReceipt_Data(Convert.ToString(vno), "FODV"));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("vchno", vchno);
            rd.SetParameterValue("pby", preparedby);
            rd.SetParameterValue("PostedDate", PostedDate);
            //if(VType=="DV")
            //rd.SetParameterValue("VType", "Debit Voucher");
            //else if (VType == "CV")
            //    rd.SetParameterValue("VType", "Credit Voucher");
            //else if (VType == "JV")
            rd.SetParameterValue("VType", "FO Debit Voucher");
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptSJVReceipt()
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
            int vno = Convert.ToInt32(TempData["vrVNo"]);
            var sd = db.Database.SqlQuery<int>("select TOP 1 VId from Vouchers where VoucherID=" + vno + "").ToList();
            int voucherNo = sd.FirstOrDefault();
            var Posted = db.Database.SqlQuery<DateTime>("select top 1  case   when (SELECT count(*) as vdate FROM VouchersLog WHERE VId = @VoucherID)=0 then (SELECT TOP 1 vdate FROM vouchers WHERE voucherid = @VoucherID)  else (SELECT TOP 1 TransDateTime FROM VouchersLog WHERE VId = @VoucherID ORDER BY TransDateTime DESC) end   from VouchersLog", new SqlParameter("VoucherID", vno)).ToList();
            DateTime PostedDate = Posted.FirstOrDefault();
            // string voucherNo = (from t1 in db.qryVoucherBooks where t1.VoucherID == vno select t1.voucherNo).First().ToString();
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            string preparedby = Convert.ToString(Session["username"]);
            //   rd.Load(Path.Combine(Server.MapPath("~/Reports/rptVoucherReceipt.rpt")));
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptJVReceipt.rpt")));
            rd.SetDataSource(GetVoucherReceipt_Data(Convert.ToString(vno), "SJV"));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("vchno", "SJV-" + voucherNo);
            rd.SetParameterValue("pby", preparedby);
            rd.SetParameterValue("PostedDate", PostedDate);
            rd.SetParameterValue("VType", "Sale Journal Voucher");
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptHOCVReceipt()
        {
            string preparedby = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            //if(VType=="DV" || VType=="CV")
          //  rd.Load(Path.Combine(Server.MapPath("~/Reports/rptVoucherReceipt.rpt")));
            //else if (VType == "JV")
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptJVReceipt.rpt")));
            string TDate = Convert.ToString(TempData["Datefrom"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            string vno = Convert.ToString(TempData["vrVNo"]);
            int vid = Convert.ToInt32(TempData["title"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            var Posted = db.Database.SqlQuery<DateTime>("select top 1  case   when (SELECT count(*) as vdate FROM VouchersLog WHERE VId = @VoucherID)=0 then (SELECT TOP 1 vdate FROM vouchers WHERE voucherid = @VoucherID)  else (SELECT TOP 1 TransDateTime FROM VouchersLog WHERE VId = @VoucherID ORDER BY TransDateTime DESC) end   from VouchersLog", new SqlParameter("VoucherID", vno)).ToList();
            DateTime PostedDate = Posted.FirstOrDefault();
            string vchno = "HOCRV-" + vid;
            //var vchid = db.qryVoucherBooks.Where(x => x.voucherNo == vchno&& x.vDate==tdate).Select(x => x.VoucherID).FirstOrDefault();
            rd.SetDataSource(GetVoucherReceipt_Data(Convert.ToString(vno), "HOCV"));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("vchno", vchno);
            rd.SetParameterValue("pby", preparedby);
            rd.SetParameterValue("PostedDate", PostedDate);
            //if(VType=="DV")
            //rd.SetParameterValue("VType", "Debit Voucher");
            //else if (VType == "CV")
            //    rd.SetParameterValue("VType", "Credit Voucher");
            //else if (VType == "JV")
            rd.SetParameterValue("VType", "HO Credit Voucher");
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }

        [HttpGet]
        public ActionResult rptFOCVReceipt()
        {
            string preparedby = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            //if(VType=="DV" || VType=="CV")
            //  rd.Load(Path.Combine(Server.MapPath("~/Reports/rptVoucherReceipt.rpt")));
            //else if (VType == "JV")
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptJVReceipt.rpt")));
            string TDate = Convert.ToString(TempData["Datefrom"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            string vno = Convert.ToString(TempData["vrVNo"]);
            int vid = Convert.ToInt32(TempData["title"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            var Posted = db.Database.SqlQuery<DateTime>("select top 1  case   when (SELECT count(*) as vdate FROM VouchersLog WHERE VId = @VoucherID)=0 then (SELECT TOP 1 vdate FROM vouchers WHERE voucherid = @VoucherID)  else (SELECT TOP 1 TransDateTime FROM VouchersLog WHERE VId = @VoucherID ORDER BY TransDateTime DESC) end   from VouchersLog", new SqlParameter("VoucherID", vno)).ToList();
            DateTime PostedDate = Posted.FirstOrDefault();
            string vchno = "FOCRV-" + vid;
            //var vchid = db.qryVoucherBooks.Where(x => x.voucherNo == vchno&& x.vDate==tdate).Select(x => x.VoucherID).FirstOrDefault();
            rd.SetDataSource(GetVoucherReceipt_Data(Convert.ToString(vno), "FOCV"));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("vchno", vchno);
            rd.SetParameterValue("pby", preparedby);
            rd.SetParameterValue("PostedDate", PostedDate);
            //if(VType=="DV")
            //rd.SetParameterValue("VType", "Debit Voucher");
            //else if (VType == "CV")
            //    rd.SetParameterValue("VType", "Credit Voucher");
            //else if (VType == "JV")
            rd.SetParameterValue("VType", "FO Credit Voucher");
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }

        [HttpGet]
        public ActionResult rptPJVReceipt2()
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
            int vno = Convert.ToInt32(TempData["vrVNo"]);
            var sd = db.Database.SqlQuery<int>("select TOP 1 VId from Vouchers where VoucherID=" + vno + "").ToList();
            int voucherNo = sd.FirstOrDefault();
            var Posted = db.Database.SqlQuery<DateTime>("select top 1  case   when (SELECT count(*) as vdate FROM VouchersLog WHERE VId = @VoucherID)=0 then (SELECT TOP 1 vdate FROM vouchers WHERE voucherid = @VoucherID)  else (SELECT TOP 1 TransDateTime FROM VouchersLog WHERE VId = @VoucherID ORDER BY TransDateTime DESC) end   from VouchersLog", new SqlParameter("VoucherID", vno)).ToList();
            DateTime PostedDate = Posted.FirstOrDefault();
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            string preparedby = Convert.ToString(Session["username"]);
            //rd.Load(Path.Combine(Server.MapPath("~/Reports/rptVoucherReceipt.rpt")));
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptJVReceipt.rpt")));
            rd.SetDataSource(GetVoucherReceipt_Data(Convert.ToString(vno), "PJV"));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("vchno", "PJV-" + voucherNo);
            rd.SetParameterValue("pby", preparedby);
            rd.SetParameterValue("PostedDate", PostedDate);
            rd.SetParameterValue("VType", "Purchase Journal Voucher");
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptPJVReceipt()
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
            int vno = Convert.ToInt32(TempData["vrVNo"]);
            var Posted = db.Database.SqlQuery<DateTime>("select top 1  case   when (SELECT count(*) as vdate FROM VouchersLog WHERE VId = @VoucherID)=0 then (SELECT TOP 1 vdate FROM vouchers WHERE voucherid = @VoucherID)  else (SELECT TOP 1 TransDateTime FROM VouchersLog WHERE VId = @VoucherID ORDER BY TransDateTime DESC) end   from VouchersLog", new SqlParameter("VoucherID", vno)).ToList();
            DateTime PostedDate = Posted.FirstOrDefault();
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            //if(VType=="DV" || VType=="CV")
            //rd.Load(Path.Combine(Server.MapPath("~/Reports/rptVoucherReceipt.rpt")));
            //else if (VType == "JV")
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptJVReceipt.rpt")));
            rd.SetDataSource(GetVoucherReceipt_Data("1", "P-JV"));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("PostedDate", PostedDate);
            //if(VType=="DV")
            //rd.SetParameterValue("VType", "Debit Voucher");
            //else if (VType == "CV")
            //    rd.SetParameterValue("VType", "Credit Voucher");
            //else if (VType == "JV")
            rd.SetParameterValue("VType", "Purchase Journal Voucher");
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptBDVReceipt()
        {
            string preparedby = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            int vno = Convert.ToInt32(TempData["vrVNo"]);
            var Posted = db.Database.SqlQuery<DateTime>("select top 1  case   when (SELECT count(*) as vdate FROM VouchersLog WHERE VId = @VoucherID)=0 then (SELECT TOP 1 vdate FROM vouchers WHERE voucherid = @VoucherID)  else (SELECT TOP 1 TransDateTime FROM VouchersLog WHERE VId = @VoucherID ORDER BY TransDateTime DESC) end   from VouchersLog", new SqlParameter("VoucherID", vno)).ToList();
            DateTime PostedDate = Posted.FirstOrDefault();
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            //if(VType=="DV" || VType=="CV")
           // rd.Load(Path.Combine(Server.MapPath("~/Reports/rptVoucherReceipt.rpt")));
            //else if (VType == "JV")
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptJVReceipt.rpt")));
            string TDate = Convert.ToString(TempData["Datefrom"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            int vid = Convert.ToInt32(TempData["title"]);
            string vchno = "BPV-" + vid;
            //var vchid = db.qryVoucherBooks.Where(x => x.voucherNo == vchno&&x.vDate==tdate).Select(x => x.VoucherID).FirstOrDefault();
            rd.SetDataSource(GetVoucherReceipt_Data(Convert.ToString(vno), "BD-JV"));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("vchno", vchno);
            rd.SetParameterValue("pby", preparedby);
            rd.SetParameterValue("PostedDate", PostedDate);
            //if(VType=="DV")
            //rd.SetParameterValue("VType", "Debit Voucher");
            //else if (VType == "CV")
            //    rd.SetParameterValue("VType", "Credit Voucher");
            //else if (VType == "JV")
            rd.SetParameterValue("VType", "Bank Debit Voucher");
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptBCVReceipt()
        {
            string preparedby = Convert.ToString(Session["username"]);
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            //if(VType=="DV" || VType=="CV")
           // rd.Load(Path.Combine(Server.MapPath("~/Reports/rptVoucherReceipt.rpt")));
            //else if (VType == "JV")
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptJVReceipt.rpt")));
            string TDate = Convert.ToString(TempData["Datefrom"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            int vno = Convert.ToInt32(TempData["vrVNo"]);
            int vid = Convert.ToInt32(TempData["title"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            string vchno = "BRV-" + vid;
            //var vchid = db.qryVoucherBooks.Where(x => x.voucherNo == vchno&&x.vDate==tdate).Select(x => x.VoucherID).FirstOrDefault();
            var Posted = db.Database.SqlQuery<DateTime>("select top 1  case   when (SELECT count(*) as vdate FROM VouchersLog WHERE VId = @VoucherID)=0 then (SELECT TOP 1 vdate FROM vouchers WHERE voucherid = @VoucherID)  else (SELECT TOP 1 TransDateTime FROM VouchersLog WHERE VId = @VoucherID ORDER BY TransDateTime DESC) end   from VouchersLog", new SqlParameter("VoucherID", vno)).ToList();
            DateTime PostedDate = Posted.FirstOrDefault();
            rd.SetDataSource(GetVoucherReceipt_Data(Convert.ToString(vno), "BC-JV"));
            string company = Convert.ToString(Session["Company"]); rd.SetParameterValue("Company", company);
            string Address = Convert.ToString(Session["Address"]); rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("vchno", vchno);
            rd.SetParameterValue("pby", preparedby);
            rd.SetParameterValue("PostedDate", PostedDate);
            //if(VType=="DV")
            //rd.SetParameterValue("VType", "Debit Voucher");
            //else if (VType == "CV")
            //    rd.SetParameterValue("VType", "Credit Voucher");
            //else if (VType == "JV")
            rd.SetParameterValue("VType", "Bank Credit Voucher");
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        /// <summary>
        /// //////////////////////////////Data Part
        /// </summary>
        SqlConnection Connection;
        SqlCommand Command;
        SqlDataAdapter da;
        string Qry = "";
        private dsCoa GetChartofAccount_Data()
        {
            try
            {
                dsCoa ds = new dsCoa();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                //                Qry = @"  select a2.AccountId,a2.Title,a2.Nature,a2.LevelKey,a2.AccType,a1.Title as Parent,a1.AccountId as ParentId from Accounts a1
                //inner join Accounts a2
                //on a1.AccountId=a2.ParentId
                //order by a1.AccountId,LevelKey
                //";
                Qry = @"select * from Accounts order by AccountId";
                Command.Connection = Connection;
                da = new SqlDataAdapter(Qry, dbconsql);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsPurchase GetPendingGRN()
        {
            try
            {
                dsPurchase ds = new dsPurchase();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.CommandTimeout = 300;
                Qry = @"SELECT GRN1.InvNo AS InvID, GRN1.Idate AS InvDate, GRN1.PartyBillNO AS YourBillNo,
      GRN1.PartyBillDate AS YourBillDate, GRN1.AccountId, Accounts.Title AS AccountTitle, GRN2.ProductID, 
      Products.ProductName AS ProductTitle,  (GRN2.WtDr+GRN2.WtCr) as Qty, GRN2.Rate, GRN2.Amount AS amount, 
      GRN1.InvTotal AS GrossAmount, GRN1.Discount as Disc, GRN1.SaleTaxAmt as SalesTaxAmt, 
      GRN1.NetTotal as NetAmount, GRN1.Brate, (GRN2.WtDr+GRN2.WtCr)/40 as VehNo, GRN2.GateNo,
      GRN2.QtyDr+ GRN2.QtyCr as Qty1, GRN1.ArrivalID as DispactchID,GRN1.Freight as Freight,
      GRN1.BagAmount as BagAmount,GRN1.BrokeryAmount as BrokeryAmount,GRN1.NumCol2 as LabourAmount,
      GRN1.MarketComAmount as MktComete , GRN1.BrokeryMode, GRN1.MillType, P.PO_No  FROM Products
      INNER JOIN Accounts INNER JOIN GRN1 ON Accounts.AccountID = GRN1.AccountId
      INNER JOIN GRN2 ON GRN1.InvNo = GRN2.InvNo ON Products.ProductID = GRN2.ProductID
      INNER JOIN ProductGroups ON Products.PG_ID = ProductGroups.PG_ID
      INNER JOIN ProductSubGroup ON Products.PT_ID = ProductSubGroup.PSG_Id 
	  left join Purchase1 P on P.PO_No = GRN1.InvNo where P.PO_No is  Null ";
                da = new SqlDataAdapter(Qry, Connection);
                da.SelectCommand.CommandTimeout = 300;
                da.Fill(ds, "qryPurchase");
                //da = new SqlDataAdapter(Sql2, Connection);
                //da.Fill(ds, "ado");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsAccountLedger GetTrialBalance_Data(DateTime TDate, string sort)
        {
            try
            {
                dsAccountLedger ds = new dsAccountLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Qry = @"  exec SPRW_GetTrialBalance @ToDate='" + TDate + "',@sort='" + sort + "'";
                Command.Connection = Connection;
                da = new SqlDataAdapter(Qry, dbconsql);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        //private dsAccountLedger GetTrialBalance_Data(DateTime TDate)
        //{
        //    try
        //    {
        //        dsAccountLedger ds = new dsAccountLedger();
        //        string dbconsql = Convert.ToString(Session["consql"]);
        //        Connection = new SqlConnection(dbconsql);
        //        Connection.Open();
        //        Command = new SqlCommand();
        //        Command.CommandType = CommandType.Text;
        //        //Qry = @"  exec SPRW_GetTrialBalance @ToDate='" + TDate + "',@sort='" + sort + "'";
        //        Qry = "SELECT AccountID, Title, SUM(debit) AS debit1, SUM(credit) AS credit1 FROM qryVoucherBook WHERE vdate <= '"+ TDate +"'  ";
        //        Command.Connection = Connection;
        //        da = new SqlDataAdapter(Qry, dbconsql);
        //        da.Fill(ds, ds.Tables[0].TableName);
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        Connection.Close();
        //    }
        //}
        private dsAccountLedger GetTrialBalanceBetween_Data(DateTime FDate, DateTime Tdate)
        {
            try
            {
                dsAccountLedger ds = new dsAccountLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Qry = @"  exec SPRW_GetTrialBalanceBetween @FromDate='" + FDate + "',@ToDate='" + Tdate + "'";
                Command.Connection = Connection;
                da = new SqlDataAdapter(Qry, dbconsql);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        public ActionResult rptLog()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string FDate = Convert.ToString(TempData["cbDatefrom"]);
            string tDate = Convert.ToString(TempData["cbDateto"]);
            string type = Convert.ToString(TempData["title"]);
            string excel = Convert.ToString(TempData["excel"]);
            DateTime todate = DateTime.ParseExact(tDate , "dd/MM/yyyy", null);
            DateTime Fromdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            //rd.Load(Path.Combine(Server.MapPath("~/Reports/rptLogInsert.rpt")));
            rd.Load(Path.Combine(Server.MapPath("~/Reports/rptVoucherLog.rpt")));
            rd.SetDataSource(GetLogData(todate.AddDays(1), Fromdate, type));
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);
            rd.SetParameterValue("prmTitle", company);
            rd.SetParameterValue("prmAddress", Address);
            if (type == "Insertion")
            {
                rd.SetParameterValue("stocktype", "Insertation Log");
            }
            else if (type == "Updation")
            {
                rd.SetParameterValue("stocktype", "Updation Log");
            }
            else if (type == "Deletion")
            {
               rd.SetParameterValue("stocktype", "Deletion Log");
            }
            else if (type == "Overall")
            {
                rd.SetParameterValue("stocktype", "Overall Log");
            }
           rd.SetParameterValue("To", todate);
           rd.SetParameterValue("From", Fromdate);
            if (excel == "Yes")
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/vnd.ms-excel");
            }
            else
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
            //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //stream.Seek(0, SeekOrigin.Begin);
            //return new FileStreamResult(stream, "application/pdf");
        }
        //private dsAccountLedger GetStockInHand_Data(DateTime Tdate, DateTime FDate, string type )
        //{
        //    try
        //    {
        //        dsAccountLedger ds = new dsAccountLedger();
        //        string dbconsql = Convert.ToString(Session["consql"]);
        //        Connection = new SqlConnection(dbconsql);
        //        Connection.Open();
        //        Command = new SqlCommand();
        //        Command.CommandType = CommandType.Text;
        //        Qry = @"  exec GetVouchersWithLogs @ToDate='" + Tdate + "', @FromDate='" + FDate + "',@type='" + type + "'";
        //        Command.Connection = Connection;
        //        da = new SqlDataAdapter(Qry, dbconsql);
        //        da.Fill(ds, ds.Tables[0].TableName);
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        Connection.Close();
        //    }
        //}
        // Function to get Log Data
        private dsAccount GetLogData(DateTime Tdate, DateTime FDate, string type)
        {
            try
            {
                dsAccount ds = new dsAccount();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                if (type == "Overall")
                {
                    Qry = @"  exec GetVouchersWithLogs2 @ToDate='" + Tdate + "', @FromDate='" + FDate + "'";
                }
                else
                {
                    Qry = @"  exec GetVouchersWithLogs @ToDate='" + Tdate + "', @FromDate='" + FDate + "',@type='" + type + "'";
                }
                da = new SqlDataAdapter(Qry, Connection);
                da.Fill(ds, "ado");
                //da = new SqlDataAdapter(Sql2, Connection);
                //da.Fill(ds, "ado");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsAccount GetAccountLedger(string id, DateTime fdate, DateTime tdate)
        {
            try
            {
                dsAccount ds = new dsAccount();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                string Sql = "select '" + fdate + "' as vDate ,'AOB' as voucherNo, 'Opening Balances' as narration,sum(debit) as debit, sum(credit) as credit,(sum(debit)-sum(credit)) as Closing from qryvoucherBook_New where accountId='" + id + "' and vdate< '" + fdate + "' ";
                string Sql1 = " Union All ";
                //Commented by Mutlib to removed Not Cleared Bank Vouchers
               // string Sql2 = "select vDate,voucherNo,narration,debit,credit,(isnull(debit,0)-isnull(credit,0)) as Closing from qryvoucherBook_New where accountId='" + id + "' and (Mode='Cleared' or Mode='' or Mode is NULL) and vdate between  '" + fdate + "' and '" + tdate + "' ";
                string Sql2 = "select vDate,voucherNo,narration,debit,credit,(isnull(debit,0)-isnull(credit,0)) as Closing from qryvoucherBook_New where accountId='" + id + "' and Mode NOT IN ('Post Dated', 'Deposited', 'Cheque Issue', 'Cancelled', 'Returned', 'Online') and vdate between  '" + fdate + "' and '" + tdate + "' ";
                string sql4 = Sql + Sql1 + Sql2;
                da = new SqlDataAdapter(sql4, Connection);
                da.Fill(ds, "ado");
                //da = new SqlDataAdapter(Sql2, Connection);
                //da.Fill(ds, "ado");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsPartyLedger GetPartyLedger(string id, DateTime fdate, DateTime tdate)
        {
            try
            {
                dsPartyLedger ds = new dsPartyLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql2 = "";
                string Sql = "";
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "select sum(debit) as dr, sum(credit) as cr from qryPartyLedger where accountId='" + id + "' and vdate< '" + fdate + "' ";
                Sql2 = "select * from qryPartyLedger where accountId='" + id + "' and vdate between  '" + fdate + "' and '" + tdate + "' ";
                Sql2 = Sql2 + " order by vdate,voucherNo ";
                using (SqlCommand command = new SqlCommand(Sql, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        decimal dblBalance = 0;
                        while (reader.Read())
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["vDate"] = fdate;
                            dr["voucherNo"] = " " + "OB";
                            dr["RefNo"] = "";
                            dr["narration"] = "Opening Balances";
                            dr["Product"] = "";
                            dr["GateNo"] = "";
                            dr["VehNo"] = "";
                            dr["Qty"] = 0;
                            dr["Weight"] = 0;
                            dr["Rate"] = 0;
                            dr["debit"] = reader["dr"] ?? 0;
                            dr["credit"] = reader["cr"] ?? 0;
                            dblBalance = dblBalance + Convert.ToDecimal(reader["dr"]) - Convert.ToDecimal(reader["cr"]);
                            dr["Closing"] = dblBalance;
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                }
                //----------------------------------------------------------
                using (SqlCommand command = new SqlCommand(Sql2, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        decimal dblBalance2 = 0;
                        while (reader.Read())
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["vDate"] = reader["vdate"];
                            dr["voucherNo"] = Convert.ToString(reader["voucherno"]);
                            dr["RefNo"] = Convert.ToString(reader["reference"]);
                            dr["narration"] = Convert.ToString(reader["narration"]);
                            dr["Product"] = Convert.ToString(reader["Product"]);
                            dr["GateNo"] = Convert.ToString(reader["GateNo"]);
                            dr["VehNo"] = Convert.ToString(reader["VehNo"]);
                            dr["Qty"] = Convert.ToDecimal(reader["qty"]);
                            dr["Weight"] = Convert.ToDecimal(reader["Weight"]);
                            dr["Rate"] = Convert.ToDecimal(reader["Rate"] ?? 0);
                            dr["debit"] = Convert.ToDecimal(reader["Debit"] ?? 0);
                            dr["credit"] = Convert.ToDecimal(reader["Credit"] ?? 0);
                            dblBalance2 = dblBalance2 + Convert.ToDecimal(reader["Debit"] ?? 0) - Convert.ToDecimal(reader["Credit"] ?? 0);
                            dr["Closing"] = dblBalance2;
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                }
                //da = new SqlDataAdapter(Sql2, Connection);
                //da.Fill(ds, "ado");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsPartyLedgersummary GetPartyLedgersummary(DateTime tdate)
        {
            try
            {
                dsPartyLedgersummary ds = new dsPartyLedgersummary();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql = "";
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT QryPartyLedger.AccountID, QryPartyLedger.Title, QryPartyLedger.Product,ISNULL(SUM(Qty), 0) AS Qty1,ISNULL(SUM(Weight), 0) AS Weight1,ISNULL(SUM(debit), 0) AS debit1, ISNULL(SUM(credit), 0) AS credit1 FROM qryPartyLedger WHERE vdate <= '" + tdate + "' ";
                Sql = Sql + " and accountid <> '0' and (accountid not like '131%' and accountid not like '3%' and accountid<>'111') ";
                Sql = Sql + " GROUP BY AccountID, Title,Product";
                using (SqlCommand command = new SqlCommand(Sql, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToDecimal(reader["qty1"]) != 0 || Convert.ToDecimal(reader["Weight1"]) != 0 || Convert.ToDecimal(reader["debit1"]) != 0 || Convert.ToDecimal(reader["Credit1"]) != 0)
                            {
                                DataRow dr = ds.Tables[0].NewRow();
                                dr["AccountID"] = reader["AccountID"];
                                dr["Title"] = reader["title"];
                                dr["Product"] = reader["Product"];
                                dr["Qty"] = reader["qty1"] ?? 0;
                                dr["Weight"] = reader["Weight1"] ?? 0;
                                dr["Debit"] = reader["debit1"] ?? 0;
                                dr["credit"] = reader["Credit1"] ?? 0;
                                ds.Tables[0].Rows.Add(dr);
                            }
                        }
                    }
                }
                //da = new SqlDataAdapter(Sql2, Connection);
                //da.Fill(ds, "ado");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsPurchase GetGRNBetween(DateTime fdate, DateTime tdate)
        {
            try
            {
                dsPurchase ds = new dsPurchase();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.CommandTimeout = 300;
                Qry = @"SELECT  Gate.GateNo  AS InvID,Accounts.Title AS AccountTitle, 
                products.ItemDetail AS ProductTitle, Gate.Itype,Gate.Itype2, Gate3.Quantity as Qty1,
                GRN.PurAccount as GateNo, Gate3.StdWeight as Qty, Gate.KatlaNo as PT_ID FROM  Products 
INNER JOIN (Accounts INNER JOIN (Gate3 INNER JOIN (Gate LEFT JOIN BardanaTypes ON Gate.BtypeId = BardanaTypes.BTypeID)
ON Gate3.GateNo = Gate.GateNo) ON Accounts.AccountID = Gate.IntCol1) ON Products.ProductID = Gate3.ProductID 
inner join GRN1 GRN on Gate.sno= GRN.Sno
where   Gate.Itype='INR' and Gate.Itype2='GPR' and Gate.GDate between'" + fdate + "' and '" + tdate + "'";
                da = new SqlDataAdapter(Qry, Connection);
                da.SelectCommand.CommandTimeout = 300;
                da.Fill(ds, "qryPurchase");
                //da = new SqlDataAdapter(Sql2, Connection);
                //da.Fill(ds, "ado");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private void getaccountBalance(string AccId, string aType, DateTime ToDate, bool IsLevelOne)
        {
            string dbconsql = Convert.ToString(Session["consql"]);
            Connection = new SqlConnection(dbconsql);
            Connection.Open();
            Command = new SqlCommand();
            Command.CommandType = CommandType.Text;
            Command.Connection = Connection;
            string Sql = "";
            int Lenn = 0;
            Lenn = AccId.Length;
            if (IsLevelOne == true)
            {
                Sql = "select sum(debit) as dr, sum(credit) as cr from qryVoucherBook where left(parentid,1)='" + AccId + "' and vdate<='" + ToDate + "' ";
            }
            else if (aType.ToUpper() == "GROUP")
            {
                Sql = "SELECT AccountID, Title From Accounts1 WHERE (ParentID LIKE '" + AccId + "') AND (Type = 'Group')";
                SqlDataAdapter adapter = new SqlDataAdapter(Sql, Connection);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);
                if (datatable.Rows.Count > 0)
                {
                    Sql = "select sum(debit) as dr, sum(credit) as cr from qryVoucherBook where left(parentid," + Lenn + ")='" + AccId + "' and vdate<='" + ToDate + "'  ";
                }
                else
                {
                    Sql = "select sum(debit) as dr, sum(credit) as cr from qryVoucherBook where parentid='" + AccId + "' and vdate<='" + ToDate + "'  ";
                }
            }
            else
            {
                Sql = "select sum(debit) as dr, sum(credit) as cr from qryVoucherBook where accountId='" + AccId + "' and vdate<='" + ToDate + "'  ";
            }
            SqlDataAdapter adapter2 = new SqlDataAdapter(Sql, Connection);
            DataTable datatable2 = new DataTable();
            adapter2.Fill(datatable2);
            if (datatable2.Rows.Count > 0)
            {
                for (int i = 0; i < datatable2.Rows.Count - 1; i++)
                {
                    dblDrAmount = Convert.ToDecimal(datatable2.Rows[i][0]);
                    dblCrAmount = Convert.ToDecimal(datatable2.Rows[i][1]);
                }
            }
        }
        private dsReceivablePayable GetReceivablePayable2(DateTime Tdate)
        {
            try
            {
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                DataRow dr = null;
                dsReceivablePayable ds = new dsReceivablePayable();
                string Sql = "";
                Sql = "select * from accounts1 where alevel=1   AND isParty = 1  order by accountid";
                SqlDataAdapter adapter = new SqlDataAdapter(Sql, Connection);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);
                if (datatable.Rows.Count > 0)
                {
                    for (int i = 0; i < datatable.Rows.Count - 1; i++)
                    {
                        getaccountBalance(Convert.ToString(datatable.Rows[i][0]), Convert.ToString(datatable.Rows[i][6]), Tdate, true);
                        dr = ds.Tables[0].NewRow();
                        dr["accountid"] = datatable.Rows[i][0];
                        dr["title"] = datatable.Rows[i][4];
                        dr["type"] = datatable.Rows[i][6];
                        dr["alevel"] = datatable.Rows[i][3];
                        dr["isParty"] = datatable.Rows[i][7];
                        dr["debit"] = dblDrAmount - dblCrAmount >= 0 ? dblDrAmount - dblCrAmount : 0;
                        dr["credit"] = dblDrAmount - dblCrAmount < 0 ? dblCrAmount - dblDrAmount : 0;
                        ds.Tables[0].Rows.Add(dr);
                        //------------------------------ Level 2------------------------------
                        SqlDataAdapter adapter2 = new SqlDataAdapter("select * from accounts1 where parentId='" + Convert.ToString(datatable.Rows[i][0]) + "'       order by sno", Connection);
                        DataTable datatable2 = new DataTable();
                        adapter2.Fill(datatable2);
                        if (datatable2.Rows.Count > 0)
                        {
                            for (int j = 0; j < datatable2.Rows.Count; j++)
                            {
                                if (Convert.ToString(datatable2.Rows[j][6]) == "Group")
                                {
                                    dr = ds.Tables[0].NewRow();
                                    dr["accountid"] = "";
                                    dr["title"] = "";
                                    dr["type"] = "";
                                    dr["alevel"] = 1;
                                    dr["isParty"] = true;
                                    dr["debit"] = 0;
                                    dr["credit"] = 0;
                                    ds.Tables[0].Rows.Add(dr);
                                }
                                getaccountBalance(Convert.ToString(datatable2.Rows[j][0]), Convert.ToString(datatable2.Rows[j][6]), Tdate, true);
                                dr = ds.Tables[0].NewRow();
                                dr["accountid"] = datatable2.Rows[j][0];
                                dr["title"] = datatable2.Rows[j][4];
                                dr["type"] = datatable2.Rows[j][6];
                                dr["alevel"] = datatable2.Rows[j][3];
                                dr["isParty"] = datatable2.Rows[j][7];
                                dr["debit"] = dblDrAmount - dblCrAmount >= 0 ? dblDrAmount - dblCrAmount : 0;
                                dr["credit"] = dblDrAmount - dblCrAmount < 0 ? dblCrAmount - dblDrAmount : 0;
                                ds.Tables[0].Rows.Add(dr);
                                //--------------------------------------------Level 3--------------------------------
                                SqlDataAdapter adapter3 = new SqlDataAdapter("select * from accounts1 where parentId='" + Convert.ToString(datatable2.Rows[j][0]) + "'            order by sno", Connection);
                                DataTable datatable3 = new DataTable();
                                adapter3.Fill(datatable3);
                                if (datatable3.Rows.Count > 0)
                                {
                                    for (int k = 0; k < datatable3.Rows.Count; k++)
                                    {
                                        if (Convert.ToString(datatable3.Rows[k][6]) == "Group")
                                        {
                                            dr = ds.Tables[0].NewRow();
                                            dr["accountid"] = "";
                                            dr["title"] = "";
                                            dr["type"] = "";
                                            dr["alevel"] = 1;
                                            dr["isParty"] = true;
                                            dr["debit"] = 0;
                                            dr["credit"] = 0;
                                            ds.Tables[0].Rows.Add(dr);
                                        }
                                        getaccountBalance(Convert.ToString(datatable3.Rows[k][0]), Convert.ToString(datatable3.Rows[k][6]), Tdate, true);
                                        dr = ds.Tables[0].NewRow();
                                        dr["accountid"] = datatable3.Rows[k][0];
                                        dr["title"] = datatable3.Rows[k][4];
                                        dr["type"] = datatable3.Rows[k][6];
                                        dr["alevel"] = datatable3.Rows[k][3];
                                        dr["isParty"] = datatable3.Rows[k][7];
                                        dr["debit"] = dblDrAmount - dblCrAmount >= 0 ? dblDrAmount - dblCrAmount : 0;
                                        dr["credit"] = dblDrAmount - dblCrAmount < 0 ? dblCrAmount - dblDrAmount : 0;
                                        ds.Tables[0].Rows.Add(dr);
                                        //--------------------------------------------Level 4--------------------------------
                                        SqlDataAdapter adapter4 = new SqlDataAdapter("select * from accounts1 where parentId='" + Convert.ToString(datatable3.Rows[k][0]) + "'     order by sno", Connection);
                                        DataTable datatable4 = new DataTable();
                                        adapter4.Fill(datatable4);
                                        if (datatable4.Rows.Count > 0)
                                        {
                                            for (int l = 0; l < datatable4.Rows.Count; l++)
                                            {
                                                if (Convert.ToString(datatable4.Rows[l][6]) == "Group")
                                                {
                                                    dr = ds.Tables[0].NewRow();
                                                    dr["accountid"] = "";
                                                    dr["title"] = "";
                                                    dr["type"] = "";
                                                    dr["alevel"] = 1;
                                                    dr["isParty"] = true;
                                                    dr["debit"] = 0;
                                                    dr["credit"] = 0;
                                                    ds.Tables[0].Rows.Add(dr);
                                                }
                                                getaccountBalance(Convert.ToString(datatable4.Rows[l][0]), Convert.ToString(datatable4.Rows[l][6]), Tdate, true);
                                                dr = ds.Tables[0].NewRow();
                                                dr["accountid"] = datatable4.Rows[l][0];
                                                dr["title"] = datatable4.Rows[l][4];
                                                dr["type"] = datatable4.Rows[l][6];
                                                dr["alevel"] = datatable4.Rows[l][3];
                                                dr["isParty"] = datatable4.Rows[l][7];
                                                dr["debit"] = dblDrAmount - dblCrAmount >= 0 ? dblDrAmount - dblCrAmount : 0;
                                                dr["credit"] = dblDrAmount - dblCrAmount < 0 ? dblCrAmount - dblDrAmount : 0;
                                                ds.Tables[0].Rows.Add(dr);
                                                //--------------------------------------------Level 5--------------------------------
                                                SqlDataAdapter adapter5 = new SqlDataAdapter("select * from accounts1 where parentId='" + Convert.ToString(datatable4.Rows[l][0]) + "'     order by sno", Connection);
                                                DataTable datatable5 = new DataTable();
                                                adapter5.Fill(datatable5);
                                                if (datatable5.Rows.Count > 0)
                                                {
                                                    for (int m = 0; m < datatable5.Rows.Count; m++)
                                                    {
                                                        if (Convert.ToString(datatable5.Rows[m][6]) == "Group")
                                                        {
                                                            dr = ds.Tables[0].NewRow();
                                                            dr["accountid"] = "";
                                                            dr["title"] = "";
                                                            dr["type"] = "";
                                                            dr["alevel"] = 1;
                                                            dr["isParty"] = true;
                                                            dr["debit"] = 0;
                                                            dr["credit"] = 0;
                                                            ds.Tables[0].Rows.Add(dr);
                                                        }
                                                        getaccountBalance(Convert.ToString(datatable5.Rows[m][0]), Convert.ToString(datatable5.Rows[m][6]), Tdate, true);
                                                        dr = ds.Tables[0].NewRow();
                                                        dr["accountid"] = datatable5.Rows[m][0];
                                                        dr["title"] = datatable5.Rows[m][4];
                                                        dr["type"] = datatable5.Rows[m][6];
                                                        dr["alevel"] = datatable5.Rows[m][3];
                                                        dr["isParty"] = datatable5.Rows[m][7];
                                                        dr["debit"] = dblDrAmount - dblCrAmount >= 0 ? dblDrAmount - dblCrAmount : 0;
                                                        dr["credit"] = dblDrAmount - dblCrAmount < 0 ? dblCrAmount - dblDrAmount : 0;
                                                        ds.Tables[0].Rows.Add(dr);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                foreach (DataRow row in ds.Tables["ado"].Rows)
                {
                    if (Convert.ToBoolean(row["isParty"]) == false && (Convert.ToDecimal(row["debit"]) == 0 && Convert.ToDecimal(row["credit"]) == 0))
                    {
                        ds.Tables["ado"].Rows.Remove(row);
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsAccountReconcilation GetAccountReconcilation(string id, DateTime fdate, DateTime tdate)
        {
            try
            {
                dsAccountReconcilation ds = new dsAccountReconcilation();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql2 = "";
                string Sql = "";
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                //Sql2 = Sql2 + " select sum(qtydr) as qdr,sum(qtycr) as qcr, sum(wtdr) as wdr,sum(wtcr) as wcr from qryInvBookFinalWeight ";
                //Sql2 = Sql2 + " where  invdate< '" + FDate + "' and ProductID= " + AccountId;
                Sql = "select sum(debit) as dr, sum(credit) as cr from qryvoucherBook where accountId='" + id + "' and vdate< '" + fdate + "' ";
                using (SqlCommand command = new SqlCommand(Sql, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["Date1"] = fdate;
                            dr["Ref"] = "OB";
                            dr["ChqNo"] = "Opening";
                            dr["chqAmount"] = 0;
                            dr["ChqStatus"] = "";
                            dr["nar"] = "Opening Balances";
                            dr["debit"] = reader.GetValue(0);
                            dr["credit"] = reader.GetValue(1);
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                }
                //----------------------------------------------------------
                Sql2 = "select * from qryvoucherBook where accountId='" + id + "' and vdate between  '" + fdate + "' and '" + tdate + "' and voucherno<>'OB' and left(reference,4)<>'Bank' ";
                Sql2 = Sql2 + " order by vdate";
                using (SqlCommand command = new SqlCommand(Sql2, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["Date1"] = Convert.ToDateTime(reader["vdate"]);
                            dr["Ref"] = reader.GetValue(1);
                            dr["ChqNo"] = "";
                            dr["chqAmount"] = 0;
                            dr["ChqStatus"] = "";
                            dr["nar"] = Convert.ToString(reader["narration"]);
                            dr["debit"] = Convert.ToDecimal(reader["debit"]);
                            dr["credit"] = Convert.ToDecimal(reader["credit"]);
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                }
                //----------------------------------------------------------
                Sql2 = "SELECT Vouchers_Bank.Clearance_Date, Vouchers_Bank.BankVch_Id, Vouchers_Bank.Narration, Vouchers_Bank2.Cheque_no, Vouchers_Bank2.Amount , Vouchers_Bank.Status, Vouchers_Bank.Type FROM Vouchers_Bank INNER JOIN Vouchers_Bank2 ON Vouchers_Bank.BankVch_Id = Vouchers_Bank2.BankVch_id AND Vouchers_Bank.Type = Vouchers_Bank2.Type WHERE (UPPER(Vouchers_Bank.Status) = 'CLEARED') and Vouchers_Bank2.dr_accountid ='" + id + "' and Vouchers_Bank.bv_date between  '" + fdate + "' and '" + tdate + "' order by Vouchers_Bank.bv_date";
                using (SqlCommand command = new SqlCommand(Sql2, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["Date1"] = Convert.ToDateTime(reader["clearance_date"]);
                            dr["Ref"] = "BankVch-" + Convert.ToString(reader["BankVch_Id"]);
                            dr["ChqNo"] = Convert.ToString(reader["cheque_no"]);
                            dr["chqAmount"] = Convert.ToDecimal(reader["Amount"]);
                            dr["ChqStatus"] = Convert.ToString(reader["Status"]);
                            dr["nar"] = Convert.ToString(reader["narration"]);
                            dr["debit"] = Convert.ToDecimal(reader["Amount"]);
                            dr["credit"] = 0;
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                }
                //----------------------------------------------------------
                //Sql2 = "SELECT Vouchers_Bank.Clearance_Date, Vouchers_Bank.BankVch_Id, Vouchers_Bank.Narration, Vouchers_Bank2.Cheque_no, Vouchers_Bank2.Amount , Vouchers_Bank.Status, Vouchers_Bank.Type FROM Vouchers_Bank INNER JOIN Vouchers_Bank2 ON Vouchers_Bank.BankVch_Id = Vouchers_Bank2.BankVch_id AND Vouchers_Bank.Type = Vouchers_Bank2.Type WHERE (UPPER(Vouchers_Bank.Status) = 'CLEARED') and Vouchers_Bank2.dr_accountid ='" + id + "' and Vouchers_Bank.bv_date between  '" + fdate + "' and '" + tdate + "' order by Vouchers_Bank.bv_date";
                Sql2 = "SELECT Vouchers_Bank.Clearance_Date, Vouchers_Bank.BankVch_Id, Vouchers_Bank.Narration, Vouchers_Bank2.Cheque_no, Vouchers_Bank2.Amount , Vouchers_Bank.Status, Vouchers_Bank.Type FROM Vouchers_Bank INNER JOIN Vouchers_Bank2 ON Vouchers_Bank.BankVch_Id = Vouchers_Bank2.BankVch_id AND Vouchers_Bank.Type = Vouchers_Bank2.Type WHERE (UPPER(Vouchers_Bank.Status) = 'CLEARED') and Vouchers_Bank2.Cr_accountid ='" + id + "' and Vouchers_Bank.bv_date between  '" + fdate + "' and '" + tdate + "'  order by Vouchers_Bank.bv_date";
                using (SqlCommand command = new SqlCommand(Sql2, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["Date1"] = Convert.ToDateTime(reader["clearance_date"]);
                            dr["Ref"] = "BankVch-" + Convert.ToString(reader["BankVch_Id"]);
                            dr["ChqNo"] = Convert.ToString(reader["cheque_no"]);
                            dr["chqAmount"] = Convert.ToDecimal(reader["Amount"]);
                            dr["ChqStatus"] = Convert.ToString(reader["Status"]);
                            dr["nar"] = Convert.ToString(reader["narration"]);
                            dr["debit"] = 0;
                            dr["credit"] = Convert.ToDecimal(reader["Amount"]);
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                }
                //----------------------------------------------------------
                Sql2 = "SELECT Vouchers_Bank.bv_date, Vouchers_Bank.BankVch_Id, Vouchers_Bank.Narration, Vouchers_Bank2.Cheque_no, Vouchers_Bank2.Amount , Vouchers_Bank.Status, Vouchers_Bank.Type FROM Vouchers_Bank INNER JOIN Vouchers_Bank2 ON Vouchers_Bank.BankVch_Id = Vouchers_Bank2.BankVch_id AND Vouchers_Bank.Type = Vouchers_Bank2.Type WHERE (UPPER(Vouchers_Bank.Status)<>'CLEARED') and (Vouchers_Bank2.Dr_accountid ='" + id + "' Or Vouchers_Bank2.Cr_accountid ='" + id + "') and Vouchers_Bank.bv_date between  '" + fdate + "' and '" + tdate + "' order by Vouchers_Bank.bv_date";
                using (SqlCommand command = new SqlCommand(Sql2, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["Date1"] = Convert.ToDateTime(reader["bv_date"]);
                            dr["Ref"] = "BankVch-" + Convert.ToString(reader["BankVch_Id"]);
                            dr["ChqNo"] = Convert.ToString(reader["cheque_no"]);
                            dr["chqAmount"] = Convert.ToDecimal(reader["Amount"]);
                            dr["ChqStatus"] = Convert.ToString(reader["Status"]);
                            dr["nar"] = Convert.ToString(reader["narration"]);
                            dr["debit"] = 0;
                            dr["credit"] = 0;
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                }

                 Sql = "select '" + fdate + "' as Date1 ,'AOB' as Ref, 'Opening Balances' as nar,sum(debit) as debit, sum(credit) as credit,(sum(debit)-sum(credit)) as Closing,NULL as ChqStatus,NULL as ChqNo,0 as chqamount from qryvoucherBook_New where accountId='" + id + "' and vdate< '" + fdate + "' ";
                string Sql1 = " Union All ";
                //Commented by Mutlib to removed Not Cleared Bank Vouchers
                // string Sql2 = "select vDate,voucherNo,narration,debit,credit,(isnull(debit,0)-isnull(credit,0)) as Closing from qryvoucherBook_New where accountId='" + id + "' and (Mode='Cleared' or Mode='' or Mode is NULL) and vdate between  '" + fdate + "' and '" + tdate + "' ";
                Sql2 = "select vDate as Date1,voucherNo as Ref,narration as nar,debit,credit,(isnull(debit,0)-isnull(credit,0)) as Closing,Mode as ChqStatus,RefNo as ChqNo,(isnull(debit,0)-isnull(credit,0)) as chqamount from qryvoucherBook_New  where accountId='" + id + "' and Mode IN ('Post Dated', 'Deposited', 'Cheque Issue', 'Cancelled', 'Returned', 'Online') and vdate between  '" + fdate + "' and '" + tdate + "' ";
                string sql4 =  Sql2;

                da = new SqlDataAdapter(sql4, Connection);
                da.Fill(ds, "ado");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsAging GetAgingData(string id, string title, string type, DateTime tdate)
        {
            try
            {
                dsAging ds = new dsAging();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.CommandTimeout = 300;
                if (type == "ARO")
                {
                    Qry = @"Exec SP_RPTAging1 '" + id + "','" + tdate + "'";
                }
                else if (type == "APO")
                {
                    Qry = @"Exec SP_Aging_PayAble_OverAll  '" + id + "','" + tdate + "'";
                }
                else if (type == "ARS")
                {
                    Qry = @"Exec SP_Aging_Receivable_PartyWise '" + id + "','" + tdate + "'";
                }
                else if (type == "APS")
                {
                    Qry = @"Exec SP_Aging_PayAble_PartyWise '" + id + "','" + tdate + "'";
                }
                da = new SqlDataAdapter(Qry, Connection);
                da.SelectCommand.CommandTimeout = 300;
                da.Fill(ds, "ado");
                //da = new SqlDataAdapter(Sql2, Connection);
                //da.Fill(ds, "ado");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsGPEmployee GetGatePassParties(int id)
        {
            try
            {
                dsGPEmployee ds = new dsGPEmployee();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                string Sql = "select * from PartiesGatePass where CompID='" + id + "'";
                da = new SqlDataAdapter(Sql, Connection);
                da.Fill(ds, "Employ");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsGPEmployee GetGatePassEmployee(int id)
        {
            try
            {
                dsGPEmployee ds = new dsGPEmployee();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                string Sql = "select * from Employ where CompID='" + id + "'";
                da = new SqlDataAdapter(Sql, Connection);
                da.Fill(ds, "Employ");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsAccount GetCashBookDayBook(int strid, DateTime fdate, DateTime tdate)
        {
            try
            {
                string Sql = "";
                dsAccount ds = new dsAccount();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                if (strid == 1)
                {
                    Sql = "select * from qryvoucherBook_new where vdate between  '" + fdate + "' and '" + tdate + "'and  accountId<>'126212' and VoucherType IN ('CPV','CRV','GDV','GCV','BPV','BRV','WCPV','WCRV','WGDV','WGCV','WBPV','WBRV')and VoucherID IN (SELECT VoucherID From qryvoucherBook_new WHERE VoucherType IN ('CPV','CRV','GDV','GCV','BPV','BRV' , 'WCPV','WCRV','WGDV','WGCV','WBPV','WBRV') AND (AccountID ='126212') AND (vdate between  '" + fdate + "' and '" + tdate + "'))";
                    //Sql = "select * from qryvoucherBook where vdate between  '" + fdate + "' and '" + tdate + "' and accountId<>'126212' and left(voucherno,3) IN ('CPV','CRV','GDV','GCV','BPV','BRV') and VoucherID IN (SELECT VoucherID From qryVoucherBook WHERE (LEFT(voucherNo, 3) IN ('CPV','CRV','GDV','GCV','BPV','BRV')) AND (AccountID ='126212') AND (vdate between  '" + fdate + "' and '" + tdate + "'))";
                }
                else
                {
                    Sql = "select * from qryvoucherBook_new where vdate between  '" + fdate + "' and '" + tdate + "' and accountId<>'126212' and VoucherType IN ('CPV','CRV','JJV','GDV','GCV','GJV','PJV','SJV','BPV','BRV','HBC','HBD','WCPV','WCRV','WGDV','WGCV','WBPV','WBRV','WJJV')";
                }
                da = new SqlDataAdapter(Sql, Connection);
                da.Fill(ds, "ado");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsCashBookDayBookDetail GetCashBookDayBookDetail(int strid, DateTime fdate, DateTime tdate)
        {
            try
            {
                dsCashBookDayBookDetail ds = new dsCashBookDayBookDetail();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql = "";
                string Sql2 = "";
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "select ISNUll(sum(debit),0) as dr, isnull(sum(credit),0) as cr from qryPartyLedger where accountId='" + 111 + "' and vdate< '" + fdate + "' ";
                Sql2 = Sql2 + " select * from qryPartyLedger where accountId<>'" + 111 + "' and vdate between  '" + fdate + "' and '" + tdate + "' ";
                Sql2 = Sql2 + " and (left(Reference,2)<>'PG' or left(Reference,3)<>'PIS' or left(Reference,3)<>'PIC') ";
                Sql2 = Sql2 + " order by vdate,voucherNo ";
                using (SqlCommand command = new SqlCommand(Sql, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        decimal dblBalance = 0;
                        while (reader.Read())
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["vDate"] = fdate;
                            dr["voucherNo"] = " " + "OB";
                            dr["RefNo"] = "";
                            dr["narration"] = "Opening Balances";
                            dr["Product"] = "";
                            dr["VehNo"] = "";
                            dr["Qty"] = 0;
                            dr["Weight"] = 0;
                            dr["Rate"] = 0;
                            dr["debit"] = reader["cr"] ?? 0;
                            dr["credit"] = reader["dr"] ?? 0;
                            dblBalance = dblBalance + ((Convert.ToDecimal(reader["dr"])) - (Convert.ToDecimal(reader["cr"])));
                            dr["Closing"] = dblBalance;
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                }
                //----------------------------------------------------------
                using (SqlCommand command = new SqlCommand(Sql2, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        decimal dblBalance2 = 0;
                        while (reader.Read())
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["vDate"] = reader["vdate"];
                            dr["voucherNo"] = reader["voucherno"];
                            dr["RefNo"] = reader["reference"];
                            dr["narration"] = Convert.ToString(reader["AccountID"]) + " - " + Convert.ToString(reader["Title"]) + " " + Convert.ToString(reader["narration"]);
                            dr["Product"] = reader["Product"];
                            dr["VehNo"] = reader["VehNo"];
                            dr["Qty"] = reader["qty"] ?? 0;
                            dr["Weight"] = reader["Weight"] ?? 0;
                            dr["Rate"] = reader["Rate"] ?? 0;
                            dr["debit"] = reader["debit"] ?? 0;
                            dr["credit"] = reader["credit"] ?? 0;
                            dblBalance2 = dblBalance2 + Convert.ToDecimal(reader["debit"]) - Convert.ToDecimal(reader["credit"]);
                            dr["Closing"] = dblBalance2;
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                }
                //da = new SqlDataAdapter(Sql2, Connection);
                //da.Fill(ds, "ado");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsDayBookRecovered GetDayBookRecovered(int strid, DateTime fdate, DateTime tdate)
        {
            try
            {
                dsDayBookRecovered ds = new dsDayBookRecovered();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql = "";
                string Sql2 = "";
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql2 = "select ISNULL(sum(debit),0) as dr,ISNULL(sum(credit),0) as cr from qryvoucherBook_New where  accountId<>'" + 030605010001 + "' and vdate< '" + fdate + "' ";
                Sql = "select * from qryvoucherBook_New where vdate between  '" + fdate + "' and '" + tdate + "' and Vouchertype IN ('HOCPV','FOCPV','HOCRV','FOCRV','JJV','GDV','GCV','GJV','PJV','SJV','BPV','BRV','HBC','HBD')";
                Sql = Sql + " order by vdate,voucherNo";
                using (SqlCommand command = new SqlCommand(Sql, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        decimal dblBalance = 0;
                        while (reader.Read())
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["vDate"] = reader["vDate"];
                            dr["voucherNo"] = reader["voucherNo"];
                            dr["AccountID"] = reader["AccountID"];
                            dr["ParentID"] = reader["ParentID"];
                            dr["Title"] = reader["Title"];
                            dr["narration"] = reader["narration"];
                            //dr["Opening"] = reader["Opening"]??0;
                            dr["Opening"] = 0;
                            dr["debit"] = reader["debit"] ?? 0;
                            dr["credit"] = reader["credit"] ?? 0;
                            dblBalance = dblBalance + ((Convert.ToDecimal(reader["debit"] ?? 0)) - (Convert.ToDecimal(reader["credit"] ?? 0)));
                            dr["Closing"] = dblBalance;
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                }
                //----------------------------------------------------------
                using (SqlCommand command = new SqlCommand(Sql2, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        decimal dblBalance2 = 0;
                        while (reader.Read())
                        {
                            dblBalance2 = dblBalance2 + Convert.ToDecimal(reader["dr"]) - Convert.ToDecimal(reader["cr"]);
                            dblOpening = dblBalance2;
                            ;
                        }
                    }
                }
                //da = new SqlDataAdapter(Sql2, Connection);
                //da.Fill(ds, "ado");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsAccount GetJournalBook(int strid, DateTime fdate, DateTime tdate)
        {
            try
            {
                string Sql = "";
                dsAccount ds = new dsAccount();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                if (strid == 1)
                {
                    Sql = "SELECT     vDate, voucherNo, AccountID, Title, narration, Debit, Credit, 0 AS Closing, MONTH(vDate) AS MonDate FROM qryVoucherBook where vdate between  '" + fdate + "' and '" + tdate + "' and VoucherType IN ('JJV','BPV','BRV','WJJV','WBPV','WBRV') ";
                    //Sql = "SELECT     vDate, voucherNo, AccountID, Title, narration, Debit, Credit, 0 AS Closing, MONTH(vDate) AS MonDate FROM qryVoucherBook where vdate between  '" + fdate + "' and '" + tdate + "'  and( VoucherType='JJV' OR VoucherType='WJJV') ";
                }
                else
                {
                    Sql = "select * from qryvoucherBook where vdate between  '" + fdate + "' and '" + tdate + "' and accountId<>'126212' and VoucherType IN ('CPV','CRV','JJV','GDV','GCV','GJV','PJV','SJV','BPV','BRV','HBC','HBD','WCPV','WCRV','WJJV','WGDV','WGCV','WGJV','WPJV','WSJV','WBPV','WBRV','WHBC','WHBD')";
                }
                da = new SqlDataAdapter(Sql, Connection);
                da.Fill(ds, "ado");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsBardana GetBardana(string OrderId, string type)
        {
            try
            {
                string Sql = "";
                dsBardana ds = new dsBardana();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                if (type == "Ba")
                {
                    Sql = @"SELECT Sale1.InvNo as BardanaID, Sale1.Idate as Bdate, Sale1.AccountId as AccountID, Accounts.Title as Party, Sale2.BTypeID, BardanaTypes.Title as BType, Sale2.QtyDr as QtyDr, Sale2.QtyCr as QtyCr, Sale1.YourBillNo as Remarks, Sale2.GateNo as GateNo FROM (Products INNER JOIN (Accounts INNER JOIN (Sale1 INNER JOIN Sale2 ON Sale1.InvNo = Sale2.InvNo) ON Accounts.AccountId = Sale1.AccountId) ON Products.ProductID = Sale2.ProductID) INNER JOIN BardanaTypes ON Sale2.BTypeID = BardanaTypes.BTypeID where Sale1.InvNo='FS-" + OrderId + "'";
                }
                if (type == "Bar")
                {
                    Sql = @"SELECT Sale1.InvNo as BardanaID, Sale1.Idate as Bdate, Sale1.AccountId as AccountID, Accounts.Title as Party, Sale2.BTypeID, BardanaTypes.Title as BType, Sale2.QtyDr as QtyDr, Sale2.QtyCr as QtyCr, Sale1.YourBillNo as Remarks, Sale2.GateNo as GateNo FROM (Products INNER JOIN (Accounts INNER JOIN (Sale1 INNER JOIN Sale2 ON Sale1.InvNo = Sale2.InvNo) ON Accounts.AccountId = Sale1.AccountId) ON Products.ProductID = Sale2.ProductID) INNER JOIN BardanaTypes ON Sale2.BTypeID = BardanaTypes.BTypeID where Sale1.InvNo='FSR-" + OrderId + "'";
                }
                da = new SqlDataAdapter(Sql, Connection);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsSale GetSaleOrder(string OrderId, string type)
        {
            try
            {
                string Sql = "";
                dsSale ds = new dsSale();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                if (type == "SO")
                {
                    Sql = @"SELECT SaleOrder1.InvNo AS InvID, SaleOrder1.Idate AS InvDate, SaleOrder1.YourBillNo AS YourBillNo, SaleOrder1.YourBillDate AS YourBillDate, SaleOrder1.AccountId, Accounts.Title AS AccountTitle, SaleOrder2.ProductID, Products.ProductName AS ProductTitle, SaleOrder2.WtDr + SaleOrder2.WtCr AS Qty, SaleOrder2.Rate,SaleOrder2.Amount AS amount, SaleOrder1.InvTotal AS GrossAmount, SaleOrder1.Discount AS Disc, SaleOrder1.SaleTaxAmt AS SalesTaxAmt,SaleOrder1.NetTotal AS NetAmount, SaleOrder1.FarmID AS FarmdId, SaleOrder1.brate, SaleOrder2.VehNo, SaleOrder2.GateNo,SaleOrder2.QtyDr + SaleOrder2.QtyCr AS Qty1, SaleOrder1.DispatchNo AS DispactchID, SaleOrder1.Freight AS Freight, SaleOrder1.BagAmount AS BagAmount, SaleOrder1.BrokeryAmount AS BrokeryAmount, SaleOrder1.NumCol2 AS LabourAmount, SaleOrder1.MarketComAmount AS MktComete, Farms.Title FROM Products INNER JOIN Accounts INNER JOIN SaleOrder1 ON Accounts.AccountId = SaleOrder1.AccountId INNER JOIN 
                        SaleOrder2 ON SaleOrder1.InvNo = SaleOrder2.InvNo ON Products.ProductID = SaleOrder2.ProductID INNER JOIN ProductGroups ON Products.PG_ID = ProductGroups.PG_ID INNER JOIN ProductSubGroup ON Products.PT_ID = ProductSubGroup.PSG_Id INNER JOIN Farms ON SaleOrder1.FarmID = Farms.FarmID WHERE SaleOrder1.Sno =" + OrderId + "";
                }
                else if (type == "PO")
                {
                    Sql = "SELECT qryInvBook.Invid, qryInvBook.InvDate, qryInvBook.YourBillNo, qryInvBook.YourBillDate, qryInvBook.AccountId, qryInvBook.AccountTitle, qryInvBook.ProductID, qryInvBook.ProductTitle, ";
                    Sql = "SELECT PurchaseOrder1.InvNo AS InvID, PurchaseOrder1.Idate AS InvDate, PurchaseOrder1.PartyBillNO AS YourBillNo, PurchaseOrder1.PartyBillDate AS YourBillDate, PurchaseOrder1.AccountId, Accounts1.Title AS AccountTitle, PurchaseOrder2.ProductID, Products.ProductName AS ProductTitle, ";
                    Sql = Sql + " (PurchaseOrder2.WtDr+PurchaseOrder2.WtCr) as Qty, PurchaseOrder2.Rate, PurchaseOrder2.Amount AS amount, PurchaseOrder1.InvTotal AS GrossAmount, PurchaseOrder1.Discount as Disc, PurchaseOrder1.SaleTaxAmt as SalesTaxAmt, ";
                    Sql = Sql + " PurchaseOrder1.NetTotal as NetAmount, PurchaseOrder1.Brate, PurchaseOrder2.VehNo, PurchaseOrder2.GateNo, PurchaseOrder2.QtyDr+ PurchaseOrder2.QtyCr as Qty1,";
                    Sql = Sql + " PurchaseOrder1.ArrivalID as DispactchID,PurchaseOrder1.Freight as Freight,PurchaseOrder1.BagAmount as BagAmount,PurchaseOrder1.BrokeryAmount as BrokeryAmount,PurchaseOrder1.NumCol2 as LabourAmount,PurchaseOrder1.MarketComAmount as MktComete";
                    Sql = Sql + " , PurchaseOrder1.BrokeryMode, PurchaseOrder1.MillType  FROM Products INNER JOIN Accounts1 INNER JOIN PurchaseOrder1 ON Accounts1.AccountID = PurchaseOrder1.AccountId INNER JOIN";
                    Sql = Sql + " PurchaseOrder2 ON PurchaseOrder1.InvNo = PurchaseOrder2.InvNo ON Products.ProductID = PurchaseOrder2.ProductID INNER JOIN";
                    Sql = Sql + " ProductGroups ON Products.PG_ID = ProductGroups.PG_ID INNER JOIN";
                    Sql = Sql + " ProductSubGroup ON Products.PT_ID = ProductSubGroup.PSG_Id";
                    Sql = Sql + " where PurchaseOrder1.InvNo= '" + OrderId + "' ";
                }
                else if (type == "SI")
                {
                    Sql = @"SELECT DISTINCT qryInvBook.InvID, qryInvBook.InvDate, qryInvBook.YourBillNo, qryInvBook.YourBillDate, qryInvBook.AccountId, qryInvBook.AccountTitle, qryInvBook.ProductID,
                            qryInvBook.ProductTitle, qryInvBook.WtDr + qryInvBook.WtCr AS Qty, qryInvBook.Rate, qryInvBook.Amount, qryInvBook.GrossAmount, qryInvBook.Disc,
                            qryInvBook.SalesTaxAmt, qryInvBook.NetAmount, qryInvBook.brate, qryInvBook.VehNo, qryInvBook.GateNo, qryInvBook.QtyDr + qryInvBook.QtyCr AS Qty1,
                            qryInvBook.DispactchID, qryInvBook.Freight, qryInvBook.BagAmount, qryInvBook.BrokeryAmount, qryInvBook.LabourAmount, qryInvBook.MktComete,
                            qryInvBook.FarmID, Farms.Title AS FarmName
                            FROM         qryInvBook INNER JOIN
                            Farms ON qryInvBook.FarmID = Farms.FarmID
                            WHERE     qryInvBook.InvID ='FS-" + OrderId + "'";
                }
                else if (type == "SR")
                {
                    Sql = @"SELECT DISTINCT qryInvBook.InvID, qryInvBook.InvDate, qryInvBook.YourBillNo, qryInvBook.YourBillDate, qryInvBook.AccountId, qryInvBook.AccountTitle, qryInvBook.ProductID,
                            qryInvBook.ProductTitle, qryInvBook.WtDr + qryInvBook.WtCr AS Qty, qryInvBook.Rate, qryInvBook.Amount, qryInvBook.GrossAmount, qryInvBook.Disc,
                            qryInvBook.SalesTaxAmt, qryInvBook.NetAmount, qryInvBook.brate, qryInvBook.VehNo, qryInvBook.GateNo, qryInvBook.QtyDr + qryInvBook.QtyCr AS Qty1,
                            qryInvBook.DispactchID, qryInvBook.Freight, qryInvBook.BagAmount, qryInvBook.BrokeryAmount, qryInvBook.LabourAmount, qryInvBook.MktComete,
                            qryInvBook.FarmID, Farms.Title AS FarmName
                            FROM         qryInvBook INNER JOIN
                            Farms ON qryInvBook.FarmID = Farms.FarmID
                            WHERE     qryInvBook.InvID ='FSR-" + OrderId + "'";
                }
                else if (type == "GSI")
                {
                    Sql = @"SELECT         InvNo as InvId, Idate as InvDate, YourBillNo, YourBillDate, AccountId, AccountTitle, SaleAccount, SaleTitle, BrockerID, BrockerageAccount, BRate, Total_Qty, Total_Kgs, Total_Gross, Total_Dis, Total_GSt, Total_Net, Sno, IType, ProductID, 
                         ProductName as ProductTitle, QtyDr, QtyCr, WtDr, WtCr, Rate, Gross_Amount as GrossAmount, Dis, Gst_Rate, Gst, Net_Amount as NetAmount, LotNo, SalesTaxNo  as SalesTaxAmt, GateNo, PG_ID, ProductGroup, PSG_ID, ProductType   FROM            qryGstBook where InvNo='FGSTS-" + OrderId + "'";
                }
                else if (type == "GSIR")
                {
                    Sql = @"SELECT         InvNo as InvId, Idate as InvDate, YourBillNo, YourBillDate, AccountId, AccountTitle, SaleAccount, SaleTitle, BrockerID, BrockerageAccount, BRate, Total_Qty, Total_Kgs, Total_Gross, Total_Dis, Total_GSt, Total_Net, Sno, IType, ProductID, 
                         ProductName as ProductTitle, QtyDr, QtyCr, WtDr, WtCr, Rate, Gross_Amount as GrossAmount, Dis, Gst_Rate, Gst, Net_Amount as NetAmount, LotNo, SalesTaxNo  as SalesTaxAmt, GateNo, PG_ID, ProductGroup, PSG_ID, ProductType   FROM            qryGstBook where InvNo='FGSTSR-" + OrderId + "'";
                }
                else if (type == "PGSTI")
                {
                    Sql = @"SELECT         InvNo as InvId, Idate as InvDate, YourBillNo, YourBillDate, AccountId, AccountTitle, SaleAccount, SaleTitle, BrockerID, BrockerageAccount, BRate, Total_Qty, Total_Kgs, Total_Gross, Total_Dis, Total_GSt, Total_Net, Sno, IType, ProductID, 
                         ProductName as ProductTitle, QtyDr, QtyCr, WtDr, WtCr, Rate, Gross_Amount as GrossAmount, Dis, Gst_Rate, Gst, Net_Amount as NetAmount, LotNo, SalesTaxNo  as SalesTaxAmt, GateNo, PG_ID, ProductGroup, PSG_ID, ProductType   FROM            qryGstBook where InvNo='FGSTP-" + OrderId + "'";
                }
                else if (type == "PI")
                {
                    Sql = "SELECT Purchase1.InvNo AS InvID, Purchase1.Idate AS InvDate, Purchase1.PartyBillNO AS YourBillNo, Purchase1.PartyBillDate AS YourBillDate, Purchase1.AccountId, Accounts.Title AS AccountTitle, Purchase2.ProductID, Products.ProductName AS ProductTitle, ";
                    Sql = Sql + " (Purchase2.WtDr+dbo.Purchase2.WtCr) as Qty, Purchase2.Rate, Purchase2.Amount AS amount, Purchase1.InvTotal AS GrossAmount, Purchase1.Discount as Disc, Purchase1.SaleTaxAmt as SalesTaxAmt, ";
                    Sql = Sql + " Purchase1.NetTotal as NetAmount, Purchase1.Brate, Purchase2.VehNo, Purchase2.GateNo, Purchase2.QtyDr+ Purchase2.QtyCr as Qty1,";
                    Sql = Sql + " Purchase1.ArrivalID as DispactchID,Purchase1.Freight as Freight,Purchase1.BagAmount as BagAmount,Purchase1.BrokeryAmount as BrokeryAmount,Purchase1.NumCol2 as LabourAmount,Purchase1.MarketComAmount as MktComete";
                    Sql = Sql + " FROM Products INNER JOIN Accounts INNER JOIN Purchase1 ON Accounts.AccountID = Purchase1.AccountId INNER JOIN";
                    Sql = Sql + " Purchase2 ON Purchase1.InvNo = Purchase2.InvNo ON Products.ProductID = Purchase2.ProductID INNER JOIN";
                    Sql = Sql + " ProductGroups ON Products.PG_ID = ProductGroups.PG_ID INNER JOIN";
                    Sql = Sql + " ProductSubGroup ON Products.PT_ID = ProductSubGroup.PSG_Id INNER JOIN";
                    Sql = Sql + " VehicleType ON Purchase1.ArrivalType = VehicleType.VID";
                    Sql = Sql + " where Purchase1.InvNo= '" + OrderId + "' ";
                }
                else if (type == "PR")
                {
                    Sql = "SELECT Purchase1.InvNo AS InvID, Purchase1.Idate AS InvDate, Purchase1.PartyBillNO AS YourBillNo, Purchase1.PartyBillDate AS YourBillDate, Purchase1.AccountId, Accounts.Title AS AccountTitle, Purchase2.ProductID, Products.ProductName AS ProductTitle, ";
                    Sql = Sql + " (Purchase2.WtDr+dbo.Purchase2.WtCr) as Qty, Purchase2.Rate, Purchase2.Amount AS amount, Purchase1.InvTotal AS GrossAmount, Purchase1.Discount as Disc, Purchase1.SaleTaxAmt as SalesTaxAmt, ";
                    Sql = Sql + " Purchase1.NetTotal as NetAmount, Purchase1.Brate, Purchase2.VehNo, Purchase2.GateNo, Purchase2.QtyDr+ Purchase2.QtyCr as Qty1,";
                    Sql = Sql + " Purchase1.ArrivalID as DispactchID,Purchase1.Freight as Freight,Purchase1.BagAmount as BagAmount,Purchase1.BrokeryAmount as BrokeryAmount,Purchase1.NumCol2 as LabourAmount,Purchase1.MarketComAmount as MktComete";
                    Sql = Sql + " FROM Products INNER JOIN Accounts INNER JOIN Purchase1 ON Accounts.AccountID = Purchase1.AccountId INNER JOIN";
                    Sql = Sql + " Purchase2 ON Purchase1.InvNo = Purchase2.InvNo ON Products.ProductID = Purchase2.ProductID INNER JOIN";
                    Sql = Sql + " ProductGroups ON Products.PG_ID = ProductGroups.PG_ID INNER JOIN";
                    Sql = Sql + " ProductSubGroup ON Products.PT_ID = ProductSubGroup.PSG_Id INNER JOIN";
                    Sql = Sql + " VehicleType ON Purchase1.ArrivalType = VehicleType.VID";
                    Sql = Sql + " where Purchase1.InvNo= '" + OrderId + "' ";
                }
                da = new SqlDataAdapter(Sql, Connection);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsPurchase GetPurchaseOrder(string OrderId, string type)
        {
            try
            {
                string Sql = "";
                dsPurchase ds = new dsPurchase();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                if (type == "PO")
                {
                    Sql = "SELECT PurchaseOrder1.InvNo AS InvID, PurchaseOrder1.Idate AS InvDate, PurchaseOrder1.PartyBillNO AS YourBillNo, PurchaseOrder1.PartyBillDate AS YourBillDate, PurchaseOrder1.AccountId, Accounts.Title AS AccountTitle, PurchaseOrder2.ProductID, Products.ProductName AS ProductTitle, ";
                    Sql = Sql + " (PurchaseOrder2.WtDr+PurchaseOrder2.WtCr) as Qty, PurchaseOrder2.Rate, PurchaseOrder2.Amount AS amount, PurchaseOrder1.InvTotal AS GrossAmount, PurchaseOrder1.Discount as Disc, PurchaseOrder1.SaleTaxAmt as SalesTaxAmt, ";
                    Sql = Sql + " PurchaseOrder1.NetTotal as NetAmount, PurchaseOrder1.Brate, (PurchaseOrder2.WtDr+PurchaseOrder2.WtCr)/40 as VehNo, PurchaseOrder2.GateNo, PurchaseOrder2.QtyDr+ PurchaseOrder2.QtyCr as Qty1,";
                    Sql = Sql + " PurchaseOrder1.ArrivalID as DispactchID,PurchaseOrder1.Freight as Freight,PurchaseOrder1.BagAmount as BagAmount,PurchaseOrder1.BrokeryAmount as BrokeryAmount,PurchaseOrder1.NumCol2 as LabourAmount,PurchaseOrder1.MarketComAmount as MktComete";
                    Sql = Sql + " , PurchaseOrder1.BrokeryMode, PurchaseOrder1.MillType  FROM Products INNER JOIN Accounts INNER JOIN PurchaseOrder1 ON Accounts.AccountID = PurchaseOrder1.AccountId INNER JOIN";
                    Sql = Sql + " PurchaseOrder2 ON PurchaseOrder1.InvNo = PurchaseOrder2.InvNo ON Products.ProductID = PurchaseOrder2.ProductID INNER JOIN";
                    Sql = Sql + " ProductGroups ON Products.PG_ID = ProductGroups.PG_ID INNER JOIN";
                    Sql = Sql + " ProductSubGroup ON Products.PT_ID = ProductSubGroup.PSG_Id";
                    Sql = Sql + " where PurchaseOrder1.InvNo= '" + OrderId + "' ";
                }
                else if (type == "GRN")
                {
                    Sql = "SELECT GRN1.InvNo AS InvID, GRN1.Idate AS InvDate, GRN1.PartyBillNO AS YourBillNo, GRN1.PartyBillDate AS YourBillDate, GRN1.AccountId, Accounts.Title AS AccountTitle, GRN2.ProductID, Products.ProductName AS ProductTitle,  (GRN2.WtDr+GRN2.WtCr) as Qty, GRN2.Rate, GRN2.Amount AS amount, GRN1.InvTotal AS GrossAmount, GRN1.Discount as Disc, GRN1.SaleTaxAmt as SalesTaxAmt,  GRN1.NetTotal as NetAmount, GRN1.Brate, (GRN2.WtDr+GRN2.WtCr)/40 as VehNo, GRN2.GateNo, GRN2.QtyDr+ GRN2.QtyCr as Qty1, GRN1.ArrivalID as DispactchID,GRN1.Freight as Freight,GRN1.BagAmount as BagAmount,GRN1.BrokeryAmount as BrokeryAmount,GRN1.NumCol2 as LabourAmount,GRN1.MarketComAmount as MktComete , GRN1.BrokeryMode, GRN1.MillType  FROM Products INNER JOIN Accounts INNER JOIN GRN1 ON Accounts.AccountID = GRN1.AccountId INNER JOIN GRN2 ON GRN1.InvNo = GRN2.InvNo ON Products.ProductID = GRN2.ProductID INNER JOIN ProductGroups ON Products.PG_ID = ProductGroups.PG_ID INNER JOIN ProductSubGroup ON Products.PT_ID = ProductSubGroup.PSG_Id where GRN1.InvNo=  '" + OrderId + "'  ";
                }
                da = new SqlDataAdapter(Sql, Connection);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private igpds GetInwardGatePass(string OrderId, string type)
        {
            try
            {
                string Sql = "";
                igpds ds = new igpds();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT substring(Gate.GateNo,5,10) as GateNo , Gate.Gdate, Gate.Gtime, Gate.BuiltyNo, Gate.ArrivalID, Gate.IntCol1 AS AccountID,";
                Sql = Sql + " Accounts.Title AS Title, products.ItemDetail AS ProductName, Gate.Through, Gate.Transport, Gate.VehReference,";
                Sql = Sql + " Gate.BagNature, Gate.BagQty, Gate.Itype, Gate.BarCode, Gate.NumCol2, Gate.BtypeId,";
                Sql = Sql + " BardanaTypes.Title AS BardanaTypeTilte, Gate3.ProductID AS Expr1, 0 as StWt, Gate3.Quantity, Gate3.StdWeight,";
                Sql = Sql + " Gate3.CalcWeight , Gate3.Bardana,Gate.KatlaNo,Gate3.BardanaType,isnull(Gate.Do,0) as Do,isnull(Gate.ArrNo,0) as ArrNo";
                Sql = Sql + " , Gate.Transportation , Gate.Weight1, Gate.Weight2 FROM  Products INNER JOIN (Accounts INNER JOIN (Gate3 INNER JOIN (Gate LEFT JOIN BardanaTypes ON Gate.BtypeId = BardanaTypes.BTypeID) ON Gate3.GateNo = Gate.GateNo) ON Accounts.AccountID = Gate.IntCol1) ON Products.ProductID = Gate3.ProductID";
                Sql = Sql + " where Gate.GateNo= '" + OrderId + "' and Gate.Itype='INR' and Gate.Itype2='GPR'";
                da = new SqlDataAdapter(Sql, Connection);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsigpMix GetInwardGatePassMix(string OrderId, string type)
        {
            try
            {
                string Sql = "";
                dsigpMix ds = new dsigpMix();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT substring(Gate.GateNo,5,10) as GateNo, Gate.Gdate, Gate.Gtime, Gate.BuiltyNo, Gate.ArrivalID, Gate.IntCol1 AS AccountID, Accounts.Title AS Title, Products.ProductName AS ProductName, Gate.Through, Gate.Transport, Gate.VehReference, Gate.BagNature, Gate.BagQty, Gate.Itype, Gate.BarCode, Gate.NumCol2, Gate.BtypeId, BardanaTypes.Title AS BardanaTypeTilte, Gate3.ProductID AS Expr1, 0 AS StWt, Gate3.Quantity, Gate3.StdWeight, Gate3.CalcWeight, Gate3.Bardana, Gate.KatlaNo, Gate3.BardanaType, Gate.Do, Gate.ArrNo  , Gate.Transportation FROM Accounts INNER JOIN (Products INNER JOIN (Gate3 INNER JOIN (Gate LEFT JOIN BardanaTypes ON Gate.BtypeId = BardanaTypes.BTypeID) ON Gate3.GateNo = Gate.GateNo) ON Products.ProductID = Gate3.ProductID) ON Accounts.AccountId = Gate.IntCol1 where Gate.GateNo= '" + OrderId + "' and Gate.Itype='INM' and Gate.Itype2='GPM'";
                //Sql = "SELECT substring(Gate.GateNo,5,10) as GateNo, Gate.Gdate, Gate.Gtime, Gate.BuiltyNo, Gate.ArrivalID, Gate.IntCol1 AS AccountID, PartiesGatePass.Title AS Title, ProductGatePass.Title AS ProductName, Gate.Through, Gate.Transport, Gate.VehReference, Gate.BagNature, Gate.BagQty, Gate.Itype, Gate.BarCode, Gate.NumCol2, Gate.BtypeId, BardanaTypes.Title AS BardanaTypeTilte, Gate3.ProductID AS Expr1, 0 AS StWt, Gate3.Quantity, Gate3.StdWeight, Gate3.CalcWeight, Gate3.Bardana, Gate.KatlaNo, Gate3.BardanaType, Gate.Do, Gate.ArrNo  , Gate.Transportation FROM PartiesGatePass INNER JOIN (ProductGatePass INNER JOIN (Gate3 INNER JOIN (Gate LEFT JOIN BardanaTypes ON Gate.BtypeId = BardanaTypes.BTypeID) ON Gate3.GateNo = Gate.GateNo) ON ProductGatePass.CompID = Gate3.ProductID) ON PartiesGatePass.CompID = Gate.IntCol1";
                //Sql = Sql + " where Gate.GateNo= '" + OrderId + "' and Gate.Itype='INM' and Gate.Itype2='GPM'";
                da = new SqlDataAdapter(Sql, Connection);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsOut GetOutwardGatePass(string OrderId, string type)
        {
            try
            {
                string Sql = "";
                dsOut ds = new dsOut();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT substring(Gate.GateNo,5,10) as GateNo, Gate.Gdate, Gate.Gtime, Gate.BuiltyNo, Gate.ArrivalID, Gate.IntCol1 AS AccountID,";
                Sql = Sql + " Accounts.Title AS Title, products.ProductName AS ProductName, Gate.Through, Gate.Transport, Gate.VehReference,";
                Sql = Sql + " Gate.BagNature, Gate.BagQty, Gate.Itype, Gate.BarCode, Gate.NumCol2, Gate.BtypeId,";
                Sql = Sql + " BardanaTypes.Title AS BardanaTypeTilte, Gate3.ProductID AS Expr1, 0 as StWt, Gate3.Quantity, Gate3.StdWeight,";
                Sql = Sql + " Gate3.CalcWeight , Gate3.Bardana,Gate.KatlaNo,Gate3.BardanaType,Gate.Do,Gate.ArrNo, Gate.Transportation, Gate.Driver, Gate.DriverCnic,";
                Sql = Sql + " ISNULL(dbo.Gate.Weight1, 0) AS Weight1, ISNULL(dbo.Gate.Weight2, 0) AS Weight2";
                Sql = Sql + " FROM  Products INNER JOIN (Accounts INNER JOIN (Gate3 INNER JOIN (Gate LEFT JOIN BardanaTypes ON Gate.BtypeId = BardanaTypes.BTypeID) ON Gate3.GateNo = Gate.GateNo) ON Accounts.AccountID = Gate.IntCol1) ON Products.ProductID = Gate3.ProductID";
                Sql = Sql + " where Gate.GateNo= '" + OrderId + "' and Gate.Itype='OutF' and Gate.Itype2='GOF'";
                da = new SqlDataAdapter(Sql, Connection);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsOGM GetOutwardGatePassMix(string OrderId, string type)
        {
            try
            {
                string Sql = "";
                dsOGM ds = new dsOGM();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT     substring(Gate.GateNo,5,10) as GateNo, Gate.Gdate, Gate.Gtime, Gate.BuiltyNo, Gate.ArrivalID, Gate.IntCol1 AS AccountID, Accounts.Title AS Title, Products.ProductName AS ProductName, Gate.Through, Gate.Transport, Gate.VehReference, Gate.BagNature, Gate.BagQty, Gate.Itype, Gate.BarCode, Gate.NumCol2, Gate.BtypeId, BardanaTypes.Title AS BardanaTypeTilte, Gate3.ProductID AS Expr1, 0 AS StWt, Gate3.Quantity, Gate3.StdWeight, Gate3.CalcWeight, Gate3.Bardana, Gate.KatlaNo , Gate3.BardanaType, Gate.DO, Gate.ArrNo, Gate.Transportation, Gate.Driver, Gate.DriverCnic, Gate.Weight1, Gate.Weight2 FROM         Accounts INNER JOIN Products INNER JOIN Gate3 INNER JOIN Gate LEFT OUTER JOIN BardanaTypes ON Gate.BtypeId = BardanaTypes.BTypeID ON Gate3.GateNo = Gate.GateNo ON Products.ProductID = Gate3.ProductID ON Accounts.AccountId = Gate.IntCol1 where Gate.GateNo= '" + OrderId + "' and Gate.Itype='OutM' and Gate.Itype2='GOM'";
                da = new SqlDataAdapter(Sql, Connection);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsAccount GetTrialBalanceToDate(DateTime tdate)
        {
            try
            {
                string Sql = "";
                dsAccount ds = new dsAccount();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT     AccountID, Title, abs(case when SUM(Debit) > sum(Credit) then (SUM(Debit)-sum(Credit)) else 0 end) as Debit,abs(case when SUM(Credit) > sum(Debit) then (SUM(Debit)-sum(Credit)) else 0 end) as Credit FROM qryVoucherBook WHERE vdate <= '" + tdate + "'  group by AccountID, Title ";
                da = new SqlDataAdapter(Sql, Connection);
                da.Fill(ds, "ado");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsAccount GetBalanceSheetToDate(DateTime tdate)
        {
            try
            {
                string Sql = "";
                dsAccount ds = new dsAccount();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT     AccountID, Title, abs(case when SUM(Debit) > sum(Credit) then (SUM(Debit)-sum(Credit)) else 0 end) as Debit,abs(case when SUM(Credit) > sum(Debit) then (SUM(Debit)-sum(Credit)) else 0 end) as Credit FROM qryVoucherBook WHERE vdate <= '" + tdate + "'  group by AccountID, Title ";
                da = new SqlDataAdapter(Sql, Connection);
                da.Fill(ds, "ado");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsAccountLedger GetBalanceSheet_Data(DateTime TDate, string type)
        {
            try
            {
                dsAccountLedger ds = new dsAccountLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                if (type.Equals("D"))
                {
                    Qry = @"  exec SPRW_GetBalanceSheet @ToDate='" + TDate + "'";
                    //Qry = @"  exec SPRW_GetBalanceSheetNew1 @ToDate='" + TDate + "'";
                }
                else
                {
                    Qry = @"  exec SPRW_GetBalanceSheet2 @ToDate='" + TDate + "'";
                }
                Command.Connection = Connection;
                da = new SqlDataAdapter(Qry, dbconsql);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsAccountLedger GetProfitLoss_Data(DateTime TDate, string type)
        {
            try
            {
                dsAccountLedger ds = new dsAccountLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                if (type.Equals("D"))
                {
                    Qry = @"  exec SPRW_GetProfitAndLoss @ToDate='" + TDate + "'";
                }
                else
                {
                    Qry = @"  exec SPRW_GetProfitAndLoss2 @ToDate='" + TDate + "'";
                }
                Command.Connection = Connection;
                da = new SqlDataAdapter(Qry, dbconsql);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private string GetOpening(DateTime fdate)
        {
            try
            {
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                string Sql = "select sum(debit)- sum(credit) from qryvoucherBook where accountId='126212' and accountId<>'126211' and vdate< '" + fdate + "'";
                Command = new SqlCommand(Sql, Connection);
                string opening = Convert.ToString(Command.ExecuteScalar());
                return opening;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsAccountLedger GetDetailTrial_Data(DateTime TDate)
        {
            try
            {
                dsAccountLedger ds = new dsAccountLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Qry = @"  exec SPRW_GetDetailTrial @ToDate='" + TDate + "'";
                Command.Connection = Connection;
                da = new SqlDataAdapter(Qry, dbconsql);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsAccountLedger GetGroupTrial_Data(DateTime TDate, string AccountId)
        {
            try
            {
                dsAccountLedger ds = new dsAccountLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.CommandTimeout = 60000000;
                Qry = @"  exec SPRW_GetGroupTrial @ToDate='" + TDate + "',@AccountId='" + AccountId + "'";
                Command.Connection = Connection;
                Command.CommandTimeout = 600000000;
                da = new SqlDataAdapter(Qry, dbconsql);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsAccountLedger GetGroupTrial_Data2(DateTime TDate, string AccountId, DateTime agDate)
        {
            try
            {
                dsAccountLedger ds = new dsAccountLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Qry = @"  exec SPRW_GetGroupTrial3 @ToDate='" + TDate + "',@AccountId='" + AccountId + "',@AgDate='" + agDate + "'";
                Command.Connection = Connection;
                Command.CommandTimeout = 0;
                da = new SqlDataAdapter(Qry, dbconsql);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsAccountLedger GetSixColumnTrial_Data(DateTime FDate, DateTime TDate)
        {
            try
            {
                dsAccountLedger ds = new dsAccountLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Qry = @"  exec SPRW_GetSixColumnTrialBalance @FromDate='" + FDate + "',@ToDate='" + TDate + "'";
                Command.Connection = Connection;
                da = new SqlDataAdapter(Qry, dbconsql);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsAccountLedger GetSixColumnTrial_Data2(DateTime FDate, DateTime TDate)
        {
            try
            {
                dsAccountLedger ds = new dsAccountLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                //commented for remove six column
                //Qry = @"Select AccountId as AccCode,Title,OpeningDr as OpBalance,CurrentDr as Balance,NetBalance,CurrentCr as Narration from(SELECT        AccountID, Title, ISNULl((select (sum(qryVoucherBook_New.Debit) - sum(qryVoucherBook_New.Credit)) 
                //        from qryVoucherBook_New where (vdate<'" + FDate.Date + "') and (Accounts.AccountId=qryVoucherBook_New.AccountID)),0) as OpeningDr,  ISNULl((select (sum(qryVoucherBook_New.Debit) )from qryVoucherBook_New where (vdate between '" + FDate.Date + "' and '" + TDate.Date + "')and Accounts.AccountId=qryVoucherBook_New.AccountID),0) as CurrentDr,  ISNULl((select(sum(qryVoucherBook_New.Credit))from qryVoucherBook_New where (vdate between '" + FDate.Date + "' and '" + TDate.Date + "')and Accounts.AccountId = qryVoucherBook_New.AccountID),0) as CurrentCr,ISNULl((select(sum(qryVoucherBook_New.Debit) - sum(qryVoucherBook_New.Credit))from qryVoucherBook_New where (Accounts.AccountId = qryVoucherBook_New.AccountID) and(vdate <= '" + TDate.Date + "')),0) as NetBalance FROM Accounts where(AccType = 'Detail' and IsParty = 'true'))temp";
              
                Qry = @"Select AccountId as AccCode,Title,OpeningDr as OpBalance,CurrentDr as Balance,NetBalance,CurrentCr as Narration from(SELECT        AccountID, Title, ISNULl((select (sum(qryVoucherBook_New.Debit) - sum(qryVoucherBook_New.Credit)) 
                        from qryVoucherBook_New where (vdate<='" + FDate.Date + "') and (Accounts.AccountId=qryVoucherBook_New.AccountID)),0) as OpeningDr,  ISNULl((select (sum(qryVoucherBook_New.Debit) )from qryVoucherBook_New where (vdate between '" + FDate.Date + "' and '" + TDate.Date + "')and Accounts.AccountId=qryVoucherBook_New.AccountID),0) as CurrentDr,  ISNULl((select(sum(qryVoucherBook_New.Credit))from qryVoucherBook_New where (vdate between '" + FDate.Date + "' and '" + TDate.Date + "')and Accounts.AccountId = qryVoucherBook_New.AccountID),0) as CurrentCr,ISNULl((select(sum(qryVoucherBook_New.Debit) - sum(qryVoucherBook_New.Credit))from qryVoucherBook_New where (Accounts.AccountId = qryVoucherBook_New.AccountID) and(vdate <= '" + TDate.Date + "')),0) as NetBalance FROM Accounts where(AccType = 'Detail' and IsParty = 'true'))temp";

                Command.Connection = Connection;
                da = new SqlDataAdapter(Qry, dbconsql);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsAccountLedger GetRecPayable_Data(DateTime FDate, DateTime TDate)
        {
            try
            {
                dsAccountLedger ds = new dsAccountLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Qry = @"Select AccountId as AccCode,Title,OpeningDr as OpBalance,CurrentDr as Balance,NetBalance,CurrentCr as Narration from(SELECT        AccountID, Title, ISNULl((select (sum(qryVoucherBook.Debit) - sum(qryVoucherBook.Credit)) 
                        from qryVoucherBook where (vdate<'" + FDate.Date + "') and (Accounts.AccountId=qryVoucherBook.AccountID)),0) as OpeningDr,  ISNULl((select (sum(qryVoucherBook.Debit) )from qryVoucherBook where (vdate between '" + FDate.Date + "' and '" + TDate.Date + "')and Accounts.AccountId=qryVoucherBook.AccountID),0) as CurrentDr,  ISNULl((select(sum(qryVoucherBook.Credit))from qryVoucherBook where (vdate between '" + FDate.Date + "' and '" + TDate.Date + "')and Accounts.AccountId = qryVoucherBook.AccountID),0) as CurrentCr,ISNULl((select(sum(qryVoucherBook.Debit) - sum(qryVoucherBook.Credit))from qryVoucherBook where (Accounts.AccountId = qryVoucherBook.AccountID) and(vdate <= '" + TDate.Date + "')),0) as NetBalance FROM Accounts where(IsParty = 'true'))temp  ";
                //and (ParentID like '1%' or ParentID like '2%')
                Command.Connection = Connection;
                da = new SqlDataAdapter(Qry, dbconsql);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsInvoices GetInvoice_Data(int VNo)
        {
            try
            {
                dsInvoices ds = new dsInvoices();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Qry = @"select InvId,Idate,ItemId,ItemName,round(Packing,2) as Boxes,round(Quantity,2) as Pieces,round(Quantity,2) as NetTotal from InvoiceDetailDelivaryChallan  WHERE InvId=" + VNo;
                Command.Connection = Connection;
                da = new SqlDataAdapter(Qry, dbconsql);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }


        private dsInvoices GetInvoiceNew_Data(int VNo, string VType)
        {
            try
            {
                dsInvoices ds = new dsInvoices();
                string dbconsql = Convert.ToString(Session["consql"]);

                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;

                 if (VType == "IFP")
                {
                    Qry = @" select a.IssueID as InvId,a.IssueDate as Idate,a.RefNo as Yourinvono ,a.RefDate as YourDate
 ,ad.ProductID as ItemId,ad.ProductName as ItemName,a.Batch as SupplierId,ad.Qty as QtyDr,isnull(p.Title,'') as Purchaseid
 ,ad.Weight as Rate from IssueMasterProduction a 
 left join IssueDetailProduction ad on a.IssueID=ad.IssueID left join Plant p on p.PlantID=a.PlantId WHERE a.IssueID=" + VNo;

                }
                else if (VType == "RFP")
                {
                    Qry = @"  select a.ReceiveID as InvId,a.ReceiveDate as Idate,a.RefNo as Yourinvono ,a.RefDate as YourDate,ad.ProductID as ItemId,ad.ProductName as ItemName,a.Batch as SupplierId,ad.Qty as QtyDr,ad.Weight as Rate,isnull(p.Title,'') as Purchaseid from ReceiveMasterProduction a left join ReceiveDetailProduction ad on a.ReceiveID=ad.ReceiveID left join Plant p on p.PlantID=a.PlantId WHERE a.ReceiveID=" + VNo;

                }
                Command.Connection = Connection;
                da = new SqlDataAdapter(Qry, dbconsql);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        private dsVouchers GetVoucherReceipt_Data2222(string VNo, string VType)
        {
            try
            {
                dsVouchers ds = new dsVouchers();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                if (VType == "DV")
                {
                    int vid = Convert.ToInt32(VNo);
                    Qry = @"SELECT dbo.Vouchers3.VoucherID  AS VId, dbo.Vouchers3.vDate AS VDate, dbo.Vouchers4.AccountID AS AccountId, dbo.Accounts1.Title, dbo.Vouchers4.Narration, dbo.Vouchers4.Debit, dbo.Vouchers4.Credit FROM dbo.Accounts1 INNER JOIN dbo.Vouchers4 ON dbo.Accounts1.AccountID = dbo.Vouchers4.AccountID INNER JOIN dbo.Vouchers3 ON dbo.Vouchers4.VoucherID = dbo.Vouchers3.VoucherID WHERE (dbo.Vouchers3.VoucherID =" + vid + ") and dbo.Vouchers4.AccountID<>'126212'";
                }
                else if (VType == "CV")
                {
                    int vid = Convert.ToInt32(VNo);
                    Qry = @"SELECT dbo.Vouchers3.VoucherID  AS VId, dbo.Vouchers3.vDate AS VDate, dbo.Vouchers4.AccountID AS AccountId, dbo.Accounts1.Title, dbo.Vouchers4.Narration, dbo.Vouchers4.Debit, dbo.Vouchers4.Credit FROM dbo.Accounts1 INNER JOIN dbo.Vouchers4 ON dbo.Accounts1.AccountID = dbo.Vouchers4.AccountID INNER JOIN dbo.Vouchers3 ON dbo.Vouchers4.VoucherID = dbo.Vouchers3.VoucherID WHERE (dbo.Vouchers3.VoucherID =" + vid + ") and dbo.Vouchers4.AccountID<>'126212'";
                    //                    Qry = @" select VId,VDate,VouchersDetail.AccountId,Title,Narration,ISNULL(Debit,0) as Debit,ISNULL(Credit,0) as Credit from VouchersDetail
                    //                where VType='CV' and isnull(Credit,0)>0 and VID=" + VNo;
                }
                else if (VType == "JV")
                {
                    int vid = Convert.ToInt32(VNo);
                    Qry = @"SELECT dbo.Vouchers3.VoucherID  AS VId, dbo.Vouchers3.vDate AS VDate, dbo.Vouchers4.AccountID AS AccountId, dbo.Accounts1.Title, dbo.Vouchers4.Narration, dbo.Vouchers4.Debit, dbo.Vouchers4.Credit FROM dbo.Accounts1 INNER JOIN dbo.Vouchers4 ON dbo.Accounts1.AccountID = dbo.Vouchers4.AccountID INNER JOIN dbo.Vouchers3 ON dbo.Vouchers4.VoucherID = dbo.Vouchers3.VoucherID WHERE (dbo.Vouchers3.VoucherID =" + vid + ")";
                }
                else if (VType == "P-JV")
                {
                    Qry = @" select VId,VDate,VouchersDetail.AccountId,Title,Narration,ISNULL(Debit,0) as Debit,ISNULL(Credit,0) as Credit from VouchersDetail
                where VType='P-JV' and VID=" + VNo;
                }
                else if (VType == "S-JV")
                {
                    Qry = @" select VId,VDate,VouchersDetail.AccountId,Title,Narration,ISNULL(Debit,0) as Debit,ISNULL(Credit,0) as Credit from VouchersDetail
                where VType='S-JV' and VID=" + VNo;
                }
                else if (VType == "BD-JV")
                {
                    int vid = Convert.ToInt32(VNo);
                    Qry = @"SELECT dbo.Vouchers3.VoucherID  AS VId, dbo.Vouchers3.vDate AS VDate, dbo.Vouchers4.AccountID AS AccountId, dbo.Accounts1.Title, dbo.Vouchers4.Narration, dbo.Vouchers4.Debit, dbo.Vouchers4.Credit FROM dbo.Accounts1 INNER JOIN dbo.Vouchers4 ON dbo.Accounts1.AccountID = dbo.Vouchers4.AccountID INNER JOIN dbo.Vouchers3 ON dbo.Vouchers4.VoucherID = dbo.Vouchers3.VoucherID WHERE (dbo.Vouchers3.VoucherID =" + vid + ")";
                }
                else if (VType == "BC-JV")
                {
                    int vid = Convert.ToInt32(VNo);
                    Qry = @"SELECT dbo.Vouchers3.VoucherID  AS VId, dbo.Vouchers3.vDate AS VDate, dbo.Vouchers4.AccountID AS AccountId, dbo.Accounts1.Title, dbo.Vouchers4.Narration, dbo.Vouchers4.Debit, dbo.Vouchers4.Credit FROM dbo.Accounts1 INNER JOIN dbo.Vouchers4 ON dbo.Accounts1.AccountID = dbo.Vouchers4.AccountID INNER JOIN dbo.Vouchers3 ON dbo.Vouchers4.VoucherID = dbo.Vouchers3.VoucherID WHERE (dbo.Vouchers3.VoucherID =" + vid + ")";
                }
                else if (VType == "SJV")
                {
                    Qry = @" Select VoucherID as Vid,vDate,AccountID,Title,narration,Debit,Credit from qryVoucherBook where VoucherID=" + VNo;
                }
                else if (VType == "PJV")
                {
                    Qry = @" Select VoucherID as Vid,vDate,AccountID,Title,narration,Debit,Credit from qryVoucherBook where VoucherID=" + VNo;
                }
                Command.Connection = Connection;
                da = new SqlDataAdapter(Qry, dbconsql);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsVouchers GetVoucherReceipt_Data(string VNo, string VType)
        {
            try
            {
                dsVouchers ds = new dsVouchers();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                if (VType == "HODV")
                {
                    Qry = @" select VId,VDate,VouchersDetail.AccountId,accounts.Title,Narration,ISNULL(Debit,0) as Debit,ISNULL(Credit,0) as Credit from VouchersDetail join accounts on VouchersDetail.AccountId=accounts.AccountId
                where VType='HOCPV' and VID=" + VNo;
                    //                    Qry = @" select VId,VDate,VouchersDetail.AccountId,Title,Narration,ISNULL(Debit,0) as Debit,ISNULL(Credit,0) as Credit from VouchersDetail
                    //                where VType='CPV' and isnull(Debit,0)>0 and VID=" + VNo;
                }
                else if (VType == "FODV")
                {
                    Qry = @" select VId,VDate,VouchersDetail.AccountId,accounts.Title,Narration,ISNULL(Debit,0) as Debit,ISNULL(Credit,0) as Credit from VouchersDetail join accounts on VouchersDetail.AccountId=accounts.AccountId
                where VType='FOCPV' and VID=" + VNo;
                    //                    Qry = @" select VId,VDate,VouchersDetail.AccountId,Title,Narration,ISNULL(Debit,0) as Debit,ISNULL(Credit,0) as Credit from VouchersDetail
                    //                where VType='CPV' and isnull(Debit,0)>0 and VID=" + VNo;
                }
                else if (VType == "HOCV")
                {
                    Qry = @" select VId,VDate,VouchersDetail.AccountId,accounts.Title,Narration,ISNULL(Debit,0) as Debit,ISNULL(Credit,0) as Credit from VouchersDetail join accounts on VouchersDetail.AccountId=accounts.AccountId
                where VType='HOCRV'  and VID=" + VNo;
                    //                    Qry = @" select VId,VDate,VouchersDetail.AccountId,Title,Narration,ISNULL(Debit,0) as Debit,ISNULL(Credit,0) as Credit from VouchersDetail
                    //                where VType='CRV' and isnull(Credit,0)>0 and VID=" + VNo;
                }
                else if (VType == "FOCV")
                {
                    Qry = @" select VId,VDate,VouchersDetail.AccountId,accounts.Title,Narration,ISNULL(Debit,0) as Debit,ISNULL(Credit,0) as Credit from VouchersDetail join accounts on VouchersDetail.AccountId=accounts.AccountId
                where VType='FOCRV'  and VID=" + VNo;
                    //                    Qry = @" select VId,VDate,VouchersDetail.AccountId,Title,Narration,ISNULL(Debit,0) as Debit,ISNULL(Credit,0) as Credit from VouchersDetail
                    //                where VType='CRV' and isnull(Credit,0)>0 and VID=" + VNo;
                }
                else if (VType == "JV")
                {
                    Qry = @" select VId,VDate,VouchersDetail.AccountId,Title,Narration,ISNULL(Debit,0) as Debit,ISNULL(Credit,0) as Credit from VouchersDetail
                where VType='JJV' and VID=" + VNo;
                }
                else if (VType == "PJV")
                {
                    Qry = @" select VId,VDate,VouchersDetail.AccountId,Title,Narration,ISNULL(Debit,0) as Debit,ISNULL(Credit,0) as Credit from VouchersDetail
                where VType='PJV' and VID=" + VNo;
                }
                else if (VType == "SJV")
                {
                    Qry = @" select VId,VDate,VouchersDetail.AccountId,Title,Narration,ISNULL(Debit,0) as Debit,ISNULL(Credit,0) as Credit from VouchersDetail
                where VType='SJV' and VID=" + VNo;
                }
                else if (VType == "BD-JV")
                {
                    Qry = @" select VId,VDate,VouchersDetail.AccountId,Title,Narration,ISNULL(Debit,0) as Debit,ISNULL(Credit,0) as Credit from VouchersDetail
                where VType='BPV' and VID=" + VNo;
                }
                else if (VType == "BC-JV")
                {
                    Qry = @" select VId,VDate,VouchersDetail.AccountId,Title,Narration,ISNULL(Debit,0) as Debit,ISNULL(Credit,0) as Credit from VouchersDetail
                where VType='BRV' and VID=" + VNo;
                }
                else if (VType == "HOJV")
                {
                    Qry = @" select VId,VDate,VouchersDetail.AccountId,Title,Narration,ISNULL(Debit,0) as Debit,ISNULL(Credit,0) as Credit from VouchersDetail
                where VType='HOJJV' and VID=" + VNo;
                }
                else if (VType == "FOJV")
                {
                    Qry = @" select VId,VDate,VouchersDetail.AccountId,Title,Narration,ISNULL(Debit,0) as Debit,ISNULL(Credit,0) as Credit from VouchersDetail
                where VType='FOJJV' and VID=" + VNo;
                }
                Command.Connection = Connection;
                da = new SqlDataAdapter(Qry, dbconsql);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
        private dsAccountLedger GetAccountLedger_Data(DateTime FDate, DateTime TDate, string AccountId, bool IsCB = false, bool IsCF = false, bool IsDB = false, bool IsJB = false)
        {
            try
            {
                dsAccountLedger ds = new dsAccountLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                if (!IsCB && !IsCF && !IsDB && !IsJB)
                    Qry = @"  exec SPRW_GetAccountLedger @FromDate='" + FDate + "',@ToDate='" + TDate + "',@AccCode='" + AccountId + "'";
                else if (IsCB)
                    Qry = @"  exec SPRW_GetCashBook @FromDate='" + FDate + "',@ToDate='" + TDate + "',@Type='" + AccountId + "'";
                else if (IsCF)
                    Qry = @"  exec SPRW_GetCashFlow @FromDate='" + FDate + "',@ToDate='" + TDate + "'";
                else if (IsDB)
                    Qry = @"  exec SPRW_GetDayBook @FromDate='" + FDate + "',@ToDate='" + TDate + "',@Type='" + AccountId + "'";
                else if (IsJB)
                    Qry = @"  exec SPRW_GetJournalBook @FromDate='" + FDate + "',@ToDate='" + TDate + "'";
                Command.Connection = Connection;
                da = new SqlDataAdapter(Qry, dbconsql);
                da.Fill(ds, ds.Tables[0].TableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
