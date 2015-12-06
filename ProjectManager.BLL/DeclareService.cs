using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL
{
   public class DeclareService
    {
       public static bool declareProject(Model.declareViewModel project){

           return DAL.DeclareDAL.declareProject(project);
           
       }
    }
}
