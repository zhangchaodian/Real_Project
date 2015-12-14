using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;

namespace ProjectManager.Controllers.Admin
{
    [Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            ViewBag.title = TempData["belongs_name"];
            Session.Timeout = 60;
            ViewBag.userinfo = Session["userinfo"];
            List<Model.User> userList = BLL.AdminServer.selectUser();

            return View("User_Manage", userList);

        }
        public ActionResult Project_Manage()
        {
            ViewBag.title = TempData["belongs_name"];
            ViewBag.user = User.Identity.Name;
            Model.User userInfo = (Model.User)Session["userinfo"];
            ViewBag.UserInfo = userInfo;
            ViewBag.project = BLL.ProjectServer.getAdminProject();
            return View("Project_Manage");
        }
        public ActionResult User_Manage()
        {
           
            List<Model.User> userList = BLL.AdminServer.selectUser();
            ViewBag.userinfo = Session["userinfo"];
            ViewBag.title = TempData["belongs_name"];
            ViewBag.userinfo = Session["userinfo"];
            return View("User_Manage", userList);
        }
        #region 用户成员管理
        public ActionResult typeSelect()
        {
            string keytype = Request.Form["keytype"];
            string keyword = Request.Form["keyword"];
            List<Model.User> user = BLL.AdminServer.selectUser();
            ViewBag.userinfo = Session["userinfo"];
            List<Model.User> userList = BLL.AdminServer.getTypeSelect(user, keytype, keyword);
            return View("User_Manage", userList);
            // return Content(keytype);
        }
        public ActionResult allSelect()
        {
            string type = Request.Form["updownorder1"];
            string sex = Request.Form["updownorder2"];
            string order = Request.Form["updownorder3"];
            ViewBag.userinfo = Session["userinfo"];
            List<Model.User> user = BLL.AdminServer.selectUser();
            List<Model.User> userList = BLL.AdminServer.getAllSelect(user, type, sex, order);
            return View("User_Manage", userList);
        }

        public JsonResult addUser()
        {
            Model.User userform = new Model.User();
            userform.ID = Request["UserID"];
            userform.pwd = Request["pwd"];
            userform.nickname = Request["nickname"];
            userform.position = Request["position"];
            userform.email = Request["email"];
            userform.phone = Request["phone"];
            userform.sex = Request["sex"];
            userform.type = Request["type"];
            string result = null;
            if (BLL.AdminServer.addUser(userform))
                result = "用户添加成功！";
            else
                result = "用户添加失败！";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getUserInfo(string UserID)
        {
            string id = Request["UserID"];
            Model.User userform = new Model.User();

            string result = null;

            result = BLL.AdminServer.getInfo(UserID);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult modify()
        {
            Model.User userform = new Model.User();
            userform.ID = Request["UserID"];
            userform.pwd = Request["pwd"];
            userform.nickname = Request["nickname"];
            userform.position = Request["position"];
            userform.email = Request["email"];
            userform.phone = Request["phone"];
            userform.sex = Request["sex"];
            userform.type = Request["type"];
            string result = BLL.AdminServer.modifyUser(userform);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult deleteUser(string UserID)
        {
            string ID = Request["UserID"];
            string result = BLL.AdminServer.deleUser(UserID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region 项目管理
        [HttpGet]
        public JsonResult getRollForm()
        {
            int ID = Convert.ToInt16(Request["p_id"]);
            List<Model.Project> project = BLL.ProjectServer.getAdminProject();
            project = (from result in project where result.p_id == ID select result).ToList();
            string str0 = project[0].pass_state;
            string str1 = project[0].comment;
            string rs = String.Format("{{\"state\":\"{0}\",\"Comment\":\"{1}\" }}", str0, str1);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        public JsonResult modifyComment()
        {
            int ID = Convert.ToInt16(Request["p_id"]);
            string redio = Request["inlineRadioOptions"];
            string comment = Request["comment"];
            string flag = BLL.ProjectServer.updateComment(ID, comment, redio);
            //Response.Write("<script>alert('"+flag+"');</script>");

            return Json(flag, JsonRequestBehavior.AllowGet);
        }
        // 搜索
        [HttpPost]
        public ActionResult selectProject()
        {
            string now_level = Request["now_level"].ToString();
            string target_level = Request["target_level"].ToString();
            string state = Request["state"].ToString();
            string order = Request["order"].ToString();
            List<Model.Project> project = BLL.ProjectServer.getAdminProject();
            project = BLL.AdminServer.getSelectProject(project, now_level, target_level, state, order);
            ViewBag.project = project;
            return View("Project_Manage");
        }


        //下载文件
        [HttpGet]
        public void DownloadFile()
        {
            int project_id = Convert.ToInt32(RouteData.Values["id"]);
            string File_Type = RouteData.Values["plus"].ToString();
            BLL.PersonalProjectServer server = new BLL.PersonalProjectServer();
            string File_Path = server.SelectEachFile(project_id, File_Type);
            string[] File_Path_Array = File_Path.Split('/');
            string file_typee = "";
            switch (File_Type)
            {
                case "report_file":
                    file_typee = "report";
                    break;
                case "paper_file":
                    file_typee = "paper";
                    break;
                case "whole_pack_file":
                    file_typee = "wholefile";
                    break;
            }
            Common.DownloadServer.WriteFile(project_id, file_typee, File_Path_Array[4]);
            //return File(new FileStream(File_Path, FileMode.Open), "application/octet-stream", Server.UrlEncode(Path.GetFileName(File_Path)));
        }


        public ActionResult projectTypeSelect()
        {
            string keytype = Request["keytype"];
            string keyword = Request["keyword"];
            List<Model.Project> project = BLL.ProjectServer.getAdminProject();
            project = BLL.AdminServer.getprojectTypeSelect(project, keytype, keyword);
            ViewBag.project = project;
            return View("Project_Manage");
        }
        #endregion

    }
}
