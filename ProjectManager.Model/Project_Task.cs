using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Model
{
    public class Project_Task
    {
        public int ID { get; set; }
        public string leader { get; set; }
        public string start_y { get; set; }
        public string start_m { get; set; }
        public string start_d { get; set; }
        public string end_y { get; set; }
        public string end_m { get; set; }
        public string end_d { get; set; }
        public string task { get; set; }
        public string state { get; set; }
    }
}
