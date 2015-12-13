using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Data.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;


namespace ProjectManager.Common
{
    public class DownloadServer
    {
        /// <summary>
        /// 使用WriteFile下载文件  
        /// </summary>
        /// <param name="filePath">相对路径</param>
        public static void WriteFile(int project_id, string file_type, string file_name)
        {
            try
            {
                string filePath = "~/Upload/" + project_id + "/" + file_type + "/" + file_name;
                filePath = HttpContext.Current.Server.MapPath(filePath);
                if (File.Exists(filePath))
                {
                    FileInfo info = new FileInfo(filePath);
                    long fileSize = info.Length;
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + HttpContext.Current.Server.UrlEncode(file_name));
                    //指定文件大小   
                    HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
                    HttpContext.Current.Response.WriteFile(filePath, 0, fileSize);
                    HttpContext.Current.Response.Flush();
                }
            }
            catch
            { }
            finally
            {
                HttpContext.Current.Response.Close();
            }
        }
    }
}
