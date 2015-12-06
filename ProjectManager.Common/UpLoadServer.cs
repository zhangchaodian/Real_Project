using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web ;
using System.Data.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ProjectManager.Common
{
   public class UpLoadServer
    {
       public static void UploadFile(Model.declareViewModel project,HttpPostedFileBase report,HttpPostedFileBase paper,HttpPostedFileBase wholefile){
           string report_path="~/Upload/" + project.p_id+"/"+"report";
           string paper_path="~/Upload/" + project.p_id+"/"+"paper";
           string whole_path="~/Upload/" + project.p_id+"/"+"wholefile";
           if(Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/"+project.p_id))==false){
               Directory.CreateDirectory(HttpContext.Current.Server.MapPath(report_path));
               Directory.CreateDirectory(HttpContext.Current.Server.MapPath(paper_path));
               Directory.CreateDirectory(HttpContext.Current.Server.MapPath(whole_path));
           }
           System.IO.DirectoryInfo DirInfo1 = new DirectoryInfo(HttpContext.Current.Server.MapPath(report_path));
            DirInfo1.Attributes = FileAttributes.Normal & FileAttributes.Directory;
            System.IO.DirectoryInfo DirInfo2 = new DirectoryInfo(HttpContext.Current.Server.MapPath(paper_path));
            DirInfo2.Attributes = FileAttributes.Normal & FileAttributes.Directory;
            System.IO.DirectoryInfo DirInfo3 = new DirectoryInfo(HttpContext.Current.Server.MapPath(whole_path));
            DirInfo3.Attributes = FileAttributes.Normal & FileAttributes.Directory;
            report.SaveAs(HttpContext.Current.Server.MapPath(report_path)+report.FileName);
            paper.SaveAs(HttpContext.Current.Server.MapPath(paper_path)+paper.FileName);
            wholefile.SaveAs(HttpContext.Current.Server.MapPath(whole_path)+wholefile.FileName);

       }
    }
}
