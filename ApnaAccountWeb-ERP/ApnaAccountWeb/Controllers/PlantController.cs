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
    public class PlantController : Controller
    {
        public ActionResult ViewPlant()
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
            return View(db.Plants.ToList());
        }
        public ActionResult RegisterPlant()
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
        public ActionResult RegisterPlant(Plant plant)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                if (ModelState.IsValid)
                {
                    db.Plants.Add(plant);
                    db.SaveChanges();
                    TempData["message"] = "Successfully Added";
                    return RedirectToAction("RegisterPlant");
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View(plant);
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
                 Plant plant = db.Plants.Find(id);
                 if (plant == null)
                 {
                     return HttpNotFound();
                 }
                 return View(plant);
             }
             else
             {
                 TempData["message"] = "You do not have permission to edit this.";
                 return RedirectToAction("ViewPlant");
             }
        }
        [HttpPost]
        public ActionResult Edit(Plant plant)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(plant).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["message"] = "Successfully Updated";
                    return RedirectToAction("ViewPlant");
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;  
            }
            return View(plant);
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
                Plant plant = db.Plants.Find(id);
                db.Plants.Remove(plant);
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
            return RedirectToAction("ViewPlant");
        }
    }
}
