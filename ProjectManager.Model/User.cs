using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Model

{
   public class User
    {
       public long ID { get; set; }
       public string pwd { get; set; }
       public string nickname { get; set; }
       public string sex { get; set; }
       public string position { get; set; }
       public string email { get; set; }
       public string phone { get; set; }
       public string belongs { get; set; }
       public string type { get; set; }
    }
}
