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
            Session.Timeout = 30;
            string id=Request["user"];
            string pass=Request["pass"];
            Session["ID"] = id;
            string p_belongs=Request["p_belongs"];
            if(p_belongs.Equals("质量工程项目管理系统")){
                 Session["p_belongs"] = "b";
                 Session["title"] = "质量工程项目管理系统";
            }
            else{
            Session["p_belongs"] = "a";
            Session["title"] = "科研项目管理系统";
            }
           
            int result= BLL.UserInfoServer.CheckLogin(id,pass);
           

            if (result < 0)
            {
                return Content("登录失败！");
            }
            else
            {
                FormsAuthentication.SetAuthCookie(id, false);
                BLL.UserInfoServer server = new BLL.UserInfoServer();
                Model.User u = server.getUserInfo(id, pass);
                Session["userinfo"] = u;
              

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
