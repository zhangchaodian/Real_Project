using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL
{
    public class ProjectServer
    {
        /*
        public Model.Project_pass getProject_pass(){
        }*/

        
        #region 获取搜索数据模型1 首页
        public static List<Model.Project_Achievement> getSearchResult(List<Model.Project_Achievement> projectInfo, string str2, string str4, string str5)
        {
            if (str4.Equals("p_id"))
            {
                switch (str5)
                {
                    case "asc":
                        projectInfo = (from rerult in projectInfo where (rerult.now_level.Equals(str2)) orderby rerult.p_id ascending select rerult).ToList();
                        break;
                    case "desc":
                        projectInfo = (from rerult in projectInfo where (rerult.now_level.Equals(str2)) orderby rerult.p_id descending select rerult).ToList();
                        break;
                    default:
                        projectInfo = (from rerult in projectInfo  orderby rerult.p_id descending select rerult).ToList();
                        break;



                }
            }
            else
            {
                switch (str5)
                {
                    case "asc":
                        projectInfo = (from rerult in projectInfo where (rerult.now_level.Equals(str2)) orderby rerult.f_time ascending select rerult).ToList();
                        break;
                    case "desc":
                        projectInfo = (from rerult in projectInfo where (rerult.now_level.Equals(str2)) orderby rerult.f_time descending select rerult).ToList();
                        break;
                    default:
                        projectInfo = (from rerult in projectInfo orderby rerult.p_id descending select rerult).ToList();
                        break;


                }
            }
          
            return projectInfo;
        }
        #endregion

        #region 获取搜索json html 首页
        public static string getJsonHtml(List<Model.Project_Achievement> projectInfo)
        {
            StringBuilder htmlStr=new StringBuilder();
          
            foreach(Model.Project_Achievement project in projectInfo){
              
                htmlStr.Append("<tr>");
                htmlStr.Append("<td>"+project.p_id+"</td>");
                htmlStr.Append("<td>"+project.p_type+"</td>");
                htmlStr.Append("<td>" + project.p_name + "</td>");
                htmlStr.Append("<td>" + project.declarant + "</td>");
                htmlStr.Append("<td>"+project.leader.nickname+"</td>");
                htmlStr.Append("<td>" + project.now_level + "</td>");
                htmlStr.Append("<td>" +  project.f_time+ "</td>");
                htmlStr.Append(" <td><button type=\"button\" class=\"btn btn-embossed btn-primary btn-xs\" data-toggle=\"modal\" data-target=\"#myModal_ShowDetail\">详情</button></td>");
                htmlStr.Append("</tr>");
            }
           
            
           // htmlStr.Append("<tr><td>1</td><td>思科</td><td>IT</td><td>某某某项目</td><td>张朝钿</td><td>审核一</td><td>国家级</td><td>2015-10-29</td></tr>");
            return htmlStr.ToString();
        }
        #endregion
        
        #region 获取搜索数据模型2 首页
        public static List<Model.Project_Achievement> getSearchResult2(List<Model.Project_Achievement> projectInfo, string keyword_type, string keyword)
        {
            switch (keyword_type)
            {
                case "p_type":
                 projectInfo=(from result in projectInfo where result.p_type.Contains(keyword) select result).ToList();
                 break;
                case "p_name":
                 projectInfo=(from result in projectInfo where result.p_name.Contains(keyword) select result).ToList();
                 break;
                case "st_name":
                   projectInfo=(from result in projectInfo where result.leader.nickname.Contains(keyword) select result).ToList();
                 break;
            }
            return projectInfo;
        }
        #endregion




        #region 获取项目实体

        public static int getProjectNum()
        {
          DAL.projectinfoDAL p= new DAL.projectinfoDAL();
          return p.getProjectNum();
        }

        public static List<Model.Project_Achievement> getProjectAchievement(){
            DAL.projectinfoDAL dal = new DAL.projectinfoDAL();
            return dal.getProject_Achievement();
        }

        public static List<Model.Project_Schedule> getSchedule()
        {
            DAL.projectinfoDAL dal = new DAL.projectinfoDAL();
            return dal.getProject_Schedule();
        }

        public static string getDetail(List<Model.Project_Achievement> project,int ID)
        {
           
            project = (from result in project where result.p_id ==ID select result).ToList();
            Model.Project_Achievement p = project[0];
            string str0=p.p_name;
            string str1=p.p_type;
            string str2=p.leader.nickname;
            string str3=string.Format("姓名:{0}, 手机号码:{1}, 邮箱:{2}",p.leader.nickname,p.leader.phone,p.leader.email);
            string str4=null;
            foreach(Model.Member m in p.member ){
                str4+=m.nickname+" ";
            }
            string str5=p.now_level;
            string str6=p.create_time;
            string str7=p.f_time;
            string htmlstr = string.Format( "{{ \"p_name\": \"{0}\", \"type\": \"{1}\", \"st_name\": \"{2}\", \"st_detail\": \"{3}\", \"group\": \"{4}\", \"rank\": \"{5}\", \"s_time\": \"{6}\", \"f_time\": \"{7}\"}}",str0,str1,str2,str3,str4,str5,str6,str7);

            return htmlstr;
        }

        #endregion

        #region 进度页面相关函数

        public static List<Model.Project_Schedule> getSearchResult(List<Model.Project_Schedule> projectInfo, string str2, string str4, string str5)
        {
            if (str4.Equals("p_id"))
            {
                switch (str5)
                {
                    case "asc":
                        projectInfo = (from rerult in projectInfo where (rerult.target_level.Equals(str2)) orderby rerult.p_id ascending select rerult).ToList();
                        break;
                    case "desc":
                        projectInfo = (from rerult in projectInfo where (rerult.target_level.Equals(str2)) orderby rerult.p_id descending select rerult).ToList();
                        break;
                    default:
                        projectInfo = (from rerult in projectInfo orderby rerult.p_id descending select rerult).ToList();
                        break;



                }
            }
            else
            {
                switch (str5)
                {
                    case "asc":
                        projectInfo = (from rerult in projectInfo where (rerult.target_level.Equals(str2)) orderby rerult.f_time ascending select rerult).ToList();
                        break;
                    case "desc":
                        projectInfo = (from rerult in projectInfo where (rerult.target_level.Equals(str2)) orderby rerult.f_time descending select rerult).ToList();
                        break;
                    default:
                        projectInfo = (from rerult in projectInfo orderby rerult.p_id descending select rerult).ToList();
                        break;


                }
            }
            
            return projectInfo;
        }

        public static string getJsonHtml(List<Model.Project_Schedule> projectInfo)
        {
            StringBuilder htmlStr = new StringBuilder();
            
            foreach (Model.Project_Schedule project in projectInfo)
            {

                htmlStr.Append("<tr class=\"table_tbody_tr\"><input type=\"hidden\" name=\"stime\" value=\""+project.s_time+"\"><input type=\"hidden\" name=\"etime\" value=\""+project.f_time+"'\"><input type=\"hidden\" name=\"per_num\" value=\""+project.schedule+"\">");
                htmlStr.Append("<td>" + project.p_id + "</td>");
                htmlStr.Append("<td>" + project.p_name + "</td>");
                htmlStr.Append("<td>" + project.p_type + "</td>");
                htmlStr.Append("<td><div class=\"progress\"><div class=\"progress-bar progress-bar-striped active progress_move\" role=\"progressbar\" aria-valuenow=\"2\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"min-width: 2em; width: 2em;\">");
                htmlStr.Append(project.schedule+"%");
                htmlStr.Append(" </div></div></td>");
                htmlStr.Append("<td>" + project.declarant + "</td>");
                htmlStr.Append("<td>" + project.leader.nickname + "</td>");
                htmlStr.Append("<td>" + project.target_level + "</td>");
                htmlStr.Append("<td>" + project.create_time + "</td>");
                htmlStr.Append(" <td><button class=\"btn btn-embossed btn-primary btn-sm\">详情</button></td>");
                htmlStr.Append("</tr>");
            }
            

            //htmlStr.Append("<tr><td>1</td><td>思科</td><td>IT</td><td>某某某项目</td><td>张朝钿</td><td>审核一</td><td>国家级</td><td>2015-10-29</td></tr>");
            return htmlStr.ToString();
        }

         public static List<Model.Project_Schedule> getSearchResult2(List<Model.Project_Schedule> projectInfo, string keyword_type, string keyword)
        {
            switch (keyword_type)
            {
                case "p_type":
                 projectInfo=(from result in projectInfo where result.p_type.Contains(keyword) select result).ToList();
                 break;
                case "p_name":
                 projectInfo=(from result in projectInfo where result.p_name.Contains(keyword) select result).ToList();
                 break;
                case "st_name":
                   projectInfo=(from result in projectInfo where result.leader.nickname.Contains(keyword) select result).ToList();
                    break;
                 case "declarant":
                   projectInfo=(from result in projectInfo where result.declarant.Contains(keyword) select result).ToList();
                 break;
            }
            return projectInfo;
        }

        #endregion

    }
}
       