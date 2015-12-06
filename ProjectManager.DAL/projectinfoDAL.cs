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
           DataTable da = SqlHelper.GetTable(sql, CommandType.Text,null);
   
           return da.Rows.Count;
       }
       #region 首页实体获取
       public List<Model.Project_Achievement>  getProject_Achievement(){
          List<Model.Project_Achievement> project = new List<Model.Project_Achievement>();
          DataTable da = SqlHelper.GetTable("getProject_Achievement", CommandType.StoredProcedure, null);
          Model.Project_Achievement p=null;
          foreach(DataRow row in da.Rows ){
              p = new Model.Project_Achievement();
              p.p_id=Convert.ToInt16(row["ID"]);
              p.p_name = row["name"].ToString();
              p.p_type =  row["type"].ToString();
             // p.p_type = ConvertToLevel("b");
              p.now_level =ConvertToLevel( row["now_level"].ToString());
              p.create_time = Convert.ToDateTime( row["create_time"]).ToString("yyyy-MM-dd");
              p.f_time = Convert.ToDateTime(row["end_time"]).ToString("yyyy-MM-dd"); ;
              p.declarant = getDeclarant(p.p_id);
              p.leader=getLeader(p.p_id);
              p.member = getMember(p.p_id);
              project.Add(p);
          }
          return project;
         

      }
        public Model.Leader getLeader(int p_id){
           
            SqlParameter[] pars ={ 
                                 new SqlParameter("@p_id",SqlDbType.Int,32),
                               
                                 };  
            pars[0].Value = p_id;
            DataTable da = SqlHelper.GetTable("getLeader", CommandType.StoredProcedure,pars);
           
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
            foreach(DataRow row in da.Rows ){
                m = new Model.Member();
                m.project_id = Convert.ToInt16(row["project_id"]);
                m.nickname=row["nickname"].ToString();
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
       public static string ConvertToLevel(string source){
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
       public List<Model.Project_Schedule> getProject_Schedule()
       {
           List<Model.Project_Schedule> project = new List<Model.Project_Schedule>();
           DataTable da = SqlHelper.GetTable("getProject_Schedule", CommandType.StoredProcedure, null);
          Model.Project_Schedule p = null;
           foreach (DataRow row in da.Rows)
           {
               p = new Model.Project_Schedule();
               p.p_id = Convert.ToInt16(row["ID"]);
               p.p_name = row["name"].ToString();
               p.p_type = row["type"].ToString();
               // p.p_type = ConvertToLevel("b");
               p.target_level = ConvertToLevel(row["target_level"].ToString());
               p.create_time = Convert.ToDateTime(row["create_time"]).ToString("yyyy-MM-dd");;
               p.f_time = Convert.ToDateTime(row["end_time"]).ToString("yyyy-MM-dd");;
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
           schedule = (getFTaskNum(p_id) / getTaskNum(p_id) * 100 ).ToString();
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
           DataRow row= da.Rows[0];
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

    }
}
