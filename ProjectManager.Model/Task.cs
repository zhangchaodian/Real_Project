using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Model
{
    public class Task
    {
        public int t_id { get; set; }
        public string leader { get; set; }
        public DateTime s_time { get; set; }
        public DateTime f_time { get; set; }
        public string task_content { get; set; }
    }
}
