using ApnaAccountWeb.DataSets;
using ApnaAccountWeb.Models;
using ApnaAccountWeb.ViewModels;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ApnaAccountWeb.Controllers
{
    public class IReportsController : Controller
    {
        string strTitle = "";

        public ActionResult Report(string rtype, string dtpDatefrom, string dtpDateto, string txtAccCode, string title,string type,string product,string group,string excel)
        {
            if (rtype == "rptDailyPurchase")
            {
                TempData["dpDateto"] = dtpDateto;
                TempData["dpDatefrom"] = dtpDatefrom;
                ViewBag.rtype = rtype;
            }
            if (rtype == "rptDailyProcess")
            {
                TempData["dpDateto"] = dtpDateto;
                TempData["dpDatefrom"] = dtpDatefrom;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptDailyGrossProfit")
            {
                TempData["dpDateto"] = dtpDateto;
                TempData["dpDatefrom"] = dtpDatefrom;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptInOut")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "ProductList")
            {
                ViewBag.rtype = rtype;
            }
            else if (rtype == "CustomerLimit")
            {
                ViewBag.rtype = rtype;
            }
            else if (rtype == "ProductListGroupWise")
            {
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptPartyWisePurchase")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptGatePassRMPartyWise")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptEmployGatePass")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptGatePassMMPartyWise")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptGatePassRMProductWise")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptGatePassMMProductWise")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptOutGatePassFGPartyWise")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptOutGatePassMixPartyWise")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptOutGatePassFGProductWise")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptOutGatePassMixProductWise")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }  
            else if (rtype == "rptPartyWiseSale")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptPartyWiseSaleRecovery")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptGroupWiseProductPurchase")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptGroupWiseProductSale")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptPartyWiseProductPurchase")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptPartyWiseProductSale")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptDailyGrossProfitParty")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptDailySale")
            {
                TempData["dsDateto"] = dtpDateto;
                TempData["dsDatefrom"] = dtpDatefrom;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptStockInHand")
            {
                TempData["sihDateto"] = dtpDateto;
                TempData["sihtype"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptStockInHandDetail")
            {
                TempData["sihDateto"] = dtpDateto;
                TempData["sihtype"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptStockInHandByGroup")
            {
                TempData["sihDateto"] = dtpDateto;
                TempData["sihtype"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptStockInHandByStore")
            {
                TempData["sihDateto"] = dtpDateto;
                TempData["sihtype"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptStockInHandValueAvgRate")
            {
                TempData["sihDateto"] = dtpDateto;
                TempData["sihtype"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptStockInHandValueAvgRateFW")
            {
                TempData["sihDateto"] = dtpDateto;
                TempData["sihtype"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptStockInHandValue")
            {
                TempData["sihDateto"] = dtpDateto;
                TempData["sihtype"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptStockLedger")
            {
                TempData["slDateto"] = dtpDateto;
                TempData["slDatefrom"] = dtpDatefrom;
                TempData["sltxtAccCode"] = txtAccCode;
                TempData["sltitle"] = title;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptBardanaPartyLedger")
            {
                TempData["slDateto"] = dtpDateto;
                TempData["slDatefrom"] = dtpDatefrom;
                TempData["sltxtAccCode"] = txtAccCode;
                TempData["sltitle"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptBardanaLedger")
            {
                TempData["slDateto"] = dtpDateto;
                TempData["slDatefrom"] = dtpDatefrom;
                TempData["sltxtAccCode"] = txtAccCode;
                TempData["sltitle"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptBardanaPartyStatus")
            {
                TempData["slDateto"] = dtpDateto;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptBardanaPartyStatus2")
            {
                TempData["slDateto"] = dtpDateto;
                TempData["slDatefrom"] = dtpDatefrom;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptBardanaDailyRegister")
            {
                TempData["slDateto"] = dtpDateto;
                TempData["slDatefrom"] = dtpDatefrom;              
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptProduction")
            {
                TempData["slDateto"] = dtpDateto;
                TempData["slDatefrom"] = dtpDatefrom;               
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptGstRegister")
            {
                TempData["slDateto"] = dtpDateto;
                TempData["slDatefrom"] = dtpDatefrom;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptStockLedgerFinalWeight")
            {
                TempData["slDateto"] = dtpDateto;
                TempData["slDatefrom"] = dtpDatefrom;
                TempData["sltxtAccCode"] = txtAccCode;
                TempData["sltitle"] = title;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptStockLedgerWithOutIssue")
            {
                TempData["slDateto"] = dtpDateto;
                TempData["slDatefrom"] = dtpDatefrom;
                TempData["sltxtAccCode"] = txtAccCode;
                TempData["sltitle"] = title;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptPurchaseRate")
            {
                TempData["slDateto"] = dtpDateto;
                TempData["slDatefrom"] = dtpDatefrom;
                TempData["sltxtAccCode"] = txtAccCode;
                TempData["sltitle"] = title;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptProductPosition")
            {
                TempData["slDateto"] = dtpDateto;
                TempData["slDatefrom"] = dtpDatefrom;
                TempData["sltxtAccCode"] = txtAccCode;
                TempData["sltitle"] = title;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptProductPurchase")
            {
                TempData["slDateto"] = dtpDateto;
                TempData["slDatefrom"] = dtpDatefrom;
                TempData["sltxtAccCode"] = txtAccCode;
                TempData["sltitle"] = title;
                TempData["type"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptGPRMPartyProduct")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["product"] = product;               
                TempData["excel"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptSalePurchaseDetailed")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["product"] = product;
                TempData["type"] = type;
                TempData["group"] = group;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptPurchaseOrderReport")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["product"] = product;
                TempData["group"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptPendingPOReport")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["product"] = product;
                TempData["group"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptSaleOrderReport")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["product"] = product;
                TempData["group"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptPendingSOReport")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["product"] = product;
                TempData["group"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptPendingDCReport")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["product"] = product;
                TempData["group"] = group;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptSalePurchaseGeneral")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                TempData["product"] = product;
                TempData["type"] = type;
                TempData["group"] = group;
                TempData["excel"] = excel;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptPurchaseLedgerInvoiceWise")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                ViewBag.rtype = rtype;
            }
            else if (rtype == "rptSaleLedgerInvoiceWise")
            {
                TempData["dtpDateto"] = dtpDateto;
                TempData["dtpDatefrom"] = dtpDatefrom;
                TempData["txtAccCode"] = txtAccCode;
                TempData["title"] = title;
                ViewBag.rtype = rtype;
            }
            return View();
        }
        public ActionResult ProductPostion()
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
                ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult ProductPosition()
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
                ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult StockLedgerNew()
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
                ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult StockLedger()
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
                ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult BardanaPartyLedger()
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
                ViewBag.AccCodeDetails = AccountsList.ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult BardanaLedger()
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
                ViewBag.AccCodeDetails = (from s in db.BardanaTypes
                                           select new DetailList { Code = s.BTypeID, Title = s.Title }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult BardanaDailyRegister()
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
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult BardanaPartyStatus()
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
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult BardanaPartyStatus2()
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
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult DailyProduction()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult GstRegister()
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
                ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult StockLedgerFinalWeight()
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
                ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult StockLedgerWithOutIssue()
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
                ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult ProductPurchase()
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
                ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult GatePassRawMaterialProductWise()
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
                ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult GatePassMixMaterialProductWise()
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
                ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult OutwardGatePassFGProductWise()
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
                ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult OutwardGatePassMixProductWise()
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
                ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult ProductSale()
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
                ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult PurchaseRate()
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
                ViewBag.AccCodeDetails = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult SalePurchaseDetailed()
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
                ViewBag.AccCodeDetails2 = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult PurchaseOrderReport()
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
                ViewBag.AccCodeDetails2 = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }

        public ActionResult PendingPOReport()
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
                ViewBag.AccCodeDetails = db.Accounts.Where(x => x.AccType.Trim() == "Detail" && x.IsParty==true).Select(x => new DetailList { AccountId = x.AccountId, Title = x.Title }).ToList();
                ViewBag.AccCodeDetails2 = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }

        public ActionResult SaleOrderReport()
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
                ViewBag.AccCodeDetails2 = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }

        public ActionResult PendingSOReport()
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
                ViewBag.AccCodeDetails = db.Accounts.Where(x => x.AccType.Trim() == "Detail" && x.IsParty==true).Select(x => new DetailList { AccountId = x.AccountId, Title = x.Title }).ToList();
                ViewBag.AccCodeDetails2 = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }


        public ActionResult PendingDCReport()
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
                ViewBag.AccCodeDetails = db.Accounts.Where(x => x.AccType.Trim() == "Detail" && x.IsParty == true).Select(x => new DetailList { AccountId = x.AccountId, Title = x.Title }).ToList();
                ViewBag.AccCodeDetails2 = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }

        public ActionResult GPRMPartyProduct()
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
                ViewBag.AccCodeDetails2 = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult SalePurchaseGeneral()
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
                ViewBag.AccCodeDetails2 = db.Products.Select(x => new DetailList { Code = x.ProductID, Title = x.ProductName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult DailyGrossProfitParty()
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
        public ActionResult PartyWiseSaleOverall()
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
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult PartyWiseSale()
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
        public ActionResult PartyWiseSaleRecovery()
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
        public ActionResult PartyWiseProductSale()
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
        public ActionResult PurchaseLedgerInvoiceWise()
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
                //ViewBag.AccCodeDetails = db.Accounts.Where(x => x.AccType.Trim() == "Detail" && x.ParentId.StartsWith("124")).Select(x => new DetailList { AccountId = x.AccountID, Title = x.Title }).ToList();
                ViewBag.AccCodeDetails = db.Accounts.Where(x => x.AccType.Trim() == "Detail").Select(x => new DetailList { AccountId = x.AccountId, Title = x.Title }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult SaleLedgerInvoiceWise()
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
                ViewBag.AccCodeDetails = db.Accounts.Where(x => x.AccType.Trim() == "Detail" ).Select(x => new DetailList { AccountId = x.AccountId, Title = x.Title }).ToList();
                //ViewBag.AccCodeDetails = db.Accounts1.Where(x => x.Type.Trim() == "Detail" && x.ParentID.StartsWith("124")).Select(x => new DetailList { AccountId = x.AccountID, Title = x.Title }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult PartyWiseProductPurchase()
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
        public ActionResult GroupWiseProductSale()
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
                ViewBag.AccCodeDetails = db.ProductGroups.Select(x => new DetailList { Code = x.PG_Id, Title = x.Title }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult GroupWiseProductPurchase()
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
                ViewBag.AccCodeDetails = db.ProductGroups.Select(x => new DetailList { Code = x.PG_Id, Title = x.Title }).ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult PartyWisePurchaseOverall()
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
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        public ActionResult PartyWisePurchase()
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
        public ActionResult GatePassRawMaterialPartyWise()
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
        public ActionResult GatePassMixMaterialPartyWise()
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
        public ActionResult EmployGatePass()
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
        public ActionResult OutwardGatePassFGPartyWise()
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
        public ActionResult OutwardGatePassMixPartyWise()
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
        public ActionResult StockInHand()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult StockInHandDetail()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult StockInHandByGroup()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult StockInHandByStore()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult StockInHandValueAvgRate()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult StockInHandValueAvgRateFW()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult StockInHandValue()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult DailyPurchase()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult DailyProcess()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult DailyGrossProfit()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult InOut()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public ActionResult DailySale()
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
        public ActionResult rptProductPosition()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["slDateto"]);
            string FDate = Convert.ToString(TempData["slDatefrom"]);
            string AccountId = Convert.ToString(TempData["sltxtAccCode"]);
            string Title = Convert.ToString(TempData["sltitle"]);
            string type = Convert.ToString(TempData["type"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            string company = Convert.ToString(Session["Company"]);
      
           
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptStoreWiseStockInhand.rpt")));
            rd.SetDataSource(GetProductPosition_Data(fdate, tdate, AccountId, type));
            rd.SummaryInfo.ReportComments = strTitle;
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Product:  " + Title + "     As on Date: "+tdate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
            [HttpGet]
            public ActionResult rptStockInHandByGroup()
            {
                string dbcon = Convert.ToString(Session["con"]);
                string dbconsql = Convert.ToString(Session["consql"]);
                if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
                {
                    return RedirectToAction("Login", "User");
                }
                string TDate = Convert.ToString(TempData["sihDateto"]);
                DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
                string company = Convert.ToString(Session["Company"]);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/IReports/rptStoreWiseStockInhand.rpt")));
                rd.SetDataSource(GetStockInHandGroup_Data( tdate, ""));
                rd.SummaryInfo.ReportComments = "Stock In Hand Group Wise";
                rd.SummaryInfo.ReportTitle = company;
                rd.SummaryInfo.ReportAuthor = "     As on Date: " + tdate;
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
            [HttpGet]
            public ActionResult rptStockInHandByStore()
            {
                string dbcon = Convert.ToString(Session["con"]);
                string dbconsql = Convert.ToString(Session["consql"]);
                if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
                {
                    return RedirectToAction("Login", "User");
                }
                string TDate = Convert.ToString(TempData["sihDateto"]);
                DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
                string company = Convert.ToString(Session["Company"]);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/IReports/rptStoreWiseStockInhand.rpt")));
                rd.SetDataSource(GetStockInHandGroup_Data(tdate, ""));
                rd.SummaryInfo.ReportComments = "Stock In Hand Store Wise";
                rd.SummaryInfo.ReportTitle = company;
                rd.SummaryInfo.ReportAuthor = "     As on Date: " + tdate;
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
            [HttpGet]
            public ActionResult rptPurchaseRate()
            {
                string dbcon = Convert.ToString(Session["con"]);
                string dbconsql = Convert.ToString(Session["consql"]);
                if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
                {
                    return RedirectToAction("Login", "User");
                }
                string TDate = Convert.ToString(TempData["slDateto"]);
                string FDate = Convert.ToString(TempData["slDatefrom"]);
                string AccountId = Convert.ToString(TempData["sltxtAccCode"]);
                string Title = Convert.ToString(TempData["sltitle"]);
                string type = Convert.ToString(TempData["type"]);
                string company = Convert.ToString(Session["Company"]);
                DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
                DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/IReports/rptPurchaseRateReport.rpt")));
                rd.SetDataSource(GetPurchaseRate_Data(fdate, tdate, AccountId, type));
                rd.SummaryInfo.ReportComments = strTitle;
                rd.SummaryInfo.ReportTitle = company;
                rd.SummaryInfo.ReportAuthor = "Dated From: " + fdate + " To: " + tdate;
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return new FileStreamResult(stream, "application/pdf");
            }
             [HttpGet]
        public ActionResult rptProductPurchase()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["slDateto"]);
            string FDate = Convert.ToString(TempData["slDatefrom"]);
            string AccountId = Convert.ToString(TempData["sltxtAccCode"]);
            string Title = Convert.ToString(TempData["sltitle"]);
            string type = Convert.ToString(TempData["type"]);
            string company = Convert.ToString(Session["Company"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
                 if(type.Equals("Sale"))
                 {
                     rd.Load(Path.Combine(Server.MapPath("~/IReports/rptPartySale.rpt")));
                 }
                 else
                 {
                     rd.Load(Path.Combine(Server.MapPath("~/IReports/rptPartyPurchase.rpt")));
                 }
            rd.SetDataSource(GetProductPurchase_Data(fdate, tdate, AccountId,type));
            rd.SummaryInfo.ReportComments = strTitle;
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Dated From: " + fdate + " To: " + tdate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptStockLedger()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["slDateto"]);
            string FDate = Convert.ToString(TempData["slDatefrom"]);
            string AccountId = Convert.ToString(TempData["sltxtAccCode"]);
            string Title = Convert.ToString(TempData["sltitle"]);
            string type = Convert.ToString(TempData["type"]);
            string company = Convert.ToString(Session["Company"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            if (type.Equals("ByQTY"))
            {
                rd.Load(Path.Combine(Server.MapPath("~/IReports/rptStockLedgerByQty.rpt")));
            }
            else if (type.Equals("ByWeight"))
            {
                rd.Load(Path.Combine(Server.MapPath("~/IReports/rptStockLedgerByWeight.rpt")));
            }
            else if (type.Equals("Combined"))
            {
                rd.Load(Path.Combine(Server.MapPath("~/IReports/rptStockLedger.rpt")));
            }
            rd.SetDataSource(GetStockLedger_Data(fdate, tdate, AccountId));
            rd.SummaryInfo.ReportComments = "Stock Ledger Report";
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Product:   " + Title + "   From: " +fdate+ " To: " +tdate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptBardanaDailyRegister()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["slDateto"]);
            string FDate = Convert.ToString(TempData["slDatefrom"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            string company = Convert.ToString(Session["Company"]);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptBardanaDaily.rpt")));
            rd.SetDataSource(GetDailyBardana_Data(fdate, tdate));
            rd.SetParameterValue("prmUrduDate", "cÍiBM");
            rd.SetParameterValue("prmUrduRefNo", "ÊiBqA");
            rd.SetParameterValue("prmUrduBagType", "›iÌI Án³");
            rd.SetParameterValue("prmUrduDebit", "ÂBÃ");
            rd.SetParameterValue("prmUrduCredit", "©ÀU");
            rd.SetParameterValue("prmUrduParty", "ÓiB‚ ÂBÃ");
            rd.SetParameterValue("prmUrduGrandTotal", "½· ÂBÀM");
            rd.SummaryInfo.ReportComments = "Daily Bardana (Bags) Report";
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Dated From: " + fdate + " To: " + tdate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptBardanaPartyStatus()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["slDateto"]);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptBardanaSummaryPartyWise.rpt")));
            rd.SetDataSource(GetBardanaPartyStatus_Data(tdate));
            rd.SetParameterValue("prmUrduDebit", "ÂBÃ");
            rd.SetParameterValue("prmUrduCredit", "©ÀU");
            rd.SetParameterValue("prmUrduParty", "ÓiB‚ ÂBÃ");
            rd.SetParameterValue("prmUrduBalance", "BÍB´I");
            rd.SetParameterValue("prmUrduGrandTotal", "½· ÂBÀM");
            rd.SummaryInfo.ReportComments = "Party Bardana Status";
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "As on: " + tdate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptBardanaPartyStatus2()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["slDateto"]);
            string FDate = Convert.ToString(TempData["slDatefrom"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptBardanaSummaryPartyWise.rpt")));
            rd.SetDataSource(GetBardanaPartyStatus_Data2(fdate,tdate));
            rd.SetParameterValue("prmUrduDebit", "ÂBÃ");
            rd.SetParameterValue("prmUrduCredit", "©ÀU");
            rd.SetParameterValue("prmUrduParty", "ÓiB‚ ÂBÃ");
            rd.SetParameterValue("prmUrduBalance", "BÍB´I");
            rd.SetParameterValue("prmUrduGrandTotal", "½· ÂBÀM");
            rd.SummaryInfo.ReportComments = "Party Bardana Status";
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "From : " +fdate+ "To :"+tdate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptBardanaPartyLedger()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["slDateto"]);
            string FDate = Convert.ToString(TempData["slDatefrom"]);
            string AccountId = Convert.ToString(TempData["sltxtAccCode"]);
            string Title = Convert.ToString(TempData["sltitle"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptBardanaPartyWise.rpt")));
            rd.SetDataSource(GetBardanaPartyWise_Data(fdate, tdate,AccountId,Title));
            rd.SetParameterValue("OBalance", "1");
            rd.SetParameterValue("prmUrduDate", "cÍiBM");
            rd.SetParameterValue("prmUrduRefNo", "ÊiBqA");
            rd.SetParameterValue("prmUrduBagType", "›iÌI Án³");
            rd.SetParameterValue("prmUrduDebit", "ÂBÃ");
            rd.SetParameterValue("prmUrduCredit", "©ÀU");
            rd.SetParameterValue("prmUrduBalance", "BÍB´I");
            rd.SetParameterValue("prmUrduGrandTotal", "½· ÂBÀM");
            rd.SummaryInfo.ReportComments = "Bardana Party Wise Report";
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Party:" + Title +"Dated From: " + fdate + " To: " + tdate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptBardanaLedger()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["slDateto"]);
            string FDate = Convert.ToString(TempData["slDatefrom"]);
            string AccountId = Convert.ToString(TempData["sltxtAccCode"]);
            string Title = Convert.ToString(TempData["sltitle"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptBardanaLedger.rpt")));
            rd.SetDataSource(GetBardanaPartyLedger_Data(fdate, tdate, AccountId, Title));
            rd.SummaryInfo.ReportComments = "Bardana Ledger Report";
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Product:   " + Title + "   From: " +fdate+ " To: " +tdate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptProduction()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["slDateto"]);
            string FDate = Convert.ToString(TempData["slDatefrom"]);            
            string type = Convert.ToString(TempData["type"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            if (type.Equals("DailyProductionOverAll")||type.Equals("DailyAdjustmentOverAll"))
            {
                rd.Load(Path.Combine(Server.MapPath("~/IReports/rptDailyProduction.rpt")));
                rd.SetDataSource(GetProduction1_Data(fdate, tdate, type));
            }
            else if (type.Equals("IndividualFormulaRunReport")||type.Equals("IndividualFormulaAdjustment"))
            {
                rd.Load(Path.Combine(Server.MapPath("~/IReports/rptIndividualFormulaRunReport.rpt")));
                rd.SetDataSource(GetProduction2_Data(fdate, tdate, type));
            }
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);
            rd.SummaryInfo.ReportComments = Address;
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = type;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
         [HttpGet]
        public ActionResult rptGstRegister()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["slDateto"]);
            string FDate = Convert.ToString(TempData["slDatefrom"]);
            string type = Convert.ToString(TempData["type"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptGstRegister.rpt")));
            rd.SetDataSource(GetGstRegister_Data(fdate, tdate, type));
            rd.SetParameterValue("prmDate", "From Date : " + fdate + " To : " + tdate);
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);
            rd.SummaryInfo.ReportComments = Address;
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "GST" + type + "Register"; ;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
             [HttpGet]
        public ActionResult rptStockLedgerWithOutIssue()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["slDateto"]);
            string FDate = Convert.ToString(TempData["slDatefrom"]);
            string AccountId = Convert.ToString(TempData["sltxtAccCode"]);
            string Title = Convert.ToString(TempData["sltitle"]);
            string type = Convert.ToString(TempData["type"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            if (type.Equals("ByQTY"))
            {
                rd.Load(Path.Combine(Server.MapPath("~/IReports/rptStockLedgerByQty.rpt")));
            }
            else if (type.Equals("ByWeight"))
            {
                rd.Load(Path.Combine(Server.MapPath("~/IReports/rptStockLedgerByWeight.rpt")));
            }
            else if (type.Equals("Combined"))
            {
                rd.Load(Path.Combine(Server.MapPath("~/IReports/rptStockLedger.rpt")));
            }
            rd.SetDataSource(GetStockLedger_Data3(fdate, tdate, AccountId));
            rd.SummaryInfo.ReportComments = "Stock Ledger Report";
            string company = Convert.ToString(Session["Company"]);
           
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Product:   " + Title + "   From: " + fdate + " To: " + tdate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptStockLedgerFinalWeight()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["slDateto"]);
            string FDate = Convert.ToString(TempData["slDatefrom"]);
            string AccountId = Convert.ToString(TempData["sltxtAccCode"]);
            string Title = Convert.ToString(TempData["sltitle"]);
            string type = Convert.ToString(TempData["type"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            if (type.Equals("ByQTY"))
            {
                rd.Load(Path.Combine(Server.MapPath("~/IReports/rptStockLedgerByQty.rpt")));
            }
            else if (type.Equals("ByWeight"))
            {
                rd.Load(Path.Combine(Server.MapPath("~/IReports/rptStockLedgerByWeight.rpt")));
            }
            else if (type.Equals("Combined"))
            {
                rd.Load(Path.Combine(Server.MapPath("~/IReports/rptStockLedger.rpt")));
            }
            rd.SetDataSource(GetStockLedger_Data2(fdate, tdate, AccountId));
            rd.SummaryInfo.ReportComments = "Stock Ledger Report";
            string company = Convert.ToString(Session["Company"]);
           
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Product:   " + Title + "   From: " + fdate + " To: " + tdate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptStockInHand()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string tDate = Convert.ToString(TempData["sihDateto"]);
            string type = Convert.ToString(TempData["sihtype"]);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptStockInHand.rpt")));
                rd.SetDataSource(GetStockInHand2_Data2(todate, type));
                if (type == "rtStockInHandEstimated")
                {
                    rd.SummaryInfo.ReportComments = "Stock In Hand Seed";
                }
                else if (type == "rtStockInHandEstimatedGunji")
                {
                    rd.SummaryInfo.ReportComments = "Stock In Hand Seed Gunji";
                }
                else if (type == "rtStockInHandProcessByFinished")
                {
                    rd.SummaryInfo.ReportComments = "Stock In Hand Processed/Finished";
                }
                else
                {
                    rd.SummaryInfo.ReportComments = "Stock In Hand";
                }
                string company = Convert.ToString(Session["Company"]);
               
                rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "As on Date:   " + tDate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptStockInHandDetail()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string tDate = Convert.ToString(TempData["sihDateto"]);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptStockInHand1.rpt")));
            rd.SetDataSource(GetStockInHand_Data(todate,""));
            rd.SummaryInfo.ReportComments = "Stock In Hand Report";
            string company = Convert.ToString(Session["Company"]);
            
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "As on Date:   " +tDate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptStockInHandValueAvgRate()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string tDate = Convert.ToString(TempData["sihDateto"]);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptStockInHandValueWise.rpt")));
            rd.SetDataSource(GetStockInHand4Val_Data(todate));
            rd.SummaryInfo.ReportComments = "Stock In Hand Value Wise (Avg Rate)";
            string company = Convert.ToString(Session["Company"]);

            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "As on Date:   " + tDate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptStockInHandValueAvgRateFW()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string tDate = Convert.ToString(TempData["sihDateto"]);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptStockInHandValueWise.rpt")));
            rd.SetDataSource(GetStockInHand5Val_Data(todate));
            rd.SummaryInfo.ReportComments = "Stock In Hand Value Wise (Avg Rate)";
            string company = Convert.ToString(Session["Company"]);

            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "As on Date:   " + tDate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptStockInHandValue()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string tDate = Convert.ToString(TempData["sihDateto"]);
            string type = Convert.ToString(TempData["sihtype"]);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptStockInHandValueWise.rpt")));
            rd.SetDataSource(GetStockInHand3_Data(todate, type));
            rd.SummaryInfo.ReportComments = "Stock In Hand Value Wise";
            string company = Convert.ToString(Session["Company"]);

            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "As on Date:   " + tDate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptDailyPurchase()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string fDate = Convert.ToString(TempData["dpDatefrom"]);
            string tDate = Convert.ToString(TempData["dpDateto"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptDailyPurchase.rpt")));
            rd.SetDataSource(GetDailyPurchase_Data(frodate, todate));
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);
          
            rd.SetParameterValue("Company", company);
            rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("From", frodate);
            rd.SetParameterValue("To", todate);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }



        [HttpGet]
        public ActionResult rptDailyProcess()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }

            string fDate = Convert.ToString(TempData["dpDatefrom"]);
            string tDate = Convert.ToString(TempData["dpDateto"]);
            string excel = Convert.ToString(TempData["type"]);

            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptDailyProcess.rpt")));

            rd.SetDataSource(GetDailyProcess_Data(frodate, todate, excel));
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);

            rd.SetParameterValue("Company", company);
            rd.SetParameterValue("Address", Address);
            //rd.SetParameterValue("Phone", CompanyInfo.Phone);
            rd.SetParameterValue("Type", excel);
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
        public ActionResult rptDailyGrossProfit()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string fDate = Convert.ToString(TempData["dpDatefrom"]);
            string tDate = Convert.ToString(TempData["dpDateto"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptDailyGrossProfit.rpt")));
            rd.SetDataSource(GetDailyGrossProfit_Data(frodate, todate));
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);

            rd.SetParameterValue("Company", company);
            rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("From", frodate);
            rd.SetParameterValue("To", todate);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptDailyGrossProfitParty()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string title = Convert.ToString(TempData["title"]);
            string accountid = Convert.ToString(TempData["txtAccCode"]);
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptDailyGrossProfit2.rpt")));
            rd.SetDataSource(GetDailyGrossProfit_Data2(frodate, todate, accountid));
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);

            rd.SetParameterValue("Company", company);
            rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("From", frodate);
            rd.SetParameterValue("To", todate);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptPartyWisePurchase()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string title = Convert.ToString(TempData["title"]);
            string accountid = Convert.ToString(TempData["txtAccCode"]);
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            string type = Convert.ToString(TempData["type"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptPartySale.rpt")));
            rd.SetDataSource(GetPartyWisePurchase_Data(frodate, todate, accountid,type));
            rd.SummaryInfo.ReportComments = strTitle;
            string company = Convert.ToString(Session["Company"]);
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Dated From: " + frodate + " To: " + todate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptGPRMPartyProduct()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string title = Convert.ToString(TempData["title"]);
            string accountid = Convert.ToString(TempData["txtAccCode"]);
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            string productid = Convert.ToString(TempData["product"]);
            string typee = Convert.ToString(TempData["excel"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptInWardGatePass.rpt")));
            rd.SetDataSource(GetGatePassRMPartyProductWise_Data(frodate, todate, accountid, productid, typee));
            rd.SetParameterValue("OBalance", 0);
            rd.SummaryInfo.ReportComments = "Inward Gate Pass Raw Material Party Product Wise Report";
            string company = Convert.ToString(Session["Company"]);
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Party:" + title + "Dated From: " + frodate + " To: " + todate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptGatePassRMPartyWise()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string title = Convert.ToString(TempData["title"]);
            string accountid = Convert.ToString(TempData["txtAccCode"]);
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            string type = Convert.ToString(TempData["type"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptInWardGatePass.rpt")));
            rd.SetDataSource(GetGatePassRMPartyWise_Data(frodate, todate, accountid, type));
            rd.SetParameterValue("OBalance", 0);
            rd.SummaryInfo.ReportComments = "Inward Gate Pass Raw Material Party Wise Report";
            string company = Convert.ToString(Session["Company"]);
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Party:" + title + "Dated From: " + frodate + " To: " + todate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptGatePassMMPartyWise()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string title = Convert.ToString(TempData["title"]);
            string accountid = Convert.ToString(TempData["txtAccCode"]);
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            string type = Convert.ToString(TempData["type"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptInWardGatePass.rpt")));
            rd.SetDataSource(GetGatePassMMPartyWise_Data(frodate, todate, accountid, type));
            rd.SetParameterValue("OBalance", 0);
            rd.SummaryInfo.ReportComments = "Inward Gate Pass Mixed  Material Party Wise Report";
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Party:" + title + "Dated From: " + frodate + " To: " + todate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptGatePassRMProductWise()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string title = Convert.ToString(TempData["title"]);
            string accountid = Convert.ToString(TempData["txtAccCode"]);
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            string type = Convert.ToString(TempData["type"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptreportPartyWise.rpt")));
            rd.SetDataSource(GetGatePassRMProductWise_Data(frodate, todate, accountid, type));
            rd.SetParameterValue("OBalance", 0);
            rd.SummaryInfo.ReportComments = "Inward Gate Pass Raw Material Product  Wise Report";
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Product:" + title + "Dated From: " + frodate + " To: " + todate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptGatePassMMProductWise()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string title = Convert.ToString(TempData["title"]);
            string accountid = Convert.ToString(TempData["txtAccCode"]);
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            string type = Convert.ToString(TempData["type"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptReportMixedProductWise.rpt")));
            rd.SetDataSource(GetGatePassMMProductWise_Data(frodate, todate, accountid, type));
            rd.SetParameterValue("OBalance", 0);
            rd.SummaryInfo.ReportComments = "Inward Gate Pass Mix Material Product  Wise Report";
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Product:" + title + "Dated From: " + frodate + " To: " + todate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptInOut()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptInWardOutRetunable.rpt")));
            rd.SetDataSource(GetInOut_Data(frodate, todate));
           // rd.SetParameterValue("OBalance", 0);
            rd.SummaryInfo.ReportComments = "In/OutWard Returnable";
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor ="Dated From: " + frodate + " To: " + todate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptEmployGatePass()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string title = Convert.ToString(TempData["title"]);
            string accountid = Convert.ToString(TempData["txtAccCode"]);
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            string type = Convert.ToString(TempData["type"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptEmployGatePass.rpt")));
            rd.SetDataSource(GetEmployGatePass_Data(frodate, todate, accountid, type));
            rd.SetParameterValue("OBalance", 0);
            rd.SummaryInfo.ReportComments = "Employ Gate Pass Report";
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Dated From: " + frodate + " To: " + todate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptOutGatePassFGPartyWise()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string title = Convert.ToString(TempData["title"]);
            string accountid = Convert.ToString(TempData["txtAccCode"]);
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            string type = Convert.ToString(TempData["type"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptOutWardGatePassFinish.rpt")));
            rd.SetDataSource(GetGatePassFGPartyWise_Data(frodate, todate, accountid, type));
            rd.SetParameterValue("OBalance", 0);
            rd.SummaryInfo.ReportComments = "OutWard Gate Pass Finish Goods Party Wise Report";
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Party:" + title + "Dated From: " + frodate + " To: " + todate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptOutGatePassMixPartyWise()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string title = Convert.ToString(TempData["title"]);
            string accountid = Convert.ToString(TempData["txtAccCode"]);
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            string type = Convert.ToString(TempData["type"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptInWardGatePass.rpt")));
            rd.SetDataSource(GetGatePassMM2PartyWise_Data(frodate, todate, accountid, type));
            rd.SetParameterValue("OBalance", 0);
            rd.SummaryInfo.ReportComments = "Outward Gate pass Mixed material Party Wise Report";
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Party:" + title + "Dated From: " + frodate + " To: " + todate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptOutGatePassFGProductWise()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string title = Convert.ToString(TempData["title"]);
            string accountid = Convert.ToString(TempData["txtAccCode"]);
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            string type = Convert.ToString(TempData["type"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptOutWardGatePassFinishP.rpt")));
            rd.SetDataSource(GetGatePassFGProductWise_Data(frodate, todate, accountid, type));
            rd.SetParameterValue("OBalance", 0);
            rd.SummaryInfo.ReportComments = "OutWard Gate Pass Finish Goods Product Wise Report";
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Product:" + title + "Dated From: " + frodate + " To: " + todate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptOutGatePassMixProductWise()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string title = Convert.ToString(TempData["title"]);
            string accountid = Convert.ToString(TempData["txtAccCode"]);
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            string type = Convert.ToString(TempData["type"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptInWardGatePass.rpt")));
            rd.SetDataSource(GetGatePassMM2ProductWise_Data(frodate, todate, accountid, type));
            rd.SetParameterValue("OBalance", 0);
            rd.SummaryInfo.ReportComments = "Outward Gate pass Mixed material Product Wise Report";
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Product:" + title + "Dated From: " + frodate + " To: " + todate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptPartyWiseSale()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string title = Convert.ToString(TempData["title"]);
            string accountid = Convert.ToString(TempData["txtAccCode"]);
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            decimal gross = 0;
            decimal discount = 0;
            decimal netamt = 0;
            SqlConnection Connection = new SqlConnection(dbconsql);
            Connection.Open();
            SqlCommand cmd = new SqlCommand("SPRW_GetPartyWiseSalePrevious1", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FromDate", frodate);
            cmd.Parameters.Add("@ToDate", todate);
            cmd.Parameters.Add("@party", accountid);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    gross = Convert.ToDecimal(reader["GrossAmount"]);
                    discount = Convert.ToDecimal(reader["Discount"]);
                    netamt = Convert.ToDecimal(reader["NetAmount"]);
                }
            }
            reader.Close();
            decimal gross1 = 0;
            decimal discount1 = 0;
            decimal netamt1 = 0;
            SqlCommand cmd1 = new SqlCommand("SPRW_GetPartyWiseSalePrevious2", Connection);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@FromDate", frodate);
            cmd1.Parameters.Add("@ToDate", todate);
            cmd1.Parameters.Add("@party", accountid);
            SqlDataReader reader1;
            reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    gross1 = Convert.ToDecimal(reader1["GrossAmount"]);
                    discount1 = Convert.ToDecimal(reader1["Discount"]);
                    netamt1 = Convert.ToDecimal(reader1["NetAmount"]);
                }
            }
            decimal tgross = gross - gross1;
            decimal tdiscount = discount - discount1;
            decimal tnetamt = netamt - netamt1;
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptPartyWiseSale.rpt")));
            rd.SetDataSource(GetPartyWiseSale_Data(frodate, todate, accountid));
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);

            rd.SetParameterValue("Company", company);
            rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("From", frodate);
            rd.SetParameterValue("To", todate);
            //rd.SetParameterValue("pgross", 0);
            //rd.SetParameterValue("pdiscount", 0);
            //rd.SetParameterValue("pnet", 0);
            rd.SetParameterValue("pgross", tgross);
            rd.SetParameterValue("pdiscount", tdiscount);
            rd.SetParameterValue("pnet", tnetamt);
            rd.SetParameterValue("customer", title);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptPartyWiseSaleRecovery()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string title = Convert.ToString(TempData["title"]);
            string accountid = Convert.ToString(TempData["txtAccCode"]);
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            string type = Convert.ToString(TempData["type"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptRecovery.rpt")));
            rd.SetDataSource(GetPartyWiseRecovery_Data(frodate, todate, accountid, type));
            rd.SummaryInfo.ReportComments = strTitle;
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Dated From: " + frodate + " To: " + todate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptPurchaseLedgerInvoiceWise()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string title = Convert.ToString(TempData["title"]);
            string accountid = Convert.ToString(TempData["txtAccCode"]);
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptPartyLedger.rpt")));
            rd.SummaryInfo.ReportTitle = "Purchase Ledger Invoice Voice";
            rd.SetDataSource(GetPurchaseLedgerInvoiceWise_Data(frodate, todate, accountid, title, "PurchaseLedger"));
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);
            rd.SetParameterValue("prmTitle", company);
            rd.SetParameterValue("prmAddress", Address);
            rd.SetParameterValue("prmTitles", "Account: " + accountid + " " + title + "Date From: " + frodate + " To: " + todate);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
[HttpGet]
public ActionResult rptSaleLedgerInvoiceWise()
{
    string dbcon = Convert.ToString(Session["con"]);
    string dbconsql = Convert.ToString(Session["consql"]);
    if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
    {
        return RedirectToAction("Login", "User");
    }
    string title = Convert.ToString(TempData["title"]);
    string accountid = Convert.ToString(TempData["txtAccCode"]);
    string fDate = Convert.ToString(TempData["dtpDatefrom"]);
    string tDate = Convert.ToString(TempData["dtpDateto"]);
    DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
    DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
    Response.Buffer = false;
    Response.ClearContent();
    Response.ClearHeaders();
    ReportDocument rd = new ReportDocument();
    rd.Load(Path.Combine(Server.MapPath("~/IReports/rptPartyLedger.rpt")));
    rd.SummaryInfo.ReportTitle = "Sale Ledger Invoice Voice";
    rd.SetDataSource(GetPurchaseLedgerInvoiceWise_Data(frodate, todate, accountid, title, "SaleLedger"));
    string company = Convert.ToString(Session["Company"]);
    string Address = Convert.ToString(Session["Address"]);
    rd.SetParameterValue("prmTitle", company);
    rd.SetParameterValue("prmAddress", Address);
    rd.SetParameterValue("prmTitles", "Account: " + accountid + " " + title + "Date From: " + frodate + " To: " + todate);
    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
    stream.Seek(0, SeekOrigin.Begin);
    rd.Close();
    rd.Dispose();
    return new FileStreamResult(stream, "application/pdf");
}
        [HttpGet]
        public ActionResult rptGroupWiseProductSale()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string title = Convert.ToString(TempData["title"]);
            string accountid = Convert.ToString(TempData["txtAccCode"]);
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptGroupWiseProductSale.rpt")));
            rd.SetDataSource(GetGroupWiseProductSale_Data(frodate, todate, accountid));
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);

            rd.SetParameterValue("Company", company);
            rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("From", frodate);
            rd.SetParameterValue("To", todate);
            rd.SetParameterValue("customer", title);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptPartyWiseProductPurchase()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string title = Convert.ToString(TempData["title"]);
            string accountid = Convert.ToString(TempData["txtAccCode"]);
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptPartyWiseProductPurchase.rpt")));
            rd.SetDataSource(GetPartyWiseProductPurchase_Data(frodate, todate, accountid));
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);

            rd.SetParameterValue("Company", company);
            rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("From", frodate);
            rd.SetParameterValue("To", todate);
            rd.SetParameterValue("customer", title);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptGroupWiseProductPurchase()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string title = Convert.ToString(TempData["title"]);
            string accountid = Convert.ToString(TempData["txtAccCode"]);
            string fDate = Convert.ToString(TempData["dtpDatefrom"]);
            string tDate = Convert.ToString(TempData["dtpDateto"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptGroupWiseProductPurchase.rpt")));
            rd.SetDataSource(GetGroupWiseProductPurchase_Data(frodate, todate, accountid));
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);

            rd.SetParameterValue("Company", company);
            rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("From", frodate);
            rd.SetParameterValue("To", todate);
            rd.SetParameterValue("customer", title);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptDailySale()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string fDate = Convert.ToString(TempData["dsDatefrom"]);
            string tDate = Convert.ToString(TempData["dsDateto"]);
            DateTime frodate = DateTime.ParseExact(fDate, "dd/MM/yyyy", null);
            DateTime todate = DateTime.ParseExact(tDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptDailySale.rpt")));
            rd.SetDataSource(GetDailySale_Data(frodate, todate));


            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);
            
            rd.SetParameterValue("Company", company);
            rd.SetParameterValue("Address", Address);
            rd.SetParameterValue("From", frodate);
            rd.SetParameterValue("To", todate);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
 [HttpGet]
        public ActionResult ProductListGroupWise()
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
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptProductListGroupWise.rpt")));
            rd.SetDataSource(GetProductListGroupwise_Data());
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);
            rd.SetParameterValue("prmTitle", company);
            rd.SetParameterValue("prmAddress", Address);        
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult ProductList()
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
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptProductList.rpt")));
            rd.SetDataSource(GetProductList_Data());
            string company = Convert.ToString(Session["Company"]);
            string Address = Convert.ToString(Session["Address"]);
            rd.SetParameterValue("prmTitle", company);
            rd.SetParameterValue("prmAddress", Address);         
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult rptSalePurchaseDetailed()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["dtpDateto"]);
            string FDate = Convert.ToString(TempData["dtpDatefrom"]);
            string AccountId = Convert.ToString(TempData["txtAccCode"]);
            string Title = Convert.ToString(TempData["title"]);
            string product = Convert.ToString(TempData["product"]);
            string type = Convert.ToString(TempData["type"]);
            string group = Convert.ToString(TempData["group"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptPurSaleDetailed.rpt")));
            rd.SetDataSource(GetSalePurchaseDetailed_Data(fdate, tdate, AccountId, Title, product, type, group));
            rd.SummaryInfo.ReportComments = strTitle;
            string company = Convert.ToString(Session["Company"]);            
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Dated From: " + fdate + " To: " + tdate;
            string excel = Convert.ToString(TempData["excel"]);
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
        [HttpGet]
        public ActionResult rptSalePurchaseGeneral()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["dtpDateto"]);
            string FDate = Convert.ToString(TempData["dtpDatefrom"]);
            string AccountId = Convert.ToString(TempData["txtAccCode"]);
            string Title = Convert.ToString(TempData["title"]);
            string product = Convert.ToString(TempData["product"]);
            string type = Convert.ToString(TempData["type"]);
            string group = Convert.ToString(TempData["group"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
          //  rd.Load(Path.Combine(Server.MapPath("~/IReports/rptPurSale.rpt")));
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptPurSale.rpt")));
            rd.SetDataSource(GetSalePurchaseGeneral_Data(fdate, tdate, AccountId, Title, product, type, group));
            rd.SummaryInfo.ReportComments = strTitle;
            string company = Convert.ToString(Session["Company"]);
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Dated From: " + fdate + " To: " + tdate;
            //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //stream.Seek(0, SeekOrigin.Begin);
            //return new FileStreamResult(stream, "application/pdf");
            string excel = Convert.ToString(TempData["excel"]);
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
        public ActionResult rptPurchaseOrderReport()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["dtpDateto"]);
            string FDate = Convert.ToString(TempData["dtpDatefrom"]);
            string AccountId = Convert.ToString(TempData["txtAccCode"]);
            string Title = Convert.ToString(TempData["title"]);
            string product = Convert.ToString(TempData["product"]);
            string group = Convert.ToString(TempData["group"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptPurchaseOrder.rpt")));
            rd.SetDataSource(GetPurchaseOrder_Data(fdate, tdate, AccountId, Title, product, group));
            rd.SummaryInfo.ReportComments = "Purchase Order  ";
            string company = Convert.ToString(Session["Company"]);
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Dated From: " + fdate + " To: " + tdate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }

        [HttpGet]
        public ActionResult rptPendingPOReport()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["dtpDateto"]);
            string FDate = Convert.ToString(TempData["dtpDatefrom"]);
            string AccountId = Convert.ToString(TempData["txtAccCode"]);
            string Title = Convert.ToString(TempData["title"]);
            string product = Convert.ToString(TempData["product"]);
            string group = Convert.ToString(TempData["group"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptPendingPOReport.rpt")));
            rd.SetDataSource(GetPendingPurchaseOrder_Data(fdate, tdate, AccountId, Title, product, group));
            rd.SummaryInfo.ReportComments = "Pending Purchase Order  ";
            string company = Convert.ToString(Session["Company"]);
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Dated From: " + fdate + " To: " + tdate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }

        [HttpGet]
        public ActionResult rptSaleOrderReport()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["dtpDateto"]);
            string FDate = Convert.ToString(TempData["dtpDatefrom"]);
            string AccountId = Convert.ToString(TempData["txtAccCode"]);
            string Title = Convert.ToString(TempData["title"]);
            string product = Convert.ToString(TempData["product"]);
            string group = Convert.ToString(TempData["group"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptPurchaseOrder.rpt")));
            rd.SetDataSource(GetSaleOrder_Data(fdate, tdate, AccountId, Title, product, group));
            rd.SummaryInfo.ReportComments = "Sale Order  ";
            string company = Convert.ToString(Session["Company"]);
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Dated From: " + fdate + " To: " + tdate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }

        [HttpGet]
        public ActionResult rptPendingSOReport()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["dtpDateto"]);
            string FDate = Convert.ToString(TempData["dtpDatefrom"]);
            string AccountId = Convert.ToString(TempData["txtAccCode"]);
            string Title = Convert.ToString(TempData["title"]);
            string product = Convert.ToString(TempData["product"]);
            string group = Convert.ToString(TempData["group"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptPendingPOReport.rpt")));
            rd.SetDataSource(GetPendingSO_Data(fdate, tdate, AccountId, Title, product, group));
            rd.SummaryInfo.ReportComments = "Pending Sale Order  ";
            string company = Convert.ToString(Session["Company"]);
            rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Dated From: " + fdate + " To: " + tdate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }


        [HttpGet]
        public ActionResult rptPendingDCReport()
        {
            string dbcon = Convert.ToString(Session["con"]);
            string dbconsql = Convert.ToString(Session["consql"]);
            if (string.IsNullOrEmpty(dbcon) || string.IsNullOrEmpty(dbconsql))
            {
                return RedirectToAction("Login", "User");
            }
            string TDate = Convert.ToString(TempData["dtpDateto"]);
            string FDate = Convert.ToString(TempData["dtpDatefrom"]);
            string AccountId = Convert.ToString(TempData["txtAccCode"]);
            string Title = Convert.ToString(TempData["title"]);
            string product = Convert.ToString(TempData["product"]);
            string group = Convert.ToString(TempData["group"]);
            DateTime fdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            DateTime tdate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptPendingPOReport.rpt")));
            rd.SetDataSource(GetPendingDC_Data(fdate, tdate, AccountId, Title, product, group));
            rd.SummaryInfo.ReportComments = "Pending Delivery Challan  ";
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
            rd.SummaryInfo.ReportAuthor = "Dated From: " + fdate + " To: " + tdate;
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            return new FileStreamResult(stream, "application/pdf");
        }

        [HttpGet]
        public ActionResult CustomerLimit()
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
            rd.Load(Path.Combine(Server.MapPath("~/IReports/rptOverLimit.rpt")));
            rd.SetDataSource(GetCreditLimit_Data());
            rd.SummaryInfo.ReportComments = "Customer Over Limit";
            string company = Convert.ToString(Session["Company"]);rd.SummaryInfo.ReportTitle = company;
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
        private dsStockInHand GetStockInHand_Data(DateTime Tdate,string type)
        {
            try
            {
                dsStockInHand ds = new dsStockInHand();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql= "";
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.Connection = Connection;
                Sql = Sql + " SELECT qryInvBook.ProductID,qryInvBook.ProductTitle, ProductGroups.Title FROM ProductGroups INNER JOIN (qryInvBook INNER JOIN Products ON qryInvBook.ProductID = Products.ProductID) ON ProductGroups.PG_ID = Products.PG_ID group by qryInvBook.ProductID,qryInvBook.ProductTitle,ProductGroups.Title";
                 SqlDataAdapter adapter = new SqlDataAdapter(Sql, Connection);
                 DataTable datatable = new DataTable();
                 adapter.Fill(datatable);
                 for (int i = 0; i < datatable.Rows.Count - 1; i++)
                 {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["ProductID"] = datatable.Rows[i][0].ToString();
                            dr["ProductTitle"] = datatable.Rows[i][1].ToString();
                            dr["Title"] = datatable.Rows[i][2].ToString();
                            SqlDataAdapter adapter2 = new SqlDataAdapter("select  sum(qtydr-qtycr) as OpeningQty,sum(wtDr-wtCr) as openingwt FROM ProductGroups INNER JOIN (qryInvBook INNER JOIN Products ON qryInvBook.ProductID = Products.ProductID) ON ProductGroups.PG_ID = Products.PG_ID where qryInvBook.InvDate<'" + Tdate + "' and qryInvBook.ProductID=" + datatable.Rows[i][0].ToString() + " and upper(ProductGroups.Title)='" + datatable.Rows[i][2].ToString() + "' group by qryInvBook.ProductID,qryInvBook.ProductTitle,ProductGroups.Title", Connection);
                            DataTable datatable2 = new DataTable();
                            adapter2.Fill(datatable2);
                            dr["opening"] = 0;
                            dr["openingwt"] = 0;
                            for (int j = 0; j < datatable2.Rows.Count ; j++)
                            {
                                dr["opening"] = Convert.ToDecimal(datatable2.Rows[j][0]);
                                dr["openingwt"] = Convert.ToDecimal(datatable2.Rows[j][1]);
                            }
                            SqlDataAdapter adapter3 = new SqlDataAdapter("select sum(qtydr) as qtydr1,sum(wtDr) as wtdr1 FROM ProductGroups INNER JOIN (qryInvBook INNER JOIN Products ON qryInvBook.ProductID = Products.ProductID) ON ProductGroups.PG_ID = Products.PG_ID where qryInvBook.InvDate='" +Tdate+ "' and qryInvBook.ProductID=" +datatable.Rows[i][0].ToString() +" and upper(ProductGroups.Title)='"+ datatable.Rows[i][2].ToString() + "' group by qryInvBook.ProductID,qryInvBook.ProductTitle,ProductGroups.Title", Connection);
                            DataTable datatable3 = new DataTable();
                            adapter3.Fill(datatable3);
                            dr["purchase"] = 0;
                            dr["PurchaseWt"] = 0;
                            for (int k = 0; k < datatable3.Rows.Count ; k++)
                            {
                                dr["purchase"] = Convert.ToDecimal(datatable3.Rows[k][0]);
                                dr["PurchaseWt"] = Convert.ToDecimal(datatable3.Rows[k][0]);
                            }
                            SqlDataAdapter adapter4 = new SqlDataAdapter("select sum(qtyCr) as qtyCr1,sum(WtCr) as WtCr1 FROM ProductGroups INNER JOIN (qryInvBook INNER JOIN Products ON qryInvBook.ProductID = Products.ProductID) ON ProductGroups.PG_ID = Products.PG_ID where qryInvBook.InvDate='" + Tdate + "' and qryInvBook.ProductID=" + datatable.Rows[i][0].ToString() + " and upper(ProductGroups.Title)='" + datatable.Rows[i][2].ToString() + "' group by qryInvBook.ProductID,qryInvBook.ProductTitle,ProductGroups.Title", Connection);
                            DataTable datatable4 = new DataTable();
                            adapter4.Fill(datatable4);
                            dr["issue"] = 0;
                            dr["issueWt"] = 0;
                            for (int l = 0; l < datatable4.Rows.Count ; l++)
                            {
                                dr["issue"] = Convert.ToDecimal(datatable4.Rows[l][0]);
                                dr["issueWt"] = Convert.ToDecimal(datatable4.Rows[l][0]);
                            }
                            ds.Tables[0].Rows.Add(dr);
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
        private dsStockValueWise GetStockInHand3_Data(DateTime Tdate, string type)
        {
            try
            {
                dsStockValueWise ds = new dsStockValueWise();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql = "";
                decimal dblQty=0;
                 decimal dblWt=0;
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.Connection = Connection;
                Sql = " SELECT TOP 100 PERCENT ProductID, ProductName, ISNULL(RateCalc1, '') AS Rate1, ISNULL(RateCalc2, '') AS Rate2, SMW, PMW, SalePrice, PurchasePrice From Products ORDER BY ProductID";
                SqlDataAdapter adapter = new SqlDataAdapter(Sql, Connection);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);
                for (int i = 0; i < datatable.Rows.Count - 1; i++)
                {
                    DataRow dr = ds.Tables[0].NewRow();
                    dr["ProductID"] = datatable.Rows[i][0].ToString();
                    dr["ProductName"] = datatable.Rows[i][1].ToString();
                    SqlDataAdapter adapter2 = new SqlDataAdapter("Select Sum(QtyDr-QtyCr) as Qty,Sum(WtDr-WtCr) as Weight from qryInvBook where Invdate <= '" + Tdate + "' and ProductID=" + datatable.Rows[i][0].ToString() + "", Connection);
                    DataTable datatable2 = new DataTable();
                    adapter2.Fill(datatable2);
                    dr["Qty"] = 0;
                    dr["Weight"] = 0;
                    for (int j = 0; j < datatable2.Rows.Count; j++)
                    {
                        dr["Qty"] = datatable2.Rows[j][0];
                        dr["Weight"] = datatable2.Rows[j][1];
                        if (!(datatable2.Rows[j][0] is DBNull))
                            dblQty = Convert.ToDecimal(datatable2.Rows[j][0]);
                        if (!(datatable2.Rows[j][1] is DBNull))
                            dblWt = Convert.ToDecimal(datatable2.Rows[j][1]);
                    }
                    if( string.IsNullOrEmpty(datatable.Rows[i][2].ToString()))
                    {
                         dr["Rate"] = 0;
                    dr["Amount"] = 0;
                    }
                    else if (datatable.Rows[i][2].ToString().ToUpper() == "BAGS" && datatable.Rows[i][3].ToString().ToUpper() == "PURCHASE PRICE")
                    {
                        decimal dblQty1 = 0;
                        if (!(datatable.Rows[i][7] is DBNull))
                              dblQty1 = Convert.ToDecimal(datatable.Rows[i][7]);
                        dr["Rate"] = dblQty1;
                        dr["Amount"] = dblQty * dblQty1;
                    }
                    else if (datatable.Rows[i][2].ToString().ToUpper() == "BAGS" && datatable.Rows[i][3].ToString().ToUpper() == "SALE PRICE")
                    {
                        decimal dblQty1 = 0;
                        if (!(datatable.Rows[i][6] is DBNull))
                            dblQty1 = Convert.ToDecimal(datatable.Rows[i][6]);
                        dr["Rate"] = dblQty1;
                        dr["Amount"] = dblQty * dblQty1;
                    }
                    else if (datatable.Rows[i][2].ToString().ToUpper() == "KG" && datatable.Rows[i][3].ToString().ToUpper() == "PURCHASE PRICE")
                    {
                        decimal dblQty1 = 0;
                        if (!(datatable.Rows[i][7] is DBNull))
                            dblQty1 = Convert.ToDecimal(datatable.Rows[i][7]);
                        dr["Rate"] = dblQty1;
                        dr["Amount"] = dblWt * dblQty1;
                    }
                    else if (datatable.Rows[i][2].ToString().ToUpper() == "KG" && datatable.Rows[i][3].ToString().ToUpper() == "SALE PRICE")
                    {
                        decimal dblQty1 = 0;
                        if (!(datatable.Rows[i][6] is DBNull))
                            dblQty1 = Convert.ToDecimal(datatable.Rows[i][6]);
                        dr["Rate"] = dblQty1;
                        dr["Amount"] = dblWt * dblQty1;
                    }
                    else if (datatable.Rows[i][2].ToString().ToUpper() == "MOND" && datatable.Rows[i][3].ToString().ToUpper() == "PURCHASE PRICE")
                    {
                        decimal dblQty1 = 0;
                        if (!(datatable.Rows[i][7] is DBNull))
                            dblQty1 = Convert.ToDecimal(datatable.Rows[i][7]);
                        dr["Rate"] = dblQty1;
                        dr["Amount"] = (dblWt / Convert.ToDecimal(datatable.Rows[i][5])) * dblQty1;
           }
                    else if (datatable.Rows[i][2].ToString().ToUpper() == "MOND" && datatable.Rows[i][3].ToString().ToUpper() == "SALE PRICE")
                    {
                        decimal dblQty1 = 0;
                        if (!(datatable.Rows[i][6] is DBNull))
                            dblQty1 = Convert.ToDecimal(datatable.Rows[i][6]);
                        dr["Rate"] = dblQty1;
                        dr["Amount"] = (dblWt / Convert.ToDecimal(datatable.Rows[i][4])) * dblQty1;
                    }
                    else{
                        dr["Rate"] = 0;
                        dr["Amount"] = 0;
                    }
                    ds.Tables[0].Rows.Add(dr);
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
        private dsStockValueWise GetStockInHand4Val_Data(DateTime Tdate)
        {
            try
            {
                dsStockValueWise ds = new dsStockValueWise();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql = "";
            decimal   dblQty = 0;
            decimal dblWt = 0;
            decimal dblRate = 0;
            decimal dblRate1 = 0;
            decimal dblRate2 = 0;
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.Connection = Connection;
                Sql = " SELECT TOP 100 PERCENT ProductID, ProductName, PG_ID From Products ORDER BY ProductID";
                SqlDataAdapter adapter = new SqlDataAdapter(Sql, Connection);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);
                if (datatable.Rows.Count > 0)
                {
                    for (int i = 0; i < datatable.Rows.Count - 1; i++)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr["ProductID"] = datatable.Rows[i][0].ToString();
                        dr["ProductName"] = datatable.Rows[i][1].ToString();
                        dblQty = 0;
                        dblWt = 0;
                        dblRate = 0;
                        SqlDataAdapter adapter2 = new SqlDataAdapter("Select Sum(QtyDr-QtyCr) as Qty,Sum(WtDr-WtCr) as Weight from qryInvBook where Invdate <= '" + Tdate + "' and ProductID=" + datatable.Rows[i][0].ToString() + "", Connection);
                        DataTable datatable2 = new DataTable();
                        adapter2.Fill(datatable2);
                        dr["Qty"] = 0;
                        dr["Weight"] = 0;
                        if (datatable2.Rows.Count > 0)
                        {
                            for (int j = 0; j < datatable2.Rows.Count; j++)
                            {
                                dr["Qty"] = datatable2.Rows[j][0];
                                dr["Weight"] = datatable2.Rows[j][1];
                                if (!(datatable2.Rows[j][0] is DBNull))
                                    dblQty = Convert.ToDecimal(datatable2.Rows[j][0]);
                                if (!(datatable2.Rows[j][1] is DBNull))
                                    dblWt = Convert.ToDecimal(datatable2.Rows[j][1]);
                            }
                        }
                        SqlDataAdapter adapter3 = new SqlDataAdapter("Select Sum(QtyDr-QtyCr) as Qty,Sum(WtDr-WtCr) as Weight from qryInvBook where Invdate <= '" + Tdate + "' and ProductID=" + datatable.Rows[i][0].ToString() + "", Connection);
                        DataTable datatable3= new DataTable();
                        adapter3.Fill(datatable3);
                        if (datatable3.Rows.Count > 0)
                        {
                            for (int j = 0; j < datatable3.Rows.Count; j++)
                            {
                                if (!(datatable3.Rows[j][0] is DBNull))
                                    dblRate = Convert.ToDecimal(datatable3.Rows[j][0]);
                            }
                        }
                        if (Convert.ToDecimal(datatable.Rows[i][2]) == 13)
                        {
                            dr["Rate"] = dblRate;
                            dr["Amount"] = dblRate*dblQty;
                        }
                        else if (Convert.ToDecimal(datatable.Rows[i][2]) == 1)
                        {
                            SqlDataAdapter adapter4 = new SqlDataAdapter("SELECT     SUM(Amount) / SUM(QtyCr-QtyDr) AS Rate1 From dbo.Sale2 where ProductID=" + datatable.Rows[i][0].ToString() + "", Connection);
                            DataTable datatable4 = new DataTable();
                            adapter4.Fill(datatable4);
                            if (datatable4.Rows.Count > 0)
                            {
                                for (int j = 0; j < datatable4.Rows.Count; j++)
                                {
                                    if (!(datatable4.Rows[j][0] is DBNull))
                                        dblRate = Convert.ToDecimal(datatable4.Rows[j][0]);
                                    else
                                        dblRate = 0;
                                }
                                dr["Rate"] = dblRate;
                                dr["Amount"] = dblRate * dblQty;
                            }
                        }
                        SqlDataAdapter adapter5 = new SqlDataAdapter("SELECT distinct RateCalc,ProductID From dbo.qryAvgPurchaseRate where ProductID=" +datatable.Rows[i][0].ToString()+ "", Connection);
                        DataTable datatable5= new DataTable();
                        adapter5.Fill(datatable5);
                        if (datatable5.Rows.Count > 0)
                        {
                            for (int k = 0; k < datatable5.Rows.Count; k++)
                            {
                            if (datatable5.Rows.Count == 1 && datatable5.Rows[k][0].ToString() == "BAGS")
                            {
                                dr["Rate"] = dblRate;
                                dr["Amount"] = dblRate * dblQty;
                            }
                            else if (datatable5.Rows.Count == 1 && datatable5.Rows[k][0].ToString() != "BAGS")
                            {
                                dr["Rate"] = dblRate;
                                dr["Amount"] = dblRate * dblWt;
                            }
                            else if (datatable5.Rows.Count >1)
                            {
                                SqlDataAdapter adapter6 = new SqlDataAdapter("SELECT SUM(Amount1) / SUM(Wt*50) AS Rate1 From dbo.qryAvgPurchaseRate where ProductID=" +datatable.Rows[i][0].ToString()+ " and RateCalc='BAGS'", Connection);
                                DataTable datatable6 = new DataTable();
                                adapter6.Fill(datatable6);
                                if (datatable6.Rows.Count > 0)
                                {
                                    for (int j = 0; j < datatable6.Rows.Count; j++)
                                    {
                                        if (!(datatable6.Rows[j][0] is DBNull))
                                            dblRate1 = Convert.ToDecimal(datatable6.Rows[j][0]);
                                    }
                                }
                                SqlDataAdapter adapter7 = new SqlDataAdapter("SELECT SUM(Amount1) / SUM(Wt*50) AS Rate1 From dbo.qryAvgPurchaseRate where ProductID=" + datatable.Rows[i][0].ToString() + " and RateCalc='BAGS'", Connection);
                                DataTable datatable7 = new DataTable();
                                adapter7.Fill(datatable7);
                                if (datatable7.Rows.Count > 0)
                                {
                                    for (int j = 0; j < datatable7.Rows.Count; j++)
                                    {
                                        if (!(datatable7.Rows[j][0] is DBNull))
                                            dblRate2 = Convert.ToDecimal(datatable7.Rows[j][0]);
                                    }
                                }
                                dblRate = (dblRate1 + dblRate2) / 2;
                                dr["Rate"] = dblRate;
                                dr["Amount"] = dblRate * dblWt;
                            }
                        }
                        }
                        ds.Tables[0].Rows.Add(dr);
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
        private dsStockValueWise GetStockInHand5Val_Data(DateTime Tdate)
        {
            try
            {
                dsStockValueWise ds = new dsStockValueWise();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql = "";
                decimal dblQty = 0;
                decimal dblWt = 0;
                decimal dblRate = 0;
                decimal dblRate1 = 0;
                decimal dblRate2 = 0;
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.Connection = Connection;
                Sql = " SELECT TOP 100 PERCENT ProductID, ProductName, PG_ID From Products ORDER BY ProductID";
                SqlDataAdapter adapter = new SqlDataAdapter(Sql, Connection);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);
                if (datatable.Rows.Count > 0)
                {
                    for (int i = 0; i < datatable.Rows.Count - 1; i++)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr["ProductID"] = datatable.Rows[i][0].ToString();
                        dr["ProductName"] = datatable.Rows[i][1].ToString();
                        dblQty = 0;
                        dblWt = 0;
                        dblRate = 0;
                        SqlDataAdapter adapter2 = new SqlDataAdapter("Select Sum(QtyDr-QtyCr) as Qty,Sum(WtDr-WtCr) as Weight from qryInvBookFinalWeight  where Invdate <= '" + Tdate + "' and ProductID=" + datatable.Rows[i][0].ToString() + "", Connection);
                        DataTable datatable2 = new DataTable();
                        adapter2.Fill(datatable2);
                        dr["Qty"] = 0;
                        dr["Weight"] = 0;
                        if (datatable2.Rows.Count > 0)
                        {
                            for (int j = 0; j < datatable2.Rows.Count; j++)
                            {
                                dr["Qty"] = datatable2.Rows[j][0];
                                dr["Weight"] = datatable2.Rows[j][1];
                                if (!(datatable2.Rows[j][0] is DBNull))
                                    dblQty = Convert.ToDecimal(datatable2.Rows[j][0]);
                                if (!(datatable2.Rows[j][1] is DBNull))
                                    dblWt = Convert.ToDecimal(datatable2.Rows[j][1]);
                            }
                        }
                        SqlDataAdapter adapter3 = new SqlDataAdapter("Select Sum(QtyDr-QtyCr) as Qty,Sum(WtDr-WtCr) as Weight from qryInvBook where Invdate <= '" + Tdate + "' and ProductID=" + datatable.Rows[i][0].ToString() + "", Connection);
                        DataTable datatable3 = new DataTable();
                        adapter3.Fill(datatable3);
                        if (datatable3.Rows.Count > 0)
                        {
                            for (int j = 0; j < datatable3.Rows.Count; j++)
                            {
                                if (!(datatable3.Rows[j][0] is DBNull))
                                    dblRate = Convert.ToDecimal(datatable3.Rows[j][0]);
                            }
                        }
                        if (Convert.ToDecimal(datatable.Rows[i][2]) == 13)
                        {
                            dr["Rate"] = dblRate;
                            dr["Amount"] = dblRate * dblQty;
                        }
                        else if (Convert.ToDecimal(datatable.Rows[i][2]) == 1)
                        {
                            SqlDataAdapter adapter4 = new SqlDataAdapter("SELECT     SUM(Amount) / SUM(QtyCr-QtyDr) AS Rate1 From dbo.Sale2 where ProductID=" + datatable.Rows[i][0].ToString() + "", Connection);
                            DataTable datatable4 = new DataTable();
                            adapter4.Fill(datatable4);
                            if (datatable4.Rows.Count > 0)
                            {
                                for (int j = 0; j < datatable4.Rows.Count; j++)
                                {
                                    if (!(datatable4.Rows[j][0] is DBNull))
                                        dblRate = Convert.ToDecimal(datatable4.Rows[j][0]);
                                    else
                                        dblRate = 0;
                                }
                                dr["Rate"] = dblRate;
                                dr["Amount"] = dblRate * dblQty;
                            }
                        }
                        SqlDataAdapter adapter5 = new SqlDataAdapter("SELECT distinct RateCalc,ProductID From dbo.qryAvgPurchaseRate where ProductID=" + datatable.Rows[i][0].ToString() + "", Connection);
                        DataTable datatable5 = new DataTable();
                        adapter5.Fill(datatable5);
                        if (datatable5.Rows.Count > 0)
                        {
                            for (int k = 0; k < datatable5.Rows.Count; k++)
                            {
                                if (datatable5.Rows.Count == 1 && datatable5.Rows[k][0].ToString() == "BAGS")
                                {
                                    dr["Rate"] = dblRate;
                                    dr["Amount"] = dblRate * dblQty;
                                }
                                else if (datatable5.Rows.Count == 1 && datatable5.Rows[k][0].ToString() != "BAGS")
                                {
                                    dr["Rate"] = dblRate;
                                    dr["Amount"] = dblRate * dblWt;
                                }
                                else if (datatable5.Rows.Count > 1)
                                {
                                    SqlDataAdapter adapter6 = new SqlDataAdapter("SELECT SUM(Amount1) / SUM(Wt*50) AS Rate1 From dbo.qryAvgPurchaseRate where ProductID=" + datatable.Rows[i][0].ToString() + " and RateCalc='BAGS'", Connection);
                                    DataTable datatable6 = new DataTable();
                                    adapter6.Fill(datatable6);
                                    if (datatable6.Rows.Count > 0)
                                    {
                                        for (int j = 0; j < datatable6.Rows.Count; j++)
                                        {
                                            if (!(datatable6.Rows[j][0] is DBNull))
                                                dblRate1 = Convert.ToDecimal(datatable6.Rows[j][0]);
                                        }
                                    }
                                    SqlDataAdapter adapter7 = new SqlDataAdapter("SELECT SUM(Amount1) / SUM(Wt*50) AS Rate1 From dbo.qryAvgPurchaseRate where ProductID=" + datatable.Rows[i][0].ToString() + " and RateCalc='BAGS'", Connection);
                                    DataTable datatable7 = new DataTable();
                                    adapter7.Fill(datatable7);
                                    if (datatable7.Rows.Count > 0)
                                    {
                                        for (int j = 0; j < datatable7.Rows.Count; j++)
                                        {
                                            if (!(datatable7.Rows[j][0] is DBNull))
                                                dblRate2 = Convert.ToDecimal(datatable7.Rows[j][0]);
                                        }
                                    }
                                    dblRate = (dblRate1 + dblRate2) / 2;
                                    dr["Rate"] = dblRate;
                                    dr["Amount"] = dblRate * dblWt;
                                }
                            }
                        }
                        ds.Tables[0].Rows.Add(dr);
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
        private dsAccountLedger GetDailyPurchase_Data(DateTime FDate, DateTime Tdate)
        {
            try
            {
                dsAccountLedger ds = new dsAccountLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Qry = @"  exec SPRW_GetDailyPurchase @FromDate='" + FDate + "',@ToDate='" + Tdate + "'";
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

        private dsProcess GetDailyProcess_Data(DateTime FDate, DateTime Tdate, string Type)
        {
            try
            {

                dsProcess ds = new dsProcess();
                string dbconsql = Convert.ToString(Session["consql"]);

                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                if (Type != "")
                {
                    Qry = @"Select * from qryViewProcess where IssueDate between'" + FDate + "' and '" + Tdate + "' and ProcessType='" + Type + "'";
                }
                else
                {
                    Qry = @"Select * from qryViewProcess where IssueDate between'" + FDate + "' and '" + Tdate + "'";
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

        private dsDailyGross GetDailyGrossProfit_Data(DateTime FDate, DateTime Tdate)
        {
            try
            {
                dsDailyGross ds = new dsDailyGross();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Qry = @"  exec SPRW_GetDailyGrossProfit @FromDate='" + FDate + "',@ToDate='" + Tdate + "',@party='NO'";
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
        private dsDailyGross GetDailyGrossProfit_Data2(DateTime FDate, DateTime Tdate,string accountid)
        {
            try
            {
                dsDailyGross ds = new dsDailyGross();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Qry = @"  exec SPRW_GetDailyGrossProfit @FromDate='" + FDate + "',@ToDate='" + Tdate + "',@party='" + accountid + "'";
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
        private dsPartywisePurchase GetPartyWiseSale_Data(DateTime FDate, DateTime Tdate, string accountid)
        {
            try
            {
                dsPartywisePurchase ds = new dsPartywisePurchase();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Qry = @"  exec SPRW_GetPartyWiseSale @FromDate='" + FDate + "',@ToDate='" + Tdate + "',@party='" + accountid + "'";
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
        private dsPurchaseSaleLedger GetPurchaseLedgerInvoiceWise_Data(DateTime FDate, DateTime Tdate, string accountid, string title, string type)
        {
            try
            {
                string Sql = "";
                string Sql2 = "";
                dsPurchaseSaleLedger ds = new dsPurchaseSaleLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                if(type.Equals("PurchaseLedger"))
                {
                    Sql = "select ''+ 'OB' as voucherNo,vDate,'Opening Balances' as narration,sum(debit) as debit, sum(credit) as credit,sum(debit)-sum(credit) as Closing  from qryPurSaleAccountLedger where accountId='" + accountid + "' and vdate< '" + FDate + "' group by voucherNo, vDate,narration,debit,credit ";
                    Sql2 = "SELECT     voucherNo, vDate, Reference as RefNo, debit, credit, narration, Product, VehNo, Qty, Weight, Rate, GateNo,0 as Closing from qryPurSaleAccountLedger where accountId='" + accountid + "' and vdate between  '" + FDate + "' and '" + Tdate + "'  and left(reference,3) in ('FP-','FPR') ";
    Sql2 = Sql2 + " order by vdate,voucherNo ";
                }
                else{
                    Sql = "select ''+ 'OB' as voucherNo,vDate,'Opening Balances' as narration,sum(debit) as debit, sum(credit) as credit,sum(debit)-sum(credit) as Closing  from qryPurSaleAccountLedger where accountId='" + accountid + "' and vdate< '" + FDate + "' group by voucherNo, vDate,narration,debit,credit ";
                    Sql2 = "SELECT     voucherNo, vDate, Reference as RefNo, debit, credit, narration, Product, VehNo, Qty, Weight, Rate, GateNo,0 as Closing from qryPurSaleAccountLedger where accountId='" + accountid + "' and vdate between  '" + FDate + "' and '" + Tdate + "' and left(reference,3) in ('FS-','FSR') ";
    Sql2 = Sql2 + " order by vdate,voucherNo ";
                }
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
                da.Fill(ds, ds.Tables[0].TableName);
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql2, dbconsql);
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
        private dsPartywisePurchase GetGroupWiseProductSale_Data(DateTime FDate, DateTime Tdate, string accountid)
        {
            try
            {
                dsPartywisePurchase ds = new dsPartywisePurchase();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Qry = @"  exec SPRW_GetGroupWiseProductSaleDetail @FromDate='" + FDate + "',@ToDate='" + Tdate + "',@party='" + accountid + "'";
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
        private dsPartywisePurchase GetPartyWiseProductPurchase_Data(DateTime FDate, DateTime Tdate, string accountid)
        {
            try
            {
                dsPartywisePurchase ds = new dsPartywisePurchase();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Qry = @"  exec SPRW_GetPartyWiseProductPurchaseDetail @FromDate='" + FDate + "',@ToDate='" + Tdate + "',@party='" + accountid + "'";
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
        private dsPartywisePurchase GetGroupWiseProductPurchase_Data(DateTime FDate, DateTime Tdate, string accountid)
        {
            try
            {
                dsPartywisePurchase ds = new dsPartywisePurchase();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Qry = @"  exec SPRW_GetGroupWiseProductPurchaseDetail @FromDate='" + FDate + "',@ToDate='" + Tdate + "',@party='" + accountid + "'";
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
        private dsPartywisePurchase GetPartyWisePurchase_Data(DateTime FDate, DateTime Tdate, string accountid,string type)
        {
            try
            {
                string Sql = "";
                dsPartywisePurchase ds = new dsPartywisePurchase();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                if (type=="1")
                {
                    Sql = "select * from qryInvBook where Invdate between '" + FDate + "'  and '" + Tdate + "'  and InvId<>'0' ";
    strTitle = "Party Wise Purchase Cost";
    Sql = Sql + " and itype in ('FP','FPR') ";
                }
        else if(type=="0")
        {
                    Sql = "select * from qryInvBook where Invdate between '" + FDate + "'  and '" + Tdate + "'  and InvId<>'0' ";
    strTitle = "Party Wise Sale";
    Sql = Sql + " and itype in ('FS','FSR') ";
}
 else if(type=="2")
        {
            Sql = "select * from qryInvBook where AccountID = '" + accountid + "' and Invdate between '" + FDate + "'  and '" + Tdate + "'  and InvId<>'0' ";
            strTitle = "Party Wise Purchase Cost";
            Sql = Sql + " and itype in ('FP','FPR') ";
}
 else if(type=="3")
        {
            Sql = "select * from qryInvBook where AccountID = '" + accountid + "' and Invdate between '" + FDate + "'  and '" + Tdate + "'  and InvId<>'0' ";
    strTitle = "Party Wise Sale";
    Sql = Sql + " and itype in ('FS','FSR') ";
}
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsGatePass GetGatePassRMPartyWise_Data(DateTime FDate, DateTime Tdate, string accountid, string type)
        {
            try
            {
                string Sql = "";
                dsGatePass ds = new dsGatePass();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT Gate.GateNo, Gate.Gdate, Gate.Gtime, Gate.BuiltyNo, Gate.IntCol1, Accounts.Title, Gate3.ProductID, Products.ItemDetail, Gate.Transport, Gate.VehReference, Gate.Through, Gate3.Quantity, Gate3.StdWeight, Gate3.CalcWeight, Gate.Transportation, Gate.Driver, Gate.DriverCnic, Gate3.BardanaType,Gate.Sno";
                Sql = Sql + " FROM (Products INNER JOIN (Gate3 INNER JOIN Gate ON Gate3.GateNo = Gate.GateNo) ON Products.ProductID = Gate3.ProductID) INNER JOIN Accounts ON Gate.IntCol1 = Accounts.AccountID where Gate.Gdate between  '" +FDate+ "' and '" +Tdate+ "'  and Gate.itype2='GPR'";
                if(!string.IsNullOrEmpty(accountid))
{
Sql = Sql + " and Gate.IntCol1='"+accountid+ "'";
}
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsGatePass GetGatePassRMPartyProductWise_Data(DateTime FDate, DateTime Tdate, string accountid, string type,string typee)
        {
            try
            {
                string Sql = "";
                dsGatePass ds = new dsGatePass();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT Gate.GateNo, Gate.Gdate, Gate.Gtime, Gate.BuiltyNo, Gate.IntCol1, Accounts.Title, Gate3.ProductID, Products.ItemDetail, Gate.Transport, Gate.VehReference, Gate.Through, Gate3.Quantity, Gate3.StdWeight, Gate3.CalcWeight, Gate.Transportation, Gate.Driver, Gate.DriverCnic, Gate3.BardanaType,Gate.Sno";
                Sql = Sql + " FROM (Products INNER JOIN (Gate3 INNER JOIN Gate ON Gate3.GateNo = Gate.GateNo) ON Products.ProductID = Gate3.ProductID) INNER JOIN Accounts ON Gate.IntCol1 = Accounts.AccountID where Gate.Gdate between  '" + FDate + "' and '" + Tdate + "'  and Gate.itype2='" + typee + "'";
                if (!string.IsNullOrEmpty(accountid))
                {
                    Sql = Sql + " and Gate.IntCol1='" + accountid + "'";
                }
                if (!string.IsNullOrEmpty(type))
                {
                    Sql = Sql + " and Gate3.ProductID='" + type + "'";
                }
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsGatePass GetGatePassFGPartyWise_Data(DateTime FDate, DateTime Tdate, string accountid, string type)
        {
            try
            {
                string Sql = "";
                dsGatePass ds = new dsGatePass();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT Gate.GateNo, Gate.Gdate, Gate.Gtime, Gate.BuiltyNo, Gate.IntCol1, Accounts.Title, Gate3.ProductID, Products.ItemDetail, Gate.Transport, Gate.VehReference, Gate.Through, Gate3.Quantity, Gate3.StdWeight, Gate3.CalcWeight, Gate.Transportation, Gate.Driver, Gate.DriverCnic, Gate3.BardanaType,Gate.Sno";
                Sql = Sql + " FROM (Products INNER JOIN (Gate3 INNER JOIN Gate ON Gate3.GateNo = Gate.GateNo) ON Products.ProductID = Gate3.ProductID) INNER JOIN Accounts ON Gate.IntCol1 = Accounts.AccountID where Gate.Gdate between  '" + FDate + "' and '" + Tdate + "'  and Gate.itype2='GOF'";
                if (!string.IsNullOrEmpty(accountid))
                {
                    Sql = Sql + " and Gate.IntCol1='" + accountid + "'";
                }
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsEmpGatePass GetEmployGatePass_Data(DateTime FDate, DateTime Tdate, string accountid, string type)
        {
            try
            {
                string Sql = "";
                dsEmpGatePass ds = new dsEmpGatePass();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT Employ.Title, EmployGatepass.Purpose, EmployGatepass.Remarks, EmployGatepass.IType, EmployGatepass.JDate, EmployGatepass.Timeout, EmployGatepass.Dateout, EmployGatepass.TimeIN, EmployGatepass.DateIN, EmployGatepass.TotalTime, EmployGatepass.EmployeID,Employ.CompID";
Sql = Sql + " FROM Employ INNER JOIN EmployGatepass ON Employ.CompID = EmployGatepass.EmployeID";
Sql = Sql + " where EmployGatepass.JDate between  '"+FDate+ "' and '" +Tdate+ "'  ";
    if (!string.IsNullOrEmpty(accountid))
                {
                    Sql = Sql + " and EmployGatepass.CompID='" +accountid+ "'";
                }             
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsInOut GetInOut_Data(DateTime FDate, DateTime Tdate)
        {
            try
            {
                string Sql = "";
                dsInOut ds = new dsInOut();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT InOutWord.CompID, InOutWord.Title, InOutWord.Product, InOutWord.VchType, InOutWord.Purpose, InOutWord.Remarks, InOutWord.DateOut, InOutWord.Timeout, InOutWord.OutID, InOutWord.IType FROM InOutWord where InOutWord.DateOut between  '" +FDate+ "' and '" +Tdate+ "'  ";
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsGatePass GetGatePassMMPartyWise_Data(DateTime FDate, DateTime Tdate, string accountid, string type)
        {
            try
            {
                string Sql = "";
                dsGatePass ds = new dsGatePass();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT Gate.GateNo, Gate.Gdate, Gate.Gtime, Gate.BuiltyNo, Gate.IntCol1, Accounts.Title, Gate3.ProductID, Products.ItemDetail, Gate.Transport, Gate.VehReference, Gate.Through, Gate3.Quantity, Gate3.StdWeight, Gate3.CalcWeight, Gate.Transportation, Gate.Driver, Gate.DriverCnic, Gate3.BardanaType,Gate.Sno";
                Sql = Sql + " FROM (Products INNER JOIN (Gate3 INNER JOIN Gate ON Gate3.GateNo = Gate.GateNo) ON Products.ProductID = Gate3.ProductID) INNER JOIN Accounts ON Gate.IntCol1 = Accounts.AccountID where Gate.Gdate between  '" + FDate + "' and '" + Tdate + "'  and Gate.itype2='GPM'";
                if (!string.IsNullOrEmpty(accountid))
                {
                    Sql = Sql + " and Gate.IntCol1='" + accountid + "'";
                }
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsGatePass GetGatePassMM2PartyWise_Data(DateTime FDate, DateTime Tdate, string accountid, string type)
        {
            try
            {
                string Sql = "";
                dsGatePass ds = new dsGatePass();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT Gate.GateNo, Gate.Gdate, Gate.Gtime, Gate.BuiltyNo, Gate.IntCol1, Accounts.Title, Gate3.ProductID, Products.ItemDetail, Gate.Transport, Gate.VehReference, Gate.Through, Gate3.Quantity, Gate3.StdWeight, Gate3.CalcWeight, Gate.Transportation, Gate.Driver, Gate.DriverCnic, Gate3.BardanaType,Gate.Sno";
                Sql = Sql + " FROM (Products INNER JOIN (Gate3 INNER JOIN Gate ON Gate3.GateNo = Gate.GateNo) ON Products.ProductID = Gate3.ProductID) INNER JOIN Accounts ON Gate.IntCol1 = Accounts.AccountID where Gate.Gdate between  '" + FDate + "' and '" + Tdate + "'  and Gate.itype2='GOM'";
                if (!string.IsNullOrEmpty(accountid))
                {
                    Sql = Sql + " and Gate.IntCol1='" + accountid + "'";
                }
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsGatePass GetGatePassMMProductWise_Data(DateTime FDate, DateTime Tdate, string accountid, string type)
        {
            try
            {
                string Sql = "";
                dsGatePass ds = new dsGatePass();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT Gate.GateNo, Gate.Gdate, Gate.Gtime, Gate.BuiltyNo, Gate.IntCol1, PartiesGatePass.Title, Gate3.ProductID,ProductGatePass.Title as ItemDetail, Gate.Transport, Gate.VehReference, Gate.Through, ";
        Sql = Sql + "Gate3.Quantity, Gate3.StdWeight, Gate3.CalcWeight, Gate.Transportation, Gate.Driver, Gate.DriverCnic, Gate3.BardanaType,Gate.Sno ";
        Sql = Sql + "FROM (ProductGatePass INNER JOIN (Gate3 INNER JOIN Gate ON Gate3.GateNo = Gate.GateNo) ON ProductGatePass.CompID = Gate3.ProductID) ";
        Sql = Sql + "Inner JOIN PartiesGatePass ON Gate.IntCol1 = PartiesGatePass.CompID";
        Sql = Sql + "  where Gate.Gdate between  '" +FDate+ "' and '" +Tdate+ "'  and Gate.itype2='GPM'";
         if (!string.IsNullOrEmpty(accountid))
                {
                    Sql = Sql + " and Gate3.ProductID='" + accountid + "'";
                }
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsGatePass GetGatePassMM2ProductWise_Data(DateTime FDate, DateTime Tdate, string accountid, string type)
        {
            try
            {
                string Sql = "";
                dsGatePass ds = new dsGatePass();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT Gate.GateNo, Gate.Gdate, Gate.Gtime, Gate.BuiltyNo, Gate.IntCol1, Employ.Title, Gate3.ProductID,ProductGatePass.Title, Gate.Transport, Gate.VehReference, Gate.Through, Gate3.Quantity, Gate3.StdWeight, Gate3.CalcWeight, Gate.Transportation, Gate.Driver, Gate.DriverCnic, Gate3.BardanaType,Gate.Sno FROM (ProductGatePass INNER JOIN (Gate3 INNER JOIN Gate ON Gate3.GateNo = Gate.GateNo) ON ProductGatePass.CompID = Gate3.ProductID) INNER JOIN Employ ON Gate.IntCol1 = Employ.CompID";
Sql = Sql + " where Gate.Gdate between  '" +FDate+ "' and '" +Tdate+ "'  and Gate.itype2='GOM'";
if (!string.IsNullOrEmpty(accountid))
                {
                    Sql = Sql + " and Gate3.ProductID='" + accountid + "'";
                }
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsGatePass GetGatePassRMProductWise_Data(DateTime FDate, DateTime Tdate, string accountid, string type)
        {
            try
            {
                string Sql = "";
                dsGatePass ds = new dsGatePass();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT Gate.GateNo, Gate.Gdate, Gate.Gtime, Gate.BuiltyNo, Gate.IntCol1, Accounts.Title, Gate3.ProductID, Products.ItemDetail, Gate.Transport, Gate.VehReference, Gate.Through, Gate3.Quantity, Gate3.StdWeight, Gate3.CalcWeight, Gate.Transportation, Gate.Driver, Gate.DriverCnic, Gate3.BardanaType,Gate.Sno";
                Sql = Sql + " FROM (Products INNER JOIN (Gate3 INNER JOIN Gate ON Gate3.GateNo = Gate.GateNo) ON Products.ProductID = Gate3.ProductID) INNER JOIN Accounts ON Gate.IntCol1 = Accounts.AccountID where Gate.Gdate between  '" + FDate + "' and '" + Tdate + "'  and Gate.itype2='GPR'";
                if (!string.IsNullOrEmpty(accountid))
                {
                  Sql = Sql + " and Gate3.ProductID='" +accountid+ "'";
                }
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsGatePass GetGatePassFGProductWise_Data(DateTime FDate, DateTime Tdate, string accountid, string type)
        {
            try
            {
                string Sql = "";
                dsGatePass ds = new dsGatePass();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT Gate.GateNo, Gate.Gdate, Gate.Gtime, Gate.BuiltyNo, Gate.IntCol1, Accounts.Title, Gate3.ProductID, Products.ItemDetail, Gate.Transport, Gate.VehReference, Gate.Through, Gate3.Quantity, Gate3.StdWeight, Gate3.CalcWeight, Gate.Transportation, Gate.Driver, Gate.DriverCnic, Gate3.BardanaType,Gate.Sno";
                Sql = Sql + " FROM (Products INNER JOIN (Gate3 INNER JOIN Gate ON Gate3.GateNo = Gate.GateNo) ON Products.ProductID = Gate3.ProductID) INNER JOIN Accounts ON Gate.IntCol1 = Accounts.AccountID where Gate.Gdate between  '" + FDate + "' and '" + Tdate + "'  and Gate.itype2='GOF'";
                if (!string.IsNullOrEmpty(accountid))
                {
                    Sql = Sql + " and Gate3.ProductID='" + accountid + "'";
                }
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsRecovery GetPartyWiseRecovery_Data(DateTime FDate, DateTime Tdate, string accountid, string type)
        {
            try
            {
                string Sql = "";
                dsRecovery ds = new dsRecovery();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                string myQuery = "SELECT Credit_Days FROM  Clients_Credit_Limit  where accountID='" + accountid+ "'";
                SqlCommand cmd = new SqlCommand(myQuery, Connection);
                int days = Convert.ToInt32(Convert.ToString(cmd.ExecuteScalar()));
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
               Sql = " SELECT qryInvBook.InvId, qryInvBook.InvDate, qryInvBook.AccountId, qryInvBook.AccountTitle, qryInvBook.ProductID, qryInvBook.ProductTitle, ";
Sql = Sql + " qryInvBook.Qtycr as qtydr ,qryInvBook.Wtcr as wtdr, qryInvBook.Rate, qryInvBook.Amount as GrossAmount,DiscRatio as Disc, NetAmount, ";
    Sql = Sql + " qryInvBook.ProductTitle AS ProductGroup,qryInvBook.GateNo,qryInvBook.VehNo FROM qryInvBook";
Sql = Sql + " where qryInvBook.Itype<>'O'";
if (!string.IsNullOrEmpty(accountid))
{
    Sql = Sql + " and qryInvBook.AccountId='" +accountid+ "'";
}
    Sql = Sql + " and qryInvBook.qtycr>0";
    Sql = Sql + " and qryInvBook.invdate between '" + FDate.AddDays(days) + "' and '" + Tdate.AddDays(days) + "'";
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsPartywisePurchase GetProductPurchase_Data(DateTime FDate, DateTime Tdate, string accountid,string type)
        {
            try
            {
                string Sql = "";
                dsPartywisePurchase ds = new dsPartywisePurchase();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                //string myQuery = "SELECT Credit_Days FROM  Clients_Credit_Limit  where accountID='" + accountid + "'";
                //SqlCommand cmd = new SqlCommand(myQuery, Connection);
                //int days = Convert.ToInt32(Convert.ToString(cmd.ExecuteScalar()));
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
               if (type=="Purchase")
               {
Sql = "SELECT Purchase1.InvNo AS InvID, Purchase1.Idate AS InvDate, Purchase1.PartyBillNO AS YourBillNo,";
Sql = Sql + " Purchase1.PartyBillDate AS YourBillDate, Purchase1.AccountId, Accounts.Title AS AccountTitle, Purchase2.ProductID,";
Sql = Sql + " Products.ProductName AS ProductTitle, Purchase2.QtyDr, Purchase2.QtyCr, Purchase2.WtDr, Purchase2.WtCr,";
Sql = Sql + " Purchase2.Rate, Purchase2.Amount AS Amount, Purchase1.InvTotal AS GrossAmount, Purchase1.Discount as Disc,";
Sql = Sql + " Purchase1.SaleTaxAmt, Purchase1.NetTotal as NetAmount, Purchase1.IType, Purchase1.Sno, Purchase1.StoreID, Purchase1.ContractID,";
Sql = Sql + " Purchase1.Brate, Purchase2.VehNo, Purchase2.GateNo, ProductGroups.PG_ID, ProductGroups.Title AS ProductGroup,";
Sql = Sql + " ProductSubGroup.PSG_Id, ProductSubGroup.Title AS ProductType, Purchase1.CalcFactor, Purchase1.ArrivalType AS ArrID,";
Sql = Sql + " (Select VType from VehicleType where VehicleType.VID=Purchase1.ArrivalType) as ArrivalType,Purchase1.ArrivalID as DispactchID,";
Sql = Sql + " Purchase2.NumCol3 as Freight,Purchase1.BagAmount as BagAmount,Purchase1.BrokeryAmount as BrokeryAmount,Purchase1.NumCol2 as LabourAmount,Purchase2.NumCol2 as MktComete,Purchase1.BitCol3 as WtBit,Purchase2.NumCol3 as WtWithOutBardana";
Sql = Sql + " FROM Products INNER JOIN Accounts INNER JOIN Purchase1 ON Accounts.AccountID = Purchase1.AccountId INNER JOIN Purchase2 ON Purchase1.InvNo = Purchase2.InvNo ON Products.ProductID = Purchase2.ProductID INNER JOIN ProductGroups ON Products.PG_ID = ProductGroups.PG_ID INNER JOIN ProductSubGroup ON Products.PT_ID = ProductSubGroup.PSG_Id";
               }
               else
               {
    Sql = "select * from qryInvBook where ProductID="+accountid+ " and Invdate between '" +FDate+ "'  and '" +Tdate+"'  and InvId<>'0' ";
}
 if (type=="Purchase")
               {
    strTitle = "Party Wise Purchase";
 Sql = Sql + " Where Purchase1.Idate between '" +FDate+ "'  and '" +Tdate+"'";
 if (!string.IsNullOrEmpty(accountid)) 
    {
     Sql = Sql + " AND  Products.ProductID = " +accountid;
   }
    Sql = Sql + " and Purchase1.itype in ('FP','FPR') ";
 }
else{
 strTitle = "Party Wise Sale";
 Sql = Sql + " and itype in ('FS','FSR') ";
 }
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsPurchaseRate GetPurchaseRate_Data(DateTime FDate, DateTime Tdate, string accountid, string type)
        {
            try
            {
                string Sql = "";
                dsPurchaseRate ds = new dsPurchaseRate();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                string VWhere = "";
                if(!string.IsNullOrEmpty(accountid) )
                {
                   // VWhere = "AND Products.PG_ID = " + accountid;
                    VWhere = "AND Products.ProductID = " + accountid;
}
Sql = " SELECT     Purchase2.ProductID, Products.ItemDetail, Products.PG_ID, ProductGroups.Title, SUM(Purchase2.WtDr) AS TotalWeight";
Sql = Sql + " , (SELECT     SUM(Vouchersdetail.Debit)- SUM(Vouchersdetail.Credit) FROM         Vouchers INNER JOIN  Vouchersdetail ON Vouchers.VoucherID = Vouchersdetail.VID Where Vouchersdetail.VDate Between '" + FDate + "' AND '" + Tdate + "' AND AccountID = Products.StrCol1 ) AS NEtAmount";
Sql = Sql + " FROM         Purchase2 INNER JOIN";
Sql = Sql + " Products ON Purchase2.ProductID = Products.ProductID INNER JOIN";
Sql = Sql + " ProductGroups ON Products.PG_ID = ProductGroups.PG_ID INNER JOIN";
Sql = Sql + " Purchase1 ON Purchase2.InvNo = Purchase1.InvNo";
Sql = Sql + " Where   Purchase1.Idate Between '" +FDate+ "' AND '" +Tdate+ "'" +VWhere;
Sql = Sql + " GROUP BY Purchase2.ProductID, Products.ItemDetail, Products.PG_ID, ProductGroups.Title , Products.StrCol1";
Sql = Sql + " ORDER BY Products.PG_ID, Purchase2.ProductID";
strTitle = "Purchase Rate Report Product Wise";
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsProductPosition GetProductPosition_Data(DateTime FDate, DateTime Tdate, string accountid, string type)
        {
            try
            {
                string Sql = "";
                dsProductPosition ds = new dsProductPosition();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = Sql + " SELECT qryInvBook.InvId, qryInvBook.InvDate, qryInvBook.ProductTitle, qryInvBook.QtyDr, qryInvBook.QtyCr, qryInvBook.WtDr, qryInvBook.WtCr, Stores.Title FROM qryInvBook LEFT JOIN Stores ON qryInvBook.StoreID = Stores.StoreID";
Sql = Sql + " where qryinvbook.Invdate <= '" +Tdate+ "' and productID=" +accountid;
strTitle = "Product Position";
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsProductPosition GetStockInHandGroup_Data(DateTime FDate,string type)
        {
            try
            {
                string Sql = "";
                dsProductPosition ds = new dsProductPosition();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "SELECT qryInvBook.InvID, qryInvBook.InvDate, qryInvBook.ProductTitle, qryInvBook.QtyDr, qryInvBook.QtyCr, qryInvBook.WtDr, qryInvBook.WtCr , ProductGroups.Title FROM qryInvBook INNER JOIN ProductGroups ON qryInvBook.PG_ID = ProductGroups.PG_ID";
Sql = Sql + " where qryinvbook.Invdate <= '" +FDate+ "' ";
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsProductPosition GetStockInHandStore_Data(DateTime FDate, string type)
        {
            try
            {
                string Sql = "";
                dsProductPosition ds = new dsProductPosition();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = Sql + " SELECT qryInvBook.InvId, qryInvBook.InvDate, qryInvBook.ProductTitle, qryInvBook.QtyDr, qryInvBook.QtyCr, qryInvBook.WtDr, qryInvBook.WtCr, Stores.Title FROM qryInvBook LEFT JOIN Stores ON qryInvBook.StoreID = Stores.StoreID";
Sql = Sql + " where qryinvbook.Invdate <= '"+FDate+ "' ";
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsAccountLedger GetDailySale_Data(DateTime FDate, DateTime Tdate)
        {
            try
            {
                dsAccountLedger ds = new dsAccountLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Qry = @"  exec SPRW_GetDailySale @FromDate='" + FDate + "',@ToDate='" + Tdate + "'";
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
        private dsProductList GetProductList_Data()
        {
            try
            {
                dsProductList ds = new dsProductList();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Qry = @"SELECT     ProductID, PG_ID, ItemDetail, PT_ID, SalePrice, PurchasePrice, ReorderLevel, Remarks, StockInHand, OpeningQty, OpeningWt, OpeningRate, ProductName, SMW, PMW, IsFixed, GstRate FROM         Products";
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
        private dsProductListGroup GetProductListGroupwise_Data()
        {
            try
            {
                dsProductListGroup ds = new dsProductListGroup();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Qry = @"SELECT Products.ProductID, Products.SalePrice, Products.PurchasePrice, Products.ReorderLevel, Products.ProductName, Products.PMW, Products.PG_ID, ProductGroups.Title AS ProductGroup FROM ProductGroups INNER JOIN Products ON ProductGroups.PG_ID = Products.PG_ID";
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
        private dsStockLedger GetStockLedger_Data(DateTime FDate, DateTime TDate, string AccountId)
        {
            try
            {
                dsStockLedger ds = new dsStockLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql2="";
                string Sql="";
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.Connection = Connection;
                Sql2 = Sql2 + " select sum(qtydr) as qdr,sum(qtycr) as qcr, sum(wtdr) as wdr,sum(wtcr) as wcr from qryinvBook ";
Sql2 = Sql2 + " where  invdate< '" +FDate+ "' and ProductID= " +AccountId;
using (SqlCommand command = new SqlCommand(Sql2, Connection))
{
    using (SqlDataReader reader = command.ExecuteReader())
    {
        while (reader.Read())
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr["InvID"] = "OB";
            dr["InvDate"] = FDate;
            dr["ProductTitle"] = "Opening";
            dr["QtyDr"] = reader.GetValue(0);
            dr["QtyCr"] = reader.GetValue(1);
            dr["WtDr"] = reader.GetValue(2);
            dr["WtCr"] = reader.GetValue(3);
            dr["Title"] = "";
            ds.Tables[0].Rows.Add(dr);
        }
    }
}
    Sql = Sql + " select * from qryInvBook where invdate between  '" +FDate+ "' and '" +TDate+ "' and ProductID= " +AccountId;
    Sql = Sql + " order by invdate";
    //da = new SqlDataAdapter(Sql2, dbconsql);
    //da.Fill(ds, ds.Tables[0].TableName);
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsBardana2 GetDailyBardana_Data(DateTime FDate, DateTime TDate)
        {
            try
            {
                dsBardana2 ds = new dsBardana2();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql = "";
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.Connection = Connection;
                Sql = "select * from qryBardanaRegister where invdate between  '" +FDate+ "' and '" +TDate+ "' ";
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsbardana3 GetBardanaPartyStatus_Data(DateTime TDate)
        {
            try
            {
                dsbardana3 ds = new dsbardana3();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql = "";
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.Connection = Connection;
                Sql = "select BtypeId,BardanaType,AccountID,Party,Sum(QtyDr) as QtyDr1,sum(QtyCr) as QtyCr1 from qryBardanaRegister where invdate <= '" +TDate+ "' Group by BtypeId,BardanaType,AccountID,Party";
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsbardana3 GetBardanaPartyStatus_Data2(DateTime FDate, DateTime TDate)
        {
            try
            {
                dsbardana3 ds = new dsbardana3();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql = "";
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.Connection = Connection;
                Sql = "select BtypeId,BardanaType,AccountID,Party,Sum(QtyDr) as QtyDr1,sum(QtyCr) as QtyCr1 from qryBardanaRegister where invdate between  '" +FDate+ "' and '" +TDate+ "'  Group by BtypeId,BardanaType,AccountID,Party";
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsBardana2 GetBardanaPartyWise_Data(DateTime FDate, DateTime TDate, string AccountId,string title)
        {
            try
            {
                dsBardana2 ds = new dsBardana2();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql2 = "";
                string Sql = "";
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.Connection = Connection;
                Sql2 = Sql2 + "select BtypeID,BardanaType,sum(qtydr) as qtydr1,sum(qtycr) as qtycr1 from qryBardanaRegister where invdate < '" +FDate+ "' and accountID='" +AccountId+ "' group by BtypeID,BardanaType";
                using (SqlCommand command = new SqlCommand(Sql2, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["InvNo"] = "OB";
                            dr["InvDate"] = FDate;
                            dr["BtypeID"] = reader.GetValue(0);
                            dr["BardanaType"] = reader.GetValue(1);
                            dr["AccountId"] = AccountId;
                            dr["Party"] = title;
                            dr["QtyDr"] = reader.GetValue(2);
                            dr["QtyCr"] = reader.GetValue(3);
                            dr["ProductID"] = 0;
                            dr["PName"] = "";
                             dr["VehNo"] = "";
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                }
                Sql = Sql + "select * from qryBardanaRegister where Invdate between  '" +FDate+ "' and '" +TDate+ "' and AccountID='" +AccountId+ "' order by InvDate ";
                using (SqlCommand command = new SqlCommand(Sql, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["InvNo"] = reader.GetValue(0);
                            dr["InvDate"] = reader.GetValue(1);
                            dr["BtypeID"] = reader.GetValue(2);
                            dr["BardanaType"] = reader.GetValue(3);
                            dr["AccountId"] = AccountId;
                            dr["Party"] = title;
                            dr["QtyDr"] = reader.GetValue(6);
                            dr["QtyCr"] = reader.GetValue(7);
                            dr["ProductID"] = 0;
                            dr["PName"] = "";
                            dr["VehNo"] = reader.GetValue(10);
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                }
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsBardana2 GetBardanaPartyLedger_Data(DateTime FDate, DateTime TDate, string AccountId, string title)
        {
            try
            {
                dsBardana2 ds = new dsBardana2();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql2 = "";
                string Sql = "";
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.Connection = Connection;
                Sql2 = Sql2 + " select BardanaType,sum(qtydr) as qdr,sum(qtycr) as qcr from qryBardanaRegister ";
Sql2 = Sql2 + " where  invdate< '" +FDate+ "' and BTypeID= " +AccountId;
Sql2 = Sql2 + " group by BardanaType";
                using (SqlCommand command = new SqlCommand(Sql2, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["InvNo"] = "OB";
                            dr["InvDate"] = FDate;
                            dr["BtypeID"] = 0;
                            dr["BardanaType"] = reader.GetValue(0);
                            dr["AccountId"] = "";
                            dr["Party"] = "Opening";
                            dr["QtyDr"] = reader.GetValue(1);
                            dr["QtyCr"] = reader.GetValue(2);
                            dr["ProductID"] = 0;
                            dr["PName"] = "";
                            dr["VehNo"] = "";
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                }
                Sql = Sql + " select * from qryBardanaRegister where invdate between  '" +FDate+ "' and '" +TDate+ "' and BTypeID= " +AccountId;
Sql = Sql + " order by invdate";
                using (SqlCommand command = new SqlCommand(Sql, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["InvNo"] = reader.GetValue(0);
                            dr["InvDate"] = reader.GetValue(1);
                            dr["BtypeID"] = reader.GetValue(2);
                            dr["BardanaType"] = reader.GetValue(3);
                            dr["AccountId"] = AccountId;
                            dr["Party"] = title;
                            dr["QtyDr"] = reader.GetValue(6);
                            dr["QtyCr"] = reader.GetValue(7);
                            dr["ProductID"] = 0;
                            dr["PName"] = "";
                            dr["VehNo"] = reader.GetValue(10);
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                }
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsProduction1 GetProduction1_Data(DateTime FDate, DateTime TDate,string type)
        {
            try
            {
                dsProduction1 ds = new dsProduction1();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql = "";
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.Connection = Connection;
                if (type.Equals("DailyProductionOverAll"))
            {
                Sql = "SELECT Process4.InvNo, Process4.Idate, Process4.YourBillNo AS Ref, Process4.IssueNo AS FeedFormula, Process4.PlantID AS PartyID,";
Sql = Sql + " Products.ItemDetail AS Title, Process4.Weight1 AS WtPerBag, dbo.Process4.NumCol1 AS Qty, dbo.Process4.NumCol1 * 50 AS Weight,";
Sql = Sql + " Process4.Sno, Products.PG_ID, ProductGroups.Title AS PGTitle";
Sql = Sql + " FROM         Products INNER JOIN";
Sql = Sql + " Process4 ON Products.ProductID = Process4.PlantID INNER JOIN";
Sql = Sql + " ProductGroups ON Products.PG_ID = ProductGroups.PG_ID";
              Sql = Sql + " WHERE (Process4.IType = N'IF') and Process4.Idate between '" +FDate+ "' and '" +TDate+ "'";
            }
            else if (type.Equals("DailyAdjustmentOverAll"))
            {
Sql = "SELECT Process4.InvNo, Process4.Idate, Process4.YourBillNo AS Ref, Process4.IssueNo AS FeedFormula, Process4.PlantID AS PartyID,";
Sql = Sql + " Products.ItemDetail AS Title, Process4.Weight1 AS WtPerBag, dbo.Process4.NumCol1 AS Qty, dbo.Process4.NumCol1 * 50 AS Weight,";
Sql = Sql + " Process4.Sno, Products.PG_ID, ProductGroups.Title AS PGTitle";
Sql = Sql + " FROM         Products INNER JOIN";
Sql = Sql + " Process4 ON Products.ProductID = Process4.PlantID INNER JOIN";
Sql = Sql + " ProductGroups ON Products.PG_ID = ProductGroups.PG_ID";
                Sql = Sql + " WHERE (Process4.IType = N'IFA') and Process4.Idate between '" +FDate+ "' and '" +TDate+ "'";
            }
else if (type.Equals("IndividualFormulaRunReport"))
            {
               Sql = "SELECT Process4.InvNo, Process4.Idate, Process4.IssueNo, Process4.PlantID, Products.ItemDetail, Process4.ReceiveQty,";
Sql = Sql + " Process5.ProductID, Products_1.ProductName AS RawMaterial, Process5.WtCr, Process4.IType, Process4.Sno";
Sql = Sql + " FROM Products INNER JOIN Process4 ON Products.ProductID = Process4.PlantID INNER JOIN";
Sql = Sql + " Process5 ON Process4.InvNo = Process5.InvNo INNER JOIN Products Products_1 ON Process5.ProductID = Products_1.ProductID";
Sql = Sql + " WHERE (Process4.IType = N'IF') AND (Process5.WtCr <> 0) and Process4.Idate between '" +FDate+ "' and '" +TDate+ "'";
            }
                else if (type.Equals("IndividualFormulaAdjustment"))
            {
               Sql = "SELECT     Process4.InvNo, Process4.Idate, Process4.IssueNo, Process4.PlantID,(SELECT products.itemdetail From products WHERE Products.productid = Process4.PlantID) AS ProductName,";
Sql = Sql + " Process4.ReceiveQty, Process5.ProductID, Products.ProductName AS RawMaterial, Process5.WtCr, Process4.IType, Process4.Sno";
Sql = Sql + " FROM Process5 INNER JOIN Process4 ON Process5.InvNo = Process4.InvNo INNER JOIN";
Sql = Sql + " Products ON Process5.ProductID = Products.ProductID";
Sql = Sql + " WHERE (Process4.IType = N'IFA') AND (Process5.WtCr <> 0) and Process4.Idate between '" +FDate+ "' and '" +TDate+ "'";
            }
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsProduction2 GetProduction2_Data(DateTime FDate, DateTime TDate, string type)
        {
            try
            {
                dsProduction2 ds = new dsProduction2();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql = "";
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.Connection = Connection;
                if (type.Equals("DailyProductionOverAll"))
                {
                    Sql = "SELECT Process4.InvNo, Process4.Idate, Process4.YourBillNo AS Ref, Process4.IssueNo AS FeedFormula, Process4.PlantID AS PartyID,";
                    Sql = Sql + " Products.ItemDetail AS Title, Process4.Weight1 AS WtPerBag, dbo.Process4.NumCol1 AS Qty, dbo.Process4.NumCol1 * 50 AS Weight,";
                    Sql = Sql + " Process4.Sno, Products.PG_ID, ProductGroups.Title AS PGTitle";
                    Sql = Sql + " FROM         Products INNER JOIN";
                    Sql = Sql + " Process4 ON Products.ProductID = Process4.PlantID INNER JOIN";
                    Sql = Sql + " ProductGroups ON Products.PG_ID = ProductGroups.PG_ID";
                    Sql = Sql + " WHERE (Process4.IType = N'IF') and Process4.Idate between '" + FDate + "' and '" + TDate + "'";
                }
                else if (type.Equals("DailyAdjustmentOverAll"))
                {
                    Sql = "SELECT Process4.InvNo, Process4.Idate, Process4.YourBillNo AS Ref, Process4.IssueNo AS FeedFormula, Process4.PlantID AS PartyID,";
                    Sql = Sql + " Products.ItemDetail AS Title, Process4.Weight1 AS WtPerBag, dbo.Process4.NumCol1 AS Qty, dbo.Process4.NumCol1 * 50 AS Weight,";
                    Sql = Sql + " Process4.Sno, Products.PG_ID, ProductGroups.Title AS PGTitle";
                    Sql = Sql + " FROM         Products INNER JOIN";
                    Sql = Sql + " Process4 ON Products.ProductID = Process4.PlantID INNER JOIN";
                    Sql = Sql + " ProductGroups ON Products.PG_ID = ProductGroups.PG_ID";
                    Sql = Sql + " WHERE (Process4.IType = N'IFA') and Process4.Idate between '" + FDate + "' and '" + TDate + "'";
                }
                else if (type.Equals("IndividualFormulaRunReport"))
                {
                    Sql = "SELECT Process4.InvNo, Process4.Idate, Process4.IssueNo, Process4.PlantID, Products.ItemDetail, Process4.ReceiveQty,";
                    Sql = Sql + " Process5.ProductID, Products_1.ProductName AS RawMaterial, Process5.WtCr, Process4.IType, Process4.Sno";
                    Sql = Sql + " FROM Products INNER JOIN Process4 ON Products.ProductID = Process4.PlantID INNER JOIN";
                    Sql = Sql + " Process5 ON Process4.InvNo = Process5.InvNo INNER JOIN Products Products_1 ON Process5.ProductID = Products_1.ProductID";
                    Sql = Sql + " WHERE (Process4.IType = N'IF') AND (Process5.WtCr <> 0) and Process4.Idate between '" + FDate + "' and '" + TDate + "'";
                }
                else if (type.Equals("IndividualFormulaAdjustment"))
                {
                    Sql = "SELECT     Process4.InvNo, Process4.Idate, Process4.IssueNo, Process4.PlantID,(SELECT products.itemdetail From products WHERE Products.productid = Process4.PlantID) AS ItemDetail,";
                    Sql = Sql + " Process4.ReceiveQty, Process5.ProductID, Products.ProductName AS RawMaterial, Process5.WtCr, Process4.IType, Process4.Sno";
                    Sql = Sql + " FROM Process5 INNER JOIN Process4 ON Process5.InvNo = Process4.InvNo INNER JOIN";
                    Sql = Sql + " Products ON Process5.ProductID = Products.ProductID";
                    Sql = Sql + " WHERE (Process4.IType = N'IFA') AND (Process5.WtCr <> 0) and Process4.Idate between '" + FDate + "' and '" + TDate + "'";
                }
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsGstRegister GetGstRegister_Data(DateTime FDate, DateTime TDate, string AccountId)
        {
            try
            {
                dsGstRegister ds = new dsGstRegister();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql2 = "";
                string Sql = "";
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.Connection = Connection;
                Sql = "SELECT * From qrygstbook where idate between '" +FDate+ "' and '" +TDate+ "' ";
if( AccountId.Equals("Input")  )
{
    Sql = Sql + " and IType='FGSTP' or IType='FGSTSR'";
    //Sql = Sql + " and qtyCr>0";
}
else
{
    Sql = Sql + " and IType='FGSTS' or IType='FGSTPR'";
 //Sql = Sql + " and qtyDr>0";
}
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsStockLedger GetStockLedger_Data3(DateTime FDate, DateTime TDate, string AccountId)
        {
            try
            {
                dsStockLedger ds = new dsStockLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql2 = "";
                string Sql = "";
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.Connection = Connection;
                Sql2 = Sql2 + " select sum(qtydr) as qdr,sum(qtycr) as qcr, sum(wtdr) as wdr,sum(wtcr) as wcr from qryinvBook ";
                Sql2 = Sql2 + " where  Itype<>'PIS' and invdate< '" + FDate + "' and ProductID= " + AccountId;
                using (SqlCommand command = new SqlCommand(Sql2, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["InvID"] = "OB";
                            dr["InvDate"] = FDate;
                            dr["ProductTitle"] = "Opening";
                            dr["QtyDr"] = reader.GetValue(0);
                            dr["QtyCr"] = reader.GetValue(1);
                            dr["WtDr"] = reader.GetValue(2);
                            dr["WtCr"] = reader.GetValue(3);
                            dr["Title"] = "";
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                }
                Sql = Sql + " select * from qryInvBook where Itype<>'PIS' and invdate between  '"+FDate+ "' and '" +TDate+ "' and ProductID= " +AccountId;
Sql = Sql + " order by invdate";
                //da = new SqlDataAdapter(Sql2, dbconsql);
                //da.Fill(ds, ds.Tables[0].TableName);
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsStockLedger GetStockLedger_Data2(DateTime FDate, DateTime TDate, string AccountId)
        {
            try
            {
                dsStockLedger ds = new dsStockLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql2 = "";
                string Sql = "";
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.Connection = Connection;
                Sql2 = Sql2 + " select sum(qtydr) as qdr,sum(qtycr) as qcr, sum(wtdr) as wdr,sum(wtcr) as wcr from qryInvBookFinalWeight ";
                Sql2 = Sql2 + " where  invdate< '" + FDate + "' and ProductID= " + AccountId;
                using (SqlCommand command = new SqlCommand(Sql2, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["InvID"] = "OB";
                            dr["InvDate"] = FDate;
                            dr["ProductTitle"] = "Opening";
                            dr["QtyDr"] = reader.GetValue(0);
                            dr["QtyCr"] = reader.GetValue(1);
                            dr["WtDr"] = reader.GetValue(2);
                            dr["WtCr"] = reader.GetValue(3);
                            dr["Title"] = "";
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }
                }
                Sql = Sql + " select * from qryInvBookFinalWeight where invdate between  '" + FDate + "' and '" + TDate + "' and ProductID= " + AccountId;
                Sql = Sql + " order by invdate";
                //da = new SqlDataAdapter(Sql2, dbconsql);
                //da.Fill(ds, ds.Tables[0].TableName);
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsStockLedger GetStockInHand2_Data2(DateTime tdate, string type)
        {
            try
            {
                dsStockLedger ds = new dsStockLedger();
                string dbconsql = Convert.ToString(Session["consql"]);
                string Sql = "";
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Command.Connection = Connection;
              if(type == "rtStockInHandEstimated")
              {
Sql = Sql + " SELECT InvID,InvDate, ProductTitle, QtyDr, QtyCr,WtDr,WtCr AS Bal From qryInvBookSeed";
Sql = Sql + " where qryInvBookSeed.Invdate <= '" +tdate+ "' ";
              }
 else if(type == "rtStockInHandEstimatedGunji")
              {  
Sql = Sql + " SELECT InvID,InvDate, ProductTitle, QtyDr, QtyCr,WtDr,WtCr AS Bal From qryInvBookSeed";
Sql = Sql + " where qryInvBookSeed.Invdate <= '" +tdate+"' ";
 }
else if(type == "rtStockInHandProcessByFinished")
              {  
Sql = Sql + " SELECT InvID,InvDate, ProductTitle, QtyDr, QtyCr,WtDr,WtCr AS Bal From qryInvBookProcessFinished";
Sql = Sql + " where qryInvBookProcessFinished.Invdate <= '" +tdate+ "' ";
}
              else
              {
                  Sql = Sql + " SELECT InvID,InvDate, ProductTitle, QtyDr, QtyCr,WtDr,WtCr AS WtCr From qryInvBook";
Sql = Sql + " where qryinvbook.Invdate <= '" + tdate + "' ";
}
         da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsSalePurchaseDetailed GetSalePurchaseDetailed_Data(DateTime FDate, DateTime TDate, string AccountId, string title, string product, string type, string group)
        {
            try
            {
                string Sql = "";
                dsSalePurchaseDetailed ds = new dsSalePurchaseDetailed();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = " SELECT qryInvBook.InvId, qryInvBook.InvDate, qryInvBook.AccountId, qryInvBook.AccountTitle, qryInvBook.ProductID, qryInvBook.ProductTitle , SalesTaxAmt AS SalesTaxAmt, Freight, MktComete, LabourAmount, BrokeryAmount, BagAmount, ";
if (type== "Purchase" || type== "SaleReturn" )
                {
    strTitle = "Purchase ";
    Sql = Sql + " qryInvBook.QtyDr ,qryInvBook.WtDr, qryInvBook.Rate, qryInvBook.Amount,Disc, NetAmount, ";
                }
                else if (type== "Sale" || type== "PurchaseReturn" )
                {
     strTitle = "Sale ";
    Sql = Sql + " qryInvBook.Qtycr as qtydr ,qryInvBook.Wtcr as wtdr, qryInvBook.Rate, qryInvBook.Amount,DiscRatio as Disc, NetAmount, ";
                }
if (group== "Party" )
                {
     strTitle = strTitle + "Party Wise Report";
    Sql = Sql + " qryInvBook.AccountTitle AS groupby1,qryInvBook.GateNo,qryInvBook.VehNo FROM qryInvBook";
                }
else if (group== "Product" )
                {
     strTitle = strTitle + "Product Wise Report";
    Sql = Sql + " qryInvBook.ProductTitle AS groupby1,qryInvBook.GateNo,qryInvBook.VehNo FROM qryInvBook";
                }
Sql = Sql + " where qryInvBook.Itype<>'O'";
if (!string.IsNullOrEmpty(AccountId))
{
    Sql = Sql + " and qryInvBook.AccountId='" + AccountId + "'";
}
if (!product.Equals(""))
{
    Sql = Sql + " and qryInvBook.productId="+product;
}
if (type== "Purchase")
{
    Sql = Sql + " and qryInvBook.qtydr>0";
}
                else if (type== "Sale")
{
     Sql = Sql + " and qryInvBook.qtycr>0";
}
 else if (type== "SaleReturn")
{
     Sql = Sql + " AND qryInvBook.Itype Like 'FSR%'";
}
else if (type== "PurchaseReturn")
{
    Sql = Sql + " AND qryInvBook.Itype Like 'FPR%'";
}
Sql = Sql + " and qryInvBook.invdate between '" + FDate + "' and '" + TDate + "' order by  qryInvBook.InvDate";
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsSalePurchaseDetailed GetSalePurchaseGeneral_Data(DateTime FDate, DateTime TDate, string AccountId, string title, string product, string type, string group)
        {
            try
            {
                string Sql = "";
                dsSalePurchaseDetailed ds = new dsSalePurchaseDetailed();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = " SELECT qryInvBook.InvId, qryInvBook.InvDate, qryInvBook.AccountId, qryInvBook.AccountTitle, qryInvBook.ProductID, qryInvBook.ProductTitle , SalesTaxAmt AS SalesTaxAmt, Freight, MktComete, LabourAmount, BrokeryAmount, BagAmount, ";
                if (type == "Purchase" || type == "SaleReturn")
                {
                    strTitle = "Purchase ";
                    Sql = Sql + " qryInvBook.QtyDr ,qryInvBook.WtDr, qryInvBook.Rate, qryInvBook.Amount as GrossAmount,DiscRatio as Disc, NetAmount, ";
                }
                else if (type == "Sale" || type == "PurchaseReturn")
                {
                    strTitle = "Sale ";
                    Sql = Sql + " qryInvBook.Qtycr as qtydr ,qryInvBook.Wtcr as wtdr, qryInvBook.Rate, qryInvBook.Amount as GrossAmount,DiscRatio as Disc, NetAmount, ";
                }
                if (group == "Party")
                {
                    strTitle = strTitle + "Party Wise Report";
                    Sql = Sql + " qryInvBook.AccountTitle AS groupby1,qryInvBook.GateNo,qryInvBook.VehNo FROM qryInvBook";
                }
                else if (group == "Product")
                {
                    strTitle = strTitle + "Product Wise Report";
                    Sql = Sql + " qryInvBook.ProductTitle AS groupby1,qryInvBook.GateNo,qryInvBook.VehNo FROM qryInvBook";
                }
                Sql = Sql + " where qryInvBook.Itype<>'O'";
                if (!string.IsNullOrEmpty(AccountId))
                {
                    Sql = Sql + " and qryInvBook.AccountId='" + AccountId + "'";
                }
                if (!product.Equals(""))
                {
                    Sql = Sql + " and qryInvBook.productId=" + product;
                }
                if (type == "Purchase")
                {
                    Sql = Sql + " and qryInvBook.qtydr>0";
                }
                else if (type == "Sale")
                {
                    Sql = Sql + " and qryInvBook.qtycr>0";
                }
                else if (type == "SaleReturn")
                {
                    Sql = Sql + " AND qryInvBook.Itype Like 'FSR%'";
                }
                else if (type == "PurchaseReturn")
                {
                    Sql = Sql + " AND qryInvBook.Itype Like 'FPR%'";
                }
                Sql = Sql + " and qryInvBook.invdate between '" + FDate + "' and '" + TDate + "'";
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        private dsPurchaseOrder GetPurchaseOrder_Data(DateTime FDate, DateTime TDate, string AccountId, string title, string product,  string group)
        {
            try
            {
                string Sql = "";
                dsPurchaseOrder ds = new dsPurchaseOrder();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                string VWhere="" ;
if (!string.IsNullOrEmpty(product))
{
VWhere = " AND ProductID = " + product;
}
if (!string.IsNullOrEmpty(AccountId))
{
VWhere = VWhere+ " AND AccountID = " + AccountId;
}
if (!string.IsNullOrEmpty(group))
{
    VWhere = VWhere + " AND InvNo = '" + group + "'";
    //VWhere = VWhere + " AND PO_NO = '" + group + "'";
}
VWhere = VWhere + "  Order By SNo,IDate";
                Sql ="Select * From qryPurOrderStatus where qryPurOrderStatus.IDATE between  '" + FDate + "' and '" + TDate + "'  " + VWhere ;
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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

        private dsPurchaseOrder GetPendingPurchaseOrder_Data(DateTime FDate, DateTime TDate, string AccountId, string title, string product, string group)
        {
            try
            {
                string Sql = "";
                dsPurchaseOrder ds = new dsPurchaseOrder();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                string VWhere = "";
                if (!string.IsNullOrEmpty(product))
                {
                    VWhere = " AND ProductID = " + product;
                }
                if (!string.IsNullOrEmpty(AccountId))
                {
                    VWhere = VWhere + " AND AccountID = " + AccountId;
                }
                if (!string.IsNullOrEmpty(group))
                {
                    VWhere = VWhere + " AND InvNo = '" + group + "'";
                    //VWhere = VWhere + " AND PO_NO = '" + group + "'";
                }
                VWhere = VWhere + "  Order By SNo,IDate";
                Sql = "Select * From qryPendingPOStatus where qryPendingPOStatus.IDATE between  '" + FDate + "' and '" + TDate + "'  " + VWhere;
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
        
        
        private dsPurchaseOrder GetSaleOrder_Data(DateTime FDate, DateTime TDate, string AccountId, string title, string product, string group)
        {
            try
            {
                string Sql = "";
                dsPurchaseOrder ds = new dsPurchaseOrder();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                string VWhere = "";
                if (!string.IsNullOrEmpty(product))
                {
                    VWhere = " AND ProductID = " + product;
                }
                if (!string.IsNullOrEmpty(AccountId))
                {
                    VWhere = VWhere + " AND AccountID = " + AccountId;
                }
                if (!string.IsNullOrEmpty(group))
                {
                    VWhere = VWhere + " AND SO_NO = '" + group + "'";
                }
                VWhere = VWhere + "  Order By SNo,IDate";
                Sql = "SELECT     InvNo, Idate, AccountId, Title, ProductID, ItemDetail, QtyDr, QtyCr, WtDr, WtCr, SO_No as PO_NO, Sno, Rate, RateCalc, AmountDR, AmountCR, VehNo FROM         qrySaleOrderStatus  where qrySaleOrderStatus.IDATE between  '" + FDate + "' and '" + TDate + "'  " + VWhere;
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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


        private dsPurchaseOrder GetPendingSO_Data(DateTime FDate, DateTime TDate, string AccountId, string title, string product, string group)
        {
            try
            {
                string Sql = "";
                dsPurchaseOrder ds = new dsPurchaseOrder();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                string VWhere = "";
                if (!string.IsNullOrEmpty(product))
                {
                    VWhere = " AND ProductID = " + product;
                }
                if (!string.IsNullOrEmpty(AccountId))
                {
                    VWhere = VWhere + " AND AccountID = " + AccountId;
                }
                if (!string.IsNullOrEmpty(group))
                {
                    VWhere = VWhere + " AND SO_NO = '" + group + "'";
                }
                VWhere = VWhere + "  Order By SNo,IDate";
                Sql = "SELECT     InvNo, Idate, AccountId, Title, ProductID, ItemDetail, QtyDr, QtyCr, WtDr, WtCr, SO_No as PO_NO, Sno, Rate, RateCalc, AmountDR, AmountCR, VehNo FROM         qryPendingSOStatus  where qryPendingSOStatus.IDATE between  '" + FDate + "' and '" + TDate + "'  " + VWhere;
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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

        private dsPurchaseOrder GetPendingDC_Data(DateTime FDate, DateTime TDate, string AccountId, string title, string product, string group)
        {
            try
            {
                string Sql = "";
                dsPurchaseOrder ds = new dsPurchaseOrder();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                string VWhere = "";
                if (!string.IsNullOrEmpty(product))
                {
                    VWhere = " AND ProductID = " + product;
                }
                if (!string.IsNullOrEmpty(AccountId))
                {
                    VWhere = VWhere + " AND AccountID = " + AccountId;
                }
                if (!string.IsNullOrEmpty(group))
                {
                    VWhere = VWhere + " AND SO_NO = '" + group + "'";
                }
                VWhere = VWhere + "  Order By SNo,IDate";
                Sql = "SELECT     InvNo, Idate, AccountId, Title, ProductID, ItemDetail, QtyDr, QtyCr, WtDr, WtCr, SO_No as PO_NO, Sno, Rate, RateCalc, AmountDR, AmountCR, VehNo FROM         qryPendingDCStatus  where qryPendingDCStatus.IDATE between  '" + FDate + "' and '" + TDate + "'  " + VWhere;
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
       
        
        
        private dsCreditLimit GetCreditLimit_Data()
        {
            try
            {
                string Sql = "";
                dsCreditLimit ds = new dsCreditLimit();
                string dbconsql = Convert.ToString(Session["consql"]);
                Connection = new SqlConnection(dbconsql);
                Connection.Open();
                Command = new SqlCommand();
                Command.CommandType = CommandType.Text;
                Sql = "select * from qryCreditLimit";
                Command.Connection = Connection;
                da = new SqlDataAdapter(Sql, dbconsql);
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
