using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProjectManager.BLL
{
    public class AdminServer
    {
        #region  管理页面搜索
        public static List<User> selectUser(string type = null, string sex = null, string order = null)
        {
            string sql = null;

            if (type == null && sex == null && order == null)
            {
                sql = "select * from [UserInfo] order by ID";
                SqlParameter[] pars = null;
                DataTable da = DAL.SqlHelper.GetTable(sql, CommandType.Text, pars);
                List<Model.User> user = new List<User>();
                foreach (DataRow row in da.Rows)
                {
                    Model.User u = new Model.User();
                    u = LoadUserEntity(row);
                    user.Add(u);
                }
                return user;
            }
            else
            {
                SqlParameter[] pars ={ 
                                 new SqlParameter("@type",SqlDbType.Char,2),
                                 new SqlParameter("@sex",SqlDbType.Char,2),
                                 };

                switch (order)
                {
                    case "desc":
                        sql = "select * from [UserInfo] where type=@type and sex=@sex order by ID desc;";
                        break;
                    default:
                        sql = "select * from [UserInfo] where type=@type and sex=@sex order by ID asc;";
                        break;
                }
                pars[0].Value = type;
                pars[1].Value = sex;
                DataTable da = DAL.SqlHelper.GetTable(sql, CommandType.Text, pars);
                List<Model.User> user = new List<User>();
                foreach (DataRow row in da.Rows)
                {
                    Model.User u = new Model.User();
                    u = LoadUserEntity(row);
                    user.Add(u);
                }
                return user;

            }


        }
        public static User LoadUserEntity(DataRow row)
        {
            User u = new User();
            u.ID = row["ID"].ToString();
            u.email = row["email"].ToString();
            u.nickname = row["nickname"].ToString();
            u.phone = row["phone"].ToString();
            u.sex = row["sex"].ToString();
            u.belongs = row["belongs"].ToString();
            u.position = row["position"].ToString();
            u.type = row["type"].ToString();
            u.pwd = row["pwd"].ToString();


            return u;
        }

        public static List<User> getTypeSelect(List<User> user, string keytype, string keyword)
        {

            switch (keytype)
            {
                case "ID":
                    user = (from result in user where result.ID.Contains(keyword) orderby result.ID select result).ToList();
                    break;
                case "nickname":
                    user = (from result in user where result.nickname.Contains(keyword) orderby result.ID select result).ToList();
                    break;
                case "position":
                    user = (from result in user where result.position.Contains(keyword) orderby result.ID select result).ToList();
                    break;
                case "email":
                    user = (from result in user where result.email.Contains(keyword) orderby result.ID select result).ToList();
                    break;
                case "phone":
                    user = (from result in user where result.phone.Contains(keyword) orderby result.ID select result).ToList();
                    break;

            }
            return user;
        }

        public static List<User> getAllSelect(List<User> user, string type, string sex, string order)
        {
            switch (type)
            {
                case "all":
                    user = (from result in user orderby result.ID select result).ToList();
                    break;
                default:
                    user = (from result in user where result.type.Equals(type) orderby result.ID select result).ToList();
                    break;
            }
            switch (sex)
            {
                case "all":
                    user = (from result in user orderby result.ID select result).ToList();
                    break;
                default:
                    user = (from result in user where result.sex.Equals(sex) orderby result.ID select result).ToList();
                    break;

            }
            switch (order)
            {
                case "asc":
                    user = (from result in user orderby result.ID ascending select result).ToList();
                    break;
                case "desc":
                    user = (from result in user orderby result.ID descending select result).ToList();
                    break;

            }

            return user;
        }
        #endregion

        #region 添加用户
        public static bool addUser(User u)
        {
            DAL.UserInfoDAL add = new DAL.UserInfoDAL();
            return add.addUser(u);

        }
        #endregion

        #region 更改用户信息
        public static string getInfo(string userID)
        {
            DAL.UserInfoDAL modify = new DAL.UserInfoDAL();
            User user = modify.getUser(userID);
            string str0 = user.ID;
            string str1 = user.pwd;
            string str2 = user.nickname;
            string str3 = user.position;
            string str4 = user.email;
            string str5 = user.phone;
            string str6 = user.sex;
            string str7 = user.type;
            string htmlstr = string.Format("{{ \"ID\": \"{0}\", \"pwd\": \"{1}\", \"nickname\": \"{2}\", \"position\": \"{3}\", \"email\": \"{4}\", \"phone\": \"{5}\", \"sex\": \"{6}\", \"type\": \"{7}\"}}", str0, str1, str2, str3, str4, str5, str6, str7);

            return htmlstr;
        }
        #endregion


        public static string modifyUser(User user)
        {
            DAL.UserInfoDAL modify = new DAL.UserInfoDAL();
            if (modify.modifyUser(user))
                return "用户信息修改成功！";
            else
                return "用户信息修改失败！";
        }
        public static string deleUser(string userID)
        {
            DAL.UserInfoDAL del = new DAL.UserInfoDAL();
            if (del.deleUser(userID))
                return "用户信息删除成功！";
            else
                return "用户信息删除失败！";
        }

        public static List<Model.Project> getSelectProject(List<Model.Project> project, string now_level, string target_level, string state, string order)
        {
            DAL.projectinfoDAL dal = new DAL.projectinfoDAL();
            return dal.selectProject(project, now_level, target_level, state, order);
        }
        public static List<Model.Project> getprojectTypeSelect(List<Model.Project> project, string keytype, string keyword)
        {
            DAL.projectinfoDAL dal = new DAL.projectinfoDAL();
            return dal.selectTypePorject(project, keytype, keyword);
        }
    }
}

