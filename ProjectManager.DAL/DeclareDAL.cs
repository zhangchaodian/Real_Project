using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProjectManager.DAL
{
   public class DeclareDAL
   {
       #region 数据库更新操作
       public static bool declareProject(Model.declareViewModel project)
       {
           int result=0;
            string str = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
           
  string sql="insert into Project values(@p_id,@p_name,@declarant_id,@type,@now_level,@target_level,@start_time,@end_time,@money,@report_file,@paper_file,@whole_file,'a',@belongs,@create_time);"
      +"insert into Leader values(@p_id,@nickname,@phone,@email);";
  string sqlmember = null;
  string sqltask = null;
           foreach(Model.Member m in project.member){
               sqlmember += "insert into Member values(@p_id,'" + m.nickname + "');";
           }
           foreach (Model.Task t in project.task)
           {
               sqltask += "insert into Task values("+t.t_id+",@nickname,'"+t.s_time+"','"+t.f_time+"','"+t.task_content+"',@p_id,'a');";
           }
           sql = sql + sqltask + sqlmember;
                         SqlParameter[] pars ={ 
                                 new SqlParameter("@p_id",SqlDbType.Int,32),
                                 new SqlParameter("@p_name",SqlDbType.NVarChar,32),
                                  new SqlParameter("@declarant_id",SqlDbType.NVarChar,32),
                                 new SqlParameter("@type",SqlDbType.NVarChar,32),
                                  new SqlParameter("@now_level",SqlDbType.NVarChar,32),
                                 new SqlParameter("@target_level",SqlDbType.NVarChar,32),
                                  new SqlParameter("@start_time",SqlDbType.DateTime,32),
                                 new SqlParameter("@end_time",SqlDbType.DateTime,32),
                                  new SqlParameter("@money",SqlDbType.NVarChar,32),
                                 new SqlParameter("@report_file",SqlDbType.NVarChar,32),
                                  new SqlParameter("@paper_file",SqlDbType.NVarChar,32),
                                 new SqlParameter("@whole_file",SqlDbType.NVarChar,32),
                                  new SqlParameter("@belongs",SqlDbType.NVarChar,32),
                                   new SqlParameter("@create_time",SqlDbType.DateTime,32),
                                   new SqlParameter("@nickname",SqlDbType.NVarChar,32),
                                   new SqlParameter("@phone",SqlDbType.NVarChar,32),
                                   new SqlParameter("@email",SqlDbType.NVarChar,32),
                               

                                 };
                          pars[0].Value = project.p_id;
                          pars[1].Value = project.project_name;
                          pars[2].Value = project.declarant_id;
                          pars[3].Value = project.p_type;
                          pars[4].Value = project.now_level;
                          pars[5].Value = project.target_level;
                          pars[6].Value = project.s_time;
                          pars[7].Value = project.f_time;
                          pars[8].Value = project.money;
                          pars[9].Value = project.report_file;
                          pars[10].Value = project.paper_file;
                          pars[11].Value = project.whole_file;
                          pars[12].Value = project.belongs;
                          pars[13].Value = project.create_time;    
                          pars[14].Value = project.leader;
                          pars[15].Value = project.phone;
                          pars[16].Value = project.email;


                         // command.CommandText = "insert into Project values(1,'2','20131003612','4','5','6','2015/11/11','2015/11/12',100,'10','11','12','a','a','2015/11/11'); ";
                          result=SqlHelper.ExecuteNonquery(sql,CommandType.Text,pars);
                    
                   
                
            
            if(result > 0)
                return true;
            else
                return false;
       }
       #endregion
    }
}
