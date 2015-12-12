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
    public class PersonalProjectDAL1
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


        #region 取出某个项目的文件材料
        public string SelectEachFile(int ID,string File_Type)
        {
            SqlParameter[] para = { new SqlParameter("@ID", SqlDbType.Int) };
            para[0].Value = ID;
            string sql = "";
            switch (File_Type)
            {
                case "report_file":
                    sql = "select report_file from Project where ID = @ID";
                    break;
                case "paper_file":
                    sql = "select paper_file from Project where ID = @ID";
                    break;
                case "whole_pack_file":
                    sql = "select whole_pack_file from Project where ID = @ID";
                    break;

            }
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, para);

            string File_Path = da.Rows[0][File_Type].ToString();
            return File_Path;
        }
        #endregion

        #region 修改某个项目的文件材料并且修改文件材料了顺便级别回退到上一级
        public Boolean UpdateEachFile(int ID, string File_Type, string File_Path)
        {
            Boolean result = false;
            SqlParameter[] para = { 
                                      new SqlParameter("@ID", SqlDbType.Int),
                                      new SqlParameter("@File_Path", SqlDbType.NVarChar,256),

                                  };
            para[0].Value = ID;
            para[1].Value = File_Path;

            string sql = "";
            switch (File_Type)
            {
                case "report_file":
                    sql = "update Project set report_file = @File_Path where ID = @ID";
                    break;
                case "paper_file":
                    sql = "update Project set paper_file = @File_Path where ID = @ID";
                    break;
                case "whole_pack_file":
                    sql = "update Project set whole_pack_file = @File_Path where ID = @ID";
                    break;
            }
            int Update_result = SqlHelper.ExecuteNonquery(sql, CommandType.Text, para);
            if (Update_result != 0) result = true;
            return result;
        }

        //修改文件材料了顺便级别回退到上一级
        public Boolean UpdateProjectState(int ID)
        {
            Boolean result = false;
            SqlParameter[] para = { 
                                      new SqlParameter("@ID", SqlDbType.Int),

                                  };
            para[0].Value = ID;
            //字符降级
            string select_sql = "select state from Project where ID = @ID";
            DataTable da = SqlHelper.GetTable(select_sql, CommandType.Text, para);
            char state = Convert.ToChar(da.Rows[0]["state"]);
            char new_state = Convert.ToChar(state - 1);

            SqlParameter[] para1 = { 
                                      new SqlParameter("@ID", SqlDbType.Int),
                                      new SqlParameter("@state", SqlDbType.Char),

                                  };
            para1[0].Value = ID;
            para1[1].Value = new_state;
            string update_sql = "Update Project set state = @state where ID=@ID";
            int Update_result = SqlHelper.ExecuteNonquery(update_sql, CommandType.Text, para1);
            if (Update_result != 0) result = true;
            return result;

        }
        #endregion


        #region 取消申报也即删除该项目
        public Boolean DeleteProject(int ID)
        {
            Boolean result = false;
            SqlParameter[] para1 = { 
                                      new SqlParameter("@ID", SqlDbType.Int),

                                  };
            para1[0].Value = ID;
            string sql1 = "Delete from Leader where Leader.project_id = @ID";
            int delete_update1 = SqlHelper.ExecuteNonquery(sql1, CommandType.Text, para1);

            SqlParameter[] para2 = { 
                                      new SqlParameter("@ID", SqlDbType.Int),

                                  };
            para2[0].Value = ID;
            string sql2 = "Delete from Task where Task.project_id = @ID";
            int delete_update2 = SqlHelper.ExecuteNonquery(sql2, CommandType.Text, para2);

            SqlParameter[] para3 = { 
                                      new SqlParameter("@ID", SqlDbType.Int),

                                  };
            para3[0].Value = ID;
            string sql3 = "Delete from Member where Member.project_id = @ID";
            int delete_update3 = SqlHelper.ExecuteNonquery(sql3, CommandType.Text, para3);

            SqlParameter[] para4 = { 
                                      new SqlParameter("@ID", SqlDbType.Int),

                                  };
            para4[0].Value = ID;
            string sql4 = "Delete from Comment where Comment.project_id = @ID";
            int delete_update4 = SqlHelper.ExecuteNonquery(sql4, CommandType.Text, para4);

            SqlParameter[] para5 = { 
                                      new SqlParameter("@ID", SqlDbType.Int),

                                  };
            para5[0].Value = ID;
            string sql5 = "Delete from Project Where ID = @ID";
            int delete_update5 = SqlHelper.ExecuteNonquery(sql5, CommandType.Text, para5);

            if (delete_update1 != 0 && delete_update2 != 0 && delete_update3 != 0 && delete_update4!= 0 && delete_update5 != 0) result = true;
            return result;

        }
        #endregion


        #region 
        #endregion
    }
}
