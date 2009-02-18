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

public partial class NameValuePairDemo_NVPDemo2 : BaseWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblSchoolType.Text = GetSessionValue(SessionVars.SchoolType);
        lblYear.Text = GetSessionValue(SessionVars.Year);
        lblAttendance.Text = GetSessionValue(SessionVars.PercentAttendance);
    }
}
