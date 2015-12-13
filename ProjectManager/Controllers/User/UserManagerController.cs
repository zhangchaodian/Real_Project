using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectManager.Model;
using System.Web.Services;
using ProjectManager.Common;
using ProjectManager.BLL;
using System.IO;

namespace ProjectManager.Controllers.User
{

    [Authorize]
    public class UserManagerController : Controller
    {
        //
        // GET: /UserManager/

        List<Model.Project_Achievement> project;


        #region 登录后进入的首个页面
        [Authorize]
        public ActionResult index(int pageIndex = 1)
        {
            ViewBag.user = User.Identity.Name;
            Model.User userInfo = (Model.User)Session["userinfo"];
            ViewBag.UserInfo = userInfo;
            project = new List<Project_Achievement>();
            project = BLL.ProjectServer.getProjectAchievement();
            PagingHelper<Project_Achievement> StudentPaging = new  PagingHelper<Project_Achievement>(4, project);
            StudentPaging.PageIndex = pageIndex;//指定当前页
            ViewBag.project = StudentPaging;
            ViewBag.title = TempData["belongs_name"];
            return View();
        }
        #endregion




        #region  搜索处理1，返回json数据 首页

        public JsonResult GetSearch()
        {
            string str1 = Request.Form["updownorder1"];
            string str2 = Request.Form["updownorder2"];//项目级别
            string str3 = Request.Form["updownorder3"];
            string str4 = Request.Form["updownorder4"];
            string str5 = Request.Form["updownorder5"];
            project = BLL.ProjectServer.getProjectAchievement();
            project = BLL.ProjectServer.getSearchResult(project, str2, str4, str5);
            string htmlstr = BLL.ProjectServer.getJsonHtml(project);
            //string tbody = "<tr><td>1</td><td>思科</td><td>IT</td><td>某某某项目</td><td>张朝钿</td><td>审核一</td><td>国家级</td><td>2015-10-29</td></tr>".ToString();
            return Json(htmlstr, JsonRequestBehavior.AllowGet);




        }
        #endregion

        #region 搜索处理2，返回json数据
        public JsonResult GetSearch2()
        {
            string keyword_type = Request.Form["updownorder1"];
            string keyword = Request.Form["updownorder2"];//项目级别
            // string tbody = "<tr><td>1</td><td>思科</td><td>IT</td><td>某某某项目</td><td>张朝钿</td><td>审核一</td><td>国家级</td><td>2015-10-29</td></tr>".ToString();
            project = BLL.ProjectServer.getProjectAchievement();
            project = BLL.ProjectServer.getSearchResult2(project, keyword_type, keyword);
            string htmlstr = BLL.ProjectServer.getJsonHtml(project);
            return Json(htmlstr, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 详情index页面返回json数据
        [WebMethod]
        [HttpGet]
        public JsonResult GetDetail(int p_id)
        {
            //int id=Convert.ToInt16( Request["ID"]);
            project = BLL.ProjectServer.getProjectAchievement();
            // string str = "{ \"p_name\": \"挑战杯\", \"type\": \"竞赛\", \"st_name\": \"张朝钿\", \"st_detail\": \"姓名:张朝钿, 行政职务:教务主任, 手机号码:159842536, 邮箱:89756458@qq.com\", \"group\": \"zx chaodian\", \"rank\": \"国家级\", \"s_time\": \"2015-1-1\", \"f_time\": \"2015-12-1\"}";
            string str = BLL.ProjectServer.getDetail(project, p_id);
            return Json(str, JsonRequestBehavior.AllowGet);
        }
        #endregion


        [Authorize]
        public ActionResult Project_Schedule(int pageIndex=1)
        {
            ViewBag.title = TempData["belongs_name"];
            ViewBag.user = User.Identity.Name;
            Model.User userInfo = (Model.User)Session["userinfo"];
            ViewBag.UserInfo = userInfo;
           List <Model.Project_Schedule> project = BLL.ProjectServer.getSchedule();
            PagingHelper<Model.Project_Schedule> StudentPaging = new PagingHelper<Model.Project_Schedule>(4, project);
            StudentPaging.PageIndex = pageIndex;//指定当前页
            ViewBag.project = StudentPaging;
            return View("Project_Schedule");
        }

        [Authorize]
        public ActionResult Project_Declare(FormCollection collect)
        {
            Model.User userInfo = (Model.User)Session["userinfo"];
            ViewBag.title = TempData["belongs_name"];
            ViewBag.user = User.Identity.Name;
            ViewBag.userInfo = userInfo;

            return View("Project_Declare");
        }
      

        #region 完成申报功能
        [HttpPost]
        public ActionResult getDeclareInfo(FormCollection collect)
        {
            Model.User userInfo = (Model.User)Session["userinfo"];
            ViewBag.title = TempData["belongs_name"];
            ViewBag.user = User.Identity.Name;
            ViewBag.userInfo = userInfo;
            Model.declareViewModel d_project = new Model.declareViewModel();
            d_project.project_name = Request["project_name"];
            // d_project.declarant_id = (string)SessionHelper.Get("ID");
            d_project.declarant_id = this.User.Identity.Name;
            d_project.phone = Request["mainer_phone"];
            d_project.email = Request["mainer_email"];
            d_project.leader = Request["mainer_name"];
            d_project.p_type = Request["p_type"];
            d_project.now_level = Request["now_level"];
            d_project.target_level = Request["target_level"];
            d_project.s_time = Convert.ToDateTime(Request["s_time"]);
            d_project.f_time = Convert.ToDateTime(Request["f_time"]);
            d_project.money = Convert.ToInt16(Request["money"]);
            d_project.p_id = BLL.ProjectServer.getProjectNum() + 1;
            d_project.create_time = DateTime.Now;
            d_project.belongs="a";
           // d_project.belongs = (string)Session["p_belongs"];
            string[] ts_time = collect.GetValues("ts_time");
            string[] tf_time = collect.GetValues("tf_time");
            string[] t_content = collect.GetValues("t_content");
            string[] member2 = collect.GetValues("member");
            d_project.member = new List<Member>();
            foreach (string m in member2)
            {

                Model.Member mem = new Model.Member();
                mem.nickname = m;
                mem.project_id = BLL.ProjectServer.getProjectNum() + 1;
                d_project.member.Add(mem);
            }
            d_project.task = new List<Task>();
            for (int i = 1; i <= ts_time.Count(); i++)
            {

                Task t = new Task();
                t.t_id = i;
                t.s_time = Convert.ToDateTime(ts_time[i - 1]);
                t.f_time = Convert.ToDateTime(tf_time[i - 1]);
                t.task_content = t_content[i - 1];
                d_project.task.Add(t);


            }

            HttpPostedFileBase paper_file = Request.Files["paper_file"];
            HttpPostedFileBase report_file = Request.Files["report_file"];
            HttpPostedFileBase whole_file = Request.Files["whole_file"];


            d_project.report_file = "~/Upload/" + d_project.p_id + "/" + "report/" + report_file.FileName;
            d_project.paper_file = "~/Upload/" + d_project.p_id + "/" + "paper/" + paper_file.FileName;
            d_project.whole_file = "~/Upload/" + d_project.p_id + "/" + "wholefile/" + whole_file.FileName;

            if (BLL.DeclareService.declareProject(d_project))
            {
                Common.UpLoadServer.UploadFile(d_project, report_file, report_file, whole_file);
                Response.Write("<script>alert('申报成功！');</script>");
            }
            return View("Project_Declare");



        }
        #endregion


        public JsonResult GetSearch_Schedule()
        {
            string str1 = Request.Form["updownorder1"];
            string str2 = Request.Form["updownorder2"];//项目级别
            string str3 = Request.Form["updownorder3"];
            string str4 = Request.Form["updownorder4"];
            string str5 = Request.Form["updownorder5"];
            List<Model.Project_Schedule> project = BLL.ProjectServer.getSchedule();
            project = BLL.ProjectServer.getSearchResult(project, str2, str4, str5);
            string htmlstr = BLL.ProjectServer.getJsonHtml(project);
            //string tbody = "<tr><td>1</td><td>思科</td><td>IT</td><td>某某某项目</td><td>张朝钿</td><td>审核一</td><td>国家级</td><td>2015-10-29</td></tr>".ToString();
            return Json(htmlstr, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getSearch_Schedule2()
        {
            string keyword_type = Request.Form["updownorder1"];
            string keyword = Request.Form["updownorder2"];//项目级别
            // string tbody = "<tr><td>1</td><td>思科</td><td>IT</td><td>某某某项目</td><td>张朝钿</td><td>审核一</td><td>国家级</td><td>2015-10-29</td></tr>".ToString();
            List<Model.Project_Schedule> project = BLL.ProjectServer.getSchedule();
            project = BLL.ProjectServer.getSchedule();
            project = BLL.ProjectServer.getSearchResult2(project, keyword_type, keyword);
            string htmlstr = BLL.ProjectServer.getJsonHtml(project);
            return Json(htmlstr, JsonRequestBehavior.AllowGet);
        }





            #region 朝钿的代码
        [Authorize]
        public ActionResult Project_Schedule_Each(int ID)
        {
            ViewBag.title = TempData["belongs_name"];
            ViewBag.user = User.Identity.Name;
            Model.User userInfo = (Model.User)Session["userinfo"];
            ViewBag.UserInfo = userInfo;

            BLL.EachProjectServer server = new BLL.EachProjectServer();
            ViewBag.eachschedule = server.GetEachSchedule(ID);
            ViewBag.members = server.GetMember(ID);
            ViewBag.tasks = server.GetProject_task(ID);
            ViewBag.per = server.Get_Schedule_Per(ID);

            return View("Project_Schedule_Each");
        }
        [Authorize]

        [Authorize]
        public ActionResult Personal_Project(string keyword = "",int pageIndex=1)
        {
            ViewBag.title = TempData["belongs_name"];
            ViewBag.user = User.Identity.Name;
            Model.User userInfo = (Model.User)Session["userinfo"];
            ViewBag.userinfo = userInfo;
            BLL.PersonalProjectServer server = new BLL.PersonalProjectServer();
            ViewBag.user = server.GetUserInfo(userInfo.ID);
            List<Model.EachSchedule> project= server.GetUserProject(userInfo.ID, keyword);
            PagingHelper<EachSchedule> StudentPaging = new PagingHelper<EachSchedule>(3, project);
            StudentPaging.PageIndex = pageIndex;//指定当前页
            ViewBag.project = StudentPaging;
            return View("Personal_Project");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateUserPwd(FormCollection formvalues)
        {
            Model.User userInfo = (Model.User)Session["userinfo"];
            string ID = userInfo.ID;
            string old_pwd = formvalues["OldPwd"];
            string new_pwd = formvalues["NewPwd"];
            BLL.PersonalProjectServer server = new BLL.PersonalProjectServer();
            char result = server.UpdateUserPwd(ID, old_pwd, new_pwd);
            return Json(result);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateUserInfo(FormCollection formvalues)
        {
            Model.User userInfo = (Model.User)Session["userinfo"];
            string ID = userInfo.ID;
            string nickname = formvalues["nickname"];
            char sex = Convert.ToChar(formvalues["sex"]);
            string position = formvalues["position"];
            string phone = formvalues["phone"];
            string email = formvalues["email"];
            BLL.PersonalProjectServer server = new BLL.PersonalProjectServer();
            Boolean result = server.UpdateUseInfo(nickname, sex, position, phone, email, ID);
            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReturnProjectComment()
        {
            int project_id = Convert.ToInt32(Request.Params["project_id"]);
            BLL.PersonalProjectServer server = new BLL.PersonalProjectServer();
            Comment comm = server.SelectProjectComment(project_id);
            return Json(comm.comment);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ReturnTasksState(int ID)
        {
            //int newID = Convert.ToInt32(ID);
            BLL.PersonalProjectServer server = new BLL.PersonalProjectServer();
            List<Project_Task> tasks = server.SelecTasksState(ID);
            //List<Project_Task> taskss = new List<Project_Task>();
            //Project_Task task1 = new Project_Task();
            //task1.ID = 1;
            //task1.leader = "aaa";
            //Project_Task task2 = new Project_Task();
            //task2.ID = 2;
            //task2.leader = "bbb";
            //taskss.Add(task1);
            //taskss.Add(task2);
            return Json(tasks);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdataTaskState(FormCollection formvalues)
        {
            BLL.PersonalProjectServer server = new BLL.PersonalProjectServer();
            int sum = 0;
            int project_id = Convert.ToInt32(formvalues["Task_project_id"]);
            foreach (var key in formvalues.Keys)
            {
                string str_key = key.ToString();
                if (str_key != "Task_project_id")
                {
                    int ID = Convert.ToInt32(str_key);
                    char state = Convert.ToChar(formvalues[str_key]);
                    Boolean result = server.UpdateTaskState(ID, project_id, state);
                    if (result) sum++;
                }
            }
            return Json(sum);
        } 
        //更新材料
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateProjectFile()
        {
            BLL.PersonalProjectServer server = new BLL.PersonalProjectServer();
            int project_id = Convert.ToInt32(Request.Params["project_id"]);
            string project_name = Request.Params["project_name"];
            HttpPostedFileBase paper_file=Request.Files["paper_file"];
            HttpPostedFileBase report_file = Request.Files["report_file"];
            HttpPostedFileBase whole_file = Request.Files["whole_file"];

            short num = 0;
            if (report_file.ContentLength > 0)
            {
                string report_file_path = "~/Upload/" + project_id + "/" + "report/" + report_file.FileName;
                string old_report_file_path = server.SelectEachFile(project_id, "report_file");
                if (System.IO.File.Exists(old_report_file_path)) System.IO.File.Delete(old_report_file_path);
                if (server.UpdateEachFile(project_id, "report_file", report_file_path))
                {
                    Common.UpLoadServer.UploadFile(project_id, report_file, "report");
                    num++;
                }
            }

            if (paper_file.ContentLength > 0)
            {
                string paper_file_path = "~/Upload/" + project_id + "/" + "report/" + paper_file.FileName;
                string old_paper_file_path = server.SelectEachFile(project_id, "paper_file");
                if (System.IO.File.Exists(old_paper_file_path)) System.IO.File.Delete(old_paper_file_path);
                if (server.UpdateEachFile(project_id, "paper_file", paper_file_path))
                {
                    Common.UpLoadServer.UploadFile(project_id, report_file, "paper");
                    num++;
                }
            }

            if (whole_file.ContentLength > 0)
            {
                string whole_file_path = "~/Upload/" + project_id + "/" + "report/" + whole_file.FileName;
                string old_whole_file_path = server.SelectEachFile(project_id, "whole_pack_file");
                if (System.IO.File.Exists(old_whole_file_path)) System.IO.File.Delete(old_whole_file_path);
                if (server.UpdateEachFile(project_id, "whole_pack_file", whole_file_path))
                {
                    Common.UpLoadServer.UploadFile(project_id, whole_file, "wholefile");
                    num++;
                }
            }

            if (num > 0) server.UpdateProjectState(project_id);
            TempData["message"] = project_name + "更新了" + num + "个文件材料";
            return RedirectToAction("Personal_Project");

        }

        //取消申报
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult DeleteProject()
        {
            int project_id = Convert.ToInt32(Request.Params["project_id"]);
            string project_name = Request.Params["project_name"];
            BLL.PersonalProjectServer server = new BLL.PersonalProjectServer();
           
            string old_whole_file_path = server.SelectEachFile(project_id, "whole_pack_file");
            string old_paper_file_path = server.SelectEachFile(project_id, "paper_file");
            string old_report_file_path = server.SelectEachFile(project_id, "report_file");
            if (System.IO.File.Exists(old_whole_file_path)) System.IO.File.Delete(old_whole_file_path);
            if (System.IO.File.Exists(old_paper_file_path)) System.IO.File.Delete(old_paper_file_path);
            if (System.IO.File.Exists(old_report_file_path)) System.IO.File.Delete(old_report_file_path);
            Boolean result = server.DeleteProject(project_id);
            if (result) TempData["delete_info"] = project_name+"项目取消申报成功!";
            else TempData["delete_info"] = project_name+"项目取消申报失败!";
            return RedirectToAction("Personal_Project");
        }

        //此处开始是个人项目及信息点进去的详情页
        [Authorize]
        public ActionResult Personal_Project_Disabled(string ID)
        {
            ViewBag.title = TempData["belongs_name"];
            ViewBag.user = User.Identity.Name;
            Model.User userInfo = (Model.User)Session["userinfo"];
            ViewBag.userinfo = userInfo;
            int project_id = Convert.ToInt32(ID);
            BLL.EachProjectServer server = new BLL.EachProjectServer();
            ViewBag.eachschedule = server.GetEachSchedule(project_id);
            ViewBag.members = server.GetMember(project_id);
            ViewBag.tasks = server.GetProject_task(project_id);

            BLL.PersonalProjectServer server1 = new BLL.PersonalProjectServer();
            Comment comm = server1.SelectProjectComment(project_id);
            ViewBag.comment = comm;




            return View("Personal_Project_Disabled");
        }
        //文件下载
        [HttpGet]
        public void DownloadFile()
        {
            int project_id = Convert.ToInt32(RouteData.Values["id"]);
            string File_Type = RouteData.Values["plus"].ToString();
            BLL.PersonalProjectServer server = new BLL.PersonalProjectServer();
            string File_Path = server.SelectEachFile(project_id, File_Type);
            string[] File_Path_Array= File_Path.Split('/');
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
            Common.DownloadServer.WriteFile(project_id,file_typee,File_Path_Array[4]);
        }
        #endregion
         
    }
}

