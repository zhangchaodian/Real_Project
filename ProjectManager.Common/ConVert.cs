using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Common
{
  public  class ConVert
    {
      //转换项目级别
      public static string ConvertToLevel(string level){
          switch (level)
          {
              case "a":
                  return "校级";
              case "b":
                  return "市级";
              case "c":
                  return "省级";
              case "d":
                  return "国家级";
              default:
                  return "校级";
             
          }
      }

      public static  string ConVertState(string state)
      {
          switch (state)
          {
              case "a":
                  return "申报,待定";
              case "b":
                  return "申报未通过";
              case "c":
                  return "通过,等待审核";
              case "d":
                  return "审核一,未通过";
              case "e":
                  return  "审核一,通过";
              case "f":
                  return "审核二,通过";
              case "g":
                  return "审核三,未通过";
              case "h":
                  return "审核三,通过";
              case "i":
                  return "通过审核";
              default:
                  return "申报,待定";

          }
             
      }
    }
}
