using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.DAL;
using ProjectManager.Model;
namespace ProjectManager.BLL
{
    public class PersonalProjectServer
    {
        DAL.PersonalProjectDAL1 ppd = new PersonalProjectDAL1();
        #region 调用相应DAL层获取个人信息以及个人所有申报项目信息的方法

        public User GetUserInfo(string ID)
        {
            return this.ppd.GetUserInfo(ID);
        }
        public List<EachSchedule> GetUserProject(string ID, string name)
        {
            List<EachSchedule> projects = this.ppd.GetUserProject(ID, name);

            foreach (EachSchedule project in projects)
            {
                switch (project.now_level)
                {
                    case "a":
                        project.now_level = "校级";
                        break;
                    case "b":
                        project.now_level = "市级";
                        break;
                    case "c":
                        project.now_level = "省级";
                        break;
                    case "d":
                        project.now_level = "国家级";
                        break;
                }
                switch (project.target_level)
                {
                    case "a":
                        project.target_level = "校级";
                        break;
                    case "b":
                        project.target_level = "市级";
                        break;
                    case "c":
                        project.target_level = "省级";
                        break;
                    case "d":
                        project.target_level = "国家级";
                        break;
                }
            }

            return projects;
        }
        #endregion


        #region 调用DAL层修改密码和个人信息的方法
        public char UpdateUserPwd(string ID, string old_pwd, string new_pwd)
        {
            return ppd.UpdateUserPwd(ID, old_pwd, new_pwd);
        }
        public Boolean UpdateUseInfo(string nickname, char sex, string position, string phone, string email, string ID)
        {

            return ppd.UpdateUseInfo(nickname, sex, position, phone, email, ID);
        }
        #endregion

        #region 获取DAL层某个项目所有任务状态
        public List<Project_Task> SelecTasksState(int ID)
        {
            return this.ppd.SelecTasksState(ID);
        }
        #endregion


        #region 调用DAL层任务更新方法
        public Boolean UpdateTaskState(int ID, int project_id, char state)
        {
            return ppd.UpdateTaskState(ID, project_id, state);
        }
        #endregion

        #region 调用DAL选出某项目的留言方法
        public Comment SelectProjectComment(int project_id)
        {
            return ppd.SelectProjectComment(project_id);
        }
        #endregion

        #region 调用DAL取出和修改文件的方法
        public string SelectEachFile(int ID, string File_Type)
        {
            return ppd.SelectEachFile(ID, File_Type);
        }
        public Boolean UpdateEachFile(int ID, string File_Type, string File_Path)
        {
            return ppd.UpdateEachFile(ID, File_Type, File_Path);
        }
        public Boolean UpdateProjectState(int ID)
        {
            return ppd.UpdateProjectState(ID);
        }
        #endregion

        #region 调用DAL层取消申报方法
        public Boolean DeleteProject(int ID)
        {
            return ppd.DeleteProject(ID);
        }
        #endregion
    }
}
