using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ApnaAccountWeb.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /Common/
        public JsonResult ResetSession()
        {
            Session.Timeout = 1000;
            return Json(Session.Timeout, JsonRequestBehavior.AllowGet);
        }
    }
}
