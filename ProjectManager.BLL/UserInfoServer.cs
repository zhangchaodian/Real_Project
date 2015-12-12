using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.DAL;
using ProjectManager.Model;

namespace ProjectManager.BLL
{
   public class UserInfoServer
   {
       #region 登录匹配
       public static int CheckLogin(string username,string pwd)
       {
           UserInfoDAL info = new UserInfoDAL();
           User user ;
           user = info.Check(username,pwd);//获取用户数据
           if (user!=null)
           {
               //a教职工，b管理员
               string usertype = user.type;
               if(usertype.Equals("a")){
                   return 0;
               }
               else{
                   return 1;
               }
           }
           else
           {
               return -1;
           }
       }
       #endregion


       #region 获取用户数据
       public User getUserInfo(string name,string pass){
           DAL.UserInfoDAL info = new DAL.UserInfoDAL();
           User user= info.GetUserInfo(name,pass);
           return user;
           
       }
       #endregion





   }
}
