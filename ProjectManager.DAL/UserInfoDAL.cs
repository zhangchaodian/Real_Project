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
  public  class UserInfoDAL
    {
      public User GetUserInfo(string name,string pass)
      {
          string sql = "select * from [UserInfo] where ID=@username and pwd=@pwd;";
          SqlParameter[] pars ={ 
                                 new SqlParameter("@username",SqlDbType.NVarChar,32),
                                 new SqlParameter("@pwd",SqlDbType.NVarChar,32),
                                 };
          pars[0].Value = name;
          pars[1].Value = pass;
          DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
          User userInfo = null;
          if (da.Rows.Count > 0)
          {
              userInfo = new User();
              LoadEntity(userInfo, da.Rows[0]);
          }
          return userInfo;

      }
      public void LoadEntity(User userInfo, DataRow row)
      {
          userInfo.ID = Convert.ToInt64(row["ID"]);
          userInfo.email= row["email"].ToString();
          userInfo.nickname = row["nickname"].ToString();
          userInfo.phone= row["phone"].ToString();
          userInfo.sex = row["sex"].ToString();
          userInfo.belongs=row["belongs"].ToString();
          userInfo.position = row["position"].ToString();
          userInfo.type = row["type"].ToString();
          

      }

      public User Check(string username, string password)
      {
          UserInfoDAL user = new UserInfoDAL();
          return user.GetUserInfo(username, password);
      }
    }
}
