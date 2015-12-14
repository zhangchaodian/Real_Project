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
            string type=Request["type"];
            Session["p_belongs"]=Request["p_belongs"];
            switch(Session["p_belongs"].ToString()){
                case "a":
                    TempData["belongs_name"] = "科研";
                    break;
                case "b":
                    TempData["belongs_name"] = "质量工程";
                    break;
            }
           
            int result= BLL.UserInfoServer.CheckLogin(id,pass,Request["p_belongs"].ToString(),Request["type"]);
           

            if (result < 0)
            {
                Response.Write("<script>alert('登录失败，用户名不存在或密码错误！');</script>");
                return View("index");
               
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
