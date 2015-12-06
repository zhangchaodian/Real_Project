using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Model
{
   public class declareViewModel
    {
       public int p_id { get; set; }
       public string project_name{get;set;}
       public string declarant_id{get;set;}
       public string phone{get;set;}
       public string email{get;set;}
       public List<Model.Member> member {get;set;}
       public string p_type{get;set;} 
       public string now_level{get;set;}
       public string target_level{get;set;}
       public System.DateTime s_time{get;set;}
       public DateTime f_time{get;set;}
       public double money{get;set;}
       public  List<Model.Task> task { get; set; }
       public string leader { get; set; }
       public DateTime create_time { get; set; }
       public string belongs { get; set; }
       public string report_file { get; set; }
       public string paper_file { get;set;}
       public string whole_file { get; set; }
    }
}
