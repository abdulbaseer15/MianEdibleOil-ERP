using ApnaAccountWeb.Models;
using ApnaAccountWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ApnaAccountWeb.Controllers
{
    public class ControlAccountsController : Controller
    {
        public ActionResult Add()
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
            var data = (from s in db.ControllAccounts select s).FirstOrDefault();
            if (data != null)
            {
                ViewBag.data = "true";
                ViewBag.ctrlacc = data;
            }
            ViewBag.AccCodeDetails = db.Accounts.Where(x => x.AccType.Trim() == "Detail").Select(x => new DetailList { AccountId = x.AccountId, Title = x.Title }).ToList();
            ViewBag.AccCodeDetails1 = db.Accounts.Where(x => x.AccType.Trim() == "Group").Select(x => new DetailList { AccountId = x.AccountId, Title = x.Title }).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AddControl(string cash, string cashtitle, string cash2, string cashtitle2, string bank, string banktitle, string purchase, string purchasetitle, string sale, string saletitle)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                if (string.IsNullOrEmpty(cashtitle) || cashtitle.Contains("-- Select Account --") || string.IsNullOrEmpty(banktitle) || banktitle.Contains("-- Select Account --") || string.IsNullOrEmpty(purchasetitle) || purchasetitle.Contains("-- Select Account --") || string.IsNullOrEmpty(saletitle) || saletitle.Contains("-- Select Account --"))
                {
                    TempData["message"] = "Select Account";
                }
                else{
                var controldata = new ControllAccount()
                {
                    CashCtrlHO = cash,
                    CashCtrlTitleHO = cashtitle,
                    CashCtrlFO = cash2,
                    CashCtrlTitleFO = cashtitle2,
                    BankCtrl = bank,
                    BankCtrlTitle = banktitle,
                    PurchaseCtrl = purchase,
                    PurchaseCtrlTitle = purchasetitle,
                    SaleCtrl=sale,
                    SaleCtrlTitle=saletitle
                };
                if (ModelState.IsValid)
                {
                    db.ControllAccounts.Add(controldata);
                    db.SaveChanges();
                    TempData["message"] = "Successfully Saved";
                }
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return RedirectToAction("Add");
        }
    }
}
