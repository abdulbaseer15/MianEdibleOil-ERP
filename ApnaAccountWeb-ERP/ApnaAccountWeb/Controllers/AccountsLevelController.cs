using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApnaAccountWeb.Models;
using ApnaAccountWeb.ViewModels;
namespace ApnaAccountWeb.Controllers
{
    public class AccountsLevelController : Controller
    {
        public ActionResult AddLevel()
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
            var data = (from s in db.AccountingLevels select s.AccountLevel).FirstOrDefault();
            if (data != 0)
            {
                ViewBag.data ="true";
                ViewBag.level = data;
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddLevel(AccountingLevel al)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                if (ModelState.IsValid)
                {
                    db.AccountingLevels.Add(al);
                    db.SaveChanges();
                    TempData["message"] = "Successfully Added";
                    return RedirectToAction("Index","Home");
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View(al);
        }
    }
}
