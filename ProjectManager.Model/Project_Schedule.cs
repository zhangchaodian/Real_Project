using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Model
{
   public class Project_Schedule
    {
        public int p_id { get; set; }
        public string p_type { get; set; }
        public string p_name { get; set; }
        public string declarant { get; set; }
        public Leader leader { get; set; }
        public string target_level { get; set; }
        public string s_time { get; set; }
        public string f_time { get; set; }
        public string create_time { get; set; }
        public string schedule { get; set; }
    }
}
