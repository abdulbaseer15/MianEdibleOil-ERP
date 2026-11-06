using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApnaAccountWeb.Models;
using ApnaAccountWeb.ViewModels;
using System.Data;
using System.Web.Security;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.IO;
namespace ApnaAccountWeb.Controllers
{
    public class UserController : Controller
    {
        private const string EncryptionKey = "YourEncryptionKey"; // Change this key
        public static string Encrypt(string plainText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(plainText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    plainText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return plainText;
        }
        // Decription
        public static string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        private Ini_ApnaAccountEntities inidb = new Ini_ApnaAccountEntities();
        public ActionResult ViewUser()
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
            return View(db.Users.ToList());
        }
        public ActionResult RegisterUser()
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
        public ActionResult RegisterUser(User user)
        {
            string dbcon = Convert.ToString(Session["con"]);
            int ccode = Convert.ToInt32(Session["comcode"]);
            user.Ccode = ccode;
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    var userid = (from s in db.Users orderby s.UserId descending select s.UserId).FirstOrDefault();
                    if (user.UserType == "Admin")
                    {
                        for (int i = 1; i <= 89; i++)
                        {
                            db.Database.ExecuteSqlCommand("insert into  UserRight (Userid,selected)values(@userid,@selected)", new System.Data.SqlClient.SqlParameter("userid", userid), new System.Data.SqlClient.SqlParameter("selected", i));
                        }
                    }
                    else if (user.UserType == "User")
                    {
                        //for (int i = 1; i <= 24; i++)
                        //{
                        //    if ((i >= 15 && i <= 18) || (i >= 20 && i <= 24))
                        //    {
                        //    }
                        //    else 
                        //    {
                        //        db.Database.ExecuteSqlCommand("insert into  UserRight (Userid,selected)values(@userid,@selected)", new System.Data.SqlClient.SqlParameter("userid", userid), new System.Data.SqlClient.SqlParameter("selected", i));
                        //    }
                        //    }
                    }
                    TempData["message"] = "Successfully Added";
                    return RedirectToAction("RegisterUser");
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return View(user);
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
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
            }
             else {
                TempData["message"] = "You do not have permission to edit this.";
                return RedirectToAction("ViewUser");
            }
        }
          [HttpPost]
        public ActionResult Edit(User user)
        {
                string dbcon = Convert.ToString(Session["con"]);
                DbConection obj = new DbConection();
                string condb = obj.Connect(dbcon);
                ApnaAccountEntities db = new ApnaAccountEntities(condb);
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        //db.Database.ExecuteSqlCommand("DELETE FROM  UserRight WHERE Userid= @userid", new System.Data.SqlClient.SqlParameter("userid", user.UserId));
                        //if (user.UserType == "Admin" || user.UserType == "Manager")
                        //{
                        //    for (int i = 1; i <= 24; i++)
                        //    {
                        //        db.Database.ExecuteSqlCommand("insert into  UserRight (Userid,selected)values(@userid,@selected)", new System.Data.SqlClient.SqlParameter("userid", user.UserId), new System.Data.SqlClient.SqlParameter("selected", i));
                        //    }
                        //}
                        //else if (user.UserType == "User")
                        //{
                        //    for (int i = 1; i <= 24; i++)
                        //    {
                        //        if ((i >= 15 && i <= 18) || (i >= 20 && i <= 24))
                        //        {
                        //        }
                        //        else
                        //        {
                        //            db.Database.ExecuteSqlCommand("insert into  UserRight (Userid,selected)values(@userid,@selected)", new System.Data.SqlClient.SqlParameter("userid", user.UserId), new System.Data.SqlClient.SqlParameter("selected", i));
                        //        }
                        //    }
                        //}
                        TempData["message"] = "Successfully Updated";
                        return RedirectToAction("ViewUser");
                    }
                }
                catch (Exception ex)
                {
                    TempData["message"] = ex.Message;
                }
            return View(user);
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
                     User user = db.Users.Find(id);
                     db.Users.Remove(user);
                     db.Database.ExecuteSqlCommand("DELETE FROM  UserRight WHERE Userid= @userid", new System.Data.SqlClient.SqlParameter("userid", id));
                     db.SaveChanges();
                     TempData["message"] = "Successfully Deleted";
                 }
                 catch (Exception ex)
                 {
                     TempData["message"] = ex.Message;
                 }
                 return RedirectToAction("ViewUser");
             }
             else
             {
                 TempData["message"] = "You do not have permission to delete this.";
                 return RedirectToAction("ViewUser");
             }
        }
        public ActionResult Logout()
        {
            // Delete the cookie named "MyCookie"
            if (Request.Cookies["MyCookie"] != null)
            {
                HttpCookie myCookie = new HttpCookie("MyCookie");
                myCookie.Expires = DateTime.Now.AddDays(-1); // Expire immediately
                Response.Cookies.Add(myCookie);
            }
            if (Request.Cookies["MyCookie1"] != null)
            {
                HttpCookie myCookie = new HttpCookie("MyCookie1");
                myCookie.Expires = DateTime.Now.AddDays(-1); // Expire immediately
                Response.Cookies.Add(myCookie);
            }
            if (Request.Cookies["MyCookie2"] != null)
            {
                HttpCookie myCookie = new HttpCookie("MyCookie2");
                myCookie.Expires = DateTime.Now.AddDays(-1); // Expire immediately
                Response.Cookies.Add(myCookie);
            }
            if (Request.Cookies["MyCookie3"] != null)
            {
                HttpCookie myCookie = new HttpCookie("MyCookie3");
                myCookie.Expires = DateTime.Now.AddDays(-1); // Expire immediately
                Response.Cookies.Add(myCookie);
            }
            if (Request.Cookies["MyCookie4"] != null)
            {
                HttpCookie myCookie = new HttpCookie("MyCookie4");
                myCookie.Expires = DateTime.Now.AddDays(-1); // Expire immediately
                Response.Cookies.Add(myCookie);
            }
            if (Request.Cookies["MyCookie5"] != null)
            {
                HttpCookie myCookie = new HttpCookie("MyCookie5");
                myCookie.Expires = DateTime.Now.AddDays(-1); // Expire immediately
                Response.Cookies.Add(myCookie);
            }
            if (Request.Cookies["MyCookie6"] != null)
            {
                HttpCookie myCookie = new HttpCookie("MyCookie6");
                myCookie.Expires = DateTime.Now.AddDays(-1); // Expire immediately
                Response.Cookies.Add(myCookie);
            }
            if (Request.Cookies["MyCookie7"] != null)
            {
                HttpCookie myCookie = new HttpCookie("MyCookie7");
                myCookie.Expires = DateTime.Now.AddDays(-1); // Expire immediately
                Response.Cookies.Add(myCookie);
            }
            Session.RemoveAll();
            return RedirectToAction("Login", "User");
        }
        //public ActionResult Login()
        //{
        //    Session.RemoveAll();
        //    //ViewBag.AccCodeDetails = inidb.Ini_Company.Select(x => new DetailList { Code = x.Ccode, Title = x.Name }).ToList();
        //    return View();
        //}
        public ActionResult Login()
        {
            try
            {
                var MyCookie = Request.Cookies["MyCookie"];
                var MyCookie2 = Request.Cookies["MyCookie2"];
                var MyCookie3 = Request.Cookies["MyCookie3"];
                if (Request.Cookies["MyCookie"] != null || Request.Cookies["MyCookie2"] != null || Request.Cookies["MyCookie3"] != null)
                {
                    // Retrieve the cookie by name
                    var cookie4 = ApnaAccountWeb.Controllers.UserController.Decrypt(MyCookie.Value.ToString());
                    var cookie5 = ApnaAccountWeb.Controllers.UserController.Decrypt(MyCookie2.Value.ToString());
                    var cookie6 = ApnaAccountWeb.Controllers.UserController.Decrypt(MyCookie3.Value.ToString());
                    string cookie7 = "";
                    if (cookie4 == null || cookie4 == "")
                        cookie7 = "";
                    else
                    {
                        if (cookie4 != "")
                            cookie7 = cookie4.ToString();
                    }
                    string cookie8 = "";
                    if (cookie5 == null || cookie5 == "")
                        cookie8 = "";
                    else
                    {
                        if (cookie5 != "")
                            cookie8 = cookie5.ToString();
                    }
                    string cookie9 = "";
                    if (cookie6 == null || cookie6 == "")
                        cookie9 = "";
                    else
                    {
                        if (cookie6 != "")
                            cookie9 = cookie6.ToString();
                    }
                    if (String.IsNullOrEmpty(cookie7) || String.IsNullOrEmpty(cookie8) || String.IsNullOrEmpty(cookie9))
                    {
                        // Set the cookie's expiration date to a past date
                        //cookie4.Expires = DateTime.Now;
                        //cookie5.Expires = DateTime.Now;
                        //cookie6.Expires = DateTime.Now;
                        //// Add the cookie to the response to remove it from the client's browser
                        //Response.Cookies.Add(cookie4);
                        //Response.Cookies.Add(cookie5);
                        //Response.Cookies.Add(cookie6);
                        // return RedirectToAction("Login", "User");
                    }
                    else
                    {
                        // Retrieve the cookie by name
                        var cookie = Request.Cookies["MyCookie"];
                        var cookie2 = Request.Cookies["MyCookie2"];
                        var cookie3 = Request.Cookies["MyCookie3"];
                        var cookie44 = Request.Cookies["MyCookie4"];
                        var cookie55 = Request.Cookies["MyCookie5"];
                        var cookie66 = Request.Cookies["MyCookie6"];
                        var cookie77 = Request.Cookies["MyCookie7"];
                        var cookie88 = Request.Cookies["MyCookie8"];
                        var cookie99 = Request.Cookies["MyCookie9"];

                        // Set the cookie's expiration date to a past date
                        cookie.Expires = DateTime.Now.AddDays(1);
                        cookie2.Expires = DateTime.Now.AddDays(1);
                        cookie3.Expires = DateTime.Now.AddDays(1);
                        cookie44.Expires = DateTime.Now.AddDays(1);
                        cookie55.Expires = DateTime.Now.AddDays(1);
                        cookie66.Expires = DateTime.Now.AddDays(1);
                        cookie77.Expires = DateTime.Now.AddDays(1);
                        cookie88.Expires = DateTime.Now.AddDays(1);
                        cookie99.Expires = DateTime.Now.AddDays(1);
                        // Add the cookie to the response to remove it from the client's browser
                        Response.Cookies.Add(cookie);
                        Response.Cookies.Add(cookie2);
                        Response.Cookies.Add(cookie3);
                        Response.Cookies.Add(cookie44);
                        Response.Cookies.Add(cookie55);
                        Response.Cookies.Add(cookie66);
                        Response.Cookies.Add(cookie77);
                        Response.Cookies.Add(cookie88);
                        Response.Cookies.Add(cookie99);
                        string dbcon = ApnaAccountWeb.Controllers.UserController.Decrypt(Request.Cookies["MyCookie"].Value.ToString());
                        Session["con"] = dbcon;
                        string dbconsql = ApnaAccountWeb.Controllers.UserController.Decrypt(Request.Cookies["MyCookie2"].Value.ToString());
                        Session["consql"] = dbconsql;
                        // Get User ID
                        string Userid = ApnaAccountWeb.Controllers.UserController.Decrypt(Request.Cookies["MyCookie3"].Value.ToString());
                        Session["userid"] = Userid;
                        // Get UserName
                        string UserName = ApnaAccountWeb.Controllers.UserController.Decrypt(Request.Cookies["MyCookie4"].Value.ToString());
                        Session["username"] = UserName;
                        // Get ShopId
                        string Office = ApnaAccountWeb.Controllers.UserController.Decrypt(Request.Cookies["MyCookie5"].Value.ToString());
                        Session["Office"] = Office;
                        // Get usertype
                        string UserType = ApnaAccountWeb.Controllers.UserController.Decrypt(Request.Cookies["MyCookie6"].Value.ToString());
                        Session["usertype"] = UserType;
                        // Get VoucherClosing
                        string VoucherClosing = ApnaAccountWeb.Controllers.UserController.Decrypt(Request.Cookies["MyCookie7"].Value.ToString());
                        Session["Vouchertype"] = VoucherClosing;
                        // Get Company name
                        string Company = ApnaAccountWeb.Controllers.UserController.Decrypt(Request.Cookies["MyCookie8"].Value.ToString());
                        Session["Company"] = Company;
                        // Get VoucherClosing
                        string Address = ApnaAccountWeb.Controllers.UserController.Decrypt(Request.Cookies["MyCookie9"].Value.ToString());
                        Session["Address"] = Address;

                        return RedirectToAction("Index", "Home");
                    }
                }
                // Session.RemoveAll();
                ViewBag.AccCodeDetails = inidb.Ini_Company.Select(x => new DetailList { Code = x.Ccode, Title = x.Name }).ToList();
                return View();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.ToString();
                return View();
            }
        }
        [HttpPost]
        public ActionResult Login(string UserName, string Password,int code)
        {
            var result = (from e in inidb.Ini_Company
                              where e.Ccode == code
                              select e).FirstOrDefault();
                if (result != null)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(Session["con"])))
                    {
                    Session["con"] = result.DBConEF;
                    // Create a new cookie
                    var cookie = new HttpCookie("MyCookie");
                    // Set the cookie value
                    // cookie.Value = result.DBConEF;
                    cookie.Value = UserController.Encrypt(result.DBConEF);
                    // Set additional cookie properties if needed, such as expiration date
                    cookie.Expires = DateTime.Now.AddDays(1); // Expires in 1 day
                    // Add the cookie to the response
                    Response.Cookies.Add(cookie);
                    // Create a new cookie
                    var cookie2 = new HttpCookie("MyCookie2");
                    // Set the cookie value
                    cookie2.Value = UserController.Encrypt(result.DBConSQL);
                    // Set additional cookie properties if needed, such as expiration date
                    cookie2.Expires = DateTime.Now.AddDays(1); // Expires in 1 day
                    // Add the cookie to the response
                    Response.Cookies.Add(cookie2);
                    Session["consql"] = result.DBConSQL;
                        Session["comcode"] = result.Ccode;
                        Session["comName"] = result.Name;
                        Session["Vouchertype"] = result.VoucherClosing;
                        string dbcon = Convert.ToString(Session["con"]);
                        DbConection obj = new DbConection();
                        string condb = obj.Connect(dbcon);
                        ApnaAccountEntities db = new ApnaAccountEntities(condb);
                        var userdata = (from s in db.Users where s.UserName == UserName && s.Password == Password select s).FirstOrDefault();
                        if (userdata != null)
                        {
                            Session["usertype"] = userdata.UserType;
                            Session["userid"] = userdata.UserId;
                            Session["username"] = userdata.UserName;
                            Session["Office"] = userdata.Office;
                        // Create a new cookie for User ID
                        var cookie3 = new HttpCookie("MyCookie3");
                        // Set the cookie value
                        cookie3.Value = UserController.Encrypt(Convert.ToString(userdata.UserId));
                        // Set additional cookie properties if needed, such as expiration date
                        cookie3.Expires = DateTime.Now.AddDays(1); // Expires in 1 day
                        // Add the cookie to the response
                        Response.Cookies.Add(cookie3);
                        // Create a new cookie for UserName
                        var cookie4 = new HttpCookie("MyCookie4");
                        // Set the cookie value
                        cookie4.Value = UserController.Encrypt(Convert.ToString(userdata.UserName));
                        // Set additional cookie properties if needed, such as expiration date
                        cookie4.Expires = DateTime.Now.AddDays(1); // Expires in 1 day
                        // Add the cookie to the response
                        Response.Cookies.Add(cookie4);
                        // Create a new cookie for ShopId
                        var cookie5 = new HttpCookie("MyCookie5");
                        // Set the cookie value
                        cookie5.Value = UserController.Encrypt(Convert.ToString(userdata.Office));
                        // Set additional cookie properties if needed, such as expiration date
                        cookie5.Expires = DateTime.Now.AddDays(1); // Expires in 1 day
                        // Add the cookie to the response
                        Response.Cookies.Add(cookie5);
                        // Create a new cookie for UserType
                        var cookie6 = new HttpCookie("MyCookie6");
                        // Set the cookie value
                        cookie6.Value = UserController.Encrypt(Convert.ToString(userdata.UserType));
                        // Set additional cookie properties if needed, such as expiration date
                        cookie6.Expires = DateTime.Now.AddDays(1); // Expires in 1 day
                        // Add the cookie to the response
                        Response.Cookies.Add(cookie6);
                        // Create a new cookie for VoucherClosing
                        var cookie7 = new HttpCookie("MyCookie7");
                        // Set the cookie value
                        cookie7.Value = UserController.Encrypt(Convert.ToString(result.VoucherClosing));
                        // Set additional cookie properties if needed, such as expiration date
                        cookie7.Expires = DateTime.Now.AddDays(1); // Expires in 1 day
                        // Add the cookie to the response
                        Response.Cookies.Add(cookie7);

                        var company = db.Companies.FirstOrDefault();
                        if (company != null)
                        {
                            Session["Company"] = company.Title;
                            Session["Address"] = company.Address;

                            // Create a new cookie for VoucherClosing
                            var cookie8 = new HttpCookie("MyCookie8");
                            // Set the cookie value
                            cookie8.Value = UserController.Encrypt(Convert.ToString(company.Title));
                            // Set additional cookie properties if needed, such as expiration date
                            cookie8.Expires = DateTime.Now.AddDays(1); // Expires in 1 day
                            // Add the cookie to the response
                            Response.Cookies.Add(cookie8);

                            // Create a new cookie for VoucherClosing
                            var cookie9 = new HttpCookie("MyCookie9");
                            // Set the cookie value
                            cookie9.Value = UserController.Encrypt(Convert.ToString(company.Address));
                            // Set additional cookie properties if needed, such as expiration date
                            cookie9.Expires = DateTime.Now.AddDays(1); // Expires in 1 day
                            // Add the cookie to the response
                            Response.Cookies.Add(cookie9);


                        }




                        if (result.Ccode == 1000)
                            {
                                Session["LoggedUserID"] =1;
                            }
                            return RedirectToAction("Index", "Home");
                        }
                        else {
                            TempData["message"] = "Username or password is incorrect! Try again.";
                            Session.RemoveAll();
                            ViewBag.AccCodeDetails = inidb.Ini_Company.Select(x => new DetailList { Code = x.Ccode, Title = x.Name }).ToList();
                            return View();
                        }
                    }
                    else
                    {
                        TempData["message"] = "Already logged in same window.Try to login in new window";
                        Session.RemoveAll();
                        ViewBag.AccCodeDetails = inidb.Ini_Company.Select(x => new DetailList { Code = x.Ccode, Title = x.Name }).ToList();
                        return View();
                    }
                }
                else
                {
                    TempData["message"] = "Company does not exist";
                    Session.RemoveAll();
                    ViewBag.AccCodeDetails = inidb.Ini_Company.Select(x => new DetailList { Code = x.Ccode, Title = x.Name }).ToList();
                    return View();
                }
        }
        public ActionResult Registeration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveRequest(Contact contact)
        {
            //string dbcon = "weighbri_ApnaAccounti";
            string dbcon = "ApnaAccounti";
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                if (ModelState.IsValid)
                {
                    db.Contacts.Add(contact);
                    db.SaveChanges();
                    TempData["message"] = "Successfully submitted!We will contact you shortly.";
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }
            return RedirectToAction("Registeration");
        }
        public ActionResult UserRights()
        {
            string dbcon = Convert.ToString(Session["con"]);
            if (string.IsNullOrEmpty(dbcon))
            {
                return RedirectToAction("Login", "User");
            }
                DbConection obj = new DbConection();
                string condb = obj.Connect(dbcon);
                ApnaAccountEntities db = new ApnaAccountEntities(condb);
                ViewBag.AccCodeDetails = db.Users.Select(x => new DetailList { Code = x.UserId, Title = x.UserName }).ToList();
                return View();
        }
        [HttpPost]
        public JsonResult SaveUserRights(UserRightViewModel model)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            short userid =  Convert.ToInt16(model.userid);
            if (model.detail!=null)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM  UserRight WHERE Userid= @userid", new System.Data.SqlClient.SqlParameter("userid", userid));
                    foreach (var udetail in model.detail)
                    {
                        var userdata = new UserRight()
                        {
                            Userid = userid,
                            Selected = udetail
                        };
                        if (ModelState.IsValid)
                        {
                            db.UserRights.Add(userdata);
                        }
                    }
                    db.SaveChanges();
                    return Json(new { success = true, responseText = "Successfully added" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                db.Database.ExecuteSqlCommand("DELETE FROM  UserRight WHERE Userid= @userid", new System.Data.SqlClient.SqlParameter("userid", userid));
                return Json(new { success = true, responseText = "All rights are removed" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult SearchRole(Int16 userid)
        {
            string dbcon = Convert.ToString(Session["con"]);
            DbConection obj = new DbConection();
            string condb = obj.Connect(dbcon);
            ApnaAccountEntities db = new ApnaAccountEntities(condb);
            try
            {
                //var lstDVChilds = db.VouchersDetails.Where(x => x.VId == 1 && x.VType == "CV" && x.Credit > 0);
                //return Json(lstDVChilds, JsonRequestBehavior.AllowGet);
                var lstuserrole = db.UserRights.Where(x => x.Userid == userid);
                return Json(lstuserrole, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
