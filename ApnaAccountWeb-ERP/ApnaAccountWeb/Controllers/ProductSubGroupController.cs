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
    public class ProductSubGroupController : Controller
    {
        public ActionResult ViewProductSubGroup()
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
            return View(db.ProductSubGroups.ToList());
        }
        public ActionResult RegisterProductSubGroup()
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
        public ActionResult RegisterProductSubGroup(ProductSubGroup psg)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                if (ModelState.IsValid)
                {
                    db.ProductSubGroups.Add(psg);
                    db.SaveChanges();
                    TempData["message"] = "Successfully Added";
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return RedirectToAction("RegisterProductSubGroup");
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
                ProductSubGroup psg = db.ProductSubGroups.Find(id);
                if (psg == null)
                {
                    return HttpNotFound();
                }
                return View(psg);
            }
            else
            {
                TempData["message"] = "You do not have permission to edit this.";
                return RedirectToAction("ViewProductSubGroup");
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductSubGroup psg)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(psg).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["message"] = "Successfully Updated";
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return RedirectToAction("ViewProductSubGroup");
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
                    ProductSubGroup psg = db.ProductSubGroups.Find(id);
                    db.ProductSubGroups.Remove(psg);
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
            return RedirectToAction("ViewProductSubGroup");
        }
    }
}
