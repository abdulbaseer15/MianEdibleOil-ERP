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
    public class PartyFarmController : Controller
    {
        public ActionResult ViewPartyFarm()
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
            return View(db.Farms.ToList());
        }
        public ActionResult RegisterPartyFarm()
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
            var AccountsList = from s in db.Accounts
                               where s.AccType == "Detail"
                               select s;                                                                                           
            ViewBag.AccCodelist = AccountsList.ToList();
            ViewBag.FarmId = (from s in db.Farms orderby s.FarmID descending select s.FarmID).FirstOrDefault() + 1;
            return View();
        }
        [HttpPost]
        public ActionResult RegisterPartyFarm(Farm plant)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                if (ModelState.IsValid)
                {
                    db.Farms.Add(plant);
                    db.SaveChanges();
                    TempData["message"] = "Successfully Added";
                    return RedirectToAction("RegisterPartyFarm");
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
                 Farm plant = db.Farms.Find(id);
                 if (plant == null)
                 {
                     return HttpNotFound();
                 }
                 var AccountsList = from s in db.Accounts
                                    where s.AccType == "Detail"
                                    select s;
                 ViewBag.AccCodelist = AccountsList.ToList();
                 return View(plant);
             }
             else
             {
                 TempData["message"] = "You do not have permission to edit this.";
                 return RedirectToAction("ViewPartyFarm");
             }
        }
        [HttpPost]
        public ActionResult Edit(Farm plant)
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
                    return RedirectToAction("ViewPartyFarm");
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
                Farm plant = db.Farms.Find(id);
                db.Farms.Remove(plant);
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
            return RedirectToAction("ViewPartyFarm");
        }
    }
}
