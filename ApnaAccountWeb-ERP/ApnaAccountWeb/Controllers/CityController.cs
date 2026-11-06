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
    public class CityController : Controller
    {
        public ActionResult ViewCity()
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
            return View(db.Cities.ToList());
        }
        public ActionResult RegisterCity()
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
        public ActionResult RegisterCity(City city)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                if (ModelState.IsValid)
                {
                    db.Cities.Add(city);
                    db.SaveChanges();
                    TempData["message"] = "Successfully Added";
                    return RedirectToAction("RegisterCity");
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
                 City city = db.Cities.Find(id);
                 if (city == null)
                 {
                     return HttpNotFound();
                 }
                 return View(city);
             }
             else
             {
                 TempData["message"] = "You do not have permission to edit this.";
                 return RedirectToAction("ViewCity");
             }
        }
        [HttpPost]
        public ActionResult Edit(City city)
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
                    return RedirectToAction("ViewCity");
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
                City city = db.Cities.Find(id);
                db.Cities.Remove(city);
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
            return RedirectToAction("ViewCity");
        }
    }
}
