using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManager.Controllers.Admin
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View("User_Manage");
        }
        public ActionResult Project_Manage()
        {
            return View("Project_Manage");
        }
        public ActionResult User_Manage()
        {
            return View("User_Manage");
        }

    }
}
