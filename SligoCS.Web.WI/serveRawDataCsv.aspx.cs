using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SligoCS.Web.WI
{
    public partial class serveRawDataCsv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RawCsvName"] == null) throw new Exception("NO CSV NAME");
            if (Session["RawCsvData"] == null) throw new Exception("NO CSV DATA");

            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition",
            "attachment;filename=" + Session["RawCsvName"].ToString());
            Response.AddHeader("Cache-Control", "max-age=360");
            Response.Write(Session["RawCsvData"].ToString());
            Response.Flush();
            Response.Close();
        }
    }
}
