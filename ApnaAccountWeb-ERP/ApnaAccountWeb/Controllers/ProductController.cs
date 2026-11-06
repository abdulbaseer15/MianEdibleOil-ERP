using ApnaAccountWeb.Models;
using ApnaAccountWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ApnaAccountWeb.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult ViewProducts()
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
            return View(db.Products.ToList());
        }
        public ActionResult RegisterProduct()
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
                ViewBag.makerDetails = db.ProductSubGroups.Select(x => new DetailList { Code = x.PSG_Id, Title = x.Title }).ToList();
                ViewBag.pgroupDetails = db.ProductGroups.Select(x => new DetailList { Code = x.PG_Id, Title = x.Title }).ToList();
                ViewBag.punitDetails = db.ProductUnits.Select(x => new DetailList { Code = x.PU_Id, Title = x.Title }).ToList();
                var AccountsList = from s in db.Accounts
                                   where s.ParentId.StartsWith("0102") &&  s.AccType == "Detail"
                                   select s;
                ViewBag.AccCodelistpur1 = AccountsList.ToList();
                var AccountsList2 = from s in db.Accounts
                                    where s.ParentId.StartsWith("0401") && s.AccType == "Detail"
                                  select s;
                ViewBag.AccCodelistSale1 = AccountsList2.ToList();
                var AccountsList3 = from s in db.Accounts
                                    where s.ParentId.StartsWith("0402") && s.AccType == "Detail"
                                    select s;
                ViewBag.AccCodelistdis1 = AccountsList3.ToList();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View();
        }
        [HttpPost]
        public ActionResult RegisterProduct(Product p)
        {
            p.ItemDetail = p.ProductName;
            p.OpeningQty = p.StockInHand;
            p.OpeningRate = p.PurchasePrice;
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(p);
                    db.SaveChanges();
                    TempData["message"] = "Successfully Added";
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message + ".May be a duplicate entry.";
            }
            return RedirectToAction("RegisterProduct");
        }
        public ActionResult Edit(int id = 0)
        {
            string usertype = Convert.ToString(Session["usertype"]);
            if (!string.IsNullOrEmpty(usertype) && (usertype == "Admin" || usertype == "Manager"))
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
                Product p = db.Products.Find(id);
                if (p == null)
                {
                    return HttpNotFound();
                }
                ViewBag.makerDetails = db.ProductSubGroups.Select(x => new DetailList { Code = x.PSG_Id, Title = x.Title }).ToList();
                ViewBag.pgroupDetails = db.ProductGroups.Select(x => new DetailList { Code = x.PG_Id, Title = x.Title }).ToList();
                ViewBag.punitDetails = db.ProductUnits.Select(x => new DetailList { Code = x.PU_Id, Title = x.Title }).ToList();
                var AccountsList = from s in db.Accounts
                                   where s.ParentId.StartsWith("0102") && s.AccType == "Detail"
                                   select s;
                ViewBag.AccCodelistpur1 = AccountsList.ToList();
                var AccountsList2 = from s in db.Accounts
                                    where s.ParentId.StartsWith("0401") && s.AccType == "Detail"
                                    select s;
                ViewBag.AccCodelistSale1 = AccountsList2.ToList();
                var AccountsList3 = from s in db.Accounts
                                    where s.ParentId.StartsWith("0402") && s.AccType == "Detail"
                                    select s;
                ViewBag.AccCodelistdis1 = AccountsList3.ToList();
                return View(p);
            }
            else
            {
                TempData["message"] = "You do not have permission to edit this.";
                return RedirectToAction("ViewProducts");
            }
        }
        [HttpPost]
        public ActionResult Edit(Product p)
        {
            p.ItemDetail = p.ProductName;
            p.OpeningQty = p.OpeningQty;
            p.OpeningRate = p.OpeningRate;
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["message"] = "Successfully Updated";
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return RedirectToAction("ViewProducts");
        }
        public ActionResult Delete(int id)
        {
            string usertype = Convert.ToString(Session["usertype"]);
            if (!string.IsNullOrEmpty(usertype) && (usertype == "Admin"))
            {
                string dbcon = Convert.ToString(Session["con"]);
                DbConection obj = new DbConection();
                string condb = obj.Connect(dbcon);
                ApnaAccountEntities db = new ApnaAccountEntities(condb);
                try
                {
                    Product p = db.Products.Find(id);
                    db.Products.Remove(p);
                    db.SaveChanges();
                    TempData["message"] = "Successfully Deleted";
                }
                catch (Exception ex)
                {
                    TempData["message"] = ex.Message;
                }
            }
            else
            {
                TempData["message"] = "You do not have permission to delete this.";
            }
            return RedirectToAction("ViewProducts");
        }
    }
}
