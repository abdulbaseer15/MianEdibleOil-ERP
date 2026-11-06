using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApnaAccountWeb.Models;
using ApnaAccountWeb.ViewModels;
namespace ApnaAccountWeb.Controllers
{
    public class BardanaTypeController : Controller
    {
        public ActionResult ViewBardanaType()
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
            return View(db.BardanaTypes.ToList());
        }
        public ActionResult RegisterBardanaType()
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
        public ActionResult RegisterBardanaType(BardanaType city)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                var id = (from s in db.BardanaTypes orderby s.BTypeID descending select s.BTypeID).FirstOrDefault();
                city.BTypeID = id + 1;
                if (ModelState.IsValid)
                {
                    db.BardanaTypes.Add(city);
                    db.SaveChanges();
                    TempData["message"] = "Successfully Added";
                    return RedirectToAction("RegisterBardanaType");
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View(city);
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
                 BardanaType city = db.BardanaTypes.Find(id);
                 if (city == null)
                 {
                     return HttpNotFound();
                 }
                 return View(city);
             }
             else
             {
                 TempData["message"] = "You do not have permission to edit this.";
                 return RedirectToAction("ViewBardanaType");
             }
        }
        [HttpPost]
        public ActionResult Edit(BardanaType city)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(city).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["message"] = "Successfully Updated";
                    return RedirectToAction("ViewBardanaType");
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;  
            }
            return View(city);
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
                BardanaType city = db.BardanaTypes.Find(id);
                db.BardanaTypes.Remove(city);
                db.SaveChanges();
                TempData["message"] = "Successfully Deleted";
            }
            catch (Exception ex)
            {
                TempData["message"]= ex.Message;
            }
             }
             else
             {
                 TempData["message"] = "You do not have permission to delete this.";
             }
             return RedirectToAction("ViewBardanaType");
        }
    }
}
