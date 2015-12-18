using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace ProjectManager.DAL
{
    public class projectinfoDAL
    {
        public int getProjectNum()
        {
            string sql = "select * from [Project];";
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, null);

            return da.Rows.Count;
        }
        #region 首页实体获取
        public List<Model.Project_Achievement> getProject_Achievement(string belongs)
        {
            List<Model.Project_Achievement> project = new List<Model.Project_Achievement>();
            SqlParameter[] pars ={ 
                                 new SqlParameter("@belongs",SqlDbType.NVarChar,1),
                               
                                 };
            pars[0].Value = belongs;
            DataTable da = SqlHelper.GetTable("getProject_Achievement", CommandType.StoredProcedure,pars);
            Model.Project_Achievement p = null;
            foreach (DataRow row in da.Rows)
            {
                p = new Model.Project_Achievement();
                p.p_id = Convert.ToInt16(row["ID"]);
                p.belongs = row["belongs"].ToString();
                p.p_name = row["name"].ToString();
                p.p_type = row["type"].ToString();
                // p.p_type = ConvertToLevel("b");
                p.now_level = ConvertToLevel(row["now_level"].ToString());
                p.create_time = Convert.ToDateTime(row["create_time"]).ToString("yyyy-MM-dd");
                p.f_time = Convert.ToDateTime(row["end_time"]).ToString("yyyy-MM-dd"); ;
                p.declarant = getDeclarant(p.p_id);
                p.leader = getLeader(p.p_id);
                p.member = getMember(p.p_id);
                project.Add(p);
            }
            return project;


        }
        public Model.Leader getLeader(int p_id)
        {

            SqlParameter[] pars ={ 
                                 new SqlParameter("@p_id",SqlDbType.Int,32),
                               
                                 };
            pars[0].Value = p_id;
            DataTable da = SqlHelper.GetTable("getLeader", CommandType.StoredProcedure, pars);

            Model.Leader leader = new Model.Leader();
            if (da.Rows.Count > 0)
            {
                DataRow r = da.Rows[0];
                leader.id = Convert.ToInt16(r["project_id"]);
                leader.nickname = r["nickname"].ToString();
                leader.phone = r["phone"].ToString();
                leader.email = r["email"].ToString();
            }
            return leader;
        }

        public List<Model.Member> getMember(int p_id)
        {
            SqlParameter[] pars ={ 
                                 new SqlParameter("@p_id",SqlDbType.Int,32),
                               
                                 };
            pars[0].Value = p_id;
            DataTable da = SqlHelper.GetTable("getMember", CommandType.StoredProcedure, pars);
            List<Model.Member> member = new List<Model.Member>();
            Model.Member m = null;
            foreach (DataRow row in da.Rows)
            {
                m = new Model.Member();
                m.project_id = Convert.ToInt16(row["project_id"]);
                m.nickname = row["nickname"].ToString();
                member.Add(m);
            }
            return member;
        }
        public string getDeclarant(int p_id)
        {
            SqlParameter[] pars ={ 
                                 new SqlParameter("@p_id",SqlDbType.Int,32),
                               
                                 };
            pars[0].Value = p_id;
            DataTable da = SqlHelper.GetTable("getDeclarant", CommandType.StoredProcedure, pars);
            if (da.Rows.Count > 0)
            {
                DataRow r = da.Rows[0];
                return r["nickname"].ToString();
            }
            else
            {
                return null;
            }
        }
        public static string ConvertToLevel(string source)
        {
            switch (source)
            {
                case "a":
                    return "校级";

                case "b":
                    return "市级";

                case "c":
                    return "省级";

                case "d":
                    return "国家级";

                default:
                    return "校级";
            }
        }
        #endregion


        #region 进度模型获取
        public List<Model.Project_Schedule> getProject_Schedule(string belongs)
        {
            List<Model.Project_Schedule> project = new List<Model.Project_Schedule>();
            SqlParameter[] pars ={ 
                                 new SqlParameter("@belongs",SqlDbType.NVarChar,1),
                               
                                 };
            pars[0].Value = belongs;
            DataTable da = SqlHelper.GetTable("getProject_Schedule", CommandType.StoredProcedure, pars);
            Model.Project_Schedule p = null;
            foreach (DataRow row in da.Rows)
            {
                p = new Model.Project_Schedule();
                p.p_id = Convert.ToInt16(row["ID"]);
                p.p_name = row["name"].ToString();
                p.belongs = row["belongs"].ToString();
                p.p_type = row["type"].ToString();
                // p.p_type = ConvertToLevel("b");
                p.target_level = ConvertToLevel(row["target_level"].ToString());
                p.create_time = Convert.ToDateTime(row["create_time"]).ToString("yyyy-MM-dd"); ;
                p.f_time = Convert.ToDateTime(row["end_time"]).ToString("yyyy-MM-dd"); ;
                p.declarant = getDeclarant(p.p_id);
                p.schedule = getSchedule(p.p_id);
                p.leader = getLeader(p.p_id);
                p.s_time = Convert.ToDateTime(row["start_time"]).ToString("yyyy-MM-dd");
                project.Add(p);
            }
            return project;


        }
        public string getSchedule(int p_id)
        {
            string schedule;
            schedule = (getFTaskNum(p_id) / getTaskNum(p_id) * 100).ToString();
            return schedule;
        }
        public double getTaskNum(int p_id)
        {
            SqlParameter[] pars ={ 
                                 new SqlParameter("@p_id",SqlDbType.Int,32),
                               
                                 };
            pars[0].Value = p_id;
            DataTable da = SqlHelper.GetTable("getTaskNum", CommandType.StoredProcedure, pars);
            double total_num;
            DataRow row = da.Rows[0];
            total_num = Convert.ToDouble(row["total_num"]);
            return total_num;

        }
        public double getFTaskNum(int p_id)
        {
            SqlParameter[] pars ={ 
                                 new SqlParameter("@p_id",SqlDbType.Int,32),
                               
                                 };
            pars[0].Value = p_id;
            DataTable da = SqlHelper.GetTable("getFTaskNum", CommandType.StoredProcedure, pars);
            double f_num;
            DataRow row = da.Rows[0];
            f_num = Convert.ToDouble(row["f_num"]);
            return f_num;
        }
        #endregion


        #region 管理员管理项目页面
        public List<Model.Project> getAdminProject(string belongs)
        {
            List<Model.Project> project = new List<Model.Project>();

            SqlParameter[] pars1 ={ 
                                 new SqlParameter("@belongs",SqlDbType.NVarChar,1),
                               
                                 };
            pars1[0].Value =belongs;
            DataTable da = SqlHelper.GetTable("getAdminProject", CommandType.StoredProcedure, pars1);
            Model.Project p = null;


            foreach (DataRow row in da.Rows)
            {
                string sql = "select * from Task where project_id=@p_id and state='a';";
                p = new Model.Project();
                p.p_id = Convert.ToInt16(row["ID"]);
                p.p_name = row["name"].ToString();
                p.now_level = row["now_level"].ToString();
                p.target_level = row["target_level"].ToString();
                p.create_time = Convert.ToDateTime(row["create_time"]).ToString("yyyy-MM-dd");
                p.f_time = Convert.ToDateTime(row["end_time"]).ToString("yyyy-MM-dd");
                p.report_path = row["report_file"].ToString();
                p.paper_path = row["paper_file"].ToString();
                p.whole_pack_file = row["whole_pack_file"].ToString();
                p.leader = row["leader"].ToString();
                p.declarant = row["nickname"].ToString();
                p.comment = row["comment"].ToString();
                p.pass_state = row["state"].ToString();
                SqlParameter[] pars ={
             new SqlParameter("@p_id",SqlDbType.Int,32),
            
           };
                pars[0].Value = p.p_id;
                DataTable result = SqlHelper.GetTable(sql, CommandType.Text, pars);
                if (result.Rows.Count > 0)
                    p.finish_state = "未完成";
                else
                    p.finish_state = "完成";

                project.Add(p);
            }
            return project;
        }
        #endregion

        public bool UpdateComment(int p_id, string comment, string state)
        {
            string sql = "update Comment set comment=@comment where project_id=@p_id;update Project set state=@state where ID=@p_id; ";
            SqlParameter[] pars ={
                                   new SqlParameter("@p_id",SqlDbType.Int,32),
                                   new SqlParameter("@comment",SqlDbType.NVarChar,500),
                                   new SqlParameter("@state",SqlDbType.NVarChar,1),



                               };
            pars[0].Value = p_id;
            pars[1].Value = comment;
            pars[2].Value = state;
            int result = SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
            if (result > 0)
                return true;
            else
                return false;

        }


        //项目管理页面搜索1
        public List<Model.Project> selectProject(List<Model.Project> project, string now_level, string target_level, string state, string order)
        {
            switch (now_level)
            {
                case "all":
                    project = (from result in project select result).ToList();
                    break;
                case "a":
                    project = (from result in project where result.now_level.Equals("a") select result).ToList();
                    break;
                case "b":
                    project = (from result in project where result.now_level.Equals("b") select result).ToList();
                    break;
                case "c":
                    project = (from result in project where result.now_level.Equals("c") select result).ToList();
                    break;
                case "d":
                    project = (from result in project where result.now_level.Equals("d") select result).ToList();
                    break;


            }
            switch (target_level)
            {
                case "all":
                    project = (from result in project select result).ToList();
                    break;
                case "a":
                    project = (from result in project where result.target_level.Equals("a") select result).ToList();
                    break;
                case "b":
                    project = (from result in project where result.target_level.Equals("b") select result).ToList();
                    break;
                case "c":
                    project = (from result in project where result.target_level.Equals("c") select result).ToList();
                    break;
                case "d":
                    project = (from result in project where result.target_level.Equals("d") select result).ToList();
                    break;
            }
            switch (state)
            {
                case "all":
                    project = (from result in project select result).ToList();
                    break;
                case "a":
                    project = (from result in project where result.pass_state.Equals("a") select result).ToList();
                    break;
                case "b":
                    project = (from result in project where result.pass_state.Equals("b") select result).ToList();
                    break;
                case "c":
                    project = (from result in project where result.pass_state.Equals("c") select result).ToList();
                    break;
                case "d":
                    project = (from result in project where result.pass_state.Equals("d") select result).ToList();
                    break;
                case "e":
                    project = (from result in project where result.pass_state.Equals("e") select result).ToList();
                    break;
                case "f":
                    project = (from result in project where result.pass_state.Equals("f") select result).ToList();
                    break;
                case "g":
                    project = (from result in project where result.pass_state.Equals("g") select result).ToList();
                    break;
                case "h":
                    project = (from result in project where result.pass_state.Equals("h") select result).ToList();
                    break;
                case "i":
                    project = (from result in project where result.pass_state.Equals("i") select result).ToList();
                    break;
                default:
                    project = (from result in project where result.pass_state.Equals("a") select result).ToList();
                    break;
            }
            switch (order)
            {
                case "asc":
                    project = (from result in project orderby result.p_id ascending select result).ToList();
                    break;
                case "desc":
                    project = (from result in project orderby result.p_id descending select result).ToList();
                    break;
            }
            return project;
        }

        //项目管理页面搜索2
        public List<Model.Project> selectTypePorject(List<Model.Project> project, string keytype, string keyword)
        {
            switch (keytype)
            {
                case "p_name":
                    project = (from result in project where result.p_name.Contains(keyword) select result).ToList();
                    break;
                case "create_time":
                    project = (from result in project where result.create_time.Contains(keyword) select result).ToList();
                    break;
                case "declarant":
                    project = (from result in project where result.declarant.Contains(keyword) select result).ToList();
                    break;
                case "leader":
                    project = (from result in project where result.leader.Contains(keyword) select result).ToList();
                    break;
            }
            return project;
        }

    }
}
