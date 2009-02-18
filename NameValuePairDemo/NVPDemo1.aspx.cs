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


/// <summary>
/// NOTE- all of our web pages will be derived from our abstract web page,
/// which will then, in turn, inherit from the System.Web.UI.Page.
/// </summary>
public partial class NameValuePairDemo_NVPDemo1 : BaseWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //long.MaxValue;

    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        SetSessionVariable(SessionVars.SchoolType, txtSchoolType.Text);
        SetSessionVariable(SessionVars.Year, txtYear.Text);
        SetSessionVariable(SessionVars.PercentAttendance, txtAttendance.Text);

        Response.Redirect("NVPDemo2.aspx");
    }
}
