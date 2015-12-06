using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.DAL;
using ProjectManager.Model;

namespace ProjectManager.BLL
{
    public class EachProjectServer
    {
        EachScheduleDAL eachschedule = new EachScheduleDAL();


        #region 调用EachScheduleDAL的获取实体方法,获取每个项目进度页所需要的数据
        public EachSchedule GetEachSchedule(int ID)
        {
            EachSchedule eachschedule = this.eachschedule.GetEachSchedule(ID);
            switch (eachschedule.now_level)
            {
                case "a":
                    eachschedule.now_level = "校级";
                    break;
                case "b":
                    eachschedule.now_level = "市级";
                    break;
                case "c":
                    eachschedule.now_level = "省级";
                    break;
            }
            switch (eachschedule.target_level)
            {
                case "b":
                    eachschedule.target_level = "市级";
                    break;
                case "c":
                    eachschedule.target_level = "省级";
                    break;
                case "d":
                    eachschedule.target_level = "国家级";
                    break;
            }
            return eachschedule;
        }


        public List<Member> GetMember(int ID)
        {
            return this.eachschedule.GetMember(ID);
        }

        public List<Project_Task> GetProject_task(int ID)
        {
            return this.eachschedule.GetProjectTask(ID);

        }
        public int Get_Schedule_Per(int ID)
        {
            List<Project_Task> tasks = this.eachschedule.GetProjectTask(ID);
            int count = tasks.Count();
            int sum = 0;
            foreach (Project_Task task in tasks)
            {
                if (task.state == "b") { sum++; }
            }
            int per = (int)(((float)sum / (float)count) * 100);
            return per;

        }
        #endregion
    }
}
