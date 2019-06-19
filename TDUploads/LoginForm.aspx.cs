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
    public partial class _LoginForm : Page
    {
        String Message2;
        String Message1;
        protected void Page_Load(object sender, EventArgs e)
        {
            nana();
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
            DataTable dt = new DataTable();
            DataTable dtSample = new DataTable();

            // here am again filling the datatable from Gridview DataSource so rows included in paging will also loaded in the table again
            dtSample = GridView1.DataSource as DataTable;

            //selecting the searched row from that datatable
            DataRow[] foundRow = dtSample.Select("File LIKE '" + TextBox1.Text + "%'");

            //adding  coloumns to datatable same as like previous datatable.
            //mendatory that this new datatable column should be same as like in xml elements.

            dt.Columns.Add("File", typeof(string));
            dt.Columns.Add("Size", typeof(string));
            dt.Columns.Add("Type", typeof(string));

            //adding each row in the searched result to this new datatable
            foreach (DataRow row in foundRow)
                dt.Rows.Add(row.ItemArray);

            //again binding new dataTable to gridview 
            //now gridview will have only searched record.
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "T" && txtPassword.Text == "D33")
            {
                Response.Redirect("WelcomeForm.aspx");
            }

            if (txtUsername.Text == "" && txtPassword.Text == "")
            {
                Message1 = "Please enter username and password";
            }


            else
            {
                Message2 = "Invalid account";
            }


            Label2.Text = Message1;

            Label1.Text = Message2;
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

        private string GetFileTypeByExtension(string extension)
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


    }
}