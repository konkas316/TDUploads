using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TDUploads
{
    public partial class WelcomeForm : Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                Response.Clear();
                Response.ContentType = "application/octect - stream";
                Response.AppendHeader("content-disposition", "filename=" + e.CommandArgument);
                Response.TransmitFile(Server.MapPath("~/Data/") + e.CommandArgument);
                Response.End();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Data/") + FileUpload1.FileName);
            }
            nana();
        }

        private void nana()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("File", typeof(string));
            dt.Columns.Add("Size", typeof(string));
            dt.Columns.Add("Type", typeof(string));

            foreach (string strFile in Directory.GetFiles(Server.MapPath("~/Data/")))
            {
                FileInfo fi = new FileInfo(strFile);

                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                double Length = new FileInfo(strFile).Length;
                int order = 0;
                while (Length >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    Length = Length / 1024;
                }

                // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
                // show a single decimal place, and no space.
                string result = String.Format("{0:0.##} {1}", Length, sizes[order]);

                dt.Rows.Add(fi.Name, result, GetFileTypeByExtension(fi.Extension));
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        private object GetFileTypeByExtension(string extension)
        {
            switch (extension.ToLower())
            {
                case ".doc":
                case ".docx":
                    return "Microsoft Word Document";
                case ".xlsx":
                case ".xls":
                    return "Microsoft Excel Document";
                case ".txt":
                    return "text Document";
                case ".jpeg":
                case ".jpg":
                case ".png":
                    return "Image";
                case ".pdf":
                    return "Portable Document Format";
                case ".zip":
                case ".7z":
                    return "archive file format";
                default:
                    return "Unknown";
            }
        }


        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginForm.aspx");
        }

        protected void LinkButton2_Click2(object sender, EventArgs e)
        {
            string filepath = (sender as LinkButton).CommandArgument;
            File.Delete(Server.MapPath("~/Data/") + filepath);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}