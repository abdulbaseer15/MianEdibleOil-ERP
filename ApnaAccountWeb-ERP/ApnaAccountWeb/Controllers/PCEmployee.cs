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
    public class PCEmployeeController : Controller
    {
        public ActionResult ViewPCEmployee()
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
            return View(db.Employs.ToList());
        }
        public ActionResult RegisterPCEmployee()
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
        public ActionResult RegisterPCEmployee(Employ p)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                if (ModelState.IsValid)
                {
                    db.Employs.Add(p);
                    db.SaveChanges();
                    TempData["message"] = "Successfully Added";
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message + ".May be a duplicate entry.";
            }
            return RedirectToAction("RegisterPCEmployee");
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
                Employ p = db.Employs.Find(id);
                if (p == null)
                {
                    return HttpNotFound();
                }
                return View(p);
            }
            else
            {
                TempData["message"] = "You do not have permission to edit this.";
                return RedirectToAction("ViewPCEmployee");
            }
        }
        [HttpPost]
        public ActionResult Edit(Employ p)
        {
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
            return RedirectToAction("ViewPCEmployee");
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
                    Employ p = db.Employs.Find(id);
                    db.Employs.Remove(p);
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
            return RedirectToAction("ViewPCEmployee");
        }
    }
}
