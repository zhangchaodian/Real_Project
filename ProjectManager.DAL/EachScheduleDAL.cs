using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Model;
using System.Data;
using System.Data.SqlClient;
//using System.Reflection;

namespace ProjectManager.DAL
{
    public class EachScheduleDAL
    {

        #region 获取每个项目进度页面所需的三个实体:基本信息与成员与任务,根据存储过程名来判断调用哪个实体
        public EachSchedule GetEachSchedule(int ID)
        {

            SqlParameter[] para1 = { new SqlParameter("@ID", SqlDbType.Int) };
            para1[0].Value = ID;


            DataTable da1 = SqlHelper.GetTable("SelectEachProject", CommandType.StoredProcedure, para1);
            EachSchedule model1 = new EachSchedule();
            model1 = LoadEachEntity(model1, da1.Rows[0]);

            return model1;
        }

        public List<Member> GetMember(int ID)
        {
            List<Member> models = new List<Member>();

            SqlParameter[] para2 = { new SqlParameter("@ID", SqlDbType.Int) };
            para2[0].Value = ID;

            DataTable da2 = SqlHelper.GetTable("selectEachProjectMembers", CommandType.StoredProcedure, para2);
            foreach (DataRow row in da2.Rows)
            {
                Member model2 = new Member();
                model2 = LoadMemberEntity(model2, row);
                models.Add(model2);
            }

            return models;
        }

        public List<Project_Task> GetProjectTask(int ID)
        {
            List<Project_Task> models = new List<Project_Task>();

            SqlParameter[] para3 = { new SqlParameter("@ID", SqlDbType.Int) };
            para3[0].Value = ID;


            DataTable da3 = SqlHelper.GetTable("SelectEachProjectTasks", CommandType.StoredProcedure, para3);

            foreach (DataRow row in da3.Rows)
            {
                Project_Task model3 = new Project_Task();
                model3 = LoadTaskEntity(model3, row);
                models.Add(model3);
            }


            return models;
        }



        #endregion




        #region 三个实体实例化注入属性值方法
        public EachSchedule LoadEachEntity(EachSchedule EachInfo, DataRow row)
        {
            EachInfo.name = row["name"].ToString();
            EachInfo.now_level = row["now_level"].ToString();
            EachInfo.target_level = row["target_level"].ToString();
            EachInfo.create_time = row["create_time"].ToString();
            EachInfo.start_time = row["start_time"].ToString();
            EachInfo.end_time = row["end_time"].ToString();
            EachInfo.User_nickname = row["User_nickname"].ToString();
            EachInfo.Leader_nickname = row["Leader_nickname"].ToString();
            EachInfo.Leader_phone = row["Leader_phone"].ToString();
            EachInfo.Leader_email = row["Leader_email"].ToString();
            EachInfo.start_y = row["start_y"].ToString();
            EachInfo.start_m = row["start_m"].ToString();
            EachInfo.start_d = row["start_d"].ToString();
            EachInfo.end_y = row["end_y"].ToString();
            EachInfo.end_m = row["end_m"].ToString();
            EachInfo.end_d = row["end_d"].ToString();
            return EachInfo;
        }


        public Member LoadMemberEntity(Member member, DataRow row)
        {
            member.nickname = row["nickname"].ToString();
            return member;

        }

        public Project_Task LoadTaskEntity(Project_Task project_task, DataRow row)
        {
            project_task.ID = Convert.ToInt16(row["ID"]);
            project_task.leader = row["leader"].ToString();
            project_task.start_y = row["start_y"].ToString();
            project_task.start_m = row["start_m"].ToString();
            project_task.start_d = row["start_d"].ToString();
            project_task.end_y = row["end_y"].ToString();
            project_task.end_m = row["end_m"].ToString();
            project_task.end_d = row["end_d"].ToString();
            project_task.task = row["task"].ToString();
            project_task.state = row["state"].ToString();
            return project_task;
        }
        #endregion

    }
}
