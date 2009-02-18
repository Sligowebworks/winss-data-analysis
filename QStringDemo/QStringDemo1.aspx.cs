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

public partial class QStringDemo_QStringDemo1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        QStringHelper qs = new QStringHelper();
        qs.FULLKEY = txtFULLKEY.Text;
        qs.GraphFile = txtGraphFile.Text;
        qs.CompareTo = txtCompareTo.Text;
        qs.ORGLEVEL = txtORGLEVEL.Text;
    	qs.Group = txtGroup.Text;
        qs.STYP = int.Parse(txtSTYP.Text);
        qs.DN = txtDN.Text;
        qs.SN = txtSN.Text;
        qs.DETAIL = txtDETAIL.Text;
        qs.COUNTY = int.Parse(txtCOUNTY.Text);
        qs.YearLocal = int.Parse(txtYearLocal.Text);
        qs.TrendStartYearLocal = int.Parse(txtTrendStartYearLocal.Text);
        qs.WhichSchool = int.Parse(txtWhichSchool.Text);
        qs.NumSchools = txtNumSchools.Text;
        qs.ConferenceKey = int.Parse(txtConferenceKey.Text);
        qs.DistrictID = int.Parse(txtDistrictID.Text);
        qs.ZBackTo = txtzBackTo.Text;
        qs.FileQuery = txtFileQuery.Text;                
        qs.FileName = txtFileName.Text;
        qs.SchoolWebaddress = txtSchoolWebaddress.Text;
        qs.DistrictWebaddress = txtDistrictWebaddress.Text;
        qs.GradeBreakout = int.Parse(txtGradeBreakout.Text);
        qs.GradeBreakoutLAG = int.Parse(txtLAG.Text);
        qs.GradeBreakoutEDISA = int.Parse(txtEDISA.Text);

        string url = string.Format("QStringDemo2.aspx?{0}", qs.GetQueryString());
        Response.Redirect(url);
        
    }
}
