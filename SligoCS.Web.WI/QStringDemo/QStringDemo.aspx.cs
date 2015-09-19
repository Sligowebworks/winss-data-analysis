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

using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.Base.WebSupportingClasses.WI;

namespace SligoCS.Web.WI.QStringDemo
{
    public partial class QStringDemo : PageBaseWI
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            ParamsHelper.LoadFromContext(StickyParameter);
            SetLabels(base.StickyParameter);            
        }


        private void SetLabels(StickyParameter myParams)
        {
            lblFULLKEY.Text = myParams.FULLKEY;
            lblGraphFile.Text = myParams.GraphFile;
            lblCompareTo.Text = myParams.CompareTo;
            lblORGLEVEL.Text = myParams.ORGLEVEL;
            lblGroup.Text = myParams.Group;
            lblSTYP.Text = myParams.STYP.ToString();
            lblDN.Text = myParams.DN;
            lblSN.Text = myParams.SN;
            lblDETAIL.Text = myParams.DETAIL;
            lblCOUNTY.Text = myParams.COUNTY.ToString();
            lblYearLocal.Text = myParams.YearLocal.ToString();
            lblTrendStartYearLocal.Text = myParams.TrendStartYearLocal.ToString();
            //lblWhichSchool.Text = myParams.WhichSchool.ToString();
            lblNumSchools.Text = myParams.NumSchools;
            lblConferenceKey.Text = myParams.ConferenceKey.ToString();
            lblDistrictID.Text = myParams.DistrictID.ToString();
            lblzBackTo.Text = myParams.ZBackTo;
            //lblFileQuery.Text = myParams.FileQuery;
            //lblFileName.Text = myParams.FileName;
            //lblSchoolWebaddress.Text = myParams.SchoolWebaddress;
            //lblDistrictWebaddress.Text = myParams.DistrictWebaddress;

            //
            byte[] myBytes = BitConverter.GetBytes(myParams.GradeBreakout);
            lblGradeBreakout.Text = string.Format("{0}={1}:<br/>{2}", myParams.GradeBreakout, BitConverter.ToString(myBytes), myParams.GetGradeBreakout(StickyParameter.GradeType.Normal));

            myBytes = BitConverter.GetBytes(myParams.GradeBreakoutLAG);
            lblLAG.Text = string.Format("{0}={1}:<br/>{2}", myParams.GradeBreakoutLAG, BitConverter.ToString(myBytes), myParams.GetGradeBreakout(StickyParameter.GradeType.LAG));

            myBytes = BitConverter.GetBytes(myParams.GradeBreakoutEDISA);
            lblEDISA.Text = string.Format("{0}={1}:<br/>{2}", myParams.GradeBreakoutEDISA, BitConverter.ToString(myBytes), myParams.GetGradeBreakout(StickyParameter.GradeType.EDISA));

        }
        public override DataSet GetDataSet()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
