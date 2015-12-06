using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProjectManager.Common;
using System.Web.Http;

namespace ProjectManager.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        
        public ActionResult Index()
        {
            return View();
        }
        #region 登录验证
        public ActionResult LoginCheck()
        {
            string id=Request["user"];
            string pass=Request["pass"];
            string p_belongs=Request["p_belongs"];
            Session["p_belongs"] =( p_belongs.Equals("a") ? "科研项目管理系统" : "质量工程项目管理系统");
            
           
            int result= BLL.UserInfoServer.CheckLogin(id,pass);
           

            if (result < 0)
            {
                return Content("登录失败！");
            }
            else
            {
                FormsAuthentication.SetAuthCookie(id, false);
                BLL.UserInfoServer server = new BLL.UserInfoServer();
                Model.User u = server.getUserInfo(id,pass);
               Session["userinfo"] = u;
               ViewBag.p_belongs=(string)Session["p_belongs"];

                return Redirect("/usermanager/");
               
            }

        }
        #endregion


        #region 
        public void Logout()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
        #endregion
    }
}
