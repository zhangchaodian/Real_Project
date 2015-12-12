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
          userInfo.ID = row["ID"].ToString();
          userInfo.email= row["email"].ToString();
          userInfo.nickname = row["nickname"].ToString();
          userInfo.phone= row["phone"].ToString();
          userInfo.sex = row["sex"].ToString();
          userInfo.belongs=row["belongs"].ToString();
          userInfo.position = row["position"].ToString();
          userInfo.type = row["type"].ToString();
          userInfo.pwd = row["pwd"].ToString();
          

      }

      public User Check(string username, string password)
      {
          UserInfoDAL user = new UserInfoDAL();
          return user.GetUserInfo(username, password);
      }

      public bool addUser(User user)
      {
          string sql = "insert into [UserInfo] (ID,pwd,nickname,sex,position,email,phone,type) values(@ID,@pwd,@nickname,@sex,@position,@email,@phone,@type)";
          SqlParameter[] pars = { 
                                  new SqlParameter("@ID",SqlDbType.NVarChar,30),
                                  new SqlParameter("@pwd",SqlDbType.NVarChar,100),
                                  new SqlParameter("@nickname",SqlDbType.NVarChar,20),
                                  new SqlParameter("@sex",SqlDbType.Char,2),
                                  new SqlParameter("@position",SqlDbType.NVarChar,20),
                                  new SqlParameter("@email",SqlDbType.NVarChar,30),
                                  new SqlParameter("@phone",SqlDbType.NVarChar,11),
                                  new SqlParameter("@type",SqlDbType.Char,1),

                                };
          pars[0].Value = user.ID;
          pars[1].Value = user.pwd;
          pars[2].Value = user.nickname;
          pars[3].Value = user.sex;
          pars[4].Value = user.position;
          pars[5].Value = user.email;
          pars[6].Value = user.phone;
          pars[7].Value = user.type;

          int flag = SqlHelper.ExecuteNonquery(sql,CommandType.Text,pars);
          if (flag > 0)
              return true;
          else
              return false;

      }

      public User getUser(string userID)
      {
          string sql = "select * from [UserInfo] where ID=@userID";
          SqlParameter []pars={
                              new SqlParameter("@userID",SqlDbType.NVarChar,30)
          };
          pars[0].Value = userID;
          DataTable da = SqlHelper.GetTable(sql,CommandType.Text,pars);
          User userInfo = null;
          if (da.Rows.Count > 0)
          {
              userInfo = new User();
              LoadEntity(userInfo, da.Rows[0]); 
              return userInfo;
          }
         
          else
          return null;

      }
      public bool modifyUser(User user)
      {
          string sql = "update [UserInfo] set ID=@ID,pwd=@pwd,nickname=@nickname,sex=@sex,position=@position,email=@email,phone=@phone,type=@type where ID=@ID;";
          SqlParameter[] pars = { 
                                  new SqlParameter("@ID",SqlDbType.NVarChar,30),
                                  new SqlParameter("@pwd",SqlDbType.NVarChar,100),
                                  new SqlParameter("@nickname",SqlDbType.NVarChar,20),
                                  new SqlParameter("@sex",SqlDbType.Char,2),
                                  new SqlParameter("@position",SqlDbType.NVarChar,20),
                                  new SqlParameter("@email",SqlDbType.NVarChar,30),
                                  new SqlParameter("@phone",SqlDbType.NVarChar,11),
                                  new SqlParameter("@type",SqlDbType.Char,1),

                                };
          pars[0].Value = user.ID;
          pars[1].Value = user.pwd;
          pars[2].Value = user.nickname;
          pars[3].Value = user.sex;
          pars[4].Value = user.position;
          pars[5].Value = user.email;
          pars[6].Value = user.phone;
          pars[7].Value = user.type;
          int flag = SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
          if (flag > 0)
              return true;
          else
              return false;
      }

      public bool deleUser(string UserID)
      {
          string sql = "delete from [UserInfo] where ID=@ID;";
          SqlParameter[] pars = { 
                                  new SqlParameter("@ID",SqlDbType.NVarChar,30),
                                 
                                };
          pars[0].Value = UserID;
          int flag = SqlHelper.ExecuteNonquery(sql, CommandType.Text, pars);
          if (flag > 0)
              return true;
          else
              return false;
      }

    }
}