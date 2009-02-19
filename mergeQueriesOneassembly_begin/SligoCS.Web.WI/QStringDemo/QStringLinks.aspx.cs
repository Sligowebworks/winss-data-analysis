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

namespace SligoCS.Web.WI.QStringDemo
{
    public partial class QStingLinks : PageBaseWI
    {

        protected string DESTURL = "/SligoWI/QstringDemo/QStringDemo.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void linkSession_Click(object sender, EventArgs e)
        {            
            StickyParameter.FULLKEY = "012619040022";
            StickyParameter.GraphFile = "HIGHSCHOOLCOMPLETION";
            StickyParameter.CompareTo = "PRIORYEARS";
            StickyParameter.ORGLEVEL = "SC";
            StickyParameter.Group = "AllStudentsFAY";
            StickyParameter.DN = "Milwaukee";
            StickyParameter.SN = "Madison Hi";
            StickyParameter.DETAIL = "YES";
            StickyParameter.NumSchools = "4";
            StickyParameter.ZBackTo = "performance.aspx";
            //StickyParameter.FileQuery = "select count(*) from sysdatabases";
            //StickyParameter.FileName = "c:\\sample.txt";
            //StickyParameter.SchoolWebaddress = "http://mpsportal.milwaukee.k12.wi.us";
            //StickyParameter.DistrictWebaddress = "http://www.milwaukee.k12.wi.us";
            StickyParameter.STYP = 3;
            StickyParameter.COUNTY = 40;
            StickyParameter.YearLocal = 2007;
            StickyParameter.TrendStartYearLocal = 1997;
            //StickyParameter.WhichSchool = 4;
            StickyParameter.ConferenceKey = 27;
            StickyParameter.DistrictID = 3619;
            StickyParameter.GradeBreakout = 999;
            StickyParameter.GradeBreakoutLAG = 555;
            StickyParameter.GradeBreakoutEDISA = 212;
            //StickyParameter.SetSessionValues(Session, true);

            Response.Redirect (DESTURL, false);
        }

        protected void linkConfig_Click(object sender, EventArgs e)
        {
            Response.Redirect(DESTURL, false);
        }

        protected void linkMixed_Click(object sender, EventArgs e)
        {
            //<a href="/QstringDemo/QStringDemo.aspx?FULLKEY=013619040022&GraphFile=HIGHSCHOOLCOMPLETION&CompareTo=PRIORYEARS&ORGLEVEL=SC&Group=AllStudentsFAY&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileQuery=select%20count(*)%20from%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20sys.databases&FileName=c:\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=9&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212">By QString</a>
            //DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=9&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212">By QString</a>

            //The URL will set the first 4 parameters
            string url = string.Format("{0}?FULLKEY=013619040022&GraphFile=HIGHSCHOOLCOMPLETION&CompareTo=PRIORYEARS&ORGLEVEL=SC", DESTURL);

            //The session will set the next 8 parameters.
            StickyParameter.Group = "AllStudentsFAY";
            StickyParameter.DN = "Milwaukee";
            StickyParameter.SN = "Madison Hi";
            StickyParameter.DETAIL = "YES";
            StickyParameter.NumSchools = "4";
            StickyParameter.ZBackTo = "performance.aspx";
            //StickyParameter.FileQuery = "select count(*) from sys.databases";
            //StickyParameter.FileName="c:\\sample.txt";
            //StickyParameter.SetSessionValues(Session, true);

            //The last 11 will use the default values found in web.config.
            //No coded needed for those...


            Response.Redirect(url, false);

        }


        public override DataSet GetDataSet()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
