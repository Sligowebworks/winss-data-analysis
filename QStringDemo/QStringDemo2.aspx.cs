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

public partial class QStringDemo_QStringDemo2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        QStringHelper qs = new QStringHelper();
        qs.Load(Request.QueryString);            

		lblFULLKEY.Text = qs.FULLKEY;
        lblGraphFile.Text = qs.GraphFile;
        lblCompareTo.Text = qs.CompareTo;
        lblORGLEVEL.Text = qs.ORGLEVEL;
        lblGroup.Text = qs.Group;
        lblSTYP.Text = qs.STYP.ToString();
        lblDN.Text = qs.DN;
        lblSN.Text = qs.SN;
        lblDETAIL.Text = qs.DETAIL;
        lblCOUNTY.Text = qs.COUNTY.ToString();
        lblYearLocal.Text = qs.YearLocal.ToString();
        lblTrendStartYearLocal.Text = qs.TrendStartYearLocal.ToString();
        lblWhichSchool.Text = qs.WhichSchool.ToString();
        lblNumSchools.Text = qs.NumSchools;
        lblConferenceKey.Text = qs.ConferenceKey.ToString();
        lblDistrictID.Text = qs.DistrictID.ToString();
        lblzBackTo.Text = qs.ZBackTo;
        lblFileQuery.Text = qs.FileQuery;
        lblFileName.Text = qs.FileName;
        lblSchoolWebaddress.Text = qs.SchoolWebaddress;
        lblDistrictWebaddress.Text = qs.DistrictWebaddress;


        byte[] myBytes = BitConverter.GetBytes(qs.GradeBreakout);
        lblGradeBreakout.Text = string.Format("{0}={1}:<br/>{2}", qs.GradeBreakout, BitConverter.ToString(myBytes), qs.GetGradeBreakout(QStringHelper.GradeType.Normal));
                
        myBytes = BitConverter.GetBytes(qs.GradeBreakoutLAG);
        lblLAG.Text = string.Format("{0}={1}:<br/>{2}", qs.GradeBreakoutLAG, BitConverter.ToString(myBytes), qs.GetGradeBreakout(QStringHelper.GradeType.LAG));

        myBytes = BitConverter.GetBytes(qs.GradeBreakoutEDISA);
        lblEDISA.Text = string.Format("{0}={1}:<br/>{2}", qs.GradeBreakoutEDISA, BitConverter.ToString(myBytes), qs.GetGradeBreakout(QStringHelper.GradeType.EDISA));

        
    }
}
