using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectManager.Model;

namespace ProjectManager.Controllers.Page
{
    public class PageController : Controller
    {
        //
        // GET: /Page/

        public ActionResult Index(string controller,string action,int pageIndex=1)
        {
            switch (controller)
            {
                case "Admin":
                    {
                        switch (action)
                        {
                            case "GetSearch":
                                break;
                        }
                    }
                    break;
                case "UserManager":
                    break;
            }

            return View();
        }

    }
}
