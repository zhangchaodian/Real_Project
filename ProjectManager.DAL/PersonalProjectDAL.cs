using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProjectManager.DAL
{
    public class PersonalProjectDAL
    {
        #region 获取个人项目及信息页面的用户个人信息
        public User GetUserInfo(string ID)
        {
            SqlParameter[] para = { new SqlParameter("@ID", SqlDbType.NVarChar, 30) };
            para[0].Value = ID;
            DataTable da = SqlHelper.GetTable("SelectEachUserInfo", CommandType.StoredProcedure, para);
            User user = new User();
            user = this.LoadUserInfoEntity(user, da.Rows[0]);
            return user;

        }

        public User LoadUserInfoEntity(User user, DataRow row)
        {
            user.ID = row["ID"].ToString();
            user.nickname = row["nickname"].ToString();
            user.sex = row["sex"].ToString();
            user.position = row["position"].ToString();
            user.email = row["email"].ToString();
            user.phone = row["phone"].ToString();
            user.type = row["type"].ToString();
            return user;
        }
        #endregion



        #region 获取个人项目及信息页面的所有个人申报项目
        public List<EachSchedule> GetUserProject(string ID, string name)
        {
            List<EachSchedule> projects = new List<EachSchedule>();
            SqlParameter[] para = { new SqlParameter("@ID", SqlDbType.NVarChar, 30),
                                      new SqlParameter("@name", SqlDbType.NVarChar,256)
                                  };
            para[0].Value = ID;
            para[1].Value = name;
            DataTable da = SqlHelper.GetTable("SelectUserProject", CommandType.StoredProcedure, para);
            foreach (DataRow row in da.Rows)
            {
                EachSchedule project = new EachSchedule();
                project = this.LoadUserProjectEntity(project, row);
                projects.Add(project);
            }
            return projects;

        }
        public EachSchedule LoadUserProjectEntity(EachSchedule project, DataRow row)
        {
            project.ID = Convert.ToInt16(row["ID"]);
            project.type = row["type"].ToString();
            project.name = row["name"].ToString();
            project.now_level = row["now_level"].ToString();
            project.target_level = row["target_level"].ToString();
            project.create_time = row["create_time"].ToString();
            project.start_time = row["start_time"].ToString();
            project.end_time = row["end_time"].ToString();
            project.state = row["state"].ToString();
            project.Leader_nickname = row["Leader_nickname"].ToString();
            return project;
        }
        #endregion

        #region 个人信息页面修改密码代码
        public char CheckUserOldPwd(string ID, string pwd)
        {
            SqlParameter[] para = { new SqlParameter("@ID", SqlDbType.NVarChar, 30) };
            para[0].Value = ID;
            string sql = "select pwd from UserInfo where ID = @ID";
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, para);
            string right_pwd = da.Rows[0]["pwd"].ToString();
            char result = 'b';
            if (pwd == right_pwd)
            {
                result = 'a';
            }
            return result;
        }
        public char UpdateUserPwd(string ID, string old_pwd, string new_pwd)
        {
            char result = 'b'; //b表示输入的旧密码与数据库密码匹配错误
            if (this.CheckUserOldPwd(ID, old_pwd) == 'a')
            {
                SqlParameter[] para = {
                                          new SqlParameter("@ID", SqlDbType.NVarChar, 30),
                                          new SqlParameter("@pwd", SqlDbType.NVarChar, 100) 
                                      };
                para[0].Value = ID;
                para[1].Value = new_pwd;
                int update_result = SqlHelper.ExecuteNonquery("UpdateUserPwd", CommandType.StoredProcedure, para);
                if (update_result != 0)
                {
                    result = 'a'; //a表示密码修改成功
                }
                else { result = 'c'; } //c表示虽然匹配成功，但是修改数据库出现错误
            }
            return result;


        }
        #endregion


        #region 个人信息页面编辑个人信息代码
        public Boolean UpdateUseInfo(string nickname, char sex, string position, string phone, string email, string ID)
        {
            Boolean result = false; ;

            SqlParameter[] para = {
                                          new SqlParameter("@ID", SqlDbType.NVarChar, 30),
                                          new SqlParameter("@nickname", SqlDbType.NVarChar, 20),
                                          new SqlParameter("@sex", SqlDbType.Char, 2),
                                          new SqlParameter("@position", SqlDbType.NVarChar, 20),
                                          new SqlParameter("@phone", SqlDbType.Char, 11),
                                          new SqlParameter("@email", SqlDbType.NVarChar, 30),
                                      };
            para[0].Value = ID;
            para[1].Value = nickname;
            para[2].Value = sex;
            para[3].Value = position;
            para[4].Value = phone;
            para[5].Value = email;
            int update_result = SqlHelper.ExecuteNonquery("UpdatePersonalInfo", CommandType.StoredProcedure, para);
            if (update_result != 0)
            {
                result = true;
            }
            return result;
        }
        #endregion


        #region 获取某个项目的所有状态
        public List<Project_Task> SelecTasksState(int ID)
        {
            List<Project_Task> tasks = new List<Project_Task>();
            SqlParameter[] para = { new SqlParameter("@ID", SqlDbType.Int) };
            para[0].Value = ID;

            string sql = "select task,state,ID from Task where project_id=@ID";

            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, para);
            foreach (DataRow row in da.Rows)
            {
                Project_Task task = new Project_Task();
                task = this.LoadTaskEntity(task, row);
                tasks.Add(task);
            }
            return tasks;

        }

        public Project_Task LoadTaskEntity(Project_Task task, DataRow row)
        {
            task.task = row["task"].ToString();
            task.state = row["state"].ToString();
            task.ID = Convert.ToInt32(row["ID"].ToString());
            return task;
        }
        #endregion


        #region 修改某个项目的任务完成状态
        public Boolean UpdateTaskState(int ID, int project_id, char state)
        {
            SqlParameter[] para = {
                                      new SqlParameter("@ID", SqlDbType.Int),
                                      new SqlParameter("@project_id", SqlDbType.Int),
                                      new SqlParameter("@state", SqlDbType.Char)
                                  };
            para[0].Value = ID;
            para[1].Value = project_id;
            para[2].Value = state;
            int update_result = SqlHelper.ExecuteNonquery("UpdateTaskState", CommandType.StoredProcedure, para);
            Boolean result = false;
            if (update_result != 0)
            {
                result = true;
            }
            return result;
        }
        #endregion


        #region 查看每个项目当前的状态描述
        public Comment SelectProjectComment(int project_id)
        {
            SqlParameter[] para = { new SqlParameter("@project_id", SqlDbType.Int) };
            para[0].Value = project_id;

            DataTable da = SqlHelper.GetTable("SelectProjectComment", CommandType.StoredProcedure, para);
            Comment comm = new Comment();
            comm = this.LoadCommentEntity(comm, da.Rows[0]);
            return comm;
        }
        public Comment LoadCommentEntity(Comment comm, DataRow row)
        {
            comm.project_id = Convert.ToInt32(row["project_id"].ToString());
            comm.comment = row["comment"].ToString();
            return comm;
        }
        #endregion


    }
}
