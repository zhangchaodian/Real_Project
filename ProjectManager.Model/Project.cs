using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Model
{
    public class Project
    {
        public int p_id { get; set; }
        public string p_name { get; set; }
        public string declarant { get; set; }
        public string leader { get; set; }
        public string now_level { get; set; }
        public string target_level { get; set; }
        public string f_time { get; set; }
        public string create_time { get; set; }
        public string finish_state { get; set; }//项目完成状态
        public string pass_state { get; set; }//项目审核状态
        public string report_path { get; set; }
        public string paper_path { get; set; }
        public string whole_pack_file { get; set; }
        public string comment { get; set; }
    }

}
